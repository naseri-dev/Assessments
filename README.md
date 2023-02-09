# Mohammad Naseri's assessment

The CodeScreen Tweets API is a service that contains one endpoint,<br/>
`GET https://app.codescreen.com/api/assessments/tweets`, which returns the details of all tweets for a
given user. 

The user name is passed in as a `query parameter` in the url.

For authentication, you need to send your API token in the `Authorization HTTP header` using the [Bearer authentication scheme](https://tools.ietf.org/html/draft-ietf-oauth-v2-bearer-20#section-2.1). Your API token is `8c5996d5-fb89-46c9-8821-7063cfbc18b1`.

Here is an example of how to send a request from `cURL`:

    curl -H "Authorization: Bearer 8c5996d5-fb89-46c9-8821-7063cfbc18b1" \
    https://app.codescreen.com/api/assessments/tweets?userName=joe_smith

When you send an `HTTP GET` request to the endpoint, the response will be a `200 OK` containing a list of tweet data in
`JSON` format. An example response is the following:

     [
       {
           "id": "52f83d7c-ad2c-4ca0-b742-b03bc27f0c96",
           "createdAt": "2017-12-01T11:12:42",
           "text": "Chrome or Firefox? #Browsers",
           "user": {
               "id": "75343078-b5dd-306f-a3f9-8203a3915144",
               "userName": "joe_smith"
           }
       },
       {
           "id": "5f52e882-36a5-4460-a33b-e834b406650e",
           "createdAt": "2017-12-02T12:17:52",
           "text": "Bought a real Christmas tree, smells a lot more christmassy! #Xmas",
           "user": {
               "id": "75343078-b5dd-306f-a3f9-8203a3915144",
               "userName": "joe_smith"
           }
       },
     ]


The `id` field represents the unique id for the tweet. The `createdAt` field contains the time at which the tweet was
published, in ISO-8601 extended offset date-time format. You can assume all date-times are in the same timezone. </br>
The `user` field contains a JSON object which is made up of the unique id and name of the user who published the tweet.

## Your Task

You are required to implement all the methods marked with `TODO Implement` in the [TweetsApiService](src/TweetsApiService.cs) and [TweetDataStatsGenerator](src/TweetsDataStatsGenerator.cs) classes in such a way that
all the unit tests in [TweetDataStatsGeneratorTest](test/TweetsDataStatsGeneratorTest.cs) pass.

[TweetsApiService](src/TweetsApiService.cs) should be implemented in such a way that you only need to call out to the CodeScreen Tweets API
endpoint once per full run of the [TweetDataStatsGeneratorTest](test/TweetsDataStatsGeneratorTest.cs) test suite.

## Requirements

The [TweetDataStatsGeneratorTest](test/TweetsDataStatsGeneratorTest.cs) class should not be modified. If you would like to add your own unit tests you
can add these in a separate class. Please make sure the class name of each test file you add is the same as the file name.

The [CodeScreen.Assessments.TweetsApi.csproj](CodeScreen.Assessments.TweetsApi.csproj) file should only be modified to add any third-party dependencies required for your solution.

You are free to use whichever libraries you want (.NET or third-party) when implementing your solution. </br>
Note we recommend using the <a href="https://nodatime.org/" target="_blank">`Noda Time`</a> library for working with dates & times, and the <a href="https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netframework-4.8" target="_blank">`HttpClient`</a> class to interact with the CodeScreen Tweets API service.

Your solution must use/be compatible with `.NET 5.0`.

##

This test should take no longer than 2 hours to complete successfully.

Good luck!
## License

At CodeScreen, we strongly value the integrity and privacy of our assessments. As a result, this repository is under exclusive copyright, which means you **do not** have permission to share your solution to this test publicly (i.e., inside a public GitHub/GitLab repo, on Reddit, etc.). <br>

## Submitting your solution

Please push your changes to the `main branch` of this repository. You can push one or more commits. <br>

Once you are finished with the task, please click the `Submit Solution` link on <a href="https://app.codescreen.com/candidate/cff170ae-317a-4eaa-af72-3da870127c76" target="_blank">this screen</a>.