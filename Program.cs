using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Mots_Meles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionnaire dico = new Dictionnaire("francais", "MotsPossiblesFR.txt");
            Plateau plateau = new Plateau(1, true, 6, 7, 8);
            for(int i = 0; i<plateau.ChoixMots(dico).Count; i++)
            {
                Console.WriteLine(plateau.ChoixMots(dico)[i]);
            }

            



            Console.WriteLine("Press a key to close the console.");
            Console.ReadKey();
        }
        

    }
}