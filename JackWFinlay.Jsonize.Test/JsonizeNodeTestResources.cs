using System.Collections.Generic;
using System.Linq;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Test
{
    public static class JsonizeNodeTestResources
    {
        public static JsonizeNode HtmlBodyP = new JsonizeNode()
        {
            Node = "document",
            Tag = "html",
            Attributes = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new JsonizeNode()
                {
                    Node = "element",
                    Tag = "head",
                    Attributes = new Dictionary<string, object>(),
                    Text = null,
                    Children = Enumerable.Empty<JsonizeNode>()
                },
                new JsonizeNode()
                {
                    Node = "element",
                    Tag = "body",
                    Attributes = new Dictionary<string, object>(),
                    Text = "test",
                    Children = new List<JsonizeNode>()
                    {
                        new JsonizeNode()
                        {
                            Node = "element",
                            Tag = "p",
                            Attributes = new Dictionary<string, object>(),
                            Text = "test",
                            Children = Enumerable.Empty<JsonizeNode>()
                        }
                    }
                }
            }
        };
    }
}