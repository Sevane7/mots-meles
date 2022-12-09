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
        static Joueur InitialisationJoueur()
        {
            Joueur joueur = null;
            Console.WriteLine("Entrez le nom du joueur 1 :");
            joueur.Nom = Console.ReadLine();
            joueur.Scores = 0;
            TimeSpan timer = TimeSpan.FromSeconds(0);
            joueur.Chrono = timer;
            joueur.Mots_trouves = new List <string> {""};
            return joueur;
        }
        static void Main(string[] args)
        {
            Dictionnaire dico = new Dictionnaire("francais", "MotsPossiblesFR.txt");

            Joueur joueur_1 = InitialisationJoueur();
            Joueur joueur_2 = InitialisationJoueur();

            int difficult = 1;
            int lignes = (difficult - 1) * 5 + 9;
             
            Plateau plateau = new Plateau(difficult, lignes, lignes, dico);


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