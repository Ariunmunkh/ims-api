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
        /// Хэрэглэгчийн үүрэг
        /// </summary>
        public int roleid { get; set; }

        /// <summary>
        /// Сайн дурын идэвхтэн
        /// </summary>
        public int? volunteerid { get; set; }

    }

    /// <summary>
    /// Дунд шатны хорооны бүртгэл
    /// </summary>
    public class Committee
    {

        /// <summary>
        /// Д/д
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Салбар нэгжүүд
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Хороодын дарга нарын нэр 
        /// </summary>
        public string bossname { get; set; }

        /// <summary>
        /// Утасны дугаар
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Байршил
        /// </summary>
        public string location { get; set; }
    }
}
