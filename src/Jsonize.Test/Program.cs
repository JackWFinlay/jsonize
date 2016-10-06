using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using JackWFinlay.Jsonize;
using System.Threading.Tasks;
using NUnit.Common;
using NUnit.Framework;
using NUnitLite;

namespace Jsonize_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new AutoRun(Assembly.GetEntryAssembly()).Execute(
                args,
                new ExtendedTextWrapper(Console.Out),
                TextReader.Null);

            Console.ReadLine();
        }

        [Test]
        public async Task Test()
        {
            string result = await TestJsonizeAsString();
            Console.WriteLine(result);
            File.WriteAllText(@"C:\Users\Public\WriteText.txt", result);
        }

        private static async Task<string> TestJsonizeAsString()
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                string url = @"http://jackfinlay.com";

                HttpResponseMessage response = await client.GetAsync(url);

                string html = await response.Content.ReadAsStringAsync();
                //html = System.IO.File.ReadAllText(@"C:\Users\Public\file.html");
                Jsonize jsonize = new Jsonize(html);
                jsonize.ShowEmptyTextNodes(false);

                return jsonize.ParseHtmlAsJsonString();
            }
        }
    }
}
