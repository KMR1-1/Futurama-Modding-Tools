using System.IO;
using System.Drawing;


namespace FuturamaLib.NIF.Structures
{
    public class NiPalette : NiObject
    {
        public byte UnknownByte;
        public uint numPalette;

        public Color[] Palette;

        public NiPalette(NIFReader file, BinaryReader reader) : base(file, reader)
        {
            UnknownByte = reader.ReadByte();
            numPalette = reader.ReadUInt32();
            Offsets["Palette"] = reader.BaseStream.Position;
            Palette = new Color[numPalette];

            for (var i = 0; i < Palette.Length; i++)
                Palette[i] = reader.ReadColor4Byte();
        }
    }
}