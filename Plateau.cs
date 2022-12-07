using System;
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
        private Dictionnaire Dico;
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
        /// Retourne une matrice vide (tous les élements sont des ' ')
        /// 
        /// </summary>
        /// <returns></returns>
        public char[,] CrationMatrice()
        {
            char[,] mat = new char[Ligne, Colonne];
            for(int i = 0; i < Ligne; i++)
            {
                for(int j = 0; j < Colonne; j++) { mat[i, j] = ' '; }
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
        /// Vrai si le point d'ancrage est possible
        /// Possible si toutes les cases suivantes selon la direction sont valides
        /// Valides si soit ' ' soit lettre en commun
        /// Prend en paramètre un string, un char [,] et 3 int (direction, positions x et position y)
        /// </summary>
        /// <param name="word"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool PointDAncrage(string word, int direction, int x, int y, char [,] plateau)
        {
            bool res = false;
            // Pour les deux premières difficultés:
            if(Difficult == 1 || Difficult == 2)
            {
                //Si le mot à la place pour être à l'horizontale:
                if(direction == 0 && word.Length < Colonne - y + 1) 
                {
                    /*Res = true si toutes les cases suivantes sont valables :
                      - case vide 
                      - caractère en commun / caractère d'intersection */
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == plateau[x, y + i] || plateau[x, y + i] == ' ')
                        {
                            res = true;
                        }
                        else { return false; }
                    }
                }
                // Pareil pour la verticale
                if(direction == 1 && word.Length < Ligne - x + 1)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == plateau[x + i, y] || plateau[x + i, y] == ' ')
                        {
                            res = true;
                        }
                        else { return false; }
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Retourne une matrice 2D
        /// récupérer la liste de mots aléatoire 
        /// Utilisée pour remplir la grille avec les mots à trouver
        /// </summary>
        /// <returns></returns>
        public char[,] RemplissageMotATrouver()
        {
            char[,] plateau = this.CrationMatrice();
            List<string> choixMots = this.ChoixMots();
            Random r = new Random();
            int counter = 0; // sera décrémenter jusqu'à 0

            switch (Difficult)
            {
                case 1:
                    {
                        while(counter < choixMots.Count)
                        {
                            string word = choixMots[counter];
                            int direction = r.Next(0, 2); //2 directions : (E,S):(0,1)
                            int position_x = r.Next(0, Ligne);
                            int position_y = r.Next(0, Colonne);
                            if (PointDAncrage(word, direction, position_x, position_y, plateau))
                            {
                                for (int i = 0; i < word.Length; i++)
                                {
                                    if (direction == 0) { plateau[position_x, position_y + i] = word[i]; } //rempli une ligne du plateau
                                    else { plateau[position_x + i, position_y] = word[i]; }                //rempli une colonne du plateau
                                }
                            }
                            counter++;
                            // belec au cas où ya pas de place 
                        }                    
                        break;
                    }
                case 2:
                    {
                        break;
                    }
            }
            return plateau;
        }

        /// <summary>
        /// Remplie une matrice avec des lettres aléatoires
        /// </summary>
        /// <returns></returns>
        public char [,] Remplissage()
        {
            char[,] res = this.RemplissageMotATrouver();
            Random random = new Random();
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                  'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'  };

            //parcours le plateau et rempli les cases vides avec des lettre aléatoires
            for (int k = 0; k < res.GetLength(0); k++)
            {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    if (res[k, j] == ' ') { res[k, j] = alphabet[random.Next(0, 26)]; }
                }
            }
            return res;
        }

        /// <summary>
        /// Affiche la matrice retournée par Remplissage()
        /// </summary>
        /// <param name="afficher"></param>
        public void Affichage(char[,] afficher)
        {
            for(int i = 0; i < afficher.GetLength(0); i++)
            {
                for(int j = 0; j < afficher.GetLength(1); j++)
                {
                    Console.Write(afficher[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        //public bool Test_Plateau a besoin de public bool lettre_suivante.
    }
}