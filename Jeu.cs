using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

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

        //methode qui verifie que le mot existe dans la direction
        //methode qui dait passer au 2eme tour
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
                        if (word[i] == plateau.Remplissage()[x, y + i])
                        {
                            count++;
                        }
                    }
                }
                if (motDansList == true && direction == 1)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == plateau.Remplissage()[x + i, y])
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
                        if (word[i] == plateau.Remplissage()[x, y - i])
                        {
                            count++;
                        }
                    }
                }
                if (motDansList == true && direction == 3)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == plateau.Remplissage()[x - i, y])
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
                        if (word[i] == plateau.Remplissage()[x - i, y + i])
                        {
                            count++;
                        }
                    }
                }
                if (motDansList == true && direction == 5)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == plateau.Remplissage()[x + i, y - i])
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
                        if (word[i] == plateau.Remplissage()[x - i, y - i])
                        {
                            count++;
                        }
                    }
                }
                if (motDansList == true && direction == 7)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == plateau.Remplissage()[x + i, y + i])
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
        /// Prend un joueur en argument
        /// Le fait jouer tant qu'il n'a pas trouvé tous les mots ou que le chrono tourne
        /// </summary>
        /// <param name="player_i"></param>
        public void Tour(Joueur player_i)
        {
            List<string> wordPerRound = new List<string>();

            plateau.Affichage(this.plateau.Remplissage());

            while(wordPerRound.Count != plateau.MotsATrouver.Count)
            {
                Console.WriteLine($"{player_i.Nom}, quel mot avez vous trouvez?");
                string word = Console.ReadLine().ToUpper();

                Console.WriteLine("Dans quelle direction?");
                int direction = 0;
                switch (plateau.Difficult)
                {
                    case 1:
                        Console.WriteLine("Est = 0 \nSud = 1");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while(direction > 1 || direction < 0)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 2:
                        Console.WriteLine("Est = 0 \n Sud = 1 \n Ouest = 2 \n Nord = 3");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction != 0 || direction != 1 || direction != 2 || direction != 3)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 3:
                        Console.WriteLine("Est = 0 \n Sud = 1 \n Ouest = 2 \n Nord = 3 \n Nord-Est = 4 \n Sud-Ouest = 5");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction != 0 || direction != 1 || direction != 2 || direction != 3 || direction != 4 || direction != 5)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 4:
                        Console.WriteLine("Est = 0 \n Sud = 1 \n Ouest = 2 \n Nord = 3 \n Nord-Est = 4 \n Sud-Ouest = 5 \n Nord-Ouest = 6 \n Sud-Est = 7");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction != 0 || direction != 1 || direction != 2 || direction != 3 || direction != 4 || direction != 5 || direction != 6 || direction != 7)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                }

                Console.WriteLine("A quelle ligne?");
                int x = Convert.ToInt32(Console.ReadLine());
                while(x >= plateau.Ligne || x < 0)
                {
                    Console.WriteLine($"Coordonnées OutOfRange! \nRappel : x_max = {plateau.Ligne - 1}");
                    x = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("A quelle colonne?");
                int y = Convert.ToInt32(Console.ReadLine());
                while (y >= plateau.Ligne || y < 0)
                {
                    Console.WriteLine($"Coordonnées OutOfRange! \nRappel : y_max = {plateau.Colonne - 1}");
                    y = Convert.ToInt32(Console.ReadLine());
                }

                if (VerifMot(word, direction, x, y)) { wordPerRound.Add(word); }
            }

            int score_supp = 0;

            //Ajoute les mots trouvés pendant le tour à la liste de mots trouvés
            for(int i = 0; i< wordPerRound.Count; i++)
            {
                player_i.Mots_trouves.Add(wordPerRound[i]);
                score_supp += wordPerRound[i].Length; //chaque lettre = un point
            }
            player_i.Add_Score(score_supp);

            Console.WriteLine($"{player_i}, vous avez trouvé un total de {wordPerRound.Count} mots.\nVotre score est maintenant de {player_i.Scores}");
        }
        public void TourSuivant()
        {
            Tour(joueur1);
            Tour(joueur2);
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
