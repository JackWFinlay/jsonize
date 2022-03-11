using System.Text;
using BenchmarkDotNet.Attributes;
using Jsonize.Abstractions.Configuration;
using Jsonize.Benchmarks.Constants;
using Jsonize.Parser;
using Jsonize.Serializer;
using JwfJsonize = JackWFinlay.Jsonize.Jsonize;

namespace Jsonize.Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class JsonizeBenchmarks
{
    private readonly Jsonizer _jsonizer;
    private readonly Stream _largeExampleStream;
    private readonly Stream _docoHtmlStream;
    private readonly Stream _htmlBodyStream;

    public JsonizeBenchmarks()
    {
        var jsonizeConfig = new JsonizeConfiguration
        {
            Parser = new JsonizeParser(),
            Serializer = new JsonizeSerializer()
        };

        _jsonizer = new Jsonizer(jsonizeConfig);
        _htmlBodyStream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.HtmlBodyP));
        _docoHtmlStream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.DocoHtmlExample));
        _largeExampleStream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.LargeExample));
    }

    [Benchmark]
    public void JwfJsonizeHtmlBodyP()
    {
        var jsonize = new JwfJsonize(StringResources.HtmlBodyP);
        jsonize.ParseHtmlAsJsonString();
    }
    
    [Benchmark]
    public void JsonizerHtmlBodyP()
    {
        _jsonizer.ParseToStringAsync(StringResources.HtmlBodyP);
    }
    
    [Benchmark]
    public void JsonizerHtmlBodyPStream()
    {
        _jsonizer.ParseToStringAsync(_htmlBodyStream);
        _htmlBodyStream.Position = 0;
    }
    
    [Benchmark]
    public void JwfJsonizeDocoHtmlExample()
    {
        var jsonize = new JwfJsonize(StringResources.DocoHtmlExample);
        jsonize.ParseHtmlAsJsonString();
    }
    
    [Benchmark]
    public void JsonizerDocoHtmlExample()
    {
        _jsonizer.ParseToStringAsync(StringResources.DocoHtmlExample);
    }
    
    [Benchmark]
    public void JsonizerDocoHtmlExampleStream()
    {
        _jsonizer.ParseToStringAsync(_docoHtmlStream);
        _docoHtmlStream.Position = 0;
    }
    
    [Benchmark]
    public void JwfJsonizeLargeExample()
    {
        var jsonize = new JwfJsonize(StringResources.LargeExample);
        jsonize.ParseHtmlAsJsonString();
    }
    
    [Benchmark]
    public void JsonizerLargeExample()
    {
        _jsonizer.ParseToStringAsync(StringResources.LargeExample);
    }
    
    [Benchmark]
    public void JsonizerLargeExampleStream()
    {
        _jsonizer.ParseToStringAsync(_largeExampleStream);
        _largeExampleStream.Position = 0;
    }
}