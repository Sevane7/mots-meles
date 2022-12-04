using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mots_Meles
{
    internal class Dictionnaire     //associe un ensemble de mots avec une longueur déterminée & une langue
    {
        private string langue;      // Qui sont dans la langue ...
        private string[][] mots;
        public Dictionnaire(string langue, string filename)
        {
            this.langue = langue;
            ReadFile(filename);
        }
        /// <summary>
        /// Retourne le nombre de mots du dictionnaire
        /// </summary>
        /// <returns></returns>
        public int NombreMots()
        {
            int n = 0;
            for (int i = 0; i < mots.Length; i++)
            {
                n += mots[i].Length;        // n est incrémenter de tous la taille de tous les tableaux[i]
            }
            return n;
        }
        public string Langue { get { return this.langue; } }


        /// <summary>
        /// Ca retourne quoi ca
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string res = "";

            return res;
        }


        /// <summary>
        /// Affiche tous les mots du [][] mots
        /// </summary>
        public void AfficherTousMots()
        {
            for (int i = 0; i < this.mots.Length; i++)
            {
                for (int j = 0; j < this.mots[i].Length; j++)
                {
                    Console.Write(this.mots[i][j] + " ");
                    if (j == this.mots[i].Length - 1) { Console.Write(this.mots[i][j] + ".\n"); }
                }
            }
        }


        /// <summary>
        ///Retourne l'index du mot cherché 
        ///Dans le (mot.Length - 2) ème  tableau de [][] mots
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="index_deb"></param>
        /// <param name="index_fin"></param>
        /// <param name="milieu"></param>
        /// <returns></returns>
        public bool RechDichRecursif(string mot, int index_deb, int index_fin, int milieu)   //tab depend de la longueur (si longueur = 9, tab contient tous les mots de longueur 9)
        {
            int len = milieu + (index_fin - index_deb) / 2;
            if (len >= index_fin) { return false; }                                                        // le mots n'est pas dans tab
            if (this.mots[mot.Length - 2][len] == mot) { return true; }                                                          //condition d'arrêt si mot est dans le tableau 
            if (mot.CompareTo(this.mots[mot.Length - 2][len]) < 0) { return RechDichRecursif(mot, index_deb, len, len); }
            else { return RechDichRecursif(mot, len + 1, index_fin, len + 1); }
        }
        public void ReadFile(string filename)
        {
            this.mots = null;
            int count = 0;
            try
            {
                var sr = new StreamReader(filename);
                string fichier = sr.ReadToEnd();                                 // Lis tous les éléments d'un fichier
                string[] AllLines = new string[14];
                for(int i = 0; i < 14; i++)
                {
                    string after = fichier.Split((i + 2).ToString() + "\n")[1]; // stocke tous les éléments après le split du premier nombre/chiffre 
                    AllLines[i] = after.Split((i + 3).ToString())[0];           // stocke tous les éléments du 2eme bloc : avant le nombre/chiffre suivant
                                                                                // [0] : premier bloc du .Split; [1] : deuxième
                }

                this.mots = new string[AllLines.Length][];

                for(int i = 0; i < AllLines.Length; i ++)                       // pour trouver Length de res
                {
                    this.mots[i] = AllLines[i].Split(' ');
                }
                Console.WriteLine(this.mots.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
