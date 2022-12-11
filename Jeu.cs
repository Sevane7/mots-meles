using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;

namespace Mots_Meles
{
    internal class Jeu
    {
        private Joueur joueur1;
        private Joueur joueur2;
        private Plateau plateau;
        private long tempsDeJeu;

        public Jeu(Joueur joueur1, Joueur joueur2, Plateau plateau, long tempsDeJeu)
        {
            this.joueur1 = joueur1;
            this.joueur2 = joueur2;
            this.plateau = plateau;
            this.tempsDeJeu = tempsDeJeu;
        }

        /// <summary>
        /// Renvoie un bool
        /// Verifie qu'un mot dans une certaine direction à un certain point est dans la grille
        /// </summary>
        /// <param name="word"></param>
        /// <param name="direction"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool VerifMot(string word, int direction, int x, int y)
        {
            bool verif = false;
            bool motDansList = false;
            int count = 0;
            if (plateau.Difficult > 0)
            {
                for (int i = 0; i < plateau.MotsATrouver.Count; i++)
                {
                    if (plateau.MotsATrouver[i] == word)
                    {
                        motDansList = true;
                    }
                }
                if (motDansList == true && direction == 0)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == this.plateau.Grille[x, y + i])
                        {
                            count++;
                        }
                    }
                }
                if (motDansList == true && direction == 1)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == this.plateau.Grille[x + i, y])
                        {
                            count++;
                        }
                    }
                }
            }
            if (plateau.Difficult > 1)
            {
                if (motDansList == true && direction == 2)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == this.plateau.Grille[x, y - i])
                        {
                            count++;
                        }
                    }
                }
                if (motDansList == true && direction == 3)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == this.plateau.Grille[x - i, y])
                        {
                            count++;
                        }
                    }
                }
            }
            if (plateau.Difficult > 2)
            {
                if (motDansList == true && direction == 4)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == this.plateau.Grille[x - i, y + i])
                        {
                            count++;
                        }
                    }
                }
                if (motDansList == true && direction == 5)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == this.plateau.Grille[x + i, y - i])
                        {
                            count++;
                        }
                    }
                }
            }
            if (plateau.Difficult > 3)
            {
                if (motDansList == true && direction == 6)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == this.plateau.Grille[x - i, y - i])
                        {
                            count++;
                        }
                    }
                }
                if (motDansList == true && direction == 7)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == this.plateau.Grille[x + i, y + i])
                        {
                            count++;
                        }
                    }
                }
            }
            if (motDansList == true && count == word.Length)
            {
                verif = true;
            }
            return verif;
        }

        /// <summary>
        /// Méthode appelée à chaque fois que le chrono s'écoule d'une seconde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="temps_max"></param>
        /// <param name="player_i"></param>
        public void OnTimedEvent(object sender, ElapsedEventArgs e, System.Timers.Timer chrono, Joueur player_i, long temps_max)
        {
            player_i.Add_chrono(1);
            if (player_i.Chrono >= temps_max)
            { 
                Console.WriteLine($"{player_i.Nom} tu n'as plus de temps de jeu, rentre ton dernier mot");
                chrono.Stop();
            }
        }

        /// <summary>
        /// Prend un joueur en argument
        /// Le fait jouer tant qu'il n'a pas trouvé tous les mots ou que le chrono tourne
        /// </summary>
        /// <param name="player_i"></param>
        public void Tour(Joueur player_i)
        {
            // Create a new timer
            System.Timers.Timer timer = new System.Timers.Timer();

            // Set the interval to 2 seconds (2000 milliseconds)
            timer.Interval = 1000;

            // Set the event that will be called when the timer elapses
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, timer, player_i, this.tempsDeJeu);

            // Start the timer
            timer.Start();

            List<string> wordPerRound = new List<string>();


            while (wordPerRound.Count != plateau.MotsATrouver.Count && timer.Enabled)
            {
                //demande le mot trouvé par le joeur
                Console.WriteLine($"{player_i.Nom}, quel mot avez vous trouvez?");
                string word = Console.ReadLine().ToUpper();

                //demande la direction et vérifie si elle est valide
                Console.WriteLine("Dans quelle direction?");
                int direction = 0;

                //vérifie la validité de la direction en fonction de la difficulté
                switch (this.plateau.Difficult)
                {
                    case 1:
                        Console.WriteLine("Est = 0 \nSud = 1");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction > 1 || direction < 0)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 2:
                        Console.WriteLine("Est = 0 \nSud = 1 \nOuest = 2 \nNord = 3");
                        
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction != 0 || direction != 1 || direction != 2 || direction != 3)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 3:
                        Console.WriteLine("Est = 0 \nSud = 1 \nOuest = 2 \nNord = 3 \nNord-Est = 4 \nSud-Ouest = 5");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction != 0 || direction != 1 || direction != 2 || direction != 3 || direction != 4 || direction != 5)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 4:
                        Console.WriteLine("Est = 0 \nSud = 1 \nOuest = 2 \nNord = 3 \nNord-Est = 4 \nSud-Ouest = 5 \nNord-Ouest = 6 \nSud-Est = 7");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction != 0 || direction != 1 || direction != 2 || direction != 3 || direction != 4 || direction != 5 || direction != 6 || direction != 7)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                }

                //demande la ligne 
                Console.WriteLine("A quelle ligne?");
                int x = Convert.ToInt32(Console.ReadLine());
                while (x >= plateau.Ligne || x < 0)
                {
                    Console.WriteLine($"Coordonnées OutOfRange! \nRappel : x_max = {plateau.Ligne - 1}");
                    x = Convert.ToInt32(Console.ReadLine());
                }

                //demande la colonne
                Console.WriteLine("A quelle colonne?");
                int y = Convert.ToInt32(Console.ReadLine());
                while (y >= plateau.Ligne || y < 0)
                {
                    Console.WriteLine($"Coordonnées OutOfRange! \nRappel : y_max = {plateau.Colonne - 1}");
                    y = Convert.ToInt32(Console.ReadLine());
                }

                //Verifie que l'ensemble (mot, direction, ligne, colonne) est valide
                if (VerifMot(word, direction, x, y))
                {
                    wordPerRound.Add(word);
                    Console.WriteLine("Super, tu as trouvé ce mot");
                }
                else { Console.WriteLine("Dommage, ce mot n'est pas valide"); }
            }

            timer.Stop();

            int score_supp = 0;
        
            //Ajoute les mots trouvés pendant le tour à la liste de mots trouvés
            for(int i = 0; i< wordPerRound.Count; i++)
            {
                player_i.Mots_trouves.Add(wordPerRound[i]);
                score_supp += wordPerRound[i].Length; //chaque lettre = un point
            }
            player_i.Add_Score(score_supp);

            Console.WriteLine($"{player_i.Nom}, vous avez trouvé un total de {wordPerRound.Count} mots.\nVotre score est maintenant de {player_i.Scores}");               
        }        
    }
}
