using System;

namespace Systems.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DropDownItem
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Эцэг бүртгэлийн дугаар
        /// </summary>
        public int headid { get; set; }

        /// <summary>
        /// Нэр
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Төрөл
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн
    /// </summary>
    public class Volunteer
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Ургийн овог
        /// </summary>
        public string familyname { get; set; }
        /// <summary>
        /// Өөрийн нэр
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        /// Эцэг эхийн нэр
        /// </summary>
        public string lastname { get; set; }
        /// <summary>
        /// Хүйс
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// Регистрийн дугаар
        /// </summary>
        public string regno { get; set; }
        /// <summary>
        /// Төрсөн огноо
        /// </summary>
        public DateTime birthday { get; set; }
        /// <summary>
        /// Утасны дугаар
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// Сайн дурын идэвхтэн эсэх
        /// </summary>
        public bool? isvolunteer { get; set; }
        /// <summary>
        /// Цусны доонор эсэх
        /// </summary>
        public bool? isblooddonor { get; set; }
        /// <summary>
        /// Цусны бүлэг
        /// </summary>
        public int? bloodgroupid { get; set; }
        /// <summary>
        /// Улс
        /// </summary>
        public int? countryid { get; set; }
        /// <summary>
        /// Аймаг, хот
        /// </summary>
        public int? divisionid { get; set; }
        /// <summary>
        /// Сум, дүүрэг
        /// </summary>
        public int? districtid { get; set; }
        /// <summary>
        /// Гудамж, байр, орц
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Хөгжлийн бэрхшээлтэй иргэн эсэх
        /// </summary>
        public bool isdisabled { get; set; }
    }

    /// <summary>
    /// Яаралтаы үед холбоо барих гэр бүлийн гишүүний мэдээлэл
    /// </summary>
    public class EmergencyContact
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Сайн дурын идэвхтэн
        /// </summary>
        public int volunteerid { get; set; }
        /// <summary>
        /// Таны юу болох
        /// </summary>
        public int? relationshipid { get; set; }
        /// <summary>
        /// Овог нэр
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        /// Утасны дугаар
        /// </summary>
        public string phone { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Сайн дурын ажлын мэдээлэл
    /// </summary>
    public class VolunteerVoluntaryWork
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Сайн дурын идэвхтэн
        /// </summary>
        public int volunteerid { get; set; }
        /// <summary>
        /// Сайн дурын ажлын төрөл
        /// </summary>
        public int voluntaryworkid { get; set; }
        /// <summary>
        /// Хугацаа
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// Огноо
        /// </summary>
        public DateTime? voluntaryworkdate { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string note { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Сургалтын мэдээлэл
    /// </summary>
    public class VolunteerTraining
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Сайн дурын идэвхтэн
        /// </summary>
        public int volunteerid { get; set; }
        /// <summary>
        /// Сургалтын төрөл
        /// </summary>
        public int trainingid { get; set; }
        /// <summary>
        /// Огноо
        /// </summary>
        public DateTime trainingdate { get; set; }
        /// <summary>
        /// Хаана
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// Хугацаа
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string note { get; set; }
    }
    /// <summary>
    /// Сайн дурын идэвхтэн Ур чадварын мэдээлэл
    /// </summary>
    public class VolunteerSkills
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Сайн дурын идэвхтэн
        /// </summary>
        public int volunteerid { get; set; }
        /// <summary>
        /// Ур чадварын төрөл
        /// </summary>
        public int skillsid { get; set; }
        /// <summary>
        /// Ур чадварын түвшин
        /// </summary>
        public int skillslevelid { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string note { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Гишүүнчлэлийн мэдээлэл
    /// </summary>
    public class VolunteerMembership
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Сайн дурын идэвхтэн
        /// </summary>
        public int volunteerid { get; set; }
        /// <summary>
        /// Гишүүнчлэлийн төрөл
        /// </summary>
        public int membershipid { get; set; }
        /// <summary>
        /// Эхэлсэн
        /// </summary>
        public DateTime? begindate { get; set; }
        /// <summary>
        /// Дууссан
        /// </summary>
        public DateTime? enddate { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string note { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Тусламжийн мэдээлэл
    /// </summary>
    public class VolunteerAssistance
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Сайн дурын идэвхтэн
        /// </summary>
        public int volunteerid { get; set; }
        /// <summary>
        /// Тусламжийн төрөл
        /// </summary>
        public int assistanceid { get; set; }
        /// <summary>
        /// Төслийн нэр
        /// </summary>
        public string projectname { get; set; }
        /// <summary>
        /// Огноо
        /// </summary>
        public DateTime? assistancedate { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string note { get; set; }
    }
}
