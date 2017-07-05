using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListSerialization
{
   public class ListNode
    {
        public ListNode Prev { get; set; }

        public ListNode Next { get; set; }

        public ListNode Rand { get; set; } // произвольный элемент внутри списка

        public string Data { get; }

        public ListNode(string data)
        {
            Data = data;
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ListNode))
                return false;
            return Data.Equals(((ListNode) obj).Data);
        }

       
    }
}
