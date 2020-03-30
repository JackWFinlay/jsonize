using System.Collections.Generic;
using System.Linq;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Test
{
    public static class JsonizeNodeTestResources
    {
        public static readonly JsonizeNode HtmlBodyP = new JsonizeNode()
        {
            NodeType = "Document",
            Attributes = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new JsonizeNode()
                {
                    Attributes = new Dictionary<string, object>(),
                    NodeType = "DocumentType",
                    Tag = "html",
                    Children = Enumerable.Empty<JsonizeNode>()
                },
                new JsonizeNode()
                {
                    Attributes = new Dictionary<string, object>(),
                    NodeType = "Element",
                    Tag = "html",
                    Children = new List<JsonizeNode>()
                    {
                        new JsonizeNode()
                        {
                            NodeType = "Element",
                            Tag = "head",
                            Attributes = new Dictionary<string, object>(),
                            Text = null,
                            Children = Enumerable.Empty<JsonizeNode>()
                        },
                        new JsonizeNode()
                        {
                            NodeType = "Element",
                            Tag = "body",
                            Attributes = new Dictionary<string, object>(),
                            Text = null,
                            Children = new List<JsonizeNode>()
                            {
                                new JsonizeNode()
                                {
                                    NodeType = "Element",
                                    Tag = "p",
                                    Attributes = new Dictionary<string, object>()
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