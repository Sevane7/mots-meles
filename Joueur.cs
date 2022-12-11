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
        private long chrono;
        private long chrono_total;

        //Constructeur
        public Joueur(string nom)
        {
            this.nom = nom;
            this.mots_trouves = new List <string> { };
            this.scores = 0;
            this.chrono = 0;
            this.chrono_total = 0;
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
            set { this.scores = value; }
        }

        /// <summary>
        /// Proprtiété du chrono
        /// </summary>
        public long Chrono
        {
            get { return this.chrono; }
            set { this.chrono = value; }
        }

        /// <summary>
        /// Propriétés en écriture et lecture du chrono total
        /// </summary>
        public long Chrono_total
        {
            get { return this.chrono_total; }
            set { this.chrono_total = value;}
        }

        /// <summary>
        /// Ajoute le mot trouvé à la liste des mots déjà trouvés.
        /// </summary>
        /// <param name="mot"></param>
        public void Add_mot(string mot)
        {
            this.mots_trouves.Add(mot);
        }

        /// <summary>
        /// Retourne un string
        /// Retourne la liste des mots trouvés par un joueur et son score
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string res =$"{this.nom} a trouvé les mots : ";
            for(int i = 0; i< Mots_trouves.Count; i++)
            {
                if (i == Mots_trouves.Count - 1) { res += Mots_trouves[i] + "."; }
                else { res += Mots_trouves[i] + ", "; }
            }
            res += $"\nSon score est de {this.scores}";
            return res;
        }

        /// <summary>
        /// Incrémente le score par une valeur
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public void Add_Score(int val)
        {
            this.scores += val;
        } 

        /// <summary>
        /// Incrémente le chrono par un TimeSpan
        /// </summary>
        /// <param name="val"></param>
        public void Add_chrono(long val)
        {
            this.chrono_total += val;
        }
    }   
}
