﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Mots_Meles
{
    internal class Plateau
    {
        private int difficult;   // niveau de difficulté
        private bool existe;     // true si dans le dictionnaire
        private int colonne;     // nombre de colonne par grille
        private int ligne;       // nombre de ligne par grille
        private int mot;         // nombre de mot par grille
        Dictionnaire Dico;
        private Excel ex;
        public Plateau(int difficult, bool existe, int colonne, int ligne, int mot, Dictionnaire Dico)
        {
            this.difficult = difficult;
            this.existe = existe;
            this.colonne = colonne;
            this.ligne = ligne;
            this.mot = mot;
            this.Dico = Dico;
        }
        public int Difficult 
        {
            get { return this.difficult; }
            set { this.difficult = value;  }
        }
        public bool Existe { get { return this.existe; } }
        public int Colonne { get { return this.colonne; } }
        public int Ligne { get { return this.ligne; } }
        public int Mot { get { return this.mot; } }


        /// <summary>
        /// Retourne une matrice vide (tous les élements sont des "")
        /// 
        /// </summary>
        /// <returns></returns>
        public string[,] CrationMatrice()
        {
            string[,] mat = new string[Ligne, Colonne];
            for(int i = 0; i < Ligne; i++)
            {
                for(int j = 0; j < Colonne; j++) { mat[i, j] = ""; }
            }
            return mat;
        }

        /// <summary>
        /// Retourne la liste de mots à trouver
        /// </summary>
        /// <param name="Dico"></param>
        /// <returns></returns>
        public List <string> ChoixMots() 
        {
            List<string> WordsToFind = new List<string> { };
            Random aleatoire = new Random();

            for (int i = 0; i < 9 + 5 * (Difficult - 1); i++)
            {
                    if (Difficult == 1 || Difficult == 2)
                    {
                        int tabMot = aleatoire.Next(0, Ligne + 1);                      //autant de lettres possibles que de lignes 
                        int index = aleatoire.Next(0, Dico.Mots[tabMot].Length);    //choisi un index aléatoire dans le tabMot
                        WordsToFind.Add(Dico.Mots[tabMot][index]);                  //l'ajoute à la liste 
                    }
                    
            }
            return WordsToFind;
        }

        /// <summary>
        /// Retourne un bool
        /// Vrai si le point d'ancrage du mot à trouver bon, faux sinon
        /// Prend en paramètre un string et 3 int : direction, positions x et y
        /// </summary>
        /// <param name="word"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool PointDAncrage(string word, int direction, int x, int y)
        {
            bool res = false;
            if(Difficult == 1 || Difficult == 2)
            {
                if(direction == 0 && word.Length < Colonne - y) { res = true; }
                if(direction == 1 && word.Length < Ligne - x) { res = true; }
            }
            return res;
        }

        /// <summary>
        /// Retourne une matrice 2D
        /// récupérer la liste de mots aléatoire 
        /// Utilisée pour remplir la grille
        /// </summary>
        /// <returns></returns>
        public string[,] ReplissageMotATrouver()
        {
            string[,] res = null;
            List<string> choixMots = ChoixMots();
            Random r = new Random();

            switch (Difficult)
            {
                case 1:
                    {
                        foreach (string word in choixMots)
                        {
                            int direction = r.Next(0, 2); //2 directions : (E,S):(0,1)
                            int position_x = r.Next(0, Ligne);
                            int position_y = r.Next(0, Colonne);
                            if(PointDAncrage(word, direction, position_x, position_y))
                            {

                            }
                        }                      
                        break;
                    }
                case 2:
                    {
                        break;
                    }
            }
            return res;
        }

        /// <summary>
        /// retourne une matrice 2D
        /// remplie une matrice avec des lettres aléatoires
        /// </summary>
        /// <returns></returns>
        public string[,] RemplissageChar(string[,] A_remplir)
        {
            Random random = new Random();
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                  'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'  };
            string[,] plateau = new string [A_remplir.GetLength(0),A_remplir.GetLength(1)];

            //teste toutes les difficultés
            for (int i = 1; i < 6; i++) 
            {
                // pour la difficultée actuelle
                if (i == Difficult) 
                {
                    //parcours le plateau et rempli les cases vides avec des lettre aléatoires
                    for (int k = 0; k < 7; i++) //belec
                    {
                        for (int j = 0; j < 6; j++) //belec
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