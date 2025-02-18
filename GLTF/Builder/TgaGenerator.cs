using System;
using System.IO;
using System.Drawing;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder
{


    public class TGAGenerator
    {
        private Color[] palette;
        private byte[] pixelData;
        private int width;
        private int height;

        public TGAGenerator(NiPixelData texdata, string path)
        {

            pixelData = texdata.PixelData;
            width = (int)texdata.MipMaps[0].Width;
            height = (int)texdata.MipMaps[0].Height;
            var pxfmt = (uint)texdata.PixelFormat;
            if (pxfmt == 2 || pxfmt == 3)
            {
                palette = texdata.Palette.Object.Palette;
                SaveTGAPalette(path, pxfmt);
            }
            if (texdata.PixelFormat == FuturamaLib.NIF.Enums.PixelFormat.PX_FMT_RGBA8)
            {
                SaveTgaRGB(path);
            }
        }
        public void SaveTgaRGB(string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                // Écrire l'en-tête TGA (18 octets)
                writer.Write((byte)0);              // ID Length (0 pour pas d'ID)
                writer.Write((byte)0);              // Color Map Type (0 pour pas de palette)
                writer.Write((byte)2);              // Image Type (2 pour image non compressée en RGBA)
                writer.Write((ushort)0);             // Color Map Origin (0)
                writer.Write((ushort)0);             // Color Map Length (0)
                writer.Write((byte)0);              // Color Map Depth (non utilisé)
                writer.Write((ushort)0);             // X-Origin (0)
                writer.Write((ushort)0);             // Y-Origin (0)
                writer.Write((ushort)width);         // Largeur de l'image
                writer.Write((ushort)height);        // Hauteur de l'image
                writer.Write((byte)32);             // Pixel Depth (32 bits par pixel, RGBA)
                writer.Write((byte)0);              // Image Descriptor (0)

                // Écrire les données des pixels (RGBA8)
                for (int i = 0; i < pixelData.Length; i += 4)
                {
                    // Le format attendu par TGA est BGRA, donc on inverse l'ordre des couleurs
                    writer.Write(pixelData[i + 2]); // Bleu (B)
                    writer.Write(pixelData[i + 1]); // Vert (G)
                    writer.Write(pixelData[i]);     // Rouge (R)
                    writer.Write(pixelData[i + 3]); // Alpha (A)
                }
            }
        }

        public void SaveTGAPalette(string filePath, uint pxfmt)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                // Write TGA header
                writer.Write((byte)0); // ID Length
                writer.Write((byte)1); // Color Map Type
                writer.Write((byte)1); // Image Type (Indexed, uncompressed)
                writer.Write((ushort)0); // Color Map Origin
                writer.Write((ushort)palette.Length); // Color Map Length
                writer.Write((byte)32); // Color Map Depth (32 bits = BGRA)
                writer.Write((ushort)0); // X-Origin
                writer.Write((ushort)0); // Y-Origin
                writer.Write((ushort)width); // Image Width
                writer.Write((ushort)height); // Image Height
                writer.Write((byte)8); // Pixel Depth (8 bits per pixel for indices)
                writer.Write((byte)0); // Image Descriptor

                if (pxfmt == 2)
                {
                    foreach (var color in palette)
                    {
                        writer.Write(color.B);
                        writer.Write(color.G);
                        writer.Write(color.R);
                        writer.Write(color.A);
                    }
                }
                if (pxfmt == 3)
                {
                    foreach (var color in palette)
                    {
                        writer.Write(color.R);
                        writer.Write(color.G);
                        writer.Write(color.B);
                        writer.Write(color.A);
                    }
                }

                for (var i = 0; i < width * height; i++)
                {
                    writer.Write(pixelData[i]);
                }
            }
        }
    }
}
