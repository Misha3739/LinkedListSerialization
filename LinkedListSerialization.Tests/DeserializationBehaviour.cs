using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LinkedListSerialization.Tests
{
    [TestFixture]
    public class DeerializationBehaviour
    {
        static string s_file = @"C:\Serialization\TestFile.txt";

        [OneTimeSetUp]
        public void SetUp()
        {
            try
            {
                using (FileStream fs = File.Create(s_file))
                {
                    
                }
            }
            catch
            {
            }
        }

        [Test]
        static void CanDeserialize()
        {
            var listRand = new ListRand();
          
            using (FileStream fs = File.OpenRead(s_file))
            {
                listRand.Deserialize(fs);
            }
        }

       
    }
}
