﻿namespace MOTS_GLISSES
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionnaire dico = new Dictionnaire("francais");
            dico.RemplirDico("Mots_Français.txt");
            Console.WriteLine(dico.GetDictionnaire.Length);                                // Test pour connaître la taille du dico, on fera des tests unitaires plus tard
            dico.TriRapide(dico.GetDictionnaire, 0, dico.GetDictionnaire.Length - 1);     //On envoie l'indice - 1 pour une tentative d'accès à un indice inexistant
            /*for(int i = 0; i < 500; i++)
            {
                Console.Write(dico.GetDictionnaire[i] + " ");                           //Test temporaire pour érifier que le tri fonctionne bien
            }*/

            dico.InitMatLettres();
            dico.CompteMotParLettre();
            Console.Write(dico.toString());
        }
    }
}