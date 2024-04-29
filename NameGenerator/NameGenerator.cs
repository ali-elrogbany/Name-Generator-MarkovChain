using AliElRogbany.Utils;

namespace AliElRogbany.NameGenerator
{
    public class NameGenerator
    {
        private int nStep;
        private Dictionary<string, Dictionary<string, float>> markovChain;
        private string[] referenceNames;

        public NameGenerator(int nStep)
        {
            this.nStep = nStep;
            this.markovChain = new Dictionary<string, Dictionary<string,float>>();
            this.referenceNames = new string[0];
        }

        public void Initialize(string[] referenceNames)
        {
            this.referenceNames = referenceNames;

            GatherStateTransitions();
            CalculateTransitionProbabilities();
        }

        private void GatherStateTransitions()
        {
            foreach (string name in this.referenceNames)
            {
                string lowerCaseName = name.ToLower();
                for (int i = 0; i < lowerCaseName.Length - this.nStep; i++)
                {
                    if (lowerCaseName.Substring(i + this.nStep).Length >= this.nStep)
                    {
                        string state = "";
                        for (int j = 0; j < this.nStep; j++)
                        {
                            state += lowerCaseName[i + j];
                        }

                        if (!this.markovChain.ContainsKey(state))
                        {
                            this.markovChain.Add(state, new Dictionary<string, float>());
                        }

                        string nextState = "";
                        for (int k = 0; k < this.nStep; k++)
                        {
                            if (i + this.nStep + k < lowerCaseName.Length)
                                nextState += lowerCaseName[i + this.nStep + k];
                        }

                        if (!this.markovChain[state].ContainsKey(nextState))
                        {
                            this.markovChain[state].Add(nextState, 1f);
                        }
                        else
                        {
                            this.markovChain[state][nextState] += 1f;
                        }
                    }
                    else
                        break;
                }
            }
        }

        private void CalculateTransitionProbabilities()
        {
            foreach (var pair in this.markovChain)
            {
                int totalTransitions = 0;
                foreach (var transitionPair in pair.Value)
                {
                    totalTransitions += (int) transitionPair.Value;
                }

                foreach (var transitionPair in pair.Value)
                {
                    this.markovChain[pair.Key][transitionPair.Key] = transitionPair.Value / totalTransitions;
                }
            }
        }

        public string GenerateName()
        {
            Random random = new Random();

            int iterations = random.Next(GetShortestNameLength(), GetLongestNameLength() + random.Next(0, 4)) / this.nStep;

            string name = "";

            List<string> chainStates = this.markovChain.Keys.ToList();

            string lastState = chainStates[random.Next(0, chainStates.Count)];
            name += lastState;

            for (int i = 1; i < iterations; i++)
            {
                DistributedRandomGenerator drg = new DistributedRandomGenerator();
                if (this.markovChain.ContainsKey(lastState))
                {
                    foreach (var pair in this.markovChain[lastState])
                    {
                        drg.AddItem(pair.Key, pair.Value);
                    }
                    lastState = drg.GetDistributedRandomItem();
                    name += lastState;
                }
                else
                    break;
            }
            return name;
        }

        private void CheckShortestName()
        {
            int shortestLength = GetShortestNameLength();
            if (shortestLength - this.nStep <= 0)
            {
                this.nStep = shortestLength - 1;
            }
        }

        private int GetShortestNameLength()
        {
            int shortestLength = int.MaxValue;
            foreach (string name in this.referenceNames)
            {
                if (name.Length < shortestLength)
                {
                    shortestLength = name.Length;
                }
            }
            return shortestLength;
        }

        private int GetLongestNameLength()
        {
            int longestLength = int.MinValue;
            foreach (string name in this.referenceNames)
            {
                if (name.Length > longestLength)
                {
                    longestLength = name.Length;
                }
            }
            return longestLength;
        }

        private void PrintMarkovChain()
        {
            foreach (var kvp in markovChain)
            {
                Console.Write($"State: {kvp.Key}, Transitions: ");
                foreach (var transition in kvp.Value)
                {
                    Console.Write($"{transition.Key}: {transition.Value}, ");
                }
                Console.WriteLine();
            }
        }
    }
}