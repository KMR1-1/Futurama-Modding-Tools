using FuturamaLib.NIF.Enums;
using System.IO;
using FuturamaLib.NIF;

namespace FuturamaLib.NIF.Structures
{
    public class NiObject
    {
        public NIFReader File;

        public NifVersion Version => File.Header.Version;
        public Dictionary<string, long> Offsets { get; private set; } = new();


        public NiObject(NIFReader file, BinaryReader _)
        {
            File = file;
        }
    }
}