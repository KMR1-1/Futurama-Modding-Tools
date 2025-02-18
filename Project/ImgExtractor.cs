using System.Text;
using Newtonsoft.Json;

public class ImgExtractor
{
    private FileStream fileStream;
    public Dictionary<string, long> FilePaths { get; private set; }
    public string outPutPath {get; set;}

    public ImgExtractor(List<long> offsets, string projectPath)
    {
        outPutPath = Path.Combine(projectPath, "Extracted");
        var isoPath = Path.Combine(projectPath, "Futurama.iso");
        fileStream = new FileStream(isoPath, FileMode.Open, FileAccess.Read);
        FilePaths = new Dictionary<string, long>();
        
        foreach (var offset in offsets)
        {
            ListFiles(offset*2048);
        }
        Close(projectPath);
    }

    private int ReadInt32()
    {
        byte[] buffer = new byte[4];
        fileStream.Read(buffer, 0, 4);
        return BitConverter.ToInt32(buffer, 0);
    }

    private string ReadString()
    {
        StringBuilder sb = new StringBuilder();
        byte b;
        while ((b = (byte)fileStream.ReadByte()) != 0)
        {
            sb.Append((char)b);
        }
        return sb.ToString();
    }

    private void Align(int alignment)
    {
        while (fileStream.Position % alignment != 0)
        {
            fileStream.ReadByte();
        }
    }

    private void ListFiles(long offset)
    {
        fileStream.Seek(offset, SeekOrigin.Begin);
        int rootDirectorySize = ReadInt32();
        rootDirectorySize &= ~3;
        long rootDirectoryEnd = fileStream.Position + rootDirectorySize - 4;
        ListDirectory("", rootDirectoryEnd, offset);
    }

    private void ListDirectory(string basePath, long directoryEnd, long archiveOffset)
    {
        var outpath = Path.Combine(outPutPath, basePath.TrimStart('/'));
        if (!Directory.Exists(outpath))
        {
            Directory.CreateDirectory(outpath);
        }
        while (fileStream.Position < directoryEnd)
        {
            string entryName = ReadString();
            Align(4);

            bool isFolder = (entryName[0] & 0x80) != 0;
            if (isFolder)
            {
                entryName = ((char)(entryName[0] ^ 0x80)) + entryName.Substring(1);
            }

            string fullPath = basePath + "/" + entryName;
            int entrySize = ReadInt32();

            if (isFolder)
            {
                long subDirEnd = fileStream.Position + entrySize - 4;
                ListDirectory(fullPath, subDirEnd, archiveOffset);
            }
            else
            {
                outpath = Path.Combine(outPutPath, fullPath.TrimStart('/'));
                int dataOffset = ReadInt32();
                long realOffset = archiveOffset+dataOffset;
                var currentOffset = fileStream.Position;
                ExtractFile(outpath, realOffset, entrySize);
                fileStream.Seek(currentOffset, SeekOrigin.Begin);


                
                System.Console.WriteLine(outpath);
                FilePaths[fullPath] = realOffset;
            }
        }
    }
    public void ExtractFile(string outpath, long offset, int size)
    {
        fileStream.Seek(offset, SeekOrigin.Begin);
        byte[] buffer = new byte[size];
        fileStream.Read(buffer, 0, size);
        File.WriteAllBytes(outpath, buffer);
    }
    public void Close(string projectPath)
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(FilePaths, Newtonsoft.Json.Formatting.Indented);
        string jsonFilePath = Path.Combine(projectPath, "filePaths.json");
        File.WriteAllText(jsonFilePath, json);
        fileStream.Close();
    }
}
