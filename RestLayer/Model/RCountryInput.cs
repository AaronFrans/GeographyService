using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestLayer.Model
{
    public class RCountryInput
    {

        /// <summary>
        /// Id of the country.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
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
        /// Id of the country.
        /// </summary>
        [JsonPropertyName("continentId")]
        public int ContinentId { get; set; }
    }
}
