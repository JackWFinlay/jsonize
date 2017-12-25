using System;
using System.Threading.Tasks;
using JackWFinlay.EscapeRoute;
using Xunit;

namespace JackWFinlay.Jsonize.Tests
{
    public class Tests
    {
        private readonly String _htmlBodyP = @"<!DOCTYPE html>
                                                <html>
                                                    <body>
                                                        <p>test</p>
                                                    </body>
                                                </html>
                                            ";
        [Fact]
        public async void ReturnHtmlBodyP()
        {
            Jsonize jsonize = Jsonize.FromHtmlString(_htmlBodyP);
            string actual = await CleanOutput(jsonize.ParseHtmlAsJsonString());
            Console.WriteLine(actual);
            string expected = "{\\\"node\\\": \\\"Document\\\",\\\"child\\\": [{\\\"node\\\": \\\"Comment\\\",\\\"text\\\": \\\"<!DOCTYPE html>\\\"},{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"html\\\",\\\"child\\\": [{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"body\\\",\\\"child\\\": [{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"p\\\",\\\"child\\\": [{\\\"node\\\": \\\"Text\\\",\\\"text\\\": \\\"test\\\"}]}]}]}]}";
            //string expected = "{ \"node\": \"Document\", \"child\": [ { \"node\": \"Comment\", \"text\": \"<!DOCTYPE html>\" }, { \"node\": \"Element\", \"tag\": \"html\", \"child\": [ { \"node\": \"Element\", \"tag\": \"body\", \"child\": [ { \"node\": \"Element\", \"tag\": \"p\", \"child\": [ { \"node\": \"Text\", \"text\": \"test\" } ] } ] } ] } ]}";
            Assert.Equal(expected, actual);
        }

        private async Task<String> CleanOutput(string rawString)
        {
            // Cleans up and creates json friendly escaped string.
            IEscapeRoute escapeRoute = new EscapeRoute.EscapeRoute();
            return await escapeRoute.ParseStringAsync(rawString);
        }
    }
}
