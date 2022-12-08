using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Mots_Meles
{
    internal class Jeu
    {
        private Stopwatch timer;
        private Joueur[] joueurs;

        public Jeu(Stopwatch timer)
        {
            this.timer = timer;
            this.joueurs = InitialisationJoueur();
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
        public TimeSpan TempsEcoule()
        {
            timer.Stop();
            TimeSpan temps = timer.Elapsed;
            return temps;
        }
        public Joueur[] InitialisationJoueur()
        {
            Joueur[] joueurs = new Joueur{joueur1,joueur2};
            Console.WriteLine("Entrez le nom du joueur 1 :");
            joueur1.Nom=Console.ReadLine();
            Console.WriteLine("Entrez le nom du joueur 2 :");
            joueur2.Nom=Console.ReadLine();
            joueur1.Scores=0;
            joueur2.Scores=0;
            joueur1.chrono=0.0;
            joueur2.chrono=0.0;
            joueur1.Mots_trouves = List<>;
            joueur2.Mots_trouves = List<>;
            return joueurs;
        }
        public string ResultatPartie()
        {
            if (joueur1.Scores!=joueur2.Scores)
            {
                if (joueur1.Scores>joueur2.Scores)
                {
                    Console.WriteLine("Le joueur 1: "+joueur1.Nom+" a gagné la partie avec " +joueur2.Scores+" points et un temps cumulé de " +joueur1.Chrono+"secondes.");
                }
                else
                {
                    Console.WriteLine("Le joueur 2: "+joueur2.Nom+" a gagné la partie avec " +joueurs.Scores+ " points et un temps cumulé de "+joueur2.Chrono+"secondes.");
                }
            }
            else
            {
                if (joueur1.Chrono<joueur2.Chrono)
                {
                    Console.WriteLine("Le joueur 1: "+joueur1.Nom+" a gagné la partie avec un score de " +joueur1.Scores+" points comme son adversaire, mais un meilleur temps cumulé, "+joueur1.Chrono+" secondes.");
                }
                else
                {
                    Console.WriteLine("Le joueur 2: "+joueur2.Nom+" a gagné la partie avec un score de " +joueur2.Scores+ " points comme son adversaire, mais un meilleur temps cumulé: "+joueur2.Chrono+" secondes.");
                }
            }
        }
    }
}
