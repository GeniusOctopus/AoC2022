namespace Day7.Models
{
    internal class Node
    {
        public Node(Node parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public Node Parent { get; set; }

        public string Name { get; set; }
        public long Size { get; set; }
        public bool IsDirectory { get; set; }

        public List<Node> DirContent { get; set; }
    }
}
