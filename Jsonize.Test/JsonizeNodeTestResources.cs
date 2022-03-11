using System.Collections.Generic;
using System.Linq;
using Jsonize.Abstractions.Models;

namespace Jsonize.Test
{
    public static class JsonizeNodeTestResources
    {
        public static readonly JsonizeNode HtmlBodyP = new ()
        {
            NodeType = "Document",
            Attr = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new JsonizeNode()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "DocumentType",
                    Tag = "html",
                    Children = Enumerable.Empty<JsonizeNode>()
                },
                new JsonizeNode()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "Element",
                    Tag = "html",
                    Children = new List<JsonizeNode>()
                    {
                        new JsonizeNode()
                        {
                            NodeType = "Element",
                            Tag = "head",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = Enumerable.Empty<JsonizeNode>()
                        },
                        new JsonizeNode()
                        {
                            NodeType = "Element",
                            Tag = "body",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = new List<JsonizeNode>()
                            {
                                new JsonizeNode()
                                {
                                    NodeType = "Element",
                                    Tag = "p",
                                    Attr = new Dictionary<string, object>()
                                    {
                                        {"class", new List<string>(){"test-class"}}
                                    },
                                    Text = "test",
                                    Children = Enumerable.Empty<JsonizeNode>()
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}