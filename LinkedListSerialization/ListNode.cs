namespace LinkedListSerialization
{
   public class ListNode
    {
        public ListNode Prev { get; set; }

        public ListNode Next { get; set; }

        public ListNode Rand { get; set; } 

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
