using FuturamaLib.NIF.Structures;
using FuturamaLib.NIF;
using System.IO;

namespace FuturamaLib.GLTF.Init
{


    public class NifManager
    {
        public string rootDir;
        public List<NIFReader> Readers { get; set; }
        public List<string> stageNames {get; set;}
        public NIFReader defsReader { get; set; }
        public NifManager(string rootDir)
        {
            this.rootDir = rootDir;
            if (!Path.Exists(rootDir))
            {
                System.Console.WriteLine($"could not get Readers for files in: {rootDir}");
            }
        }
        public void GetLevelReaders()
        {
            string DefsPath = Path.Combine(rootDir, "defs.nif");
            if (!File.Exists(DefsPath))
            {
                System.Console.WriteLine($"{rootDir} does not contain defs.nif");
            }
            else
            {
                defsReader = new NIFReader(DefsPath);
                var stages = new StageManager(defsReader, rootDir);
                Readers = stages.Readers;
                stageNames = stages.StageNames;
                fixTexRef(defsReader);
                foreach (var reader in Readers)
                {
                    fixTexRef(reader);
                }
            }
        }
        public void GetReaders(List<string> fileNames)
        {
            foreach (var name in fileNames)
            {
                stageNames.Add(name);
                var filePath = Path.Combine(rootDir, name, ".nif");
                AddReader(filePath);
            }
        }
        private void AddReader(string nifFilePath)
        {
            if (File.Exists(nifFilePath))
            {
                var reader = new NIFReader(nifFilePath);
                Readers.Add(reader);
                fixTexRef(reader);
            }
            else
            {
                Console.WriteLine($"File not found: {nifFilePath}");
            }
        }
        private void fixTexRef(NIFReader reader)
        {
            foreach (var kvp in reader.ObjectsByRef)
                if (kvp.Value is NiTexturingProperty niTexturingProperty && niTexturingProperty.BaseTexture.Source.IsValid)
                    niTexturingProperty.BaseTexture.Source.SetRef(reader);
        }
    }
}