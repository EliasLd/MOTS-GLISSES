namespace MOTS_GLISSES
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionnaire dico = new Dictionnaire("francais");
            dico.RemplirDico("Mots_Français.txt");
            Console.WriteLine(dico.GetDictionnaire.Length); // Test pour connaître la taille du dico, on fera des tests unitaires plus tard
        }
    }
}