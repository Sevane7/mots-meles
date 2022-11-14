using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mots_Meles
{
    internal class Dictionnaire     //associe un ensemble de mots avec une longueur déterminée & une langue
    {
        private int nbr_mots;       // Il y a ... mots
        private int [] longueur;    // Chaqun d'une longueur de ...
        private string langue;      // Qui sont dans la langue ...
        public Dictionnaire(int nbr_mots, string langue, int [] longueur)
        {
            this.nbr_mots = nbr_mots;
            this.langue = langue;
            this.longueur = longueur;
        }
        public int Nbr_mots { get { return nbr_mots; } }
        public int [] Longueur { get { return this.longueur; } }
        public string Langue { get { return this.langue; } }
        public override string ToString()
        {
            string res = ($"Il a {this.nbr_mots} en {this.langue} de taille : ");
            for(int i = 0; i<this.longueur.Length; i++) { res += this.longueur[i] + " ";  }
            return res;
        }
        public bool RechDichRecursif(string[] tab, string mot, int index_deb, int index_fin, int milieu)   //tab depend de la longueur (si longueur = 9, tab contient tous les mots de longueur 9)
        {
            int len = milieu + (index_fin - index_deb) / 2;
            if (len >= index_fin) { return false; }                                                        // le mots n'est pas dans tab
            if (tab[len] == mot) { return true; }                                                          //condition d'arrêt si mot est dans le tableau 
            if (mot.CompareTo(tab[len]) < 0) { return RechDichRecursif(tab, mot, index_deb, len, len); }
            else { return RechDichRecursif(tab, mot, len + 1, index_fin, len + 1); }
        }
    }
}
