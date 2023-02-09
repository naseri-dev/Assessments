using System;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using CodeScreen.Assessments.TweetsApi.src.Dtos;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/**
* Generates various statistics about the tweets data set returned by the given TweetsApiService instance.
*/
namespace CodeScreen.Assessments.TweetsApi
{
    class TweetDataStatsGenerator
    {
        private readonly TweetsApiService _tweetsApiService;
        public TweetDataStatsGenerator(TweetsApiService tweetsApiService)
        {
            _tweetsApiService = tweetsApiService;
        }

        public List<TweetDto> GetTweets(string userName)
        {
            Task<List<TweetDto>> task = Task.Run(async () 
                => await _tweetsApiService.GetTweets(userName));

            return task.Result;
        }

        /**
         * Retrieves the highest number of tweets that were created on any given day by the given user.
         *
         * A day's time period here is defined from 00:00:00 to 23:59:59
         * If there are no tweets for the given user, this method should return 0.
         *
         * @param userName the name of the user
         * @return the highest number of tweets that were created on a any given day by the given user
        */
        public int GetMostTweetsForAnyDay(string userName)
        {
            var tweets = GetTweets(userName);

            var mostTweetsForAnyDay = tweets.GroupBy(i => i.CreatedAt.ToString("yyyyMMdd"))
             .Select(tweet => new
             {
                 Date = DateTime.ParseExact(tweet.Key, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                 Count = tweet.Count()
             }).ToList().Max(v => v.Count);

            return mostTweetsForAnyDay;
        }

        /**
         * Finds the ID of longest tweet for the given user.
         *
         * You can assume there will only be one tweet that is the longest.
         * If there are no tweets for the given user, this method should return null.
         *
         * @param userName the name of the user
         * @return the ID of longest tweet for the given user
        */
        public string GetLongestTweet(string userName)
        {
            var tweets = GetTweets(userName);

            var tweet = tweets
             .Select(tweet => new
             {
                 tweet.Text.Length,
                 tweet.Id,
             }).ToList().OrderByDescending(v => v.Length).FirstOrDefault();

            string longestTweet = tweet != null ? tweet.Id.ToString() : null;
            return longestTweet;
        }

        /**
         * Retrieves the most number of days between tweets by the given user, wrapped as an OptionalInt.
         *
         * This should always be rounded down to the complete number of days, i.e. if the time is 12 days & 3 hours, this
         * method should return 12.
         * If there are no tweets for the given user, this method should return 0.
         *
         * @param userName the name of the user
         * @return the most number of days between tweets by the given user
        */
        public int FindMostDaysBetweenTweets(string userName)
        {
            var tweets = GetTweets(userName);

            var items = tweets
                .Select(v => new DiffTimeDto
                {
                    Date = v.CreatedAt,
                    Diff = 0
                })
                .OrderBy(x => x.Date)
                .ToList();

            for (int i = 0; i < items.Count - 1; i++)
            {
                items[i].Diff = (int)Math.Floor(Math.Abs((items[i].Date - items[i + 1].Date).TotalDays));
            }
            return items.Max(v => v.Diff);
        }

        /**
         * Retrieves the most popular hash tag tweeted by the given user.
         *
         * Note that the string returned by this method should include the hashtag itself.
         * For example, if the most popular hash tag is "#Java", this method should return "#Java".
         * If there are no tweets for the given user, this method should return null.
         *
         * @param userName the name of the user
         * @return the most popular hash tag tweeted by the given user.
        */
        public string GetMostPopularHashTag(string userName)
        {
            var regex = new Regex(@"#\w+");
            List<string> hashTags = new List<string>();
            var tweets = GetTweets(userName);
            string mostPopularHashTag = string.Empty;

            var matches = tweets.Select(v => regex.Matches(v.Text)).ToList();
            matches.ForEach(item =>
            {
                hashTags.AddRange(item.ToList().Select(v => v.Value));
            });
            var orderedHashtags = hashTags.GroupBy(v => new { HashTag = v })
                .Select(v => new { v.Key, Count = v.Count() })
                .OrderByDescending(o => o.Count).ToList();

            mostPopularHashTag = orderedHashtags.FirstOrDefault()?.Key.HashTag;

            return mostPopularHashTag;
        }
    }
}
