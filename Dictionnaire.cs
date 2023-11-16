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

    }
}
