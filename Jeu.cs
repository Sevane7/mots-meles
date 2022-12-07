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
            this.timer= timer;
        }
        public Stopwatch Timer
        {
            get {return this.timer;}
            set {this.timer = value;}
        }

        public Stopwatch DebutTimer()
        {
         timer.Start();
        }
        public TimeSpan TempsEcoule()
        {
         timer.Stop();
            TimeSpan temps = timer.Elapsed;
            return TimeSpan;
        }
    }
}
