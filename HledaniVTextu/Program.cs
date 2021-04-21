using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HledaniVTextu
{
    class Program
    {
        static void Main()
        {
            string path = "testovaci.txt";
            Words.CreateList(path);
        }
    }
    public class Words 
    {
        public static void CreateList(string path)
        {
            HashSet<string> pouzitaSlova = new HashSet<string>();
            string[] poleSlov = File.ReadAllLines(path);
            string[] slovaVRadku;
            int radek = 0;
            int zachytRadek = 0;
            int slovo = 0;
            string hledaneSlovo = string.Empty;
            while (radek != poleSlov.Length)
            {
                slovaVRadku = poleSlov[radek].Split(' ');

                if (radek == zachytRadek)
                    hledaneSlovo = slovaVRadku[slovo];

                if (pouzitaSlova.Contains(hledaneSlovo) == false)
                {
                    if (poleSlov[radek].Contains(hledaneSlovo))
                    {
                        for (int i = 0; i < slovaVRadku.Length; i++)
                        {
                            if (hledaneSlovo.Equals(slovaVRadku[i]))
                            { Console.WriteLine($"{hledaneSlovo} - {radek}"); break; }
                        }
                    }
                    radek++;
                    if (radek == poleSlov.Length)
                    {
                        pouzitaSlova.Add(hledaneSlovo);
                        radek = 0;//////////////Nesmí se začít od řádku 0, ale od toho dalšího
                        if (slovo <= slovaVRadku.Length)
                        {  slovo++; radek = zachytRadek; }
                        else { slovo = 0; radek = ++zachytRadek; }
                    }
                }
            }
        }
    }
}
