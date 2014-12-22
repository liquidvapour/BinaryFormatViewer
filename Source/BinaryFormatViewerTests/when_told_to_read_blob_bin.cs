using System.IO;
using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    [Ignore("the blob.bin has gone AWOL unignore once it returns.")]
    public class when_told_to_read_blob_bin : SpecificationBase<BinaryFormatReader>
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


            ReadFile(stream);

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

        protected Node FindNodeWithNameIn(string name, RuntimeObjectNode objectNode)
        {
            for (int i = 0; i < objectNode.Fields.Count; i++)
            {
                if (objectNode.Fields[i].Name == name)
                {
                    return objectNode.Children[i];
                }
            }

            return null;
        }

        private void ReadFile(MemoryStream stream)
        {
            stream.Position = 0;

            using (FileStream file = File.OpenRead(GetFileName()))
            {
                var buff = new byte[file.Length];
                file.Read(buff, 0, buff.Length);
                file.Close();
                stream.Write(buff, 0, buff.Length);
            }

            stream.Position = 0;
        }

        protected string GetFileName()
        {
            return "blob.bin";
        }

        [Test]
        public void ShouldWork()
        {
            Assert.IsNotNull(result);
        }
    }
}