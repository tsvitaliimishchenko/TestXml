using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXml.Interfaces;
using TestXml.Models;

namespace TestXml
{
    public class TreeNodesTranslator: ITranslator<List<InputNode>, TreeNode>
    {
        public TreeNode Translate(List<InputNode> inputArray)
        {
            if (inputArray == null || inputArray.Count == 0)
                throw new ArgumentException("input Array cannot be empty");

            var orderedInputList = inputArray.OrderBy(o => o.tagLevel).ToList();

            if (orderedInputList.First().tagLevel != 0)
                throw new ArgumentException("Incorrect array, does not contain root node");

            TreeNode root = new TreeNode(orderedInputList.First().tagname);

            List<List<TreeNode>> levelLayers = new List<List<TreeNode>> { new List<TreeNode> { root } };

            List<RepeatableNode> repeatableNodes = new List<RepeatableNode>();

            //Could be splitted to threads. But need to refactor the code to gain the accurate splitting logic to subtrees 
            foreach (var inputNode in orderedInputList.Skip(1))
            {
                //If layer is skipped. 
                if (levelLayers.Count - inputNode.tagLevel < 0)
                    throw new ArgumentException($"Incorrect input array. Missing tags level. Levels with gap between: {levelLayers.Count - 1} and {inputNode.tagLevel}");
                // If current tag level is greater than existing layers then add one
                if (levelLayers.Count == inputNode.tagLevel)
                    levelLayers.Add(new List<TreeNode>());

                var treeNode = new TreeNode(inputNode.tagname);

                levelLayers[inputNode.tagLevel].Add(treeNode);

                var parentNode = levelLayers[inputNode.tagLevel - 1].Where(x => x.Name == inputNode.parenttag).Single();

                if (parentNode == null)
                    throw new ArgumentException("Incorrect inputArray. Can't find parent node");

                parentNode.Children.Add(treeNode);
                treeNode.Parent = parentNode;

                if (inputNode?.isrepeatable == true)
                {
                    if (inputNode.count == null)
                        throw new ArgumentException("Incorrect array, count not provided for repeatable");

                    var repeatableCount = Int32.Parse(inputNode.count);
                    if (repeatableCount > 0)
                    {
                        repeatableNodes.Add(new RepeatableNode(treeNode, repeatableCount, inputNode.tagLevel));
                    }
                }
            }

            var orderedRepeatableNodes = repeatableNodes.OrderByDescending(o => o.Level).ToList();

            foreach (var repeatableNode in orderedRepeatableNodes)
            {
                if (repeatableNode.SimpleNode.Parent == null)
                    continue;
                for (var i = 0; i < repeatableNode.RepeatCount; i++)
                {
                    var clonedNode = repeatableNode.SimpleNode.DeepCopy();
                    clonedNode.Parent = repeatableNode.SimpleNode.Parent;
                    repeatableNode.SimpleNode.Parent.Children.Add(clonedNode);
                }
            }
            return root;

        }
    }
    public class RepeatableNode
    {
        public RepeatableNode(TreeNode node, int repeatCount, int level)
        {
            SimpleNode = node;
            RepeatCount = repeatCount;
            Level = level;
        }

        public TreeNode SimpleNode { get; set; }
        public int RepeatCount { get; set; }
        public int Level { get; set; }
    }
}
