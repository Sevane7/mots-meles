using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Ex = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace Mots_Meles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Le jeu commence à la difficulté 1 et s'arrête à la fin de la difficulté 4

            Jeu mot_mele = new Jeu(1);
            while(mot_mele.Difficult < 5)
            {
                //une manche fait jouer les deux joueurs pour chaque difficulté
                mot_mele.Manche();
                mot_mele.Difficult++;
                mot_mele.Joueur1.Chrono = 0;
                mot_mele.Joueur2.Chrono = 0;
                mot_mele.TempsDeJeu += 60;
            }

            mot_mele.ResulstatPartie();
            


            //test plateau
            /*for(int i = 0; i<plateau.MotsATrouver.Count; i++)
            {
                Console.Write(plateau.MotsATrouver[i] + " ");
            }
            Console.WriteLine();

            plateau.Affichage(plateau.Remplissage());*/

            //test Excel (probleme avec le path)
            /*string path = Path.GetFullPath("Test.xlsx");
            Console.WriteLine(path);  //OK

            Application app = new Application();
            Workbook wb = app.Workbooks.Open(path); 
            Worksheet ws = app.Worksheets[0];

            Excel ex = new Excel(path, 2);

            ex.WriteString(2, plateau.Ligne + 2, 1, plateau.Colonne + 1);*/


            Console.WriteLine("Press a key to close the console.");
            Console.ReadKey();
        }
    }
}