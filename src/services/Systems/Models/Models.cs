using Newtonsoft.Json;
using System;

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

        /// <summary>
        /// Дунд шатны хорооны бүртгэл
        /// </summary>
        public int? committeeid { get; set; }

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

    /// <summary>
    /// Дунд шатны хорооны сарын тайлан
    /// </summary>
    public class CommitteeReport
    {
        /// <summary>
        /// Д/д
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Дунд шатны хороо
        /// </summary>
        public int committeeid { get; set; }

        /// <summary>
        /// Огноо
        /// </summary>
        public DateTime reportdate { get; set; }

        /// <summary>
        /// Дэлгэрэнгүй
        /// </summary>
        public CommitteeReportDtl[] dtls { get; set; }
    }

    /// <summary>
    /// Дунд шатны хорооны сарын тайлан
    /// </summary>
    public class CommitteeReportDtl
    {
        /// <summary>
        /// Д/д
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Дунд шатны хорооны сарын тайлан
        /// </summary>
        public int reportid { get; set; }
        /// <summary>
        /// Хөтөлбөр
        /// </summary>
        public int programid { get; set; }
        /// <summary>
        /// Хөтөлбөрийн индикатор
        /// </summary>
        public int indicatorid { get; set; }
        /// <summary>
        /// Насны ангилал
        /// </summary>
        public int agegroupid { get; set; }
        /// <summary>
        /// Эрэгтэй
        /// </summary>
        public int male { get; set; }
        /// <summary>
        /// Эмэгтэй
        /// </summary>
        public int female { get; set; }
    }

    /// <summary>
    /// Хэрэгжүүлж буй төсөл, хөтөлбөр
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Хөтөлбөр
        /// </summary>
        public int? programid { get; set; }
        /// <summary>
        /// Төслийн нэр
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Санхүүжүүлэгч
        /// </summary>
        public string funder { get; set; }
        /// <summary>
        /// Төслийн товч мэдээлэл
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// Хүрсэн үр дүн
        /// </summary>
        public string results { get; set; }
    }
}
