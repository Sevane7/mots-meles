using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
using System.Xml.Schema;

namespace Mots_Meles
{
    internal class Jeu
    {
        //attributs
        private Joueur joueur1;
        private Joueur joueur2;
        private long tempsDeJeu;
        private Plateau plateau;
        private Dictionnaire dico;
        private int difficult;

        //Constructeur
        public Jeu(int difficult)
        {
            this.difficult = difficult;
            Init(this.difficult);
        }

        /// <summary>
        /// Propriété en écriture et en lecture du joueur 1
        /// </summary>
        public Joueur Joueur1
        {
            get { return this.joueur1; }
            set { this.joueur1 = value; }
        }

        /// <summary>
        /// Propriété en écriture et en lecture du joeur 2
        /// </summary>
        public Joueur Joueur2
        {
            get { return this.joueur1; }
            set { this.joueur1 = value; }
        }

        /// <summary>
        /// Propriété en écriture et en lecture de la difficulté
        /// </summary>
        public int Difficult
        {
            get { return this.difficult; }
            set { this.difficult = value; }
        }

        /// <summary>
        /// Propriété en lecture et en écriture du temps de jeu
        /// </summary>
        public long TempsDeJeu
        {
            get { return this.tempsDeJeu; }
            set { this.tempsDeJeu = value; }
        }

        /// <summary>
        /// Initialise les joeurs, le plateau et le temps de jeu dans le constructeur
        /// </summary>
        /// <param name="difficult"></param>
        public void Init(int difficult)
        {
            // Initialise le nom des joeurs
            //leur score, mots trouvés et chrono sont nulls
            Console.WriteLine("Entrez le nom du joueur 1 :");
            this.joueur1 = new Joueur(Console.ReadLine());
            Console.WriteLine("Entrez le nom du joueur 2 :");
            this.joueur2 = new Joueur(Console.ReadLine());

            //Initialise le dictionnaire (en anglais ou en français)
            Dictionnaire dico;
            Console.WriteLine("Dans quelle langue voulez-vous que les mots soient? \n (F = Francais) \n (A = Anglais)");
            string lg = Console.ReadLine();
            if (lg.ToUpper() == "A") { this.dico = new Dictionnaire("anglais", "MotsPossiblesEN.txt"); }
            else { this.dico = new Dictionnaire("francais", "MotsPossiblesFR.txt"); }

            //initilaise le temps de jeu (en minutes)
            Console.WriteLine("Combien de secondes pour la première manche? (60 secondes seront ajoutées à chaque manche)");
            this.tempsDeJeu = Convert.ToInt64(Console.ReadLine());

            int lignes = (this.difficult - 1) * 5 + 8;
            Plateau plateau = new Plateau(difficult, lignes, lignes, this.dico);
            this.plateau = plateau;
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
        static public void OnTimedEvent(object sender, ElapsedEventArgs e, System.Timers.Timer chrono, Joueur player_i, long temps_max)
        {
            player_i.Chrono++;
            if (player_i.Chrono >= temps_max)
            {
                Console.WriteLine($"{player_i.Nom} tu n'as plus de temps de jeu, rentre ton dernier mot");
                chrono.Enabled = false;
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

            // Set the interval to 1 seconds (1000 milliseconds)
            timer.Interval = 1000;

            // Set the event that will be called when the timer elapses
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, timer, player_i, this.tempsDeJeu);

            // Start the timer
            timer.Start();

            //enregistre l'heure de début de jeu
            DateTime start = DateTime.Now;

            List<string> wordPerRound = new List<string>();

            while (wordPerRound.Count != this.plateau.MotsATrouver.Count && timer.Enabled)
            {
                //demande le mot trouvé par le joeur
                Console.WriteLine($"{player_i.Nom}, quel mot avez vous trouvez?");
                string word = Console.ReadLine().ToUpper();

                //demande la direction
                Console.WriteLine("Dans quelle direction?");
                int direction = 0;

                //vérifie la validité de la direction en fonction de la difficulté
                switch (this.plateau.Difficult)
                {
                    case 1:
                        Console.WriteLine("Est = 0 \nSud = 1");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction < 0 || direction > 1)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 2:
                        Console.WriteLine("Est = 0 \nSud = 1 \nOuest = 2 \nNord = 3");

                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction < 0 || direction > 3)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 3:
                        Console.WriteLine("Est = 0 \nSud = 1 \nOuest = 2 \nNord = 3 \nNord-Est = 4 \nSud-Ouest = 5");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction < 0 || direction > 5)
                        {
                            Console.WriteLine("Direction non valide pour cette difficulté, veuillez resaisir une direction.");
                            direction = Convert.ToInt32(Console.ReadLine());
                        }
                        break;
                    case 4:
                        Console.WriteLine("Est = 0 \nSud = 1 \nOuest = 2 \nNord = 3 \nNord-Est = 4 \nSud-Ouest = 5 \nNord-Ouest = 6 \nSud-Est = 7");
                        direction = Convert.ToInt32(Console.ReadLine());
                        while (direction < 0 || direction > 7)
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
                    Console.WriteLine("Super, tu as trouvé ce mot\n");
                }
                else { Console.WriteLine("Dommage, ce mot n'est pas valide\n"); }
            }

