using System;
using System.IO;

class ImgFileOverwriter
{
    public ImgFileOverwriter(string sourceFilePath, int offset, string archiveFilePath)
    {
        if (!File.Exists(sourceFilePath))
        {
            throw new FileNotFoundException("Source file not found", sourceFilePath);
        }
        if (!File.Exists(archiveFilePath))
        {
            throw new FileNotFoundException("Archive file not found", archiveFilePath);
        }
        
        byte[] data = File.ReadAllBytes(sourceFilePath);
        
        using (FileStream archiveStream = new FileStream(archiveFilePath, FileMode.Open, FileAccess.Write))
        {
            if (offset + data.Length > archiveStream.Length)
            {
                throw new ArgumentOutOfRangeException("Offset exceeds archive file size");
            }
            
            archiveStream.Seek(offset, SeekOrigin.Begin);
            archiveStream.Write(data, 0, data.Length);
        }
    }
}