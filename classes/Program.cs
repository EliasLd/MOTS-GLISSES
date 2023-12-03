using System;
using System.Text;
using System.IO;

namespace MOTS_GLISSES
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionnaire dico = new Dictionnaire("francais");
            dico.RemplirDico("Mots_Français.txt");
            dico.Tri_Rapide(dico.GetDictionnaire, 0, dico.GetDictionnaire.Length - 1);
            dico.InitMatLettres();
            dico.CompteMotParLettre();

            Plateau plateau = new Plateau();
            plateau.RemplirTabLettresDepuisFichierLettre("Lettres.txt");

            Joueur[] joueurs = new Joueur[2];

            string messageBienvenu = "~~ Bienvenue dans le jeu des Mots Glissés! ~~\n\n";

            Console.SetCursorPosition((Console.WindowWidth - messageBienvenu.Length) / 2 , 3);
            Console.WriteLine(messageBienvenu);

            joueurs[0] = new Joueur(setNom(1, 5, 6));
            joueurs[1] = new Joueur(setNom(2, 5, 13));

            /*
            Dictionnaire dico = new Dictionnaire("francais");
            dico.RemplirDico("Mots_Français.txt");
            //Console.WriteLine(dico.GetDictionnaire.Length);                                // Test pour connaître la taille du dico, on fera des tests unitaires plus tard
            dico.Tri_Rapide(dico.GetDictionnaire, 0, dico.GetDictionnaire.Length - 1);     //On envoie l'indice - 1 pour éviter une tentative d'accès à un indice inexistant


            dico.InitMatLettres();
            dico.CompteMotParLettre();
            /*Console.Write(dico.toString());
            Console.WriteLine("\n\n\n");
            Console.WriteLine(dico.RechDichoRecursif("camion", 0, dico.GetDictionnaire.Length - 1));
            Console.WriteLine();
            Plateau plateau = new Plateau();
            plateau.RemplirTabLettresDepuisFichierLettre("Lettres.txt");
            plateau.ToRead("Test1.csv");
            Console.WriteLine(plateau.toString());
            Console.WriteLine();
            string choix = null;
            choix = Console.ReadLine();
            if (plateau.Recherche_mot(choix, plateau.nombreApparitionsLettreSurPremiereLignePlateau(choix[0])))
            {
                Console.WriteLine("Bravo, " +  choix + " est dans le plateau");
            }
            else
            {
                Console.WriteLine("Raté");  
            }
            Console.WriteLine();
            Console.WriteLine(plateau.toString());

            choix = Console.ReadLine();
            if (plateau.Recherche_mot(choix, plateau.nombreApparitionsLettreSurPremiereLignePlateau(choix[0])))
            {
                Console.WriteLine("Bravo, " + choix + " est dans le plateau");
            }
            else
            {
                Console.WriteLine("Raté");
            }
            Console.WriteLine();
            Console.WriteLine(plateau.toString());

            //plateau.afficherLettres();
            */
        }

        public static string setNom(int numJoueur, int posX, int posY)
        {
            string nom = null;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine("Quel est le nom du joueur " + numJoueur);

            do
            {
                Console.SetCursorPosition(posX, posY + 2);
                nom = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(nom))
                {
                    Console.SetCursorPosition(posX, posY + 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalide");
                    Console.ResetColor();
                }

            } while (string.IsNullOrEmpty(nom));

            return nom;
        }
    }
}