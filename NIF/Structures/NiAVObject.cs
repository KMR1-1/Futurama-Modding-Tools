using System.IO;
using System.Numerics;
using Matrix = System.Numerics.Matrix4x4;

namespace FuturamaLib.NIF.Structures
{
    public class NiAVObject : NiObjectNET
    {
        public ushort Flags;

        public Vector3 Translation;

        public Matrix Rotation;

        public float Scale;

        public Vector3 Velocity;

        public NiRef<NiProperty>[] Properties;

        public bool HasBoundingBox;

        public BoundingBox BoundingBox;

        public NiAVObject(NIFReader file, BinaryReader reader) : base(file, reader)
        {
            Flags = reader.ReadUInt16();
            Offsets["pos"] = reader.BaseStream.Position;
            Translation = reader.Read<Vector3>();
            Rotation = reader.ReadMatrix33();
            Offsets["spos"] = reader.BaseStream.Position;
            Scale = reader.ReadSingle();
            Velocity = reader.Read<Vector3>();

            Properties = new NiRef<NiProperty>[reader.ReadUInt32()];
            for (var i = 0; i < Properties.Length; i++)
                Properties[i] = new NiRef<NiProperty>(reader.ReadUInt32());

            HasBoundingBox = reader.ReadBoolean(Version);
            if (HasBoundingBox)
                BoundingBox = reader.Read<BoundingBox>();
        }

        public override Dictionary<string, object> GetDetails()
        {
            var details = base.GetDetails();
            var properties = new Dictionary<string, object>();
            details.Add("Flags", Flags);
            details.Add("Translation", Translation);
            details.Add("Rotation", Rotation);
            details.Add("Scale", Scale);
            details.Add("Velocity", Velocity);
            foreach (var property in Properties)
            {
                properties[$"{property.RefId}"] = property.IsValid ? property.Object.GetType().Name : null;
            }
            details["BoundingBox"] = HasBoundingBox ? BoundingBox : null;
            details["Properties"] = properties;
            return details;
        }
    }
}