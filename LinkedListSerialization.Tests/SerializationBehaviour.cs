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
    public class SerializationBehaviour
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
        public void CanSerializeNullItem()
        {
            ListNode node = null;
            ListRand rnd = new ListRand();
            string result = "";
            Assert.DoesNotThrow(() =>
            {
                result = rnd.SerializeSingleItem(node);
            });

            Assert.That(result.Contains("<ListNode>"));
            Assert.That(result.Contains("</ListNode>"));

        }

        [Test]
        public void CanSerializeSingleUnboundItem()
        {
            ListNode node = new ListNode("Value1");
            ListRand rnd = new ListRand();
            string result = "";
            Assert.DoesNotThrow(() =>
            {
                result = rnd.SerializeSingleItem(node);
            });

            Assert.That(result.Contains("<ListNode>"));

            Assert.That(result.Contains("<Prev></Prev>"));
            Assert.That(result.Contains("<Next></Next>"));
            Assert.That(result.Contains("<Rand></Rand>"));

            Assert.That(result.Contains("</ListNode>"));

        }

        [Test]
        public void CanSerializeSingleItem_WithBoundItems()
        {
            ListNode node = new ListNode("Value1");

            ListNode nodePrev = new ListNode("prev");
            ListNode nodeNext = new ListNode("next");
            ListNode nodeRand = new ListNode("rand");
            node.Next = nodeNext;
            node.Rand = nodeRand;
            node.Prev = nodePrev;

            ListRand rnd = new ListRand();
            string result = "";
            Assert.DoesNotThrow(() =>
            {
                result = rnd.SerializeSingleItem(node);
            });

            Assert.That(result.Contains("<ListNode>"));

            Assert.That(result.Contains("<Prev>prev</Prev>"));
            Assert.That(result.Contains("<Next>next</Next>"));
            Assert.That(result.Contains("<Rand>rand</Rand>"));

            Assert.That(result.Contains("</ListNode>"));

        }

        [Test]
        public void CanWriteSerializedDataToStream()
        {
            var listRand = new ListRand();
            for (int i = 0; i < 50; i++)
            {
                listRand.AddNewToTheEnd(new ListNode($"value{i}"));
            }


            Assert.DoesNotThrow(() =>
            {
          
                using (FileStream fs = File.OpenWrite(s_file))
                {
                    listRand.Serialize(fs);
                }
              
            });

        }
    }
}
