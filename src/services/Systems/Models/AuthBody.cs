using Newtonsoft.Json;

namespace Systems.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class authbody
    {
        /// <summary>
        /// Нэвтрэх нэр
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Нууц үг
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Нууц үг
        /// </summary>
        [JsonIgnore]
        public string encryptpass { get; set; }
    }
}
