namespace JeuDePoints
{
    public class Joueur
    {
        private String Nom;
        public int marque;
        public int score;
        public JeuDePoint? Jeu;
        public Joueur adversaire;
        public Joueur( String nom, int m, JeuDePoint? j = null)
        {
            Nom = nom;
            marque = m;
            score = 0;
            Jeu = j;
            adversaire = this;
        }

        public bool VerifierLigne()
{
    if( Jeu != null)
    {
        int[,] gll = Jeu.grille;

    // Vérification des lignes horizontales
    for (int i = 0; i < Jeu.tailleGrille; i++)
    {
        for (int j = 0; j < Jeu.tailleGrille - 4; j++)
        {
            if (gll[i, j] == marque && gll[i, j + 1] == marque && gll[i, j + 2] == marque && gll[i, j + 3] == marque && gll[i, j + 4] == marque)
            {
                return true;
            }
        }
    }

    // Vérification des lignes verticales
    for (int i = 0; i < Jeu.tailleGrille - 4; i++)
    {
        for (int j = 0; j < Jeu.tailleGrille; j++)
        {
            if (gll[i, j] == marque && gll[i + 1, j] == marque && gll[i + 2, j] == marque && gll[i + 3 , j] == marque && gll[i + 4, j] == marque)
            {
                return true;
            }
        }
    }

    // Vérification des diagonales
    for (int i = 0; i < Jeu.tailleGrille - 4; i++)
    {
        for (int j = 0; j < Jeu.tailleGrille - 4; j++)
        {
            if (gll[i, j] == marque && gll[i + 1, j + 1] == marque && gll[i + 2, j + 2] == marque && gll[i + 3, j + 3] == marque && gll[i + 4, j + 4] == marque)
            {
                return true;
            }
        }
    }

    }
    

    return false;
}

private bool BloquerAdversaire()
{
    if( Jeu != null)
    {
        int[,] gll = Jeu.grille;
    
    int adversairemarque = adversaire.marque;
    

    // Vérifie les alignements horizontaux
    for (int i = 0; i < Jeu.tailleGrille; i++)
    {
        for (int j = 0; j < Jeu.tailleGrille - 4; j++)
        {
            if (gll[i, j] == adversairemarque && gll[i, j + 1] == adversairemarque && gll[i, j + 2] == adversairemarque && gll[i, j + 3] == adversairemarque && gll[i, j + 4] == 0)
            {
                // D'abord essayer de bloquer après
                if (j + 4 < Jeu.tailleGrille && gll[i, j + 4] == 0)
                {
                    Jeu.grille[i, j + 4] = marque;
                        Jeu.SauvegarderJeu($"joueur {adversairemarque} ({i}, {j+5}).");
                    Jeu.Invalidate();
                    return true;
                }
                // Si la case après est occupée, essayer avant
                else if (j - 1 >= 0 && gll[i, j - 1] == 0)
                {
                    Jeu.grille[i, j - 1] = marque;
                        Jeu.SauvegarderJeu($"joueur {adversairemarque} ({i}, {j-1}).");
                    Jeu.Invalidate();
                    return true;
                }
            }
        }
    }

    // Vérifie les alignements verticaux
    for (int i = 0; i < Jeu.tailleGrille - 4; i++)
    {
        for (int j = 0; j < Jeu.tailleGrille; j++)
        {
            if (gll[i, j] == adversairemarque && gll[i + 1, j] == adversairemarque && gll[i + 2, j] == adversairemarque && gll[i + 3, j] == adversairemarque && gll[i + 4, j] == 0)
            {
                // D'abord essayer de bloquer après
                if (i + 4 < Jeu.tailleGrille && gll[i + 4, j] == 0)
                {
                    Jeu.grille[i + 4, j] = marque;
                        Jeu.SauvegarderJeu($"joueur {adversairemarque} ({i+5}, {j}).");
                    Jeu.Invalidate();
                    return true;
                }
                // Si la case après est occupée, essayer avant
                else if (i - 1 >= 0 && gll[i - 1, j] == 0)
                {
                    Jeu.grille[i - 1, j] = marque;
                        Jeu.SauvegarderJeu($"joueur {adversairemarque} ({i-1}, {j}).");
                    Jeu.Invalidate();
                    return true;
                }
            }
        }
    }

    // Vérifie les alignements diagonaux (haut-gauche à bas-droite)
    for (int i = 0; i < Jeu.tailleGrille - 4; i++)
    {
        for (int j = 0; j < Jeu.tailleGrille - 4; j++)
        {
            if (gll[i, j] == adversairemarque && gll[i + 1, j + 1] == adversairemarque && gll[i + 2, j + 2] == adversairemarque && gll[i + 3, j + 3] == adversairemarque && gll[i + 4, j + 4] == 0)
            {
                // D'abord essayer de bloquer après
                if (i + 4 < Jeu.tailleGrille && j + 4 < Jeu.tailleGrille && gll[i + 4, j + 4] == 0)
                {
                    Jeu.grille[i + 4, j + 4] = marque;
                        Jeu.SauvegarderJeu($"joueur {adversairemarque} ({i+5}, {j+5}).");
                    Jeu.Invalidate();
                    return true;
                }
                // Si la case après est occupée, essayer avant
                else if (i - 1 >= 0 && j - 1 >= 0 && gll[i - 1, j - 1] == 0)
                {
                    Jeu.grille[i - 1, j - 1] = marque;
                        Jeu.SauvegarderJeu($"joueur {adversairemarque} ({i-1}, {j-1}).");
                    Jeu.Invalidate();
                    return true;
                }
            }
        }
    }

    // Vérifie les alignements diagonaux (bas-gauche à haut-droite)
    for (int i = 4; i < Jeu.tailleGrille; i++)
    {
        for (int j = 0; j < Jeu.tailleGrille - 4; j++)
        {
            if (gll[i, j] == adversairemarque && gll[i - 1, j + 1] == adversairemarque && gll[i - 2, j + 2] == adversairemarque && gll[i - 3, j + 3] == adversairemarque && gll[i - 4, j + 4] == 0)
            {
                // D'abord essayer de bloquer après
                if (i - 4 >= 0 && j + 4 < Jeu.tailleGrille && gll[i - 4, j + 4] == 0)
                {
                    Jeu.grille[i - 4, j + 4] = marque;
                        Jeu.SauvegarderJeu($"joueur {adversairemarque} ({i-5}, {j+5}).");
                    Jeu.Invalidate();
                    return true;
                }
                // Si la case après est occupée, essayer avant
                else if (i + 1 < Jeu.tailleGrille && j - 1 >= 0 && gll[i + 1, j - 1] == 0)
                {
                    Jeu.grille[i + 1, j - 1] = marque;
                        Jeu.SauvegarderJeu($"joueur {adversairemarque} ({i+1}, {j-1}).");
                    Jeu.Invalidate();
                    return true;
                }
            }
        }
    }
    }

    return false; // Aucun alignement bloquant trouvé
}


