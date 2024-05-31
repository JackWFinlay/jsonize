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
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "DocumentType",
                    Tag = "html",
                    Children = []
                },
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "Element",
                    Tag = "html",
                    Children = new List<JsonizeNode>()
                    {
                        new()
                        {
                            NodeType = "Element",
                            Tag = "head",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = []
                        },
                        new()
                        {
                            NodeType = "Element",
                            Tag = "body",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = new List<JsonizeNode>()
                            {
                                new()
                                {
                                    NodeType = "Element",
                                    Tag = "p",
                                    Attr = new Dictionary<string, object>()
                                    {
                                        {"class", new List<string>(){"test-class"}}
                                    },
                                    Text = "test",
                                    Children = []
                                }
                            }
                        }
                    }
                }
            }
        };
        
        public static readonly JsonizeNode HtmlBodyPEnhanced = new ()
        {
            NodeType = "Document",
            Attr = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "DocumentType",
                    Tag = "html",
                    Children = []
                },
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "Element",
                    Tag = "html",
                    Children = new List<JsonizeNode>()
                    {
                        new()
                        {
                            NodeType = "Element",
                            Tag = "head",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = []
                        },
                        new()
                        {
                            NodeType = "Element",
                            Tag = "body",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = new List<JsonizeNode>()
                            {
                                new()
                                {
                                    NodeType = "Element",
                                    Tag = "p",
                                    Attr = new Dictionary<string, object>()
                                    {
                                        {"class", new List<string>(){"test-class"}}
                                    },
                                    Text = null,
                                    Children = new List<JsonizeNode>()
                                    {
                                        new()
                                        {
                                            NodeType = "Text",
                                            Tag = "#text",
                                            Attr = new Dictionary<string, object>(),
                                            Text = "test",
                                            Children = []
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
        
        public static readonly JsonizeNode HtmlBodyPSpan = new ()
        {
            NodeType = "Document",
            Attr = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "DocumentType",
                    Tag = "html",
                    Children = []
                },
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "Element",
                    Tag = "html",
                    Children = new List<JsonizeNode>()
                    {
                        new()
                        {
                            NodeType = "Element",
                            Tag = "head",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = []
                        },
                        new()
                        {
                            NodeType = "Element",
                            Tag = "body",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = new List<JsonizeNode>()
                            {
                                new()
                                {
                                    NodeType = "Element",
                                    Tag = "p",
                                    Attr = new Dictionary<string, object>()
                                    {
                                        {"class", new List<string>(){"test-class"}}
                                    },
                                    Text = "test",
                                    Children = new JsonizeNode[]
                                    {
                                        new()
                                        {
                                            NodeType = "Element",
                                            Tag = "span",
                                            Attr = new Dictionary<string, object>(),
                                            Text = "span",
                                            Children = []
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
        
        public static readonly JsonizeNode HtmlBodyPSpanEnhanced = new ()
        {
            NodeType = "Document",
            Attr = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "DocumentType",
                    Tag = "html",
                    Children = []
                },
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "Element",
                    Tag = "html",
                    Children = new List<JsonizeNode>()
                    {
                        new()
                        {
                            NodeType = "Element",
                            Tag = "head",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = []
                        },
                        new()
                        {
                            NodeType = "Element",
                            Tag = "body",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = new List<JsonizeNode>()
                            {
                                new()
                                {
                                    NodeType = "Element",
                                    Tag = "p",
                                    Attr = new Dictionary<string, object>()
                                    {
                                        {"class", new List<string>(){"test-class"}}
                                    },
                                    Text = null,
                                    Children = new List<JsonizeNode>()
                                    {
                                        new()
                                        {
                                            NodeType = "Text",
                                            Tag = "#text",
                                            Attr = new Dictionary<string, object>(),
                                            Text = "test",
                                            Children = []
                                        },
                                        new()
                                        {
                                            NodeType = "Element",
                                            Tag = "span",
                                            Attr = new Dictionary<string, object>(),
                                            Text = null,
                                            Children = new List<JsonizeNode>()
                                            {
                                                new()
                                                {
                                                    NodeType = "Text",
                                                    Tag = "#text",
                                                    Attr = new Dictionary<string, object>(),
                                                    Text = "span",
                                                    Children = []
                                                }
                                            }
                                        },
                                        new()
                                        {
                                            NodeType = "Text",
                                            Tag = "#text",
                                            Attr = new Dictionary<string, object>(),
                                            Text = "test",
                                            Children = []
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
        
        public static readonly JsonizeNode HtmlBodyH1SpanEnhanced = new ()
        {
            NodeType = "Document",
            Attr = new Dictionary<string,object>(),
            Text = null,
            Children = new List<JsonizeNode>()
            {
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "DocumentType",
                    Tag = "html",
                    Children = []
                },
                new()
                {
                    Attr = new Dictionary<string, object>(),
                    NodeType = "Element",
                    Tag = "html",
                    Children = new List<JsonizeNode>()
                    {
                        new()
                        {
                            NodeType = "Element",
                            Tag = "head",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = []
                        },
                        new()
                        {
                            NodeType = "Element",
                            Tag = "body",
                            Attr = new Dictionary<string, object>(),
                            Text = null,
                            Children = new List<JsonizeNode>()
                            {
                                new()
                                {
                                    NodeType = "Element",
                                    Tag = "h1",
                                    Attr = new Dictionary<string, object>()
                                    {
                                        {"class", new List<string>(){"test-class"}}
                                    },
                                    Text = null,
                                    Children = new List<JsonizeNode>()
                                    {
                                        new()
                                        {
                                            NodeType = "Text",
                                            Tag = "#text",
                                            Attr = new Dictionary<string, object>(),
                                            Text = "test",
                                            Children = []
                                        },
                                        new()
                                        {
                                            NodeType = "Element",
                                            Tag = "span",
                                            Attr = new Dictionary<string, object>(),
                                            Text = null,
                                            Children = new List<JsonizeNode>()
                                            {
                                                new()
                                                {
                                                    NodeType = "Text",
                                                    Tag = "#text",
                                                    Attr = new Dictionary<string, object>(),
                                                    Text = "span",
                                                    Children = []
                                                }
                                            }
                                        },
                                        new()
                                        {
                                            NodeType = "Text",
                                            Tag = "#text",
                                            Attr = new Dictionary<string, object>(),
                                            Text = "test",
                                            Children = []
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}