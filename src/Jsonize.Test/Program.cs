using System;
using System.IO;
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
            File.WriteAllText(@"C:\Users\Public\WriteText.txt", "");
            new AutoRun(Assembly.GetEntryAssembly()).Execute(
                args,
                new ExtendedTextWrapper(Console.Out),
                TextReader.Null);

            Console.ReadLine();
        }

        [Test]
        public async Task TestEmptyTextNodesHandlingInlcude()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                EmptyTextNodeHandling = EmptyTextNodeHandling.Include
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(@"C:\Users\Public\WriteText.txt", ("\r\nTestEmptyTextNodesHandlingInclude\r\n" + result));
        }

        [Test]
        public async Task TestEmptyTextNodesHandlingIgnore()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                EmptyTextNodeHandling = EmptyTextNodeHandling.Ignore
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(@"C:\Users\Public\WriteText.txt", ("\r\nTestEmptyTextNodesHandlingIgnore\r\n" + result));
        }

        [Test]
        public async Task TestNullValueHandlingInclude()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                NullValueHandling = NullValueHandling.Include
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(@"C:\Users\Public\WriteText.txt", ("\r\nTestNullValueHandlingInclude\r\n" + result));
        }

        [Test]
        public async Task TestNullValueHandlingIgnore()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                NullValueHandling= NullValueHandling.Ignore
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(@"C:\Users\Public\WriteText.txt", ("\r\nTestNullValueHandlingIgnore\r\n" + result));
        }

        [Test]
        public async Task TestTextTrimHandlingInclude()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                TextTrimHandling = TextTrimHandling.Include
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(@"C:\Users\Public\WriteText.txt", ("\r\nTestTextTrimHandlingInclude\r\n" + result));
        }

        [Test]
        public async Task TestTextTrimHandlingTrim()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                TextTrimHandling = TextTrimHandling.Trim
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(@"C:\Users\Public\WriteText.txt", ("\r\nTestTextTrimHandlingIgnore\r\n" + result));
        }

        [Test]
        public async Task TestClassAttributeHandlingArray()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                ClassAttributeHandling = ClassAttributeHandling.Array
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(@"C:\Users\Public\WriteText.txt", ("\r\nTestClassAttributeHandlingArray\r\n" + result));
        }

        [Test]
        public async Task TestClassAttributeHandlingString()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                ClassAttributeHandling = ClassAttributeHandling.String
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(@"C:\Users\Public\WriteText.txt", ("\r\nTestClassAttributeHandlingString\r\n" + result));
        }

        private static async Task<string> TestJsonizeAsString(JsonizeConfiguration jsonizeConfiguration)
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                string url = @"http://jackfinlay.com";

                HttpResponseMessage response = await client.GetAsync(url);

                string html = await response.Content.ReadAsStringAsync();
                Jsonize jsonize = new Jsonize(html);

                return jsonize.ParseHtmlAsJsonString(jsonizeConfiguration);
            }
        }
    }
}
