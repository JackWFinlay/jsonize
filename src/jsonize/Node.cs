using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace JackWFinlay.Jsonize
{
    public class Node
    {
        public string node { get; set; }
        public string tag { get; set; }
        public string text { get; set; }
        public ExpandoObject attr { get; set; }
        public List<Node> child { get; set; }
    }
}
