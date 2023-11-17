using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MOTS_GLISSES
{
    public class Dictionnaire
    {
        private static string[] dictionnaire;
        private static int[,] nombreMotParLettre = new int[26, 2];
        private string langue;

        public string[] GetDictionnaire
        {
            get { return dictionnaire; }
        }

        public Dictionnaire(string langue)
        {
            this.langue = langue;
        }

        public void RemplirDico(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            try
            {
                while (!sr.EndOfStream)
                {
                    string lignes = sr.ReadToEnd();
                    dictionnaire = lignes.Split(" ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur de lecture" + e.Message);
            }
            sr.Close();
        }

        /// <summary>
        /// Fonction de tri rapide (Quicksort) Cette fonction utilise le principe de partitionnement et de divisier pour régner 
        /// afin de trier notre dictionnaire (tableau de chaines de caractères). Concrètement, on choisit un élément du tableau que l'on va nommé pivot, 
        /// d'après la documentation internet https://fr.wikipedia.org/wiki/Tri_rapide, il y a plusieurs choix de pivots possibles qui peuvent impacter le
        /// temps d'éxecution de l'algo (ou du moins ses performances). Usuellement, on choisit le pivot comme étant le dernier élément du tableau.
        /// Ensuite, on applique le partitionnement qui est décrit juste en dessous et qui nous retourne l'indice du pivot. A partir duquel on fait des appels 
        /// récursifs des deux côtés du tableau (à gauche et droite du pivot). Enfin le programme s'arrête lorsque "début" atteint l'indice "fin" car cela
        /// signifie que l'on a parcouru tout le tableau et qu'il est trié.
        /// </summary>
        /// <param name="tab">Dans notre cas, il s'agit du dicionnaire que l'on doit trier </param>
        /// <param name="debut">littéralement le début du tableau, càd l'index de départ du tableau -> 0 (dans notre cas évidemment) </param>
        /// <param name="fin">par analogie à "début", il s'agit du dernier indice du tableau dans notre cas car c'est tout le tableau que l'on souahite trier</param>
        public void TriRapide(string[] tab, int debut, int fin)
        {
            if (debut < fin)   //Condition d'arrêt
            {
                int index_pivot = FairePartition(tab, debut, fin);
                TriRapide(tab, debut, index_pivot - 1);
                TriRapide(tab, index_pivot + 1, fin);
            }
        }

        /// <summary>
        /// Cette fontion fait en réalité partie du corps du tri rapide. Son vrai nom serait "Partitionnement"
        /// et a pour but de réorganiser le tableau dans le but que tous les éléments plus petit que le pivot soit à un index inférieur au sien et que tous les éléments
        /// plus grand que le pivot soient à un index supérieur au sien
        /// </summary>
        /// Les paramètres utilisés sont les mêmes que pour la fonction du tri rapide
        /// <returns></returns>
        public int FairePartition(string[] tab, int debut, int fin)
        {
            string pivot = tab[fin];   // -1 Pour éviter une erreur d'indice
            int i = debut -1;
            for(int j = debut; j <= fin - 1; j++) 
            {
                if (string.Compare(tab[j], pivot) <= 0)
                {
                    i++;
                    string tmp = tab[i];       //On échange les éléments aux indices i et j
                    tab[i] = tab[j];
                    tab[j] = tmp;
                }
            }
            string tmp2 = tab[i + 1];         //Puis on échange le pivot avec l'élément à l'indice i + 1
            tab[i + 1] = tab[fin];
            tab[fin] = tmp2;

            return i + 1;
        }

        public void InitMatLettres()
        {
            int k = 97;
            for (int i = 0; i < nombreMotParLettre.GetLength(0); i++)
            {
                nombreMotParLettre[i, 0] = k;
                k = (char)(k + 1);
            }
            for(int j = 0; j < nombreMotParLettre.GetLength(0); j++ ) 
            {
                nombreMotParLettre[j, 1] = 1;     //on initialise à 1 pour éviter lors du parcours avec des variables itératives d'oublier une lettre
            }
        }

        public void CompteMotParLettre()
        {
            for(int i = 0; i<dictionnaire.Length; i++)
            {
                string mot = dictionnaire[i].ToLower();
                for(int j = 0; j < nombreMotParLettre.GetLength(0); j++)
                {
                    if (mot[0] == (char)nombreMotParLettre[j, 0])
                    {
                        nombreMotParLettre[j, 1]++;
                    }

                }
            }
        }

        public string toString()
        {
            string str =  ("Il y a " + dictionnaire.Length + " mots dans notre dictionnaire "+ this.langue);
            for(int i = 0; i < nombreMotParLettre.GetLength(0); i++)
            {
                str += ("\n" + "Il y a " + nombreMotParLettre[i, 1] + " mots commencant par la lettre  " + (char)nombreMotParLettre[i, 0]);
            }
            return str;
        }
    }
}
