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

        public void ToRead(string nomfile)
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
                        plateauJeu[i, j] = new Lettre(Convert.ToChar(lettres[j]), 0, 0, 0);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Erreur de lecture du fichier");
            }
            finally { sr.Close(); }
            

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
    }
}
