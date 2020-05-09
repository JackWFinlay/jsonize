namespace JackWFinlay.Jsonize.Test
{
    public static class StringResources
    {
        public const string HtmlBodyP = @"<!DOCTYPE html><html><head></head><body><p class=""test-class"">test</p></body></html>";
        public const string HtmlBodyPResult = "{\"nodeType\":\"Document\",\"tag\":null,\"text\":null,\"attr\":{},\"children\":[{\"nodeType\":\"DocumentType\",\"tag\":\"html\",\"text\":null,\"attr\":{},\"children\":[]},{\"nodeType\":\"Element\",\"tag\":\"html\",\"text\":null,\"attr\":{},\"children\":[{\"nodeType\":\"Element\",\"tag\":\"head\",\"text\":null,\"attr\":{},\"children\":[]},{\"nodeType\":\"Element\",\"tag\":\"body\",\"text\":null,\"attr\":{},\"children\":[{\"nodeType\":\"Element\",\"tag\":\"p\",\"text\":\"test\",\"attr\":{\"class\":[\"test-class\"]},\"children\":[]}]}]}]}";
        public const string DocoHtmlExample = "<!DOCTYPE html><html><head><title>Jsonize</title></head><body><div id=\"parent\" class=\"parent-div\"><div id=\"child1\" class=\"child-div child1\">Some Text</div><div id=\"child2\" class=\"child-div child2\">Some Text</div><div id=\"child3\" class=\"child-div child3\">Some Text</div></div></body></html>";
    }
}