using System.Text.Json.Serialization;

namespace RestLayer.Model
{
    public class RContinentInput
    {
        #region Properties

        /// <summary>
        /// Id of the continent.
        /// </summary>
        [JsonPropertyName("continentId")]
        public int Id { get; set; }


        /// <summary>
        /// Name of the continent.
        /// </summary>
        [JsonPropertyName("naam")]
        public string Name { get; set; }


        #endregion
    }
}
