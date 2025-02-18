using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace FuturamaLib.GLTF.Builder
{


    class TgaToPng
    {
        public static void ConvertirImageTgaEnPng(string cheminFichierTga)
        {
            // Vérifie si le fichier existe
            if (!File.Exists(cheminFichierTga))
            {
                Console.WriteLine("Le fichier spécifié n'existe pas.");
                return;
            }

            // Vérifie si le fichier a l'extension .tga
            if (Path.GetExtension(cheminFichierTga)?.ToLower() != ".tga")
            {
                Console.WriteLine("Le fichier spécifié n'est pas un fichier TGA.");
                return;
            }

            try
            {
                // Charge l'image TGA
                using (Image image = Image.Load(cheminFichierTga))
                {
                    // Détermine le chemin de destination avec l'extension PNG
                    string cheminDestination = Path.ChangeExtension(cheminFichierTga, ".png");

                    // Sauvegarde l'image au format PNG
                    image.Save(cheminDestination, new PngEncoder());

                }

                // Supprime le fichier TGA original
                File.Delete(cheminFichierTga);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la conversion de l'image {cheminFichierTga}: {ex.Message}");
            }
        }
    }
}