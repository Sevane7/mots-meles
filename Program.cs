using System;
using System.IO;
using System.IO.File;
using System.Collections.Generic;
using System.Text;

namespace Mots_Meles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            Joueur joueur1 = new Joueur();
            Joueur joueur2 = new Joueur();
            Console.WriteLine("Entrez le nom du joueur 1");
            this.nom.joueur1 = Console.ReadLine();
            Console.WriteLine("Entrez le nom du joueur 2");
            this.nom.joueur2 = Console.ReadLine();

        }
        public string[] GenererMots(string path)
        {
            if (this.difficulty == 1)
            {

            }
        }
        private void ReadFile(string filename)
        {
            try
            {
                string[] file_lines = File.ReadAllLines(filename);
                tab_value = new string[file_lines.Length];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}