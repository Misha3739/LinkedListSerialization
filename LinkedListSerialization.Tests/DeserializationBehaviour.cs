using System.IO;
using NUnit.Framework;

namespace LinkedListSerialization.Tests
{
    [TestFixture]
    public class DeerializationBehaviour
    {
        static string s_file = @"C:\Serialization\TestFileDeser.txt";
        static string s_file_ser_deser = @"C:\Serialization\TestFileSerDeser.txt";

        [Test]
        public void CanDeserialize()
        {
            var listRand = new ListRand();
          
            using (FileStream fs = File.OpenRead(s_file))
            {
                Assert.DoesNotThrow(() =>
                {
                    listRand.Deserialize(fs);
                });
             
            }
        }

        [Test]
        [Ignore("Developer-Machine-binded test")]
        public void CanDeserialize_CorrectItemsCountAndValues()
        {
            var listRand = new ListRand();

            using (FileStream fs = File.OpenRead(s_file))
            {
                listRand.Deserialize(fs);

                Assert.That(listRand.Count,Is.EqualTo(50));

                for (int i = 0; i < 50; i++)
                {
                    Assert.That(listRand.Search("value"+i),Is.Not.Null);
                }


                Assert.That(listRand.Search("value2").Rand.Data,Is.EqualTo("value6"));
                Assert.That(listRand.Search("value3").Rand.Data, Is.EqualTo("value7"));
                Assert.That(listRand.Search("value49").Rand.Data, Is.EqualTo("value3"));
            }
        }


        [Test]
        public void CanSerialzize_Deserialize_CorrectItemsCountAndValues()
        {
            var listRand = new ListRand();
            for (int i = 0; i < 50; i++)
            {
                listRand.AddNewToTheEnd(new ListNode($"value{i}"));
            }
            listRand.Search("value2").Rand = listRand.Search("value6");
            listRand.Search("value3").Rand = listRand.Search("value7");
            listRand.Search("value49").Rand = listRand.Search("value3");

            using (FileStream fs = File.Create(s_file_ser_deser))
            {
                listRand.Serialize(fs);
            }

            listRand = new ListRand();

            using (FileStream fs = File.OpenRead(s_file_ser_deser))
            {
                listRand.Deserialize(fs);
            }


            Assert.That(listRand.Count, Is.EqualTo(50));

                for (int i = 0; i < 50; i++)
                {
                    Assert.That(listRand.Search("value" + i), Is.Not.Null);
                }


                Assert.That(listRand.Search("value2").Rand.Data, Is.EqualTo("value6"));
                Assert.That(listRand.Search("value3").Rand.Data, Is.EqualTo("value7"));
                Assert.That(listRand.Search("value49").Rand.Data, Is.EqualTo("value3"));
            }
        

    }
}
