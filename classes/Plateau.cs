using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MOTS_GLISSES
{
    public class Plateau
    {
        private static Lettre[] tableauLettres = new Lettre[26];
        private static Lettre[,] plateauJeu;
        int[,] indexCrush;




        public Lettre[,] PlateauJeu
        {
            get { return plateauJeu;}
        }

        public void RemplirTabLettresDepuisFichierLettre(string filename)
        {
            int i = 0;
            StreamReader sr = new StreamReader(filename);
            try
            {
                while (!sr.EndOfStream)
                {
                    string ligne = sr.ReadLine();
                    string[] info = ligne.Split(",");

                    tableauLettres[i] = new Lettre(char.ToLower(char.Parse(info[0])), int.Parse(info[1]), int.Parse(info[2]), 0);
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur de lecture du fichier" + e.Message);
            }
            finally { sr.Close(); }
        }

        public void RemplirPlateauDeJeu8par8()
        {
            Random aleatoire = new Random();
            plateauJeu = new Lettre[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8;)
                {
                    int nombreAleatoire;
                    do
                    {
                      nombreAleatoire = aleatoire.Next(0, 26);
                    } while (tableauLettres[nombreAleatoire].NombreApparitionsActuel >= tableauLettres[nombreAleatoire].NombreApparitions);

                    plateauJeu[i, j] = tableauLettres[nombreAleatoire];
                    tableauLettres[nombreAleatoire].NombreApparitionsActuel++;
                    j++;
                }
            }

        }

        public bool ToRead(string nomfile)
        { 
            int nombreLignes = 0;
            StreamReader sr = new StreamReader(nomfile);
            try
            {
                string[] contenu = File.ReadAllLines(nomfile);
                int lignes = contenu.Length;
                int colonnes = contenu[0].Split(';').Length;
                plateauJeu = new Lettre[lignes, colonnes];
                for(int i = 0; i < lignes; i++)
                {
                    string[] lettres = contenu[i].Split(";");
                    for (int j = 0; j < colonnes; j++)
                    {
                        for(int k = 0; k < tableauLettres.Length; k++)
                        {
                            if (Convert.ToChar(lettres[j]) == tableauLettres[k].Symbole)
                            {
                                plateauJeu[i, j] = new Lettre(Convert.ToChar(lettres[j]), tableauLettres[k].NombreApparitions, tableauLettres[k].Poids, tableauLettres[k].NombreApparitionsActuel);
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Erreur de lecture du fichier");
                return false;
            }
            finally { sr.Close(); }
            return true;
            

        }

        public void afficherLettres()
        {
            for(int i = 0; i < tableauLettres.Length; i++)
            {
                Console.WriteLine(tableauLettres[i].toString());
            }
        }

        public string toString()
        {
            string str = null;
            for (int i = 0; i < plateauJeu.GetLength(0); i++)
            {
                for (int j = 0; j < plateauJeu.GetLength(1); j++)
                {
                    str += (plateauJeu[i, j].Symbole + " ");
                }
                str += "\n";
            }
            return str;
        }

        public int nombreApparitionsLettreSurPremiereLignePlateau(char c)
        {
            int cpt = 0;
            for(int j = 0; j < plateauJeu.GetLength(1); j++)
            {
                if(c == plateauJeu[plateauJeu.GetLength(0) - 1, j].Symbole)
                    cpt++;
            }
            return cpt;
        }

        public bool Recherche_mot(string mot, int n, int index = 0, bool premiereLettreSurLaBase = false, int i = 0, int j = 0)
        {
            if(!premiereLettreSurLaBase)
            {
                i = plateauJeu.GetLength(0) - 1;
                if (mot[index] == plateauJeu[i, j].Symbole && !plateauJeu[i, j].Found)   //il faudra penser à mettre le mot de l'utilisateur en minuscule avant
                {
                    plateauJeu[i, j].Found = true;
                    indexCrush = new int[mot.Length + 1, 2];
                    indexCrush[index, 0] = i;
                    indexCrush[index, 1] = j;
                    return Recherche_mot(mot, n, index + 1, true, i, j);
                }
                else if (j == plateauJeu.GetLength(1) - 1)
                {
                    return false;
                }
                else
                {
                    return Recherche_mot(mot, n, index, false, i, j + 1);
                }
            }
            else
            {
                if (index == mot.Length)     //condition d'arrêt
                {
                    indexCrush[index, 0] = i;
                    indexCrush[index, 1] = j;
                    StateIndicesCrush();              //ce n'est qu'à ce moment là que l'on valide que les lettres vont être crush pour faire tomber les autres
                    Maj_Plateau();
                    return true;
                }
                if (i != 0 && mot[index] == plateauJeu[i - 1, j].Symbole)     //Recherche verticale
                {
                    indexCrush[index, 0] = i;
                    indexCrush[index, 1] = j;                
                    return Recherche_mot(mot, n, index + 1, true, i - 1, j);
                }
                else if (j != plateauJeu.GetLength(1) - 1 && mot[index] == plateauJeu[i, j + 1].Symbole)    //Recherche horizontale (droite)
                {
                    indexCrush[index, 0] = i;
                    indexCrush[index, 1] = j;
                    return Recherche_mot(mot, n, index + 1, true, i, j + 1);    
                }
                else if (j != 0 && mot[index] == plateauJeu[i, j - 1].Symbole)     //Recherche horizontale (gauche)
                {
                    indexCrush[index, 0] = i;
                    indexCrush[index, 1] = j;
                    return Recherche_mot(mot, n, index + 1, true, i, j - 1);
                }
                else if (i != 0 && j != 0 && mot[index] == plateauJeu[i - 1, j - 1].Symbole)     //Recherche diagonale (gauche)
                {
                    indexCrush[index, 0] = i;
                    indexCrush[index, 1] = j;
                    return Recherche_mot(mot, n, index + 1, true, i - 1, j - 1);
                }
                else if (i != 0 && j != plateauJeu.GetLength(1) - 1 && mot[index] == plateauJeu[i - 1, j + 1].Symbole)   //Recherche diagonale (droite)
                {
                    indexCrush[index, 0] = i;
                    indexCrush[index, 1] = j;
                    return Recherche_mot(mot, n, index + 1, true, i - 1, j + 1);
                }
                else if (n > 1)
                    return Recherche_mot(mot, n - 1, 0, false, 0, 0);
                else
                    return false;
            }
        }

        void StateIndicesCrush()
        {
           
               for (int i = 0; i < plateauJeu.GetLength(0); i++)
               {
                   for (int j = 0; j < plateauJeu.GetLength(1); j++)
                   {
                      for (int k = 0; k < indexCrush.GetLength(0); k++)
                      {
                        if (indexCrush[k, 0] == i && indexCrush[k, 1] == j)
                            plateauJeu[i, j].Crush = true;
                      }
                   }
               }
        }


        public void Maj_Plateau()
        {
            for (int i = 0; i < plateauJeu.GetLength(0); i++)
            {
                for (int j = 0; j < plateauJeu.GetLength(1); j++)
                {
                    if (plateauJeu[i, j].Crush)
                    {
                        plateauJeu[i, j].Symbole = ' ';
                    }
                }
            }
            for (int k = 0; k < plateauJeu.GetLength(0); k++)
            {

                for (int i = 0; i < plateauJeu.GetLength(0); i++)
                {
                    for (int j = 0; j < plateauJeu.GetLength(1); j++)
                    {
                        if (i < plateauJeu.GetLength(0) - 1 && plateauJeu[i + 1, j].Crush)
                        {
                            while (plateauJeu[i + 1, j].Crush && !plateauJeu[i, j].Crush)
                            {
                                Lettre tmp = plateauJeu[i + 1, j];
                                plateauJeu[i + 1, j] = plateauJeu[i, j];
                                plateauJeu[i, j] = tmp;
                            }
                        }

                    }
                }
            }
        }
    }
}
