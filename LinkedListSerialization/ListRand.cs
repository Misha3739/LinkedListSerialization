using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace LinkedListSerialization
{
    public class ListRand
    {
        public ListNode Head { get; private set; }

        public ListNode Tail { get; private set; }

        public int Count { get; private set; }

        public void AddNewToTheEnd(ListNode node)
        {
            if (Search(node) == null)
            {
                if (Head == null)
                {
                    Head = Tail = node;
                    Head.Next = Tail;
                    Tail.Prev = Head;
                }
                else
                {
                    node.Prev = Tail;
                    Tail.Next = node;
                    Tail = node;

                    Tail.Next = null;
                    Head.Prev = null;
                }

                this.Count += 1;
            }
            else
            {
                throw new ArgumentException($"Node with value {node.Data} already exist!");
            }
        }

        public ListNode Search(ListNode node)
        {
            var tmp = Head;

            if (Count == 1)
            {
                if (tmp.Equals(node))
                    return node;
                return null;
            }

            while (tmp != null)
            {
                if (Equals(tmp, node))
                    return tmp;
                tmp = tmp.Next;
            }
            return null;
        }


        public void Serialize(FileStream s)
        {
            var tmp = Head;
            StringBuilder sb = new StringBuilder();
            while (tmp != null)
            {
                sb.Append(SerializeSingleItem(tmp));
                tmp = tmp.Next;
            }

            try
            {
                byte[] serializedData = Encoding.UTF8.GetBytes(sb.ToString());
                s.Write(serializedData,0,serializedData.Length);
            }
            catch (Exception e)
            {
               throw new Exception("Error writing serialization data to stream!",e);
            }
        }

       internal string SerializeSingleItem(ListNode node)
        {

            StringBuilder sb = new StringBuilder();
            Type nodeType = typeof(ListNode);
            var properties = nodeType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            sb.AppendLine($"<{nodeType.Name}>");

            if (node != null)
            {
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(node, null);

                    if (prop.Name == nameof(ListNode.Prev) || prop.Name == nameof(ListNode.Next) ||
                        prop.Name == nameof(ListNode.Rand))
                    {
                        var refData = (value as ListNode)?.Data;
                        sb.AppendLine($"<{prop.Name} = \"{refData}\"/>");
                    }
                    else
                    {
                        sb.AppendLine($"<{prop.Name} = \"{value}\"/>");
                    }
                }
            }
            sb.AppendLine($"</{nodeType.Name}>");
            return sb.ToString();
        }

        public void Deserialize(FileStream s)
        {

        }

        
    }
}