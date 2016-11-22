using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace PureCSharpWeb.Framework.Utilities
{
     public class HtmlBuilder
    {
        int _openedElements;
        XmlWriter _writer;

        private HtmlBuilder(XmlWriter writer)
        {
            _openedElements = 0;
            _writer = writer;
        }

        public static HtmlBuilder OpenElement(XmlWriter writer, string elementName)
        {
            var builder = new HtmlBuilder(writer);
            return builder.OpenElement(elementName);
        }

        public static HtmlBuilder OpenElement(XmlWriter writer, string elementName, string attributeName, string attributeValue)
        {
            var builder = new HtmlBuilder(writer);
            return builder.OpenElement(elementName, attributeName, attributeValue);
        }

        public static HtmlBuilder OpenElement(XmlWriter writer, string elementName, string attributeName1, string attributeValue1, string attributeName2, string attributeValue2)
        {
            var builder = new HtmlBuilder(writer);
            return builder.OpenElement(elementName, attributeName1, attributeValue1, attributeName2, attributeValue2);
        }

        public HtmlBuilder OpenElement(string elementName)
        {
            _writer.WriteStartElement(elementName);
            _openedElements--;
            return this;
        }

        public HtmlBuilder Build(Action<XmlWriter> builder)
        {
            builder(_writer);
            return this;
        }

        public HtmlBuilder Element(string elementName, string attributeName, string attributeValue)
        {
            _writer.WriteStartElement(elementName);
            _writer.WriteAttributeString(attributeName, attributeValue);
            _writer.WriteEndElement();
            return this;
        }

        public HtmlBuilder InnerText(string value)
        {
            _writer.WriteString(value);
            return this;
        }

        public HtmlBuilder OpenElement(string elementName, string attributeName, string attributeValue)
        {
            _writer.WriteStartElement(elementName);
            _writer.WriteAttributeString(attributeName, attributeValue);
            _openedElements--;
            return this;
        }

        public HtmlBuilder OpenElement(string elementName, string attributeName1, string attributeValue1, string attributeName2, string attributeValue2)
        {
            _writer.WriteStartElement(elementName);
            _writer.WriteAttributeString(attributeName1, attributeValue1);
            _writer.WriteAttributeString(attributeName2, attributeValue2);
            _openedElements--;
            return this;
        }

        public HtmlBuilder Element(string elementName, string attributeName1, string attributeValue1, string attributeName2, string attributeValue2)
        {
            _writer.WriteStartElement(elementName);
            _writer.WriteAttributeString(attributeName1, attributeValue1);
            _writer.WriteAttributeString(attributeName2, attributeValue2);
            _writer.WriteEndElement();
            return this;
        }
        public HtmlBuilder OpenElement(string elementName, string attributeName1, string attributeValue1, string attributeName2, string attributeValue2, string attributeName3, string attributeValue3)
        {
            _writer.WriteStartElement(elementName);
            _writer.WriteAttributeString(attributeName1, attributeValue1);
            _writer.WriteAttributeString(attributeName2, attributeValue2);
            _writer.WriteAttributeString(attributeName3, attributeValue3);
            _openedElements--;
            return this;
        }

        public HtmlBuilder WriteScript(string scriptBody)
        {
            _writer.WriteCData(scriptBody);
            return this;
        }

        public HtmlBuilder CloseElement()
        {
            if (_openedElements == 0)
            {
                throw new InvalidOperationException("All Elements have already been closed");
            }
            _writer.WriteEndElement();
            _openedElements--;
            return this;
        }

        public HtmlBuilder CloseAllElements()
        {
            if (_openedElements == 0)
            {
                throw new InvalidOperationException("All Elements have already been closed");
            }
            while (_openedElements > 0)
            {
                _writer.WriteEndElement();
                _openedElements--;
            }
            return this;
        }

        public void Complete()
        {
            _writer.Flush();
            if (_openedElements > 0)
            {
                throw new InvalidOperationException("Not all elements have been close");
            }
        }
    }
}
