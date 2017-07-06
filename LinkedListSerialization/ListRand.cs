using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

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

        public ListNode Search(string nodeValue)
        {
            var node = new ListNode(nodeValue);
            return Search(node);
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

        static readonly Type s_nodeType = typeof(ListNode);

        internal string SerializeSingleItem(ListNode node)
        {

            StringBuilder sb = new StringBuilder();
          
            var properties = s_nodeType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            sb.AppendLine($"<{s_nodeType.Name}>");

            if (node != null)
            {
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(node, null);

                    if (prop.Name == nameof(ListNode.Prev) || prop.Name == nameof(ListNode.Next) ||
                        prop.Name == nameof(ListNode.Rand))
                    {
                        var refData = (value as ListNode)?.Data;
                        sb.AppendLine($"<{prop.Name}>{refData}</{prop.Name}>");
                    }
                    else
                    {
                        sb.AppendLine($"<{prop.Name}>{value}</{prop.Name}>");
                    }
                }
            }
            sb.AppendLine($"</{s_nodeType.Name}>");
            return sb.ToString();
        }

        public void Deserialize(FileStream s)
        {
            Head = Tail = null;

            StreamReader reader = new StreamReader(s);
            string content = reader.ReadToEnd();
            content = content.Replace("\r\n", "");
            var listItems = GetSubStrings(content,$"<{s_nodeType.Name}>",$"</{s_nodeType.Name}>").ToList();

            foreach (var listNode in listItems)
            {
                var node = ParseSingleNode(listNode);
                AddNewToTheEnd(node);
            }

            foreach (var listNode in listItems)
            {
                UpdateRandomReferences(listNode);
            }
        }

        private ListNode ParseSingleNode(string nodeSerialized)
        {
            var prop =
                s_nodeType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Single(x => x.Name == nameof(ListNode.Data));

            var value = GetSubString(nodeSerialized, $"<{prop.Name}>", $"</{prop.Name}>");

             return new ListNode(value);

        }

        private void UpdateRandomReferences(string nodeSerialized)
        {
            var randProperty = s_nodeType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Single(x => x.Name == nameof(ListNode.Rand));
            var dataProperty = s_nodeType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Single(x => x.Name == nameof(ListNode.Data));

            var randValue = GetSubString(nodeSerialized, $"<{randProperty.Name}>", $"</{randProperty.Name}>");
            var dataValue = GetSubString(nodeSerialized, $"<{dataProperty.Name}>", $"</{dataProperty.Name}>");


            var existedRefRandomNode = Search(randValue);
            var tempValue = Search(dataValue);
            tempValue.Rand = existedRefRandomNode;
        }

        private IEnumerable<string> GetSubStrings(string input, string start, string end)
        {
            string regex = $"{Regex.Escape(start)}(.*?){Regex.Escape(end)}";

            return Regex.Matches(input, regex, RegexOptions.Singleline)
                .Cast<Match>()
                .Select(match => match.Groups[1].Value);
        }

        private string GetSubString(string input, string start, string end)
        {
           return GetSubStrings(input,start,end).FirstOrDefault();
        }

    }
}