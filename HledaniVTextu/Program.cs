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
            string path = "check.txt"; //Cesta k souboru, ze kterého čtu
            Words Slova = new Words();
            Slova.CreateList(path, "testovaci.txt");
        }
    }
    public class Words
    {
        readonly Dictionary<string, List<int>> seznamSlov = new Dictionary<string, List<int>>(); //Vytvářím si seznam slov, která mají řádky jako list integerů
        void ReadLine(string path) //Metoda, která mi vytvoří pole slov a jejich řádků
        {
            using (StreamReader sr = new StreamReader(path)) //Používáme using pro jednodušší práci, pohodlný syntax, který zajišťuje správné použití IDisposable objektů
            {
                int radek = 0; //Řádek, na kterém se nacházím
                string slovo = string.Empty; //Slovo, které zrovna kontrolujeme
                while (sr.EndOfStream.Equals(false)) //Dokud se nedostanu na konec souboru, budu číst řádky
                {
                    radek++; //Posunul jsem se o řádek
                    string[] slovaNaRadku = sr.ReadLine().Split(' '); //Beru si veškerá slova na určitém řádku
                    for (int i = 0; i < slovaNaRadku.Length; i++) //Dokud se nedostanu na poslední slovo na řádce, proces se opakuje
                    {
                        slovo = slovaNaRadku[i].ToLower(); //Převedení písmen slov v řádku na malá, abych neměl case-sensitive kód
                        if (seznamSlov.ContainsKey(slovo).Equals(false)) //Pokud nemám zapsané slovo v listu, vytvoří se
                        {
                            List<int> radky = new List<int>(); //Vytvoření listu řádků, na kterých se slovo objevuje
                            radky.Add(radek); //Přidání řádku do listu
                            seznamSlov.Add(slovo, radky); //Přiřazení řádků ke slovům v dictionary
                        }
                        else //V případě, že už mám slovo projeté, pouze se připíše řádek do listu
                        {
                            List<int> radkyMezikrok = seznamSlov[slovo]; //Vytvoření nového listu integerů, který obsahuje původní řádky slova
                            radkyMezikrok.Add(radek); //Přidání řádku do listu
                            seznamSlov[slovo] = radkyMezikrok; //Vrácení listu do dictionary
                        }
                    }
                }
            }
        }
        public void CreateList(string path, string save) //Vytvoření souboru
        {
            ReadLine(path);
            using (StreamWriter sw = new StreamWriter(save)) //Používáme using pro jednodušší práci, pohodlný syntax, který zajišťuje správné použití IDisposable objektů
            {
                foreach (var Item in seznamSlov) //Každé slovo v dictionary se mi vypíše + jeho řádky
                {
                    sw.WriteLine(Item.Key + " - " + string.Join(", ", Item.Value.Select(i => i.ToString()).ToArray())); //Zapsání do souboru
                    //Zkoušel jsem své oblíbené formátování, ale potom mi to házelo chybu a v souboru se mi místo řádků objevovaly arraye charů
                    //Lambda převzata ze StackOverflow, nejsem si jistý, jak funguje
                }
            }
        }
    }
}