        public void PlacerPoint()
{

    
    if( Jeu != null)
   {
    if (Jeu.pause)
    {
        // Affiche un message et empêche le placement
        MessageBox.Show("Le jeu est en pause. Reprenez le jeu pour continuer.", "Pause active");
        return;
    } 
        int[,] gll = Jeu.grille;
   
    
    bool pointPlace = false; // Indique si un point a été placé

    if (score >= 3)
    {
        // Vérifie si l'adversaire est sur le point de gagner
        if (BloquerAdversaire()) return;

        // Cherche à compléter un alignement horizontal (derrière puis devant)
        for (int i = 0; i < Jeu.tailleGrille; i++)
        {
            for (int j = 0; j < Jeu.tailleGrille - 3; j++)
            {
                if (gll[i, j] == marque && gll[i, j + 1] == marque && gll[i, j + 2] == marque)
                {
                    
                    if (j + 3 < Jeu.tailleGrille && gll[i, j + 3] == 0) // Devant
                    {
                        Jeu.grille[i, j + 3] = marque;
                        Jeu.SauvegarderJeu($"joueur {marque} ({i}, {j+3}).");
                        score++;
                        Jeu.Invalidate();
                        pointPlace = true;
                        return;
                    }
                    else if (j > 0 && gll[i, j - 1] == 0) // Derrière
                    {
                        Jeu.grille[i, j - 1] = marque;
                        Jeu.SauvegarderJeu($"joueur {marque} ({i}, {j-1}).");
                        score++;
                        Jeu.Invalidate();
                        pointPlace = true;
                        return;
                    }
                }
            }
        }

        // Cherche à compléter un alignement vertical (derrière puis devant)
        for (int i = 0; i < Jeu.tailleGrille - 3; i++)
        {
            for (int j = 0; j < Jeu.tailleGrille; j++)
            {
                if (gll[i, j] == marque && gll[i + 1, j] == marque && gll[i + 2, j] == marque)
                {
                    
                     if (i + 3 < Jeu.tailleGrille && gll[i + 3, j] == 0) // Devant
                    {
                        Jeu.grille[i + 3, j] = marque;
                        Jeu.SauvegarderJeu($"joueur {marque} ({i+3}, {j}).");
                        score++;
                        Jeu.Invalidate();
                        pointPlace = true;
                        return;
                    }
                    else if (i > 0 && gll[i - 1, j] == 0) // Derrière
                    {
                        Jeu.grille[i - 1, j] = marque;
                        Jeu.SauvegarderJeu($"joueur {marque} ({i-1}, {j}).");
                        score++;
                        Jeu.Invalidate();
                        pointPlace = true;
                        return;
                    }
                }
            }
        }

        // Cherche à compléter un alignement diagonal (haut-gauche à bas-droite)
        for (int i = 0; i < Jeu.tailleGrille - 3; i++)
        {
            for (int j = 0; j < Jeu.tailleGrille - 3; j++)
            {
                if (gll[i, j] == marque && gll[i + 1, j + 1] == marque && gll[i + 2, j + 2] == marque)
                {
                    
                    if (i + 3 < Jeu.tailleGrille && j + 3 < Jeu.tailleGrille && gll[i + 3, j + 3] == 0) // Devant
                    {
                        Jeu.grille[i + 3, j + 3] = marque;
                        Jeu.SauvegarderJeu($"joueur {marque} ({i+3}, {j+3}).");
                        score++;
                        Jeu.Invalidate();
                        pointPlace = true;
                        return;
                    }
                    else if (i > 0 && j > 0 && gll[i - 1, j - 1] == 0) // Derrière
                    {
                        Jeu.grille[i - 1, j - 1] = marque;
                        Jeu.SauvegarderJeu($"joueur {marque} ({i-1}, {j-1}).");
                        score++;
                        Jeu.Invalidate();
                        pointPlace = true;
                        return;
                    }
                }
            }
        }

        // Cherche à compléter un alignement diagonal (bas-gauche à haut-droite)
        for (int i = 3; i < Jeu.tailleGrille; i++)
        {
            for (int j = 0; j < Jeu.tailleGrille - 3; j++)
            {
                if (gll[i, j] == marque && gll[i - 1, j + 1] == marque && gll[i - 2, j + 2] == marque)
                {
                    
                     if (i - 3 >= 0 && j + 3 < Jeu.tailleGrille && gll[i - 3, j + 3] == 0) // Devant
                    {
                        Jeu.grille[i - 3, j + 3] = marque;
                        Jeu.SauvegarderJeu($"joueur {marque} ({i-3}, {j+3}).");
                        score++;
                        Jeu.Invalidate();
                        pointPlace = true;
                        return;
                    }
                    else if (i + 1 < Jeu.tailleGrille && j > 0 && gll[i + 1, j - 1] == 0) // Derrière
                    {
                        Jeu.grille[i + 1, j - 1] = marque;
                        Jeu.SauvegarderJeu($"joueur {marque} ({i+1}, {j-1}).");
                        score++;
                        Jeu.Invalidate();
                        pointPlace = true;
                        return;
                    }
                }
                }
            }
        }

        // Si aucune opportunité de compléter un alignement n'est trouvée
        if (!pointPlace)
        {
            // Affiche un message et garde le joueur courant
            MessageBox.Show("Aucune place disponible pour compléter un alignement. Tour du joueur suivant annulé.", "Impossible");
        }
    }
}


    }
}