using System;
using System.Windows.Forms;

namespace JeuDePoints
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Cette méthode est utilisée pour configurer l'application.
            Application.EnableVisualStyles(); // Active les styles visuels de l'application.
            Application.SetCompatibleTextRenderingDefault(false); // Définit la compatibilité de rendu du texte.
            
            // Démarre l'application et ouvre le formulaire principal
            Joueur j1 = new Joueur("Bako", 1);
            Joueur j2 = new Joueur("Cousin", 2);
            Application.Run(new JeuDePoint( j1, j2, 6));
        }
    }
}
