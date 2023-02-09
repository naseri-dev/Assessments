using CodeScreen.Assessments.TweetsApi.src.Dtos;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CodeScreen.Assessments.TweetsApi
{
    /**
    * Service that retrieves data from the CodeScreen Tweets API.
    */
    class TweetsApiService
    {
        private static readonly string TweetsEndpointURL = "https://app.codescreen.com/api/assessments/tweets";

        //Your API token. Needed to successfully authenticate when calling the tweets endpoint.
        //This needs to be included in the Authorization header (using the Bearer authentication scheme) in the request you send to the tweets endpoint.
        private static readonly string ApiToken = "8c5996d5-fb89-46c9-8821-7063cfbc18b1";
        
        /**
         * Retrieves the data for all tweets, for the given user,
         * by calling the https://app.codescreen.com/api/assessments/tweets endpoint.
         *
         * The userName should be passed in the request as a query parameter called userName.
         *
         * @param userName the name of the user
         * @return a list containing the data for all tweets for the given user
        */
        public async Task<List<TweetDto>> GetTweets(string userName) {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", ApiToken);

            var query = new Dictionary<string, string>
            {
                ["userName"] = userName
            };

            var response = await client.GetAsync(QueryHelpers.AddQueryString(TweetsEndpointURL, query));

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            List<TweetDto> tweets = JsonConvert.DeserializeObject<List<TweetDto>>(json);

            return tweets;
        }
    }
}
