namespace JackWFinlay.Jsonize.Test
{
    public static class StringResources
    {
        public const string HtmlBodyP = @"<!DOCTYPE html><html><head></head><body><p class=""test-class"">test</p></body></html>";
        public const string HtmlBodyPResult = "{\"node\":\"Document\",\"tag\":null,\"text\":null,\"attr\":{},\"children\":[{\"node\":\"DocumentType\",\"tag\":\"html\",\"text\":null,\"attr\":{},\"children\":[]},{\"node\":\"Element\",\"tag\":\"html\",\"text\":null,\"attr\":{},\"children\":[{\"node\":\"Element\",\"tag\":\"head\",\"text\":null,\"attr\":{},\"children\":[]},{\"node\":\"Element\",\"tag\":\"body\",\"text\":null,\"attr\":{},\"children\":[{\"node\":\"Element\",\"tag\":\"p\",\"text\":\"test\",\"attr\":{\"class\":[\"test-class\"]},\"children\":[]}]}]}]}";
    }
}