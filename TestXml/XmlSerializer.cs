using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestXml.Interfaces;
using TestXml.Models;

namespace TestXml
{
    public class XmlSerializer: ISerializer<List<InputNode>>
    {
        private readonly ITranslator<List<InputNode>, TreeNode> _treeNodesTranslator;
        public XmlSerializer(ITranslator<List<InputNode>, TreeNode> treeNodesTranslator)
        {
            _treeNodesTranslator = treeNodesTranslator;
        }
        public string Serialize(List<InputNode> inputArray)
        {
            TreeNode treeRootNode = _treeNodesTranslator.Translate(inputArray);

            XDocument xmlDocument = XDocumentConverter.Convert(treeRootNode);

            return xmlDocument.ToString();
        }
    }

    public static class XDocumentConverter
    {
        public static XDocument Convert(TreeNode node)
        {
            XElement rootXmlNode = new XElement(node.Name);

            SerializeNodeToXml(rootXmlNode, node);

            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), rootXmlNode);

            return doc;
        }
        private static void SerializeNodeToXml(XElement xmlNode, TreeNode node)
        {
            foreach (var child in node.Children)
            {
                XElement xmlChild = new XElement(child.Name);
                xmlNode.Add(xmlChild);
                SerializeNodeToXml(xmlChild, child);
            }
        }
    }
}
