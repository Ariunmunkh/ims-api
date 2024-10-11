using Newtonsoft.Json;

namespace Systems.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Authbody
    {
        /// <summary>
        /// Нэвтрэх нэр
        /// </summary>
        [JsonRequired]
        public string username { get; set; } = string.Empty;

        /// <summary>
        /// Нууц үг
        /// </summary>
        [JsonRequired]
        public string password { get; set; } = string.Empty;

        /// <summary>
        /// Нууц үг
        /// </summary>
        [JsonIgnore]
        public string encryptpass { get; set; } = string.Empty;
    }
}
