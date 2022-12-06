using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mots_Meles
{
    internal class Plateau
    {
        private int difficult;   // niveau de difficulté
        private bool existe;     // true si dans le dictionnaire
        private int colonne;     // nombre de colonne par grille
        private int ligne;       // nombre de ligne par grille
        private int mot;         // nombre de mot par grille
        private string[] mot_a_trouver;
        private Excel ex;
        public Plateau(int difficult, bool existe, int colonne, int ligne, int mot)
        {
            this.difficult = difficult;
            this.existe = existe;
            this.colonne = colonne;
            this.ligne = ligne;
            this.mot = mot;
        }
        public int Difficult { get { return this.difficult; } }
        public bool Existe { get { return this.existe; } }
        public int Colonne { get { return this.colonne; } }
        public int Ligne { get { return this.ligne; } }
        public int Mot { get { return this.mot; } }
        public string[,] Remplissage()
        {
            Random random = new Random();
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                  'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'  };
            string[,] plateau = null;

            //teste toutes les difficultés
            for (int i = 1; i < 6; i++) 
            {
                // pour la difficultée actuelle
                if (i == this.difficult) 
                {
                    //instancie le plateau avec les bonnes dimensions
                    plateau = new string[3 + 5 * i, 1 + 5 * i]; 


                    //parcours le plateau et rempli les cases vides avec des lettre aléatoires
                    for (int k = 0; k < 7; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (plateau[k, j] == "") { plateau[k, j] = alphabet[random.Next(0, 27)].ToString(); }
                        }
                    }
                }
            }
            return plateau;
        }

        //public bool Test_Plateau a besoin de public bool lettre_suivante.
    }
}