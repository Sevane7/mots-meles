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
        /*public override string ToString()
        {
            string res = ($"Il a {this.nbr_mots} en {this.langue} de taille : ");
            for(int i = 0; i<this.longueur.Length; i++) { res += this.longueur[i] + " ";  }
            return res;
        }*/

        public void AfficherTousMots()
        {
            for (int i = 0; i < this.mots.Length; i++)
            {
                for (int j = 0; j < this.mots[i].Length; j++)
                {
                    Console.WriteLine(this.mots[i][j]);
                }
            }
        }
        public bool RechDichRecursif(string[] tab, string mot, int index_deb, int index_fin, int milieu)   //tab depend de la longueur (si longueur = 9, tab contient tous les mots de longueur 9)
        {
            int len = milieu + (index_fin - index_deb) / 2;
            if (len >= index_fin) { return false; }                                                        // le mots n'est pas dans tab
            if (tab[len] == mot) { return true; }                                                          //condition d'arrêt si mot est dans le tableau 
            if (mot.CompareTo(tab[len]) < 0) { return RechDichRecursif(tab, mot, index_deb, len, len); }
            else { return RechDichRecursif(tab, mot, len + 1, index_fin, len + 1); }
        }
        public void ReadFile(string filename)
        {
            this.mots = null;
            int count = 0;
            try
            {
                var sr = new StreamReader(filename);
                string fichier = sr.ReadToEnd();
                string[] AllLines = new string[14];
                for(int i = 0; i < 14; i++)
                {
                    string after = fichier.Split((i + 2).ToString() + "\n")[1]; // stocke tous les éléments après le split du premier nombre/chiffre
                    AllLines[i] = after.Split((i + 3).ToString())[0];           // stocke tous les éléments du 2eme bloc avant le nombre/chiffre suivant
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
