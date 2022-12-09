using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Mots_Meles
{
    internal class Jeu
    {
        private Stopwatch timer;
        private Joueur joueur1;
        private Joueur joueur2;
        private Plateau plateau;

        public Jeu(Stopwatch timer, Joueur joueur1, Joueur joueur2, Plateau plateau)
        {
            this.timer = timer;
            this.joueur1 = joueur1;
            this.joueur2 = joueur2;
            this.plateau = plateau;
        }
        public Stopwatch Timer
        {
            get {return this.timer;}
            set {this.timer = value;}
        }
        public void DebutTimer()
        {
            timer.Start();
        }
        public void FinTimer()
        {
            timer.Stop();
        }

        /// <summary>
        /// Retourne une TimeSpan
        /// Prend un Joueur en argument
        /// Retourne le temps de  jeu pour un joueur
        /// </summary>
        /// <param name="joueur"></param>
        /// <returns></returns>
        public TimeSpan TempsDeJeu(Joueur joueur)
        {
            TimeSpan temps;
            timer.Start();
            temps = timer.Elapsed;
            return temps;
        }
        public string ResultatPartie()
        {
            string res = "";
            if (joueur1.Scores != joueur2.Scores)
            {
                if (joueur1.Scores > joueur2.Scores)
                {
                    res += $"{ joueur1.Nom } a gagné la partie avec {joueur1.Scores} points";
                }
                else
                {
                    res += $"{joueur2.Nom} a gagné la partie avec {joueur2.Scores} points";
                }
            }
            else
            {
                if (joueur1.Chrono < joueur2.Chrono)
                {
                    res += $"{ joueur1.Nom} a gagné la partie avec un chrono de {joueur1.Chrono}";
                }
                else
                {
                    res += $"{joueur2.Nom} a gagné la partie avec un chrono de {joueur2.Chrono}";
                }
            }
            return res;
        }
    }
}
