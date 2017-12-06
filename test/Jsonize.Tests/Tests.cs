using System;
using JackWFinlay.Jsonize;
using Xunit;

namespace JackWFinlay.Jsonize.Tests
{
    public class Tests
    {
        private readonly String _htmlBodyP = @"<!DOCTYPE html>
                                                <html>
                                                    <body>
                                                        <p class=""test"">test<p>
                                                    </body>
                                                </html>
                                            ";
        [Fact]
        public void returnHtmlBodyP()
        {
            Jsonize jsonize = Jsonize.FromHtmlString(_htmlBodyP);
            string output = jsonize.ParseHtmlAsJsonString();
            Console.WriteLine(output);
        }
    }
}
