using Newtonsoft.Json;

namespace Systems.Models
{
    /// <summary>
    /// Хэрэглэгч
    /// </summary>
    public class tbluser
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int userid { get; set; }
        
        /// <summary>
        /// Нэвтрэх нэр
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Имэйл
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Нууц үг
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Нууц үг
        /// </summary>
        [JsonIgnore]
        public string encryptpass { get; set; }

        /// <summary>
        /// Хэрэглэгчийн төрөл
        /// </summary>
        public int roleid { get; set; }
    }
}
