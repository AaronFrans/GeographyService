using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestLayer.Model
{
    public class RCountryOutput
    {
        /// <summary>
        /// Id of the country.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// Population of the country.
        /// </summary>
        [JsonPropertyName("populatie")]
        public int Population { get;  set; }
        /// <summary>
        /// Name of the country.
        /// </summary>
        [JsonPropertyName("naam")]
        public string Name { get;  set; }
        /// <summary>
        /// Surface of the country.
        /// </summary>
        [JsonPropertyName("opervlakte")]
        public float Surface { get;  set; }
        /// <summary>
        /// Continent the country is part of.
        /// </summary>
        [JsonPropertyName("continentId")]
        public string Continent { get;  set; }
    }
}
