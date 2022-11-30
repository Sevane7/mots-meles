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
        public void ToFile(string filename)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename);


            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        public string [,] Remplissage(int difficulty)
        {
            string[,] plateau = null;
            switch(difficulty)
            {
                case 1:                             // difficulté 1 : plateau de taille 7 x 6
                    plateau = new string[7, 6]; 
                    for(int i = 0; i< 7;i++)
                    {
                        for(int j = 0; j<6; j++)
                        {
                            if (plateau[i, j] == "")
                            {

                            }
                        }
                    }
                    break;
            }
            return plateau;
        }

        //public bool Test_Plateau a besoin de public bool lettre_suivante.
    }
}