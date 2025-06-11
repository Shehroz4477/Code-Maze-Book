using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Entities.LinkModels;

namespace Entities.Models;

public class Entity
{
    private void WirteLinksToXml(string key, object value, XmlWriter writer)
    {
        writer.WriteStartElement(key);

        if(value.GetType() == typeof(List<Link>))
        {
            foreach (var val in value as List<Link>)
            {
                writer.WriteStartElement(nameof(Link));
                WirteLinksToXml(nameof(val.Href),val.Href,writer);
                WirteLinksToXml(nameof(val.Rel),val.Rel,writer);
                WirteLinksToXml(nameof(val.Method),val.Method,writer);
                writer.WriteEndElement();
            }
        }
        else
        {
            writer.WriteString(value.ToString());
        }

        writer.WriteEndElement();
    }
}
