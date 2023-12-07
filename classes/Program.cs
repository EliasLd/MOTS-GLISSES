using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Diagnostics.SymbolStore;

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

            joueurs[0] = new Joueur(setNom(1, 5, 6), " ", 0);
            joueurs[1] = new Joueur(setNom(2, 5, 13), " ", 0);

            bool fin = false;
            string choix = null;

            Console.SetCursorPosition(5, 21);

            do
            {
                Console.WriteLine("Tapez le nom du mode de jeu : 'fichier' ; 'aléatoire' ou tapez 'Sortir' pour quitter");
                while (choix != "fichier" && choix != "aléatoire" && choix != "sortir")
                {
                    Console.SetCursorPosition(5, 23);
                    choix = Console.ReadLine().Trim();
                }

                switch (choix)
                {
                    case "fichier":

                        string nomFile = " ";
                        Console.Clear();
                        Console.SetCursorPosition(5, 5);
                        Console.WriteLine("Entrez le nom du fichier : ");
                        Console.SetCursorPosition(35, 5);
                        nomFile = Console.ReadLine();
                        while (!IsInDirectory(nomFile))
                        {
                            Console.SetCursorPosition(35, 7);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Introuvable");
                            Console.ResetColor();
                            Console.SetCursorPosition(35, 5);
                            nomFile = Console.ReadLine();
                        }
                        plateau = new Plateau();
                        plateau.ToRead(nomFile);
                        Jeu gameFichier = new Jeu(dico, plateau, joueurs);
                        gameFichier.Partie();
                        break;
                    case "aléatoire":
                        Console.Clear();
                        plateau = new Plateau();
                        plateau.RemplirTabLettresDepuisFichierLettre("Lettres.txt");
                        plateau.ToFile("save.txt");
                        plateau.ToRead("save.txt");
                        Jeu gameAléatoire = new Jeu(dico, plateau, joueurs);
                        gameAléatoire.Partie();
                        break;
                    case "sortir":

                        fin = true;

                        break;
                    default: break;
                }
                joueurs[0].Score = 0;
                joueurs[0].MotTrouves = " ";
                joueurs[1].Score = 0;
                joueurs[1].MotTrouves = " ";
                choix = null;

            } while (!fin);
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

        public static bool IsInDirectory(string nomFile) 
        {
            return File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomFile));
        }

        public static int calculScore(string mot, Plateau plateau)
        {
            int score = 0;
            int longueur = mot.Length;

            for (int j = 0; j < mot.Length; j++)
            {
                for (int i = 0; i < plateau.TableauLettres.Length; i++)
                {
                    if (mot[j] == plateau.TableauLettres[i].Symbole)
                    {
                        score += plateau.TableauLettres[i].Poids;      //on ajoute le poids de la lettre correspondante
                    }
                }
            }
            score += longueur;
            return score;
        }
    }
}