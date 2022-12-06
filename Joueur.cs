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
        private List <string> mots_trouves;
        private int scores;             // score du joueur 
        private double chrono;          // chrono total du joueur
        public Joueur(string nom, List <string> mots_trouves, int scores, double chrono)
        {
            this.nom = nom;
            this.mots_trouves = mots_trouves;
            this.scores = scores;
            this.chrono = chrono;
        }

        /// <summary>
        /// Proprtiété du nom
        /// </summary>
        public string Nom { get { return nom; } }

        /// <summary>
        /// Proprtiété des mots trouvés
        /// </summary>
        public List <string >Mots_trouves
        {
            get { return this.mots_trouves; }
            set { this.mots_trouves = value; } 
        }

        /// <summary>
        /// Proprtiété du score
        /// </summary>
        public int Scores
        {
            get { return this.scores; }
            set { this.scores = value; }
        }

        /// <summary>
        /// Proprtiété du chrono
        /// </summary>
        public double Chrono
        {
            get { return this.chrono; }
            set { this.chrono = value; }
        }


        /// <summary>
        /// Ajoute le mot trouvé à la liste des mots déjà trouvés.
        /// </summary>
        /// <param name="mot"></param>
        public void Add_mot(string mot)
        {
            Mots_trouves.Add(mot);
        } 
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
