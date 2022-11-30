using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mots_Meles
{
    internal class Joueur
    {
        //attributs
        private string nom;
        private string [] mots_trouves; // taille?
        private int scores;             // score du joueur 
        private double chrono;          // chrono total du joueur
        public Joueur(string nom, string[] mots_trouves, int scores, double chrono)
        {
            this.nom = nom;
            this.mots_trouves = mots_trouves;
            this.scores = scores;
            this.chrono = chrono;
        }
        public string [] Mots_trouves
        {
            get { return this.mots_trouves; }
            set { this.mots_trouves = value; } //sans doute inutile
        } 
        public int Scores
        {
            get { return this.scores; }
            set { this.scores = value; }
        }
        public double Chrono
        {
            get { return this.chrono; }
            set { this.chrono = value; }
        }
        public string [] Add_mot(string mot)
        {
            string[] add = new string[Mots_trouves.Length + 1];
            for(int i = 0; i < add.Length - 1; i++)
            {
                add[i] = Mots_trouves[i];
            }
            add[add.Length - 1] = mot;
            return add;
        } //retourne la liste des mots trouvés
        public string ToString()
        {
            string res = this.nom + " a trouvé les mots : ";
            for(int i = 0; i< Mots_trouves.Length; i++)
            {
                if (i == Mots_trouves.Length - 1) { res += Mots_trouves[i] + "."; }
                else { res += Mots_trouves[i] + ", "; }
            }
            res += "\n" + "Son score est de " + Scores;
            return res;
        }
        public int Add_Score(int val)
        {
            return Scores + val;
        }
        public double Add_chrono(double val)
        {
            return Chrono + val;
        }
    }   
}
