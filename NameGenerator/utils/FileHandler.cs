using System;
using System.Collections.Generic;
using System.IO;

namespace AliElRogbany.Utils
{
    class FileHandler
    {
        private string filePath = "";

        public FileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public List<string[]> ReadCSV()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    List<string[]> values = new List<string[]>();
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line != null && !string.IsNullOrEmpty(line.Trim())) // Check for null and empty line
                            {
                                values.Add(line.Split(","));
                            }
                        }
                    }
                    return values;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return new List<string[]>(); // or handle the exception as appropriate
                }
            }
            return new List<string[]>();
        }
    }
}