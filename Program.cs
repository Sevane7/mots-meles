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
            
            dico.AfficherTousMots(5);



            Console.WriteLine("Press a key to close the console.");
            Console.ReadKey();
        }
        

    }
}