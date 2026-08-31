using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dz1908
{
    public class MovieSearchResponse
    {
        [JsonPropertyName("results")]
        public List<MovieInfo> Results { get; set; }
    }

    public class MovieInfo
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }
    }
}