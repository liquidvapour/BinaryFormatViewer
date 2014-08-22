using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BinaryFormatViewer;

namespace SerializationSpike
{
    public abstract class BinarySerializedObjectSpec : SpecificationBase<BinaryFormatReader>
    {
        private MemoryStream stream;
        protected Node result;

        protected override BinaryFormatReader CreateSUT()
        {
            return new BinaryFormatReader();
        }

        protected override void SetUpContext()
        {
            stream = new MemoryStream();
            object test = GetObjectToSerialize();

            var bf = new BinaryFormatter();
            bf.Serialize(stream, test);

            WriteToFile(stream);

            stream.Position = 0;
        }

        protected override void OnSetUpCompleted()
        {
            if (stream != null)
            {
                stream.Close();
            }
        }

        protected override void Because()
        {
            result = sut.Read(stream);
        }

        protected abstract object GetObjectToSerialize();
        protected abstract string GetFileName();

        protected Node FindNodeWithNameIn(string name, RuntimeObjectNode objectNode)
        {
            for (int i = 0; i < objectNode.Fields.Count; i++)
            {
                if (objectNode.Fields[i].Name == name)
                {
                    return objectNode.Values[i];
                }
            }

            return null;
        }

        private void WriteToFile(MemoryStream stream)
        {
            stream.Position = 0;

            using (FileStream file = File.OpenWrite(GetFileName()))
            {
                stream.WriteTo(file);
                file.Close();
            }
        }
    }
}