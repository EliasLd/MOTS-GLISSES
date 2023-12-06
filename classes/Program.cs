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

                        DateTime startMenu = DateTime.Now;
                        Console.Clear();
                        while(DateTime.Now - startMenu < TimeSpan.FromSeconds(5))
                        {
                            Console.SetCursorPosition(5, 5);
                            Console.Write("La partie va commencer dans ");
                            Console.ForegroundColor= ConsoleColor.Yellow;
                            TimeSpan tempsRestant = TimeSpan.FromSeconds(5) - (DateTime.Now - startMenu);
                            Console.Write(tempsRestant);
                            Console.ResetColor();
                            Console.Write(" secondes ! ");
                        }
                        

                        bool win = false;
                        int i = 0;
                        DateTime débutPartie = DateTime.Now;
                        TimeSpan duréeGlobale = TimeSpan.FromSeconds(15);   //test pour une partie qui dure 15 secondes en tout

                            while (!win && DateTime.Now - débutPartie < duréeGlobale)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(39, 3);
                                Console.WriteLine(game.Joueurs[0].toString());
                                Console.SetCursorPosition(39, 4);
                                Console.WriteLine(game.Joueurs[1].toString());
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine(plateau.toString());
                                Console.WriteLine("entrez un mot " + game.Joueurs[i].Nom);

                                DateTime début = DateTime.Now;
                                TimeSpan durée = TimeSpan.FromSeconds(5);           //test pour le tour d'un joueur qui dure 5 secondes

                                string mot = null;



                                while (DateTime.Now - début < durée)  //une minute en millisecondes
                                {
                                
                                mot = Console.ReadLine().Trim();
                                    

                                    if (string.IsNullOrEmpty(mot))
                                    {
                                        Console.SetCursorPosition(39, 0);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Invalide, Réessayez.");
                                        Console.SetCursorPosition(0, 20);
                                        Console.ResetColor();
                                    }
                                    else if (!game.Joueurs[i].Contient(mot) && mot.Length >= 2 && DateTime.Now - début < durée
                                        && game.PlateauCourant.Recherche_mot(mot, game.PlateauCourant.nombreApparitionsLettreSurPremiereLignePlateau(mot[0]))
                                        && game.Dico.RechDichoRecursif(mot, 0, game.Dico.GetDictionnaire.Length - 1))
                                    {
                                        game.Joueurs[i].Add_Mot(mot);
                                        game.Joueurs[i].Add_Score(calculScore(mot, plateau));

                                        Console.WriteLine("Bravo " + game.Joueurs[i].Nom + ", le mot " + mot + " était dans le plateau, appuie sur ENTREER pour continuer");
                                        break;
                                    }

                                    System.Threading.Thread.Sleep(100);

                                    if ((DateTime.Now - début) >= durée)    //pour éviter qu'un mot que le joueur ait écris et valider après son temps de jeu ne soit compté
                                        break;

                                }

                                mot = null;

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