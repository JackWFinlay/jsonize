using System.Collections.Generic;
using System.Linq;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Test
{
    public class JsonizeNodeTestResources
    {
        public JsonizeNode HtmlBodyP = new JsonizeNode()
        {
            Node = "document",
            Tag = "html",
            Attributes = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new JsonizeNode()
            }
        };
    }
}