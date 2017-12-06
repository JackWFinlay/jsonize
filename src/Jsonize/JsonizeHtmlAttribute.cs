namespace JackWFinlay.Jsonize
{
    public class JsonizeHtmlAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public JsonizeHtmlAttribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}