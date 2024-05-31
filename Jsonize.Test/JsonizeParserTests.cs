using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Jsonize.Abstractions.Configuration;
using Jsonize.Abstractions.Models;
using Jsonize.Parser;
using Jsonize.Test.Fixtures;
using Xunit;

namespace Jsonize.Test
{
    public class JsonizeParserTests : IClassFixture<JsonizeParserTestFixture>
    {
        private JsonizeParserTestFixture _testFixture;
        
        public JsonizeParserTests(JsonizeParserTestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        public async Task HtmlBodyPStringEnhancedResource_DefaultConfiguration_ReturnsJsonizeNode()
        {
            var actual = await _testFixture.JsonizeParser.ParseAsync(StringResources.HtmlBodyP);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyPEnhanced);
        }
        
        [Fact]
        public async Task HtmlBodyPStringResource_ClassicConfiguration_ReturnsJsonizeNode()
        {
            var actual = await _testFixture.JsonizeParserClassic.ParseAsync(StringResources.HtmlBodyP);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyP);
        }
        
        [Fact]
        public async Task HtmlBodyPSpanEnhancedStringResource_DefaultConfiguration_ReturnsJsonizeNode()
        {
            var actual = await _testFixture.JsonizeParser.ParseAsync(StringResources.HtmlBodyPSpan);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyPSpanEnhanced);
        }
        
        [Fact]
        public async Task HtmlBodyH1SpanEnhancedStringResource_DefaultConfiguration_ReturnsJsonizeNode()
        {
            var actual = await _testFixture.JsonizeParser.ParseAsync(StringResources.HtmlBodyH1Span);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyH1SpanEnhanced);
        }
        
        [Fact]
        public async Task HtmlBodyPSpanStringResource_ClassicConfiguration_ReturnsJsonizeNode()
        {
            var actual = await _testFixture.JsonizeParserClassic.ParseAsync(StringResources.HtmlBodyPSpan);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyPSpan);
        }
        
        [Fact]
        public async Task HtmlBodyPStream_DefaultConfiguration_ReturnsJsonizeNode()
        {
            await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.HtmlBodyP));
            var actual = await _testFixture.JsonizeParser.ParseAsync(stream);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyPEnhanced);
        }
        
        [Fact]
        public async Task HtmlBodyPStream_ForceCancellation_ReturnsCancelledTask()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.Cancel();
            
            await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.HtmlBodyP));
            
            var task = _testFixture.JsonizeParser.ParseAsync(stream, cancellationToken);

            task.IsCanceled
                .Should()
                .Be(true);

            task.IsFaulted
                .Should()
                .Be(false);
        }
        
        [Fact]
        public async Task HtmlBodyPStream_PassCancellationToken_ReturnsCompletedTask()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            
            await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.HtmlBodyP));

            var task = _testFixture.JsonizeParser.ParseAsync(stream, cancellationToken);

            task.IsCanceled
                .Should()
                .Be(false);

            task.IsCompleted
                .Should()
                .Be(true);
        }
    }
}