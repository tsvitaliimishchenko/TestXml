using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXml.Models
{
    public class TreeNode
    {
        public string Name { get; set; }
        public TreeNode? Parent { get; set; }
        //Data structure of children can be improved depending on extended information about input data 
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();

        public TreeNode(string name)
        {
            Name = name;
        }
        public TreeNode DeepCopy()
        {
            TreeNode newNode = new TreeNode(Name);
            foreach (var child in Children)
            {
                TreeNode newChild = child.DeepCopy();
                newNode.Children.Add(newChild);
                newChild.Parent = newNode;
            }
            return newNode;
        }

    }
}
