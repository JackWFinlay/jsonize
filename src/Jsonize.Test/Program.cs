using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using JackWFinlay.Jsonize;
using System.Threading.Tasks;
using NUnit.Common;
using NUnit.Framework;
using NUnitLite;
// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable once CheckNamespace
namespace Jsonize_Test
{
    public class Program
    {
        private const string TEXTFILE = "WriteText.txt";
        private static string _html = null;

        public static void Main(string[] args)
        {
            new AutoRun(Assembly.GetEntryAssembly()).Execute(
                args,
                new ExtendedTextWrapper(Console.Out),
                TextReader.Null);

            Console.ReadLine();
        }

        [SetUp] 
        public void Init()
        {
            File.WriteAllText(TEXTFILE, "");
        }

        [TearDown] 
        public void Cleanup()
        {
            File.Delete(TEXTFILE);
        }

        [Test]
        public void TestEmptyTextNodesHandlingInlcude()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                EmptyTextNodeHandling = EmptyTextNodeHandling.Include
            };

            string html = @"<html><head></head><body><form></form><p> </p></body></html>";
            Jsonize jsonize = new Jsonize(html);
            string result = jsonize.ParseHtmlAsJsonString(jsonizeConfiguration);
            File.AppendAllText(TEXTFILE, ("\r\nTestEmptyTextNodesHandlingInclude\r\n" + result));
            Assert.AreEqual("{\"node\":\"Document\",\"child\":[{\"node\":\"Element\",\"tag\":\"html\",\"child\":[{\"node\":\"Element\",\"tag\":\"head\"},{\"node\":\"Element\",\"tag\":\"body\",\"child\":[{\"node\":\"Element\",\"tag\":\"form\"},{\"node\":\"Element\",\"tag\":\"p\",\"child\":[{\"node\":\"Text\"}]}]}]}]}",
                result.Replace("\r\n", "").Replace(" ", ""));
        }

        [Test]
        public void TestEmptyTextNodesHandlingIgnore()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                EmptyTextNodeHandling = EmptyTextNodeHandling.Ignore
            };

            string html = "<html><head></head><body><form></form><p></p></body></html>";
            Jsonize jsonize = new Jsonize(html);
            string result = jsonize.ParseHtmlAsJsonString(jsonizeConfiguration);
            Assert.AreEqual("{\"node\":\"Document\",\"child\":[{\"node\":\"Element\",\"tag\":\"html\",\"child\":[{\"node\":\"Element\",\"tag\":\"head\"},{\"node\":\"Element\",\"tag\":\"body\",\"child\":[{\"node\":\"Element\",\"tag\":\"form\"},{\"node\":\"Element\",\"tag\":\"p\"}]}]}]}",
                result.Replace("\r\n", "").Replace(" ", ""));
        }

        [Test]
        public async Task TestNullValueHandlingInclude()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                NullValueHandling = NullValueHandling.Include
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(TEXTFILE, ("\r\nTestNullValueHandlingInclude\r\n" + result));
        }

        [Test]
        public async Task TestNullValueHandlingIgnore()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                NullValueHandling= NullValueHandling.Ignore
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(TEXTFILE, ("\r\nTestNullValueHandlingIgnore\r\n" + result));
        }

        [Test]
        public async Task TestTextTrimHandlingInclude()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                TextTrimHandling = TextTrimHandling.Include
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(TEXTFILE, ("\r\nTestTextTrimHandlingInclude\r\n" + result));
        }

        [Test]
        public async Task TestTextTrimHandlingTrim()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                TextTrimHandling = TextTrimHandling.Trim
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(TEXTFILE, ("\r\nTestTextTrimHandlingIgnore\r\n" + result));
        }

        [Test]
        public async Task TestClassAttributeHandlingArray()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                ClassAttributeHandling = ClassAttributeHandling.Array
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(TEXTFILE, ("\r\nTestClassAttributeHandlingArray\r\n" + result));
        }

        [Test]
        public async Task TestClassAttributeHandlingString()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration
            {
                ClassAttributeHandling = ClassAttributeHandling.String
            };

            string result = await TestJsonizeAsString(jsonizeConfiguration);
            File.AppendAllText(TEXTFILE, ("\r\nTestClassAttributeHandlingString\r\n" + result));
        }

        [Test]
        public async Task TestCreatingJsonizeMetaObject()
        {
            JsonizeNode result = await TestJsonizeAsJsonizeNode();
            JsonizeMeta jsonizeMeta = new JsonizeMeta(result, @"http://jackfinlay.com/?something=something" );

            Assert.AreEqual( result, jsonizeMeta.DocumentJsonizeNode);
            Assert.AreEqual("jackfinlay.com", jsonizeMeta.Domain);
            Assert.AreEqual(@"http://jackfinlay.com/?something=something", jsonizeMeta.Url);
        }

        [Test]
        public void TestFormNodeShouldBeNode()
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration();

            Jsonize jsonize = new Jsonize("<html><head></head><body><form></form></body></html>");
            var result = jsonize.ParseHtmlAsJsonString(jsonizeConfiguration);

            Assert.AreEqual("{\"node\":\"Document\",\"child\":[{\"node\":\"Element\",\"tag\":\"html\",\"child\":[{\"node\":\"Element\",\"tag\":\"head\"},{\"node\":\"Element\",\"tag\":\"body\",\"child\":[{\"node\":\"Element\",\"tag\":\"form\"}]}]}]}", 
                result.Replace("\r\n", "").Replace(" ", ""));
        }
        
        private static async Task<JsonizeNode> TestJsonizeAsJsonizeNode()
        {
            if (_html == null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    const string url = @"http://jackfinlay.com";
                    HttpResponseMessage response = await client.GetAsync(url);
                    _html = await response.Content.ReadAsStringAsync();
                }
            }

            Jsonize jsonize = new Jsonize(_html);

            return jsonize.ParseHtmlAsJsonizeNode();
        }


        private static async Task<string> TestJsonizeAsString(JsonizeConfiguration jsonizeConfiguration)
        {
            if (_html == null) 
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    const string url = @"http://jackfinlay.com";
                    HttpResponseMessage response = await client.GetAsync(url);
                    _html = await response.Content.ReadAsStringAsync();
                }
            }
            
            Jsonize jsonize = new Jsonize(_html);

            return jsonize.ParseHtmlAsJsonString(jsonizeConfiguration);
        }
    }
}
