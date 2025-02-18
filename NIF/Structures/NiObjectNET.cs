using System.IO;

namespace FuturamaLib.NIF.Structures
{
    public class NiObjectNET : NiObject
    {
        public NiString Name;

        public NiRef<NiExtraData> ExtraData;

        public NiRef<NiTimeController> Controller;

        public NiObjectNET(NIFReader file, BinaryReader reader) : base(file, reader)
        {
            Name = new NiString(reader);
            ExtraData = new NiRef<NiExtraData>(reader.ReadUInt32());
            Controller = new NiRef<NiTimeController>(reader.ReadUInt32());
        }
        public virtual Dictionary<string, object> GetDetails()
        {
            var details = new Dictionary<string, object>
            {
                {"Name", !string.IsNullOrEmpty(Name.Value) ? Name.Value : null},
                {"Controller", Controller.IsValid ? $"{Controller.RefId}: {Controller.GetType().Name}": null},
                {"ExtraData", ExtraData.IsValid ? $"{ExtraData.RefId}: {ExtraData.Object.GetType().Name}": null},
            };
            return details;
            
        }
    }
}