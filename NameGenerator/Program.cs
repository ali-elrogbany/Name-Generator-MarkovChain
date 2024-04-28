using AliElRogbany.NameGenerator;

NameGenerator nameGenerator= new NameGenerator(2);
nameGenerator.Initialize(["Elodie", "Felix", "Soren", "Hugo", "Cillian", "Elio", "Elowen", "Otto", "Romy", "Magnus", "Astrid", "Flora", "Ronan", "Sylvie", "Ottilie", "Casimir", "Nico", "Cleo", "Cosmo", "Lorcan"]);
Console.WriteLine(nameGenerator.GenerateName());