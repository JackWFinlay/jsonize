using System.Collections.Generic;
using System.Linq;
using JackWFinlay.Jsonize.Abstractions.Configuration;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Test
{
    public static class JsonizeNodeTestResources
    {
        public static readonly JsonizeNode HtmlBodyP = new JsonizeNode()
        {
            Node = "Document",
            Attributes = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new JsonizeNode()
                {
                    Attributes = new Dictionary<string, object>(),
                    Node = "DocumentType",
                    Tag = "html",
                    Children = Enumerable.Empty<JsonizeNode>()
                },
                new JsonizeNode()
                {
                    Attributes = new Dictionary<string, object>(),
                    Node = "Element",
                    Tag = "html",
                    Children = new List<JsonizeNode>()
                    {
                        new JsonizeNode()
                        {
                            Node = "Element",
                            Tag = "head",
                            Attributes = new Dictionary<string, object>(),
                            Text = null,
                            Children = Enumerable.Empty<JsonizeNode>()
                        },
                        new JsonizeNode()
                        {
                            Node = "Element",
                            Tag = "body",
                            Attributes = new Dictionary<string, object>(),
                            Text = null,
                            Children = new List<JsonizeNode>()
                            {
                                new JsonizeNode()
                                {
                                    Node = "Element",
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