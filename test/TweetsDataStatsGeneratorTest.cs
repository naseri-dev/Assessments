using NUnit.Framework;

/**
 * Tests for TweetDataStatsGenerator.
 *
 * Note this file should not be modified. If you would like to add your own unit tests you can add these in a separate class.
 */
namespace CodeScreen.Assessments.TweetsApi.Tests
{
    [TestFixture]
    public class TweetDataStatsGeneratorTest
    {
        private static readonly string UserName = "joe_smith";
        private TweetDataStatsGenerator TweetDataStatsGenerator;

        [OneTimeSetUp] public void Init()
        {
            TweetDataStatsGenerator = new TweetDataStatsGenerator(new TweetsApiService());
        }

        [TestCase]
        public void TestMostTweets()
        {
            Assert.AreEqual(10,TweetDataStatsGenerator.GetMostTweetsForAnyDay(UserName));
        }

        [TestCase]
        public void TestLongestTweet()
        {
            Assert.AreEqual("0c2dc961-a0ae-470e-81a6-8320504dae14", TweetDataStatsGenerator.GetLongestTweet(UserName));
        }

        [TestCase]
        public void TestMostDaysBetweenTweets()
        {
            Assert.AreEqual(120,TweetDataStatsGenerator.FindMostDaysBetweenTweets(UserName));
        }

        [TestCase]
        public void TestMostPopularHashTag()
        {
            Assert.AreEqual("#WorldCup2018",TweetDataStatsGenerator.GetMostPopularHashTag(UserName));
        }

    }
}
