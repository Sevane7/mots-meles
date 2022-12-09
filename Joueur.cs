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
        private int scores;
        private TimeSpan chrono;

        //Constructeur
        public Joueur(string nom, List <string> mots_trouves, int scores, TimeSpan chrono)
        {
            this.nom = nom;
            this.mots_trouves = mots_trouves;
            this.scores = scores;
            this.chrono = chrono;
        }

        /// <summary>
        /// Proprtiété du nom
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        /// <summary>
        /// Proprtiété des mots trouvés
        /// </summary>
        public List <string >Mots_trouves
        {
            get { return this.mots_trouves; }
            set { this.mots_trouves = value; } 
        }

        /// <summary>
        /// Proprtiétés du score en lecture et en écriture
        /// </summary>
        public int Scores
        {
            get { return this.scores; }
            set
            {
                for(int i = 0; i < mots_trouves.Count; i ++) { this.scores += mots_trouves[0].Length; }
            }
        }

        /// <summary>
        /// Proprtiété du chrono
        /// </summary>
        public TimeSpan Chrono
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

        /// <summary>
        /// Retourne un string
        /// Retourne la liste de mots trouvée par un joueur et son score
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string res = this.nom + " a trouvé les mots : ";
            for(int i = 0; i< Mots_trouves.Count; i++)
            {
                if (i == Mots_trouves.Count - 1) { res += Mots_trouves[i] + "."; }
                else { res += Mots_trouves[i] + ", "; }
            }
            res += "\n" + "Son score est de " + Scores;
            return res;
        }

        /// <summary>
        /// Retourne un entier
        /// Invrémente le score par une valeur
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public int Add_Score(int val)
        {
            return Scores + val;
        } //inutile car set Scores
        public TimeSpan Add_chrono(TimeSpan val)
        {
            return Chrono + val;

        } //pareil
    }   
}
