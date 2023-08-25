using System.Diagnostics.Metrics;
using TestXml;
using TestXml.Interfaces;
using TestXml.Models;

namespace XmlTest
{
    public class XmlSerializerTest
    {
        private ISerializer<List<InputNode>> _serializer;

        public XmlSerializerTest()
        { 
            _serializer = new XmlSerializer(new TreeNodesTranslator()); 
        }
        private static IEnumerable<object[]> TryCreate_GetInputArrays()
        {
            yield return new object[]
            {
                new List<InputNode> {
                new InputNode{tagname="USER", tagLevel=0, parenttag=""},
                new InputNode{tagname="FIRSTNAME", tagLevel=1, parenttag="USER"},
                new InputNode{tagname="LASTNAME", tagLevel=1, parenttag="USER"},
                new InputNode{tagname="PAYMENT", tagLevel=1, isrepeatable = true, count="1", parenttag="USER"},
                new InputNode{tagname="PAYMENTCODE", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTAMOUNT", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="HOMEADDRESS",tagLevel=1, parenttag="USER"}
                },
                "<USER>\r\n  <FIRSTNAME />\r\n  <LASTNAME />\r\n  <PAYMENT>\r\n    <PAYMENTCODE />\r\n    <PAYMENTAMOUNT />\r\n  </PAYMENT>\r\n  <HOMEADDRESS />\r\n  <PAYMENT>\r\n    <PAYMENTCODE />\r\n    <PAYMENTAMOUNT />\r\n  </PAYMENT>\r\n</USER>"
            };
            yield return new object[]
            {
                new List<InputNode> {
                new InputNode{tagname="USER", tagLevel=0, parenttag=""},
                new InputNode{tagname="FIRSTNAME", tagLevel=1, parenttag="USER"},
                new InputNode{tagname="LASTNAME", tagLevel=1, parenttag="USER"},
                new InputNode{tagname="PAYMENT", tagLevel=1, isrepeatable = true, count="1", parenttag="USER"},
                new InputNode{tagname="PAYMENTCODE", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTAMOUNT", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTAMOUNT", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTAMOUNT", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTNEW", tagLevel=1, isrepeatable = true, count="3", parenttag="USER"},
                new InputNode{tagname="HOMEADDRESS",tagLevel=1, parenttag="USER"}
                },
                "<USER>\r\n  <FIRSTNAME />\r\n  <LASTNAME />\r\n  <PAYMENT>\r\n    <PAYMENTCODE />\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n  </PAYMENT>\r\n  <PAYMENTNEW />\r\n  <HOMEADDRESS />\r\n  <PAYMENT>\r\n    <PAYMENTCODE />\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n  </PAYMENT>\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n</USER>"
            };
            yield return new object[]
            {
                new List<InputNode> {
                new InputNode{tagname="USER", tagLevel=0, parenttag=""},
                new InputNode{tagname="FIRSTNAME", tagLevel=1, parenttag="USER"},
                new InputNode{tagname="LASTNAME", tagLevel=1, parenttag="USER"},
                new InputNode{tagname="PAYMENT", tagLevel=1, isrepeatable = true, count="2", parenttag="USER"},
                new InputNode{tagname="PAYMENTCODE", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTAMOUNT", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTAMOUNTCURRENCY", tagLevel=3, parenttag="PAYMENTCODE"},
                new InputNode{tagname="PAYMENTAMOUNT", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTAMOUNT", tagLevel=2, parenttag="PAYMENT"},
                new InputNode{tagname="PAYMENTNEW", tagLevel=1, isrepeatable = true, count="10", parenttag="USER"},
                new InputNode{tagname="HOMEADDRESS",tagLevel=1, parenttag="USER"}
                },
                "<USER>\r\n  <FIRSTNAME />\r\n  <LASTNAME />\r\n  <PAYMENT>\r\n    <PAYMENTCODE>\r\n      <PAYMENTAMOUNTCURRENCY />\r\n    </PAYMENTCODE>\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n  </PAYMENT>\r\n  <PAYMENTNEW />\r\n  <HOMEADDRESS />\r\n  <PAYMENT>\r\n    <PAYMENTCODE>\r\n      <PAYMENTAMOUNTCURRENCY />\r\n    </PAYMENTCODE>\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n  </PAYMENT>\r\n  <PAYMENT>\r\n    <PAYMENTCODE>\r\n      <PAYMENTAMOUNTCURRENCY />\r\n    </PAYMENTCODE>\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n    <PAYMENTAMOUNT />\r\n  </PAYMENT>\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n  <PAYMENTNEW />\r\n</USER>"
            };
        }

        [Theory]
        [MemberData(nameof(TryCreate_GetInputArrays))]
        public void CreateXml_CorrectArray(List<InputNode> inputArray, string outputXml)
        {
            Assert.Equal(outputXml, _serializer.Serialize(inputArray));
        }
    }
}