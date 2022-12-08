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
        private int difficult;
        private int colonne;
        private int ligne;
        private List<string> motsATrouver;
        private Dictionnaire Dico;

        //Constructeur
        public Plateau(int difficult, int colonne, int ligne, Dictionnaire Dico)
        {
            this.difficult = difficult;
            this.colonne = colonne;
            this.ligne = ligne;
            this.Dico = Dico;
            this.motsATrouver = TriListe();
        }

        /// <summary>
        /// Propriété en lecture de Mot A Trouver
        /// </summary>
        public List <string> MotsATrouver { get { return this.motsATrouver; } }

        /// <summary>
        /// Propriété en lecture de la difficulté
        /// </summary>
        public int Difficult { get { return this.difficult; } }

        /// <summary>
        /// Propriété 
        /// </summary>
        public int Colonne { get { return this.colonne; } }

        /// <summary>
        /// Propriété en Lecture du nombre de colonne
        /// </summary>
        public int Ligne { get { return this.ligne; } }

        /// <summary>
        /// Retourne une matrice vide (tous les élements sont des ' ')
        /// 
        /// </summary>
        /// <returns></returns>
        public char[,] CreationMatrice()
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
            Random r = new Random();

            for (int i = 0; i < 8 + 5 * (Difficult - 1); i++)
            {
                    if (Difficult == 1 || Difficult == 2)
                    {
                        int tabMot = r.Next(1, Ligne - 1);                  //autant de lettres possibles que de lignes 
                        int index = r.Next(0, Dico.Mots[tabMot].Length);    //choisi un index aléatoire dans le tabMot
                        WordsToFind.Add(Dico.Mots[tabMot][index]);                  //l'ajoute à la liste 
                    }
                    if(Difficult == 3 || Difficult == 4)
                    {
                        int tabMot = r.Next(2, Math.Min(Ligne - 2, 14));
                        int index = r.Next(0, Dico.Mots[tabMot].Length);
                        WordsToFind.Add(Dico.Mots[tabMot][index]);
                    }                    
            }
            return WordsToFind;
        }

        /// <summary>
        /// Retourne une liste
        /// Tri la liste des mots à trouver 
        /// </summary>
        /// <returns></returns>
        public List <string> TriListe()
        {
            List <string> list = this.ChoixMots();
            for(int i = 0; i <= list.Count; i++)
            {
                for(int j = 0; j < list.Count - 1; j++)
                {
                    if (list[j].Length < list[j+1].Length)
                    {
                        string commute = list[j + 1];
                        list[j + 1] = list[j]; 
                        list[j] = commute; 
                    }
                }
            }
            return list;
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
        public bool PointDAncrage(string word, int direction, int x, int y, char[,] plateau)
        {
            bool res = false;

            //Si le mot peut être placé vers l'Est
            if (direction == 0 && word.Length < Colonne - y + 1)
            {
                /*Res = true si toutes les cases suivantes sont valables :
                  - cases vides
                  - caractères en commun / caractères d'intersection */
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x, y + i] || plateau[x, y + i] == ' ') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Sud
            if (direction == 1 && word.Length < Ligne - x + 1)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x + i, y] || plateau[x + i, y] == ' ') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers l'Ouest
            if (direction == 2 && word.Length < y + 1)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x, y - i] || plateau[x, y - i] == ' ') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Nord
            if (direction == 3 && word.Length < x + 2)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x - i, y] || plateau[x - i, y] == ' ') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Nord-Est
            if (direction == 4 && word.Length < Math.Min(x + 1, Colonne - y + 1))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x - i, y + i] || plateau[x - i, y + i] == ' ') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Sud-Ouest
            if(direction == 5 && word.Length < Math.Min(Ligne - x + 1, y + 1))
            {
                for(int i = 0; i < word.Length;i++)
                {
                    if (word[i] == plateau[x + i, y - i] || plateau[x + i, y - i] == ' ') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Nord-Ouest
            if (direction == 6 && word.Length < Math.Min(x + 1, y + 1))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x - i, y - i] || plateau[x - i, y - i] == ' ') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Sud-Est
            if (direction == 7 && word.Length < Math.Min(Ligne - x + 1,Colonne - y + 1))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x + i, y + i] || plateau[x + i, y + i] == ' ') { res = true; }
                    else { return false; }
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
            char[,] plateau = this.CreationMatrice();
            Random r = new Random();
            int counter = 0; 

            switch (Difficult)
            {
                case 1:
                {
                    while(counter < MotsATrouver.Count)
                    {
                        int itteration = 0;
                        string word = MotsATrouver[counter];
                        int direction = r.Next(0, 2); //2 directions : (E,S):(0,1)
                        int x = r.Next(0, Ligne);
                        int y = r.Next(0, Colonne);
                        if (PointDAncrage(word, direction, x, y, plateau))
                        {
                            for (int i = 0; i < word.Length; i++)
                            {
                                //condition ? 
                                //(direction == 0) ? plateau[x, y + i] = word[i] : plateau[x + i, y] = word[i];

                                if (direction == 0) { plateau[x, y + i] = word[i]; } //rempli une ligne du plateau
                                else { plateau[x + i, y] = word[i]; }                //rempli une colonne du plateau
                            }
                            counter++;
                        }
                        itteration++;
                        if(itteration > 10000)
                        {
                            plateau = CreationMatrice();
                            counter= 0;
                        }
                    }                    
                    break;
                }
                case 2:
                {
                    while(counter < MotsATrouver.Count)
                    {
                        int itteration = 0;
                        string word = MotsATrouver[counter];
                        int direction = r.Next(0, 4); //4 directions : (E,S,O,N) : (0,1,2,3)
                        int x = r.Next(0, Ligne);
                        int y = r.Next(0, Colonne);
                        if(PointDAncrage(word, direction, x, y, plateau))
                        {
                            for(int i = 0; i < word.Length; i++)
                            {
                                switch (direction)
                                {
                                    case 0:
                                        plateau[x, y + i] = word[i];
                                        break;

                                    case 1:
                                        plateau[x + i, y] = word[i];
                                        break;
                                    case 2:
                                        plateau[x, y - i] = word[i];
                                        break;
                                    case 3:
                                        plateau[x - i, y] = word[i];
                                        break;
                                }
                            }
                            counter ++;
                        }
                        itteration++;
                        if (itteration > 10000)
                        {
                            plateau = CreationMatrice();
                            counter = 0;
                        }
                    }
                    break;
                }
                case 3:
                {
                    while (counter < MotsATrouver.Count)
                    {
                        int itteration = 0;
                        string word = MotsATrouver[counter];
                        int direction = r.Next(0, 6); //6 directions : (E,S,O,N,N-E,S-O) : (0,1,2,3,4,5)
                        int x = r.Next(0, Ligne);
                        int y = r.Next(0, Colonne);
                        if (PointDAncrage(word, direction, x, y, plateau))
                        {
                            for (int i = 0; i < word.Length; i++)
                            {
                                switch (direction)
                                {
                                    case 0:
                                        plateau[x, y + i] = word[i];
                                        break;
                                    case 1:
                                        plateau[x + i, y] = word[i];
                                        break;
                                    case 2:
                                        plateau[x, y - i] = word[i];
                                        break;
                                    case 3:
                                        plateau[x - i, y] = word[i];
                                        break;
                                    case 4:
                                        plateau[x - i, y + i] = word[i];
                                        break;
                                    case 5:
                                        plateau[x + i, y - i] = word[i];
                                        break;
                                }
                            }
                            counter++;
                        }
                        itteration++;
                        if (itteration > 10000)
                        {
                            plateau = CreationMatrice();
                            counter = 0;
                        }
                    }
                    break;
                }
                case 4:
                {
                     while (counter < MotsATrouver.Count)
                     {
                        int itteration = 0;
                        string word = MotsATrouver[counter];
                        int direction = r.Next(0, 8); //8 directions : (E,S,O,N,N-E,S-O,N-O,S-E) : (0,1,2,3,4,5,6,7)
                        int x = r.Next(0, Ligne);
                        int y = r.Next(0, Colonne);
                        if (PointDAncrage(word, direction, x, y, plateau))
                        {
                        Console.Write(x + " " + y + " " + direction + "\n");
                            for (int i = 0; i < word.Length; i++)
                            {
                                switch (direction)
                                {
                                    case 0:
                                        plateau[x, y + i] = word[i];
                                        break;
                                    case 1:
                                        plateau[x + i, y] = word[i];
                                        break;
                                    case 2:
                                        plateau[x, y - i] = word[i];
                                        break;
                                    case 3:
                                        plateau[x - i, y] = word[i];
                                        break;
                                    case 4:
                                        plateau[x - i, y + i] = word[i];
                                        break;
                                    case 5:
                                        plateau[x + i, y - i] = word[i];
                                        break;
                                    case 6:
                                        plateau[x - i, y - i] = word[i];
                                        break;
                                    case 7:
                                        plateau[x + i, y + i] = word[i];
                                        break;
                                }
                            }
                            counter++;
                        }
                        itteration++;
                        if (itteration > 10000)
                        {
                            plateau = CreationMatrice();
                            counter = 0;
                        }
                     }
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