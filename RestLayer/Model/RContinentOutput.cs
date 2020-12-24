using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestLayer.Model
{
    public class RContinentOutput
    {
        /// <summary>
        /// a string with the path to the continent.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the continent.
        /// </summary>
        [JsonPropertyName("naam")]
        public string Name { get;  set; }


        /// <summary>
        /// Population of the continent.
        /// </summary>
        [JsonPropertyName("populatie")]
        public int Population { get; set; }

        /// <summary>
        /// A collection of strings with each one being the path to a country.
        /// </summary>
        [JsonPropertyName("landen")]
        public List<string> Countries { get; set; } = new List<string>();
    }
}
