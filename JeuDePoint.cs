using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace JeuDePoints
{
    public partial class JeuDePoint : Form
    {
        public int[,] grille;
        private Joueur joueur1;
        private Joueur joueur2;
        public Joueur currentPlayer; // Joueur actuel (1 ou 2)
        public int tailleGrille = 10; // Taille de la grille (10x10 par défaut)
        private const int tailleCellule = 50; // Taille de chaque cellule en pixels
        private const int marge = 50; // Marge pour le dessin de la grille
        private Button boutonJ1;
        private Button boutonJ2;
        private Button boutonPause;
        private Button boutonReprendre;
        public bool pause = false;
        private int limite;
        private List<Point> pointsRouges = new List<Point>();
        private List<Point> pointsBleus = new List<Point>();


        public JeuDePoint(Joueur j1, Joueur j2, int lim)
        {
            InitializeComponent();
            joueur1 = j1;
            joueur2 = j2;
            joueur1.Jeu = this;
            joueur2.Jeu = this;
            joueur1.adversaire = joueur2;
            joueur2.adversaire = joueur1;
            limite = lim;
            currentPlayer = joueur1;
            grille = new int[tailleGrille, tailleGrille];
            this.Text = "Jeu de Points";
            this.Size = new Size(tailleGrille * tailleCellule + marge * 2, tailleGrille * tailleCellule + marge * 2);
            this.DoubleBuffered = true;
            InitialiserGrille();

            boutonJ1 = new Button();
            boutonJ1.Text = "GB J1";
            boutonJ1.Location = new Point(10, 10);
            boutonJ1.Click += Button_Click;
            this.Controls.Add(boutonJ1);

            boutonJ2 = new Button();
            boutonJ2.Text = "GB J2";
            boutonJ2.Location = new Point(500, 10);
            boutonJ2.Click += Button_Click;
            this.Controls.Add(boutonJ2);

            boutonPause = new Button();
            boutonPause.Text = "Pause";
            boutonPause.Location = new Point(200, 10);
            boutonPause.Click += BoutonPause_Click;
            this.Controls.Add(boutonPause);

            boutonReprendre = new Button();
            boutonReprendre.Text = "Reprendre";
            boutonReprendre.Location = new Point(350, 10);
            boutonReprendre.Click += BoutonReprendre_Click;
            this.Controls.Add(boutonReprendre);
        }

        private void InitialiserGrille()
        {
            for (int i = 0; i < tailleGrille; i++)
            {
                for (int j = 0; j < tailleGrille; j++)
                {
                    grille[i, j] = 0; // 0 signifie que l'intersection est vide
                }
            }

            this.Paint += Form1_Paint; // On s'abonne à l'événement Paint pour dessiner la grille
            this.MouseClick += Form1_MouseClick; // On s'abonne à l'événement MouseClick pour gérer les clics
        }

        // Méthode pour dessiner la grille et les points
        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen blackPen = new Pen(Color.Green, 2);

            // Dessin des lignes horizontales et verticales
            for (int i = 0; i <= tailleGrille; i++)
            {
                g.DrawLine(blackPen, marge, marge + (i * tailleCellule), marge + (tailleGrille * tailleCellule), marge + (i * tailleCellule));
                g.DrawLine(blackPen, marge + (i * tailleCellule), marge, marge + (i * tailleCellule), marge + (tailleGrille * tailleCellule));
            }
            if( !pause )
            {
                // Dessin des points
                for (int i = 0; i < tailleGrille; i++)
                {
                    for (int j = 0; j < tailleGrille; j++)
                    {
                        if (grille[i, j] != 0) // Si la case est occupée par un point
                        {
                            Brush brush = (grille[i, j] == 1) ? Brushes.Red : Brushes.Blue;
                            g.FillEllipse(brush, marge + (i * tailleCellule) - 10, marge + (j * tailleCellule) - 10, 20, 20);
                        }
                    }
                }
            }
            
        }

        private void UpdateScores()
        {
            // Met à jour les scores en fonction des alignements
            joueur1.score = joueur1.VerifierLigne() ? joueur1.score + 1 : joueur1.score;
            joueur2.score = joueur2.VerifierLigne() ? joueur2.score + 1 : joueur2.score;
        }

        private void ResetGrille()
        {
            for (int i = 0; i < tailleGrille; i++)
            {
                for (int j = 0; j < tailleGrille; j++)
                {
                    grille[i, j] = 0; // Réinitialisation de la grille
                }
            }
            this.Invalidate(); // Redessine la grille
            currentPlayer = joueur1; // Recommence avec le joueur 1
            joueur1.score = 0; // Réinitialisation des scores
            joueur2.score = 0;
            UpdateScores();
        }

        private void CoordonneValide(int x, int y)
{
    if (x >= 0 && x < tailleGrille && y >= 0 && y < tailleGrille)
    {
        if (grille[x, y] == 0) // Si la case est vide
        {
            // Ajouter le point actuel à la grille
            grille[x, y] = currentPlayer.marque;

            // Si c'est un point rouge (grille[x, y] == 1)
            if (currentPlayer.marque == joueur1.marque)
            {
                pointsRouges.Add(new Point(x, y)); // Ajouter à la liste des points rouges
                // Si la limite est atteinte, supprimer le premier point rouge
                if (pointsRouges.Count == (limite + 1))
                {
                    Point premierPointRouge = pointsRouges[0];
                    grille[premierPointRouge.X, premierPointRouge.Y] = 0; // Supprimer le premier point rouge de la grille
                    pointsRouges.RemoveAt(0); // Retirer le premier point de la liste
                }
            }
            // Si c'est un point bleu (grille[x, y] == 2)
            else if (currentPlayer.marque == joueur2.marque)
            {
                pointsBleus.Add(new Point(x, y)); // Ajouter à la liste des points bleus
                // Si la limite est atteinte, supprimer le premier point bleu
                if (pointsBleus.Count == (limite + 1))
                {
                    Point premierPointBleu = pointsBleus[0];
                    grille[premierPointBleu.X, premierPointBleu.Y] = 0; // Supprimer le premier point bleu de la grille
                    pointsBleus.RemoveAt(0); // Retirer le premier point de la liste
                }
            }

            // Sauvegarder l'action
            SauvegarderJeu($"joueur {currentPlayer.marque} ({x}, {y})");

            // Vérification si le joueur a gagné
            if (currentPlayer.VerifierLigne())
            {
                SauvegarderJeu($"Le joueur {currentPlayer.marque} a gagné !");
                MessageBox.Show("Le Joueur " + currentPlayer.marque + " a gagné !");
                ResetGrille();
                return;
            }

            // Mise à jour du score et changement de joueur
            if (currentPlayer.marque == 1)
            {
                joueur1.score++;
            }
            else
            {
                joueur2.score++;
            }

            currentPlayer = (currentPlayer.marque == joueur1.marque) ? joueur2 : joueur1;
            this.Invalidate();
        }
        else
        {
            MessageBox.Show("Cette intersection est déjà occupée !");
        }
    }
}

        private void Form1_MouseClick(object? sender, MouseEventArgs e)
        {
            int x = (e.X - marge) / tailleCellule; // Calcul de la colonne
            int y = (e.Y - marge) / tailleCellule; // Calcul de la ligne
            if( !pause)
            {
                 CoordonneValide(x, y);
                
            }
           
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (currentPlayer == joueur1)
            {
                joueur1.PlacerPoint();

                if (currentPlayer.VerifierLigne())
                {
                    SauvegarderJeu($"Le joueur {currentPlayer.marque} a gagné !");
                    MessageBox.Show("Le Joueur " + currentPlayer.marque + " a gagné !");
                    ResetGrille();
                    return;
                }
                currentPlayer = joueur2;
            }
            else if (currentPlayer == joueur2)
            {
                joueur2.PlacerPoint();

                if (currentPlayer.VerifierLigne())
                {
                    SauvegarderJeu($"Le joueur {currentPlayer.marque} a gagné !");
                    MessageBox.Show("Le Joueur " + currentPlayer.marque + " a gagné !");
                    ResetGrille();
                    return;
                }
                currentPlayer = joueur1;
            }
        }

        public void SauvegarderJeu(string message)
{
    string filePath = "jeu_de_points.txt"; // Chemin du fichier de sauvegarde
    using (StreamWriter writer = new StreamWriter(filePath, true))
    {
        if (!pause)
        {
            writer.WriteLine($"{message}");
        }
    }
}


        private void BoutonPause_Click(object? sender, EventArgs e)
        {
            // Sauvegarder l'état actuel du jeu
            pause = true;
            SauvegarderJeu("Jeu mis en pause.");
            MessageBox.Show("Le jeu a été mis en pause.");
        }

        private void BoutonReprendre_Click(object? sender, EventArgs e)
        {
            // Charger l'état du jeu depuis le fichier
            ChargerEtatJeu();
            MessageBox.Show("Le jeu a repris.");
        }

        private void ChargerEtatJeu()
{
    pause = false;
    this.Invalidate(); 
    string filePath = "jeu_de_points.txt";
    if (File.Exists(filePath))
    {
        string[] lignes = File.ReadAllLines(filePath);
        foreach (var ligne in lignes)
        {
            try
            {
                // Vérifier si la ligne contient "joueur"
                if (ligne.Contains("joueur"))
                {
                    // Récupérer les coordonnées et l'état du joueur
                    string[] parts = ligne.Split(' ');
                    if (parts.Length < 2) continue;

                    string joueur = parts[0];
                    string[] coordonnees = parts[1].Trim('(', ')').Split(',');

                    // Vérifier si les coordonnées sont valides
                    if (coordonnees.Length != 2) continue;

                    if (int.TryParse(coordonnees[0], out int x) && int.TryParse(coordonnees[1], out int y))
                    {
                        // Vérifier si les indices sont dans les limites de la grille
                        if (x >= 0 && x < tailleGrille && y >= 0 && y < tailleGrille)
                        {
                            // Placer les points dans la grille
                            if (joueur == "joueur1")
                            {
                                grille[x, y] = 1;
                            }
                            else if (joueur == "joueur2")
                            {
                                grille[x, y] = 2;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion des erreurs pour éviter de planter
                MessageBox.Show($"Erreur lors du chargement de la ligne : {ligne}\n{ex.Message}", "Erreur");
            }
        }
    }
}

    }
}
