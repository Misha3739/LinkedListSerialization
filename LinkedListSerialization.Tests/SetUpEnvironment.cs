using System.IO;
using NUnit.Framework;

namespace LinkedListSerialization.Tests
{
    [SetUpFixture]
    public class SetUpEnvironment
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            if (!Directory.Exists(@"C:\Serialization"))
                Directory.CreateDirectory(@"C:\Serialization");
                using (File.Create(@"C:\Serialization\TestFile.txt"))
                {

                }
                using (File.Create(@"C:\Serialization\TestFileDeser.txt"))
                {

                }
                using (File.Create(@"C:\Serialization\TestFileSerDeser.txt"))
                {

                }
            
        }
    }
}
