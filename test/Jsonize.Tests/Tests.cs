using System;
using System.Threading.Tasks;
using JackWFinlay.EscapeRoute;
using Xunit;
using static Xunit.Assert;

namespace JackWFinlay.Jsonize.Tests
{
    public class Tests
    {
        private const string HtmlBodyP = @"<!DOCTYPE html>
                                                <html>
                                                    <body>
                                                        <p>test</p>
                                                    </body>
                                                </html>
                                            ";
        
        private const string HtmlBodyFormP = @"<html><head></head><body><form></form><p> </p></body></html>";
        
        [Fact]
        public async void ReturnHtmlBodyP()
        {
            Jsonize jsonize = Jsonize.FromHtmlString(HtmlBodyP);
            string actual = await CleanOutput(jsonize.ParseHtmlAsJsonString());
            Console.WriteLine(actual);
            const string expected = "{\\\"node\\\": \\\"Document\\\",\\\"child\\\": [{\\\"node\\\": \\\"Comment\\\",\\\"text\\\": \\\"<!DOCTYPE html>\\\"},{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"html\\\",\\\"child\\\": [{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"body\\\",\\\"child\\\": [{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"p\\\",\\\"child\\\": [{\\\"node\\\": \\\"Text\\\",\\\"text\\\": \\\"test\\\"}]}]}]}]}";
            //string expected = "{ \"node\": \"Document\", \"child\": [ { \"node\": \"Comment\", \"text\": \"<!DOCTYPE html>\" }, { \"node\": \"Element\", \"tag\": \"html\", \"child\": [ { \"node\": \"Element\", \"tag\": \"body\", \"child\": [ { \"node\": \"Element\", \"tag\": \"p\", \"child\": [ { \"node\": \"Text\", \"text\": \"test\" } ] } ] } ] } ]}";
            Equal(expected, actual);
        }

        [Fact]
        public async void EmptyTextNodesHandlingInclude()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                EmptyTextNodeHandling = EmptyTextNodeHandling.Include
            };

            Jsonize jsonize = new Jsonize(HtmlBodyFormP);
            string result = jsonize.ParseHtmlAsJsonString(jsonizeConfiguration);
            string actual = await CleanOutput(result);
            const string expected =
                "{\\\"node\\\":\\\"Document\\\",\\\"child\\\":[{\\\"node\\\":\\\"Element\\\",\\\"tag\\\":\\\"html\\\",\\\"child\\\":[{\\\"node\\\":\\\"Element\\\",\\\"tag\\\":\\\"head\\\"},{\\\"node\\\":\\\"Element\\\",\\\"tag\\\":\\\"body\\\",\\\"child\\\":[{\\\"node\\\":\\\"Element\\\",\\\"tag\\\":\\\"form\\\"},{\\\"node\\\":\\\"Element\\\",\\\"tag\\\":\\\"p\\\",\\\"child\\\":[{\\\"node\\\":\\\"Text\\\"}]}]}]}]}";
                
            Assert.Equal(expected, actual);
        }
        
        private static async Task<string> CleanOutput(string rawString)
        {
            // Cleans up and creates json friendly escaped string.
            IEscapeRoute escapeRoute = new EscapeRoute.EscapeRoute();
            return await escapeRoute.ParseStringAsync(rawString);
        }
    }
}
