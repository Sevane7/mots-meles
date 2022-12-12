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
            //Initialise le nom des joeurs
            //leur score, mots trouvés et chrono sont nulls
            Console.WriteLine("Entrez le nom du joueur 1 :");
            Joueur joueur_1 = new Joueur(Console.ReadLine());
            Console.WriteLine("Entrez le nom du joueur 2 :");
            Joueur joueur_2 = new Joueur(Console.ReadLine());

            //Initialise le dictionnaire (en anglais ou en français)
            Dictionnaire dico;
            Console.WriteLine("Dans quelle langue voulez-vous que les mots soient? \n (F = Francais) \n (A = Anglais)");
            string lg = Console.ReadLine();
            if (lg.ToUpper() == "A") { dico = new Dictionnaire("anglais", "MotsPossiblesEN.txt"); }
            else { dico = new Dictionnaire("francais", "MotsPossiblesFR.txt"); }

            //initilaise le temps de jeu (en minutes)
            Console.WriteLine("Combien de secondes pour la première manche? (100 secondes seront ajoutées à chaque manche)");
            long gametime = Convert.ToInt64(Console.ReadLine());

            //Le jeu commence à la difficulté 1 et s'arrête à la fin de la difficulté 4
            int difficult = 1;            
            while(difficult < 5)
            {
                int lignes = (difficult - 1) * 5 + 9;
                for(int i = 0; i < 2; i++)
                {
                    Plateau plateau = new Plateau(difficult, lignes, lignes, dico);
                    plateau.Affichage();
                    Jeu mots_meles = new Jeu(joueur_1, joueur_2, plateau, (gametime + 100 * (difficult - 1)));
                    if (i == 0) mots_meles.Tour(joueur_1);
                    else mots_meles.Tour(joueur_2);
                }
                difficult++;
                joueur_1.Chrono = 0;
                joueur_2.Chrono = 0;
            }

            //Resultat de la partie
            string res = ""; 
            //En fonction du score
            if (joueur_1.Scores != joueur_2.Scores)
            {
                if (joueur_1.Scores > joueur_2.Scores)
                {
                    res += $"{joueur_1.Nom} a gagné la partie avec {joueur_1.Scores} points";
                }
                else
                {
                    res += $"{joueur_2.Nom} a gagné la partie avec {joueur_2.Scores} points";
                }
            }
            //Sinon en fonction du chrono
            else
            {
                if (joueur_1.Chrono_total < joueur_2.Chrono_total)
                {
                    res += $"{joueur_1.Nom} a gagné la partie avec un chrono de {joueur_1.Chrono_total}";
                }
                else
                {
                    res += $"{joueur_2.Nom} a gagné la partie avec un chrono de {joueur_2.Chrono_total}";
                }
            }
            Console.WriteLine(res);




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