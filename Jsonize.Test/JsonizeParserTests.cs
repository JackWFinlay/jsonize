using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Jsonize.Abstractions.Models;
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
        public async Task HtmlBodyPStringResource_DefaultConfiguration_ReturnsJsonizeNode()
        {
            var actual = await _testFixture.JsonizeParser.ParseAsync(StringResources.HtmlBodyP);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyP);
        }
        
        [Fact]
        public async Task HtmlBodyPStream_DefaultConfiguration_ReturnsJsonizeNode()
        {
            await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.HtmlBodyP));
            var actual = await _testFixture.JsonizeParser.ParseAsync(stream);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyP);
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