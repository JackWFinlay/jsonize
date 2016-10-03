using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using JackWFinlay.Jsonize;
using System.Threading.Tasks;

namespace Jsonize_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Test();
            Console.ReadLine();
        }

        private static async void Test()
        {
            string result = await TestJsonizeAsString();
            Console.WriteLine(result);
            System.IO.File.WriteAllText(@"C:\Users\Public\WriteText.txt", result);
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
                jsonize.ShowEmptyTextNodes(true);

                return jsonize.ParseHtmlAsJsonString();
            }
        }
    }
}
