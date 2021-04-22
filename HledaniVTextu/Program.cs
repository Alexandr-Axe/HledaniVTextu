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
            string path = "check.txt";
            Words Slova = new Words();
            Slova.CreateList(path, "testovaci.txt");
        }
    }
    public class Words
    {
        Dictionary<string, List<int>> seznamSlov = new Dictionary<string, List<int>>(); //Vytvářím si seznam slov, která mají řádky jako list integerů
        void ReadLine(string path)
        {
            using (StreamReader sr = new StreamReader(path)) //Používáme using pro jednodušší práci (abych nemusel sám odchytávat, kdy se dostanu na konec souboru)
            {
                int radek = 0; //Řádek, na kterém se nacházím
                string slovo = string.Empty;
                while (sr.EndOfStream.Equals(false)) //Dokud se nedostanu na konec souboru, budu číst řádky
                {
                    radek++; //Posunul jsem se o řádek
                    string[] slovaNaRadku = sr.ReadLine().Split(' '); //Beru si veškerá slova na určitém řádku
                    for (int i = 0; i < slovaNaRadku.Length; i++) //Dokud se nedostanu na poslední slovo na řádce, proces se opakuje
                    {
                        slovo = slovaNaRadku[i].ToLower();
                        if (seznamSlov.ContainsKey(slovo).Equals(false))
                        {
                            List<int> radky = new List<int>();
                            radky.Add(radek);
                            seznamSlov.Add(slovo, radky);
                        }
                        else
                        {
                            List<int> radkyMezikrok = seznamSlov[slovo];
                            radkyMezikrok.Add(radek);
                            seznamSlov[slovo] = radkyMezikrok;
                        }
                    }
                }
            }
        }
        public void CreateList(string path, string save) 
        {
            ReadLine(path);
            using (StreamWriter sw = new StreamWriter(save))
            {
                foreach (var Item in seznamSlov) //var mi zjednoduší práci a nemusím zapisovat přímo typ proměnné
                {
                    sw.WriteLine(Item.Key + " - " + string.Join(", ", Item.Value.Select(x => x.ToString()).ToArray()));
                }
            }
        }
    }
}
