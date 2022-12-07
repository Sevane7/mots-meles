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
            Plateau plateau = new Plateau(1, 7, 8, 8, dico);

            for(int i = 0; i<plateau.MotsATrouver.Count; i++)
            {
                Console.Write(plateau.MotsATrouver[i] + " ");
            }
            Console.WriteLine();

            plateau.Affichage(plateau.Remplissage());



            Console.WriteLine("Press a key to close the console.");
            Console.ReadKey();
        }
        

    }
}