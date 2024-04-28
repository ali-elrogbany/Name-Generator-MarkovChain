namespace AliElRogbany.Utils
{
    public class DistributedRandomGenerator 
    {
        private Dictionary<string, Double> distribution;
        private double distSum;

        public DistributedRandomGenerator() 
        {
            distribution = new Dictionary<string, Double>();
        }

        public void AddItem(string value, double distribution) 
        {
            if (this.distribution.ContainsKey(value)) 
            {
                distSum -= this.distribution[value];
            }
            this.distribution.Add(value, distribution);
            distSum += distribution;
        }

        public string GetDistributedRandomItem() 
        {
            Random random = new Random();
            double rand = random.NextDouble();

            double ratio = 1.0f / distSum;
            double tempDist = 0;

            foreach (string i in distribution.Keys)
            {
                tempDist += distribution[i];
                if (rand / ratio <= tempDist)
                {
                    return i;
                }
            }
            return "";
        }

    }
}