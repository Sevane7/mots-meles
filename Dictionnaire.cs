﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mots_Meles
{
    internal class Dictionnaire     //associe un ensemble de mots avec une longueur déterminée & une langue
    {
        private string langue;      // Qui sont dans la langue ...
        private string[][] mots;
        public Dictionnaire(string langue, string filename)
        {
            this.langue = langue;
            ReadFile(filename);
        }
        /// <summary>
        /// rempli le [][]mots avec le fichier
        /// </summary>
        /// <param name="filename"></param>
        public void ReadFile(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);
                this.mots = new string[lines.Length / 2][];
                /* Explication boucle : 
                    On part de la ligner 1 du fichier 
                    On incrémente de 2 l'index car le fichier est tel que les numéros sont sur les lignes impaires
                    donc i a valeurs 1 à 28 
                    et i / 2 a valeurs 0 à 14
                    on rempli mots[i/2] avec toutes les valeurs des lignes paires qu'on split avec un espace
                 */
                for (int i = 1; i < lines.Length; i += 2)
                {
                    mots[i / 2] = lines[i].Split(' ');
                }
            }
            catch (IOException e)
            {
                mots = null;
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Retourne le nombre de mots du dictionnaire
        /// </summary>
        /// <returns></returns>
        public int NombreMots()
        {
            int n = 0;
            for (int i = 0; i < mots.Length; i++)
            {
                n += mots[i].Length;        // n est incrémenter de tous la taille de tous les tableaux[i]
            }
            return n;
        }

        /// <summary>
        /// Retourne le nombre de mots d'un tableau_i
        /// </summary>
        /// <returns></returns>
        public int NombreMotsTab(int tableau_i)
        {
           return this.mots[tableau_i].Length;
        }

        /// <summary>
        /// Propriété en lecture de la langue
        /// </summary>
        public string Langue { get { return this.langue; } }   

        /// <summary>
        /// Propriétés en lecture et en écriture
        /// </summary>
        public string[][] Mots 
        { 
            get { return mots; }
            set { mots = value; }
        }

        /// <summary>
        ///Retourne le nombre de mots par longueur en français.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string res = "En français : \n";
            for(int i = 0; i < this.mots.Length; i++)
            {
                res += $"Il y a {this.mots[i].Length} mots de taille {i + 2}. \n";
            }
            return res;
        }

        /// <summary>
        /// Affiche tous les mots du [][] mots
        /// </summary>
        public void AfficherTousMots(int tableau_i)
        {
            for (int i = 0; i < Mots[tableau_i - 2].Length; i++)
            {
                Console.Write(Mots[tableau_i - 2][i].ToString() + " ");
            }
        }

        /// <summary>
        ///Retourne l'index du mot cherché 
        ///Dans le (mot.Length - 2) ème  tableau de [][] mots
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="index_deb"></param>
        /// <param name="index_fin"></param>
        /// <param name="milieu"></param>
        /// <returns></returns>
        public bool RechDichRecursif(string mot, int index_deb, int index_fin, int milieu)   
        {
            int len = milieu + (index_fin - index_deb) / 2;
            if (len >= index_fin) { return false; }                                                        // le mots n'est pas dans tab
            if (this.mots[mot.Length - 2][len] == mot) { return true; }                                                          //condition d'arrêt si mot est dans le tableau 
            if (mot.CompareTo(this.mots[mot.Length - 2][len]) < 0) { return RechDichRecursif(mot, index_deb, len, len); }
            else { return RechDichRecursif(mot, len + 1, index_fin, len + 1); }
        }
    }
}
