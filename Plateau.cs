using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTS_GLISSES
{
    public class Plateau
    {
        private static Lettre[] tableauLettres = new Lettre[26];
        private static Lettre[,] plateauJeu = new Lettre[8, 8];

        

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

                    tableauLettres[i] = new Lettre(char.Parse(info[0]), int.Parse(info[1]), int.Parse(info[2]));
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur de lecture du fichier" + e.Message);
            }
            sr.Close();
        }

        public void afficherLettres()
        {
            for(int i = 0; i < tableauLettres.Length; i++)
            {
                Console.WriteLine(tableauLettres[i].toString());
            }
        }
    }
}
