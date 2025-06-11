using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels;

internal class LinkResourceBase
{
    public LinkResourceBase()
    {
        //TODO    
    }
       
    public List<Link> Links { get; set; } = new List<Link>();
}
