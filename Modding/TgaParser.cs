using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

public class TGAParser
{
    public Color[] Palette { get; private set; }
    public byte[] PixelIndices { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Size {get; private set;}

    public TGAParser(string filePath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            reader.ReadByte(); // ID Length
            byte colorMapType = reader.ReadByte(); // Color Map Type
            byte imageType = reader.ReadByte(); // Image Type

            if (colorMapType != 1 || imageType != 1)
            {
                throw new NotSupportedException("Only indexed (colormapped) uncompressed TGA files are supported.");
            }

            ushort colorMapOrigin = reader.ReadUInt16(); // Color Map Origin (ignored)
            ushort colorMapLength = reader.ReadUInt16(); // Color Map Length
            byte colorMapDepth = reader.ReadByte(); // Color Map Depth (must be 32 for BGRA)

            if (colorMapDepth != 32)
            {
                throw new NotSupportedException("Only 32-bit color maps are supported.");
            }

            reader.ReadUInt16(); // X-Origin (ignored)
            reader.ReadUInt16(); // Y-Origin (ignored)
            Width = reader.ReadUInt16(); // Image Width
            Height = reader.ReadUInt16(); // Image Height
            Size = Width * Height;
            byte pixelDepth = reader.ReadByte(); // Pixel Depth (should be 8 for indexed color)
            reader.ReadByte(); // Image Descriptor (ignored)

            if (pixelDepth != 8)
            {
                throw new NotSupportedException("Only 8-bit indexed TGA files are supported.");
            }
            Palette = new Color[colorMapLength];
            for (int i = 0; i < colorMapLength; i++)
            {
                byte b = reader.ReadByte();
                byte g = reader.ReadByte();
                byte r = reader.ReadByte();
                byte a = reader.ReadByte();
                Palette[i] = Color.FromArgb(a, r, g, b);
            }
            // Read pixel indices
            PixelIndices = reader.ReadBytes(Size);
        }
    }
}