            //Enregistre l'heure de fin de jeu
            DateTime end = DateTime.Now;

            timer.Stop();

            int score_supp = 0;

            //Ajoute les mots trouvés pendant le tour à la liste de mots trouvés
            for (int i = 0; i < wordPerRound.Count; i++)
            {
                player_i.Mots_trouves.Add(wordPerRound[i]);
                //chaque lettre = un point
                score_supp += wordPerRound[i].Length;
            }
            //Ajoute chaque point au score totale du joueur
            player_i.Add_Score(score_supp);

            //Ajoute le temps qu'a mis le joueur pour finir sa manche
            player_i.Add_chrono(start - end);

            Console.WriteLine($"{player_i.Nom}, vous avez trouvé un total de {wordPerRound.Count} mots.\nVotre score est maintenant de {player_i.Scores}\n");
        }

        /// <summary>
        /// Fais jouer les deux joueurs pour chaque difficulté
        /// </summary>
        public void Manche()
        {
            int lignes = (this.difficult - 1) * 5 + 8;
            this.plateau = new Plateau(this.difficult, lignes, lignes, this.dico);

            //On affiche le plateau pour le joueur 1
            this.plateau.Affichage();
            Tour(this.joueur1);

            //On créer un nouveau plateau pour le joueur 2
            this.plateau = new Plateau(this.difficult, lignes, lignes, this.dico);
            this.plateau.Affichage();
            Tour(this.joueur2);

        }

        /// <summary>
        /// Fait un affichage console du vainqueur
        /// </summary>
        public void ResulstatPartie()
        {
            //Resultat de la partie
            string res = "";
            //En fonction du score
            if (joueur1.Scores != joueur2.Scores)
            {
                if (joueur1.Scores > joueur2.Scores)
                {
                    res += $"{joueur1.Nom} a gagné la partie avec {joueur1.Scores} points";
                }
                else
                {
                    res += $"{joueur2.Nom} a gagné la partie avec {joueur2.Scores} points";
                }
            }
            //Sinon en fonction du chrono
            else if (joueur1.Chrono_total < joueur2.Chrono_total)
            {
                res += $"{joueur1.Nom} a gagné la partie avec un chrono de {joueur1.Chrono_total}";
            }
            else if (joueur1.Chrono_total > joueur2.Chrono_total)
            {
                    res += $"{joueur2.Nom} a gagné la partie avec un chrono de {joueur2.Chrono_total}";
            }
            if(joueur1.Scores == Joueur2.Scores && Joueur2.Chrono_total == Joueur1.Chrono_total)
            {
                Console.WriteLine("Il y a égalité");
            }
            else
            {
                Console.WriteLine(res);
            }
            
        }

    }
}
