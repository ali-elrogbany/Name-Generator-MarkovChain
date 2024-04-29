using AliElRogbany.NameGenerator;
using AliElRogbany.Utils;

string filePath = "./data/babynames-clean.csv";
FileHandler fh = new FileHandler(filePath);
List<string[]> names = fh.ReadCSV();

int genderInput = 0;
string genderName = "";
int numberOfNames = 0;

Console.WriteLine("Welcome to the markov chain name generator!");

Console.WriteLine("Enter the number corresponding to the desired gender: ");
Console.WriteLine("1: Boy");
Console.WriteLine("2: Girl");
genderInput = Convert.ToInt32(Console.ReadLine());
while (genderInput <= 0 || genderInput > 2)
{
    Console.WriteLine("Choose a valid number:");
    genderInput = Convert.ToInt32(Console.ReadLine());
}
genderName = genderInput == 1 ? "boy" : "girl";

Console.WriteLine("Enter the number of names desired:");
numberOfNames = Convert.ToInt32(Console.ReadLine());
while (numberOfNames <= 0 || numberOfNames > 50)
{
    Console.WriteLine("Choose a positive and valid number:");
    numberOfNames = Convert.ToInt32(Console.ReadLine());
}

string[][] genderCorrectNames = names.Where(name => name[1] == genderName).ToArray();
string[] newNames = new string[genderCorrectNames.Length];
for (int i = 0; i < genderCorrectNames.Length; i++)
{
    newNames[i] = genderCorrectNames[i][0];
}


NameGenerator nameGenerator= new NameGenerator(3);
nameGenerator.Initialize(newNames);
// nameGenerator.Initialize(["Elodie", "Felix", "Soren", "Hugo", "Cillian", "Elio", "Elowen", "Otto", "Romy", "Magnus", "Astrid", "Flora", "Ronan", "Sylvie", "Ottilie", "Casimir", "Nico", "Cleo", "Cosmo", "Lorcan"]);
for(int i = 0; i < numberOfNames; i++)
{
    Console.WriteLine(nameGenerator.GenerateName());
}