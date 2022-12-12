using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private char[,] plateau;
        private string filename;

        //Constructeur Pour generer aléatoirement
        public Plateau(int difficult, int colonne, int ligne, Dictionnaire Dico)
        {
            this.difficult = difficult;
            this.colonne = colonne;
            this.ligne = ligne;
            this.Dico = Dico;
            this.motsATrouver = ChoixMotsTri();
            this.plateau = new char[this.ligne, this.colonne];
        }
        public Plateau(string filename)
        {
            this.filename = filename;
            ToRead();
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
        /// Propriété en lecture du nombre de colonnes
        /// </summary>
        public int Colonne { get { return this.colonne; } }

        /// <summary>
        /// Propriété en Lecture du nombre de lignes
        /// </summary>
        public int Ligne { get { return this.ligne; } }

        /// <summary>
        /// Propriété en lecture du plateau
        /// </summary>
        public char[,] Grille { get { return this.plateau; } }

        /// <summary>
        /// Rempli la grille, mots à trouver etc grace à un fichier
        /// </summary>
        /// <param name="filename"></param>
        public void ToRead()
        {
            try
            {
                //lis toutes les lignes du fichier
                string[] file_lines = File.ReadAllLines(filename);

                //split la premiere ligne en fonction des ;
                string[] splitligne0 = file_lines[0].Split(';');

                //Constructeur (premire ligne du fichier indique la difficulté, ligne, colonnes, nombre de mots à trouver)
                this.difficult = Convert.ToInt32(splitligne0[0]);
                this.ligne = Convert.ToInt32(splitligne0[1]);
                this.colonne = Convert.ToInt32(splitligne0[2]);
                this.motsATrouver = new List<string> (Convert.ToInt32(splitligne0[3]));

                //split la deuxieme ligne en fonction des ;
                string[] splitline2 = file_lines[1].Split(';');

                //Remplie la liste de mots à trouver 
                for (int i = 0; i < Convert.ToInt32(splitligne0[3]); i++)
                {
                    this.motsATrouver.Add(splitline2[i]);
                }

                //Remplissage de la grille
                this.plateau = new char[this.ligne, this.colonne];
                
                //Rempli this.plateau avec tous les item à partir de la ligne 2 du fichier 
                for(int i = 0; i < this.ligne; i++)
                {
                    //créer un tableau avec toutes les lettres de la i eme ligne
                    string [] splitlinei = file_lines[i + 2].Split(';');

                    for(int j = 0; j < this.colonne; j++)
                    {
                        this.plateau[i - 2, j] = char.Parse(splitligne0[j]);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Ecrase la grille dans un fichier
        /// </summary>
        public void ToFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(this.filename);
                sw.WriteLine(this.difficult + ";" + this.ligne + ";" + this.colonne + ";" + (8 + (this.difficult - 1) * 5));
                string res = "";
                for (int a = 0; a < this.motsATrouver.Count; a++)
                {
                    res += this.motsATrouver[a] + ";";
                }
                sw.WriteLine(res);
                for (int i = 2; i < this.ligne; i++)
                {
                    for (int j = 0; j < this.colonne; j++)
                    {
                        sw.WriteLine(this.plateau[i, j] + ";");
                    }
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

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
        /// Retourne la liste de mots TRIEE de mots à trouver
        /// </summary>
        /// <param name="Dico"></param>
        /// <returns></returns>
        public List <string> ChoixMotsTri() 
        {
            List<string> WordsToFind = new List<string> { };
            Random r = new Random();

            for (int i = 0; i < 8 + 5 * (Difficult - 1); i++)
            {
                    if (Difficult == 1 || Difficult == 2)
                    {
                        int tabMot = r.Next(1, this.ligne - 2);                  //autant de lettres possibles que de lignes 
                        int index = r.Next(0, this.Dico.Mots[tabMot].Length);    //choisi un index aléatoire dans le tabMot
                        WordsToFind.Add(this.Dico.Mots[tabMot][index]);                  //l'ajoute à la liste 
                    }
                    if(Difficult == 3 || Difficult == 4)
                    {
                        int tabMot = r.Next(2, Math.Min(this.Ligne - 2, 14));
                        int index = r.Next(0, this.Dico.Mots[tabMot].Length);
                        WordsToFind.Add(this.Dico.Mots[tabMot][index]);
                    }                    
            }

            //Tri les mots en fonction de leur taille
            for (int i = 0; i <= WordsToFind.Count; i++)
            {
                for (int j = 0; j < WordsToFind.Count - 1; j++)
                {
                    if (WordsToFind[j].Length < WordsToFind[j + 1].Length)
                    {
                        string commute = WordsToFind[j + 1];
                        WordsToFind[j + 1] = WordsToFind[j];
                        WordsToFind[j] = commute;
                    }
                }
            }

            return WordsToFind;
        }

        /// <summary>
        /// Retourne une liste
        /// Tri la liste des mots à trouver 
        /// </summary>
        /// <returns></returns>
        /*public List <string> TriListe()
        {
            List <string> list = ChoixMots();
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
        }*/

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
        public bool PointDAncrage(string word, int direction, int x, int y)
        {
            bool res = false;

            //Si le mot peut être placé vers l'Est
            if (direction == 0 && word.Length < this.colonne - y + 1)
            {
                /*Res = true si toutes les cases suivantes sont valables :
                  - cases vides
                  - caractères en commun / caractères d'intersection */
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == this.plateau[x, y + i] || this.plateau[x, y + i] == '\0') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Sud
            if (direction == 1 && word.Length < this.ligne - x + 1)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == this.plateau[x + i, y] || this.plateau[x + i, y] == '\0') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers l'Ouest
            if (direction == 2 && word.Length < y + 1)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x, y - i] || plateau[x, y - i] == '\0') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Nord
            if (direction == 3 && word.Length < x + 2)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == plateau[x - i, y] || plateau[x - i, y] == '\0') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Nord-Est
            if (direction == 4 && word.Length < Math.Min(x + 1, this.colonne - y + 1))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == this.plateau[x - i, y + i] || this.plateau[x - i, y + i] == '\0') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Sud-Ouest
            if(direction == 5 && word.Length < Math.Min(this.ligne - x + 1, y + 1))
            {
                for(int i = 0; i < word.Length;i++)
                {
                    if (word[i] == this.plateau[x + i, y - i] || this.plateau[x + i, y - i] == '\0') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Nord-Ouest
            if (direction == 6 && word.Length < Math.Min(x + 1, y + 1))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == this.plateau[x - i, y - i] || this.plateau[x - i, y - i] == '\0') { res = true; }
                    else { return false; }
                }
            }

            //Si le mot peut être placé vers le Sud-Est
            if (direction == 7 && word.Length < Math.Min(this.ligne - x + 1, this.colonne - y + 1))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == this.plateau[x + i, y + i] || this.plateau[x + i, y + i] == '\0') { res = true; }
                    else { return false; }
                }
            }

            return res;
        }

        /// <summary>
            /// Rempli this.plateau avec les mots à trouver
            /// Rempli les cases vides avec des lettres random
            /// </summary>
            /// <returns></returns>
        public void RemplissageMotATrouver()
        {
            Random r = new Random();
            int counter = 0; 

            //En fonction de la difficulté, la grille sera remplie avec les mots à trouver dans les bonnes directions 
            switch (Difficult)
            {
                case 1:
                {
                    while(counter < this.motsATrouver.Count)
                    {
                        int itteration = 0;
                        string word = this.motsATrouver[counter];
                        //2 directions : (E,S):(0,1)
                        int direction = r.Next(0, 2); 
                        int x = r.Next(0, this.ligne);
                        int y = r.Next(0, this.colonne);
                        if (PointDAncrage(word, direction, x, y))
                        {
                            for (int i = 0; i < word.Length; i++)
                            {
                                //condition ? 
                                //(direction == 0) ? plateau[x, y + i] = word[i] : plateau[x + i, y] = word[i];

                                //mets le mot une ligne du plateau
                                if (direction == 0) {this.plateau[x, y + i] = word[i]; }
                                //mets le mot sur une colonne du plateau
                                else { this.plateau[x + i, y] = word[i]; }                
                            }
                            counter++;
                        }
                        itteration++;
                        if(itteration > 10000)
                        {
                                this.plateau = new char[this.ligne, this.colonne];
                                counter = 0;
                        }
                    }                    
                    break;
                }
                case 2:
                {
                    while(counter < this.motsATrouver.Count)
                    {
                        int itteration = 0;
                        string word = this.motsATrouver[counter];
                        //4 directions : (E,S,O,N) : (0,1,2,3)
                        int direction = r.Next(0, 4);
                        int x = r.Next(0, this.ligne);
                        int y = r.Next(0, this.colonne);
                        if(PointDAncrage(word, direction, x, y))
                        {
                            for(int i = 0; i < word.Length; i++)
                            {
                                switch (direction)
                                {
                                    case 0:
                                        this.plateau[x, y + i] = word[i];
                                        break;

                                    case 1:
                                        this.plateau[x + i, y] = word[i];
                                        break;
                                    case 2:
                                        this.plateau[x, y - i] = word[i];
                                        break;
                                    case 3:
                                        this.plateau[x - i, y] = word[i];
                                        break;
                                }
                            }
                            counter ++;
                        }
                        itteration++;
                        if (itteration > 10000)
                        {
                            this.plateau = new char[this.ligne, this.colonne];
                            counter = 0;
                        }
                    }
                    break;
                }
                case 3:
                {
                    while (counter < this.motsATrouver.Count)
                    {
                        int itteration = 0;
                        string word = this.motsATrouver[counter];
                        //6 directions : (E,S,O,N,N-E,S-O) : (0,1,2,3,4,5)
                        int direction = r.Next(0, 6); 
                        int x = r.Next(0, this.ligne);
                        int y = r.Next(0, this.colonne);
                        if (PointDAncrage(word, direction, x, y))
                        {
                            for (int i = 0; i < word.Length; i++)
                            {
                                switch (direction)
                                {
                                    case 0:
                                        this.plateau[x, y + i] = word[i];
                                        break;
                                    case 1:
                                        this.plateau[x + i, y] = word[i];
                                        break;
                                    case 2:
                                        this.plateau[x, y - i] = word[i];
                                        break;
                                    case 3:
                                        this.plateau[x - i, y] = word[i];
                                        break;
                                    case 4:
                                        this.plateau[x - i, y + i] = word[i];
                                        break;
                                    case 5:
                                        this.plateau[x + i, y - i] = word[i];
                                        break;
                                }
                            }
                            counter++;
                        }
                        itteration++;
                        if (itteration > 10000)
                        {
                            this.plateau = new char[this.ligne, this.colonne];
                            counter = 0;
                        }
                    }
                    break;
                }
                case 4:
                {
                     while (counter < this.motsATrouver.Count)
                     {
                        int itteration = 0;
                        string word = this.motsATrouver[counter];
                        //8 directions : (E,S,O,N,N-E,S-O,N-O,S-E) : (0,1,2,3,4,5,6,7)
                        int direction = r.Next(0, 8); 
                        int x = r.Next(0, this.ligne);
                        int y = r.Next(0, this.colonne);
                        if (PointDAncrage(word, direction, x, y))
                        {
                        Console.Write(x + " " + y + " " + direction + "\n");
                            for (int i = 0; i < word.Length; i++)
                            {
                                switch (direction)
                                {
                                    case 0:
                                        this.plateau[x, y + i] = word[i];
                                        break;
                                    case 1:
                                        this.plateau[x + i, y] = word[i];
                                        break;
                                    case 2:
                                        this.plateau[x, y - i] = word[i];
                                        break;
                                    case 3:
                                        this.plateau[x - i, y] = word[i];
                                        break;
                                    case 4:
                                        this.plateau[x - i, y + i] = word[i];
                                        break;
                                    case 5:
                                        this.plateau[x + i, y - i] = word[i];
                                        break;
                                    case 6:
                                        this.plateau[x - i, y - i] = word[i];
                                        break;
                                    case 7:
                                        this.plateau[x + i, y + i] = word[i];
                                        break;
                                }
                            }
                            counter++;
                        }
                        itteration++;
                        if (itteration > 10000)
                        {
                            this.plateau = new char[this.ligne, this.colonne];
                            counter = 0;
                        }
                     }
                     break;
                }
            }
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                  'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'  };

            //parcours le plateau et rempli les cases vides avec des lettre aléatoires
            for (int k = 0; k < this.ligne; k++)
            {
                for (int j = 0; j < this.colonne; j++)
                {
                    if (this.plateau[k, j] == '\0') { this.plateau[k, j] = alphabet[r.Next(0, 26)]; }
                }
            }
        }

        /// <summary>
        /// Affiche la grille avec les numeros de lignes et de colonnes
        /// Appelle RemplissageMotATrouver()
        /// </summary>
        /// <param name="afficher"></param>
        public void Affichage()
        {
            //Affiche les mots à trouver
            for(int i = 0; i < this.motsATrouver.Count; i++)
            {
                Console.Write(this.motsATrouver[i] + " ");
            }
            Console.WriteLine();

            this.RemplissageMotATrouver();
            //Affiche les colonnes
            Console.Write("   ");
            for (int i = 0; i < this.colonne; i++)
            {
                Console.Write(i + "  ");
            }
            Console.WriteLine();

            //Affiche la grille
            for (int i = 0; i < this.ligne; i++)
            {
                if (i < 10) Console.Write(i + "  ");
                else Console.Write(i + " ");
                for (int j = 0; j < this.colonne; j++)
                {                    
                    if(j > 9) Console.Write(this.plateau[i, j] + "   ");
                    else Console.Write(this.plateau[i, j] + "  ");
                }
                Console.WriteLine();              
            }
        }
    }
}