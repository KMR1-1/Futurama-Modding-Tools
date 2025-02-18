using Newtonsoft.Json;

public class TgaImport
{
    public TgaImport(string nifPath, string tgaPath, Dictionary<string, long> Offsets)
    {
        var img = new TGAParser(tgaPath);

        using (BinaryWriter writer = new BinaryWriter(File.Open(nifPath, FileMode.Open, FileAccess.Write)))
        {
            writer.Seek((int)Offsets["PixelData"], SeekOrigin.Begin);
            writer.Write(img.PixelIndices, 0, img.PixelIndices.Length);
            writer.Seek((int)Offsets["Palette"], SeekOrigin.Begin);
            for (int i = 0; i < img.Palette.Length; i++)
            {
                writer.Write(img.Palette[i].R);
                writer.Write(img.Palette[i].G);
                writer.Write(img.Palette[i].B);
                writer.Write(img.Palette[i].A);
            }
            //TODO directly change the way NiPalette gets values bytes=>float=>bytes to just bytes 
            for (int i = img.Palette.Length; i < 256; i++)
            {
                writer.Write((byte)0); // R
                writer.Write((byte)0); // G
                writer.Write((byte)0); // B
                writer.Write((byte)0); // Alpha / Padding
            }
        }
        System.Console.WriteLine($"image {tgaPath} imported in {nifPath}");

    }
}