# Projet d'Algorithme et Programmation Objet S3 : Jeu de Mots Mêlés

## Description
Ce projet est un jeu de mots mêlés en console développé en C# dans le cadre de mes études en Programmation Orientée Objet. Le jeu permet à deux joueurs de s'affronter en trouvant un maximum de mots cachés dans une grille générée dynamiquement. Le projet est structuré autour de plusieurs classes pour assurer une organisation et une modularité efficaces.

## Structure du Projet

### Classe `Dictionnaire`
La classe `Dictionnaire` stocke les mots dans un tableau de tableaux triés par longueur, pour répondre aux critères de difficulté et de langue sélectionnés par le joueur.

- **Attributs** :
  - `string langue` : la langue choisie par les joueurs.
  - `string[][] mots` : un tableau de tableaux où chaque sous-tableau contient les mots triés par longueur.

- **Fonctions Principales** :
  - Lecture de fichier `MotsPossibles.txt` : Récupère les mots en supprimant les délimiteurs via `.Split()` pour les placer dans `mots` selon la longueur et la langue sélectionnée.

### Classe `Plateau`
`Plateau` gère la création et l'affichage de la grille de mots mêlés, avec des mots placés selon la difficulté choisie.

- **Attributs** :
  - `int lignes`, `int colonnes` : dimensions de la grille.
  - `int difficulté` : niveau de difficulté affectant la taille des mots et la durée du jeu.
  - `List<string> MotsATrouver` : mots à trouver par les joueurs dans la grille.
  - `Dictionnaire dictionnaire` : lien vers la classe Dictionnaire.
  - `char[][] grille` : matrice représentant la grille de jeu.

- **Fonctions Principales** :
  - **ChoixMotsTri** : Sélectionne les mots aléatoirement en fonction de la difficulté.
  - **RemplissageMotATrouver** : Place les mots dans la grille avec des points d'ancrage calculés et des directions aléatoires.
  - **Affichage** : Affiche la grille finale et les mots à trouver dans la console.

### Classe `Joueur`
Cette classe gère les attributs et les actions des joueurs, incluant leur score, le chronomètre de chaque manche et la liste des mots trouvés.

- **Attributs** :
  - `string nom` : nom du joueur.
  - `int score` : score actuel du joueur basé sur la longueur des mots trouvés.
  - `TimeSpan chronoTotal` : temps cumulé pour toutes les manches.
  - `List<string> motsTrouves` : liste des mots trouvés par le joueur.

- **Fonctions Principales** :
  - **Add_mot** : Ajoute un mot trouvé à `motsTrouves`.
  - **Add_Score** : Incrémente le score du joueur.
  - **Add_chrono** : Met à jour le temps total pour le joueur.
  - **ToString** : Affiche les informations du joueur, y compris les mots trouvés et le score actuel.

### Classe `Jeu`
La classe `Jeu` gère le déroulement des manches, les scores et le chronométrage des joueurs.

- **Attributs** :
  - `Joueur joueur1`, `Joueur joueur2` : les deux joueurs.
  - `Plateau plateau` : la grille de jeu actuelle.
  - `TimeSpan tempsParGrille` : temps limite pour chaque grille, augmenté avec la difficulté.

- **Fonctions Principales** :
  - **VerifMot** : Vérifie si un mot trouvé est correct en fonction des mots à trouver, du point d’ancrage, et de la direction.
  - **Init** : Initialise les paramètres de la partie (joueurs, langue).
  - **OnTimedEvent** : Arrête la manche pour le joueur à la fin du temps imparti, permettant un dernier essai.
  - **Tour** : Gère chaque tour, demandant au joueur de saisir les mots et positions. Appelle également les fonctions de vérification et de score.
  - **ResultatsPartie** : Affiche les résultats à la fin de la partie.

### Fonction `Main`
Le `Main` organise la partie en lançant plusieurs manches (`Tour`) et ajuste la grille pour chaque joueur en augmentant progressivement la difficulté jusqu'à la 4ᵉ et dernière manche, après quoi les résultats finaux sont affichés.

## Installation
1. Clonez le dépôt :
   ```bash
   git clone https://github.com/Sevane7/mots-meles.git
