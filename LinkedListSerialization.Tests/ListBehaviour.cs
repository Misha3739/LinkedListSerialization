using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LinkedListSerialization.Tests
{
    [TestFixture]
    public class ListBehaviour
    {
        [Test]
        public void CanAddNewToEmptyList()
        {
            var listRand = new ListRand();
            var node = new ListNode("value1");
            listRand.AddNewToTheEnd(node);
            Assert.That(listRand.Count,Is.EqualTo(1));
            Assert.That(listRand.Head, Is.Not.Null);
            Assert.That(listRand.Head, Is.EqualTo(listRand.Tail));
        }

        [Test]
        public void CanAddTwoItems()
        {
            var listRand = new ListRand();
            var node = new ListNode("value1");
            listRand.AddNewToTheEnd(node);

            var node2 = new ListNode("value2");
            listRand.AddNewToTheEnd(node2);

            Assert.That(listRand.Count, Is.EqualTo(2));
            Assert.That(listRand.Head, Is.EqualTo(node));
            Assert.That(listRand.Tail, Is.EqualTo(node2));

            Assert.That(listRand.Head.Next, Is.EqualTo(listRand.Tail));
            Assert.That(listRand.Tail.Prev, Is.EqualTo(listRand.Head));
        }

        [Test]
        public void CanNotAddDuplicateItems()
        {
            var listRand = new ListRand();
            var node = new ListNode("value1");
            listRand.AddNewToTheEnd(node);

            var node2 = new ListNode("value1");

            Assert.Throws<ArgumentException>(() =>
            {
                listRand.AddNewToTheEnd(node2);
            });
           

        }

        [TestCase("value1")]
        [TestCase("value2")]
        [TestCase("value25")]
        [TestCase("value48")]
        [TestCase("value49")]
        public void CanNotAddDuplicateItems_MultiplyListCount(string value)
        {
            var listRand = new ListRand();
            for (int i = 0; i < 50; i++)
            {
                listRand.AddNewToTheEnd(new ListNode($"value{i}"));
            }

          

            var node2 = new ListNode(value);

            Assert.Throws<ArgumentException>(() =>
            {
                listRand.AddNewToTheEnd(node2);
            });


        }
    }

    
}
