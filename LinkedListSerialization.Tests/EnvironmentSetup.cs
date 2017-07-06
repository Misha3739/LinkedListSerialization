using System.IO;
using NUnit.Framework;

namespace LinkedListSerialization.Tests
{
    [SetUpFixture]
    public class EnvironmentSetup
    {
     
        public void SetUp()
        {
            if (!Directory.Exists(@"C:\Serialization"))
                Directory.CreateDirectory(@"C:\Serialization");
        }
    }
}
