using System;
using System.Text;
using System.IO;
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

            joueurs[0] = new Joueur(setNom(1, 5, 6));
            joueurs[1] = new Joueur(setNom(2, 5, 13));

            Jeu game;

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

                        plateau.ToRead(nomFile);
                        game = new Jeu(dico, plateau, joueurs);
                        

                        bool win = false;
                        int i = 0;
                        string mot = null;

                        while (!win)
                        {
                            Console.Clear();
                            Console.WriteLine(plateau.toString());
                            Console.WriteLine("entrez un mot " + game.Joueurs[i].Nom);

                            do
                            {
                                mot = Console.ReadLine().Trim();
                            } while (string.IsNullOrEmpty(mot) || !game.Dico.RechDichoRecursif(mot, 0, game.Dico.GetDictionnaire.Length - 1));

                            if(game.PlateauCourant.Recherche_mot(mot, game.PlateauCourant.nombreApparitionsLettreSurPremiereLignePlateau(mot[0])))
                            {
                                Console.WriteLine("Bravo " + game.Joueurs[i].Nom + ", le mot " + mot + " était dans le plateau");
                                game.Joueurs[i].Add_Mot(mot);
                                game.Joueurs[i].Add_Score(1);
                            }

                            i++;
                            if (i == 2)
                                i = 0;
                        }

                        break;
                    case "aléatoire":

                        plateau.RemplirPlateauDeJeu8par8();
                        game = new Jeu(dico, plateau, joueurs);

                        break;
                    case "sortir":

                        fin = true;

                        break;
                    default: break;
                }
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
    }
}