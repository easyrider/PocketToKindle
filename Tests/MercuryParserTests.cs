using Core;
using PocketToKindle.Parsers;
using Xunit;

namespace PocketToKindleTests
{
    public class MercuryParserTests
    {
        Config _config = new ConfigBuilder(".").Build();

        [Fact]
        public async void Parser_ParsesCorrectlySampleArticle()
        {
            string testUrl = "https://waitbutwhy.com/2015/01/artificial-intelligence-revolution-1.html";
            MercuryParser mercuryParser = new MercuryParser(_config.MercuryApiKey);

            var article = await mercuryParser.ParseAsync(testUrl);

            Assert.Equal("The Artificial Intelligence Revolution: Part 1", article.Title);
        }
    }
}