using System;

namespace HouseHolds.Models
{
    /// <summary>
    /// Өрх
    /// </summary>
    public class household
    {

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Өрхийн статус
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// Ам бүлийн тоо
        /// </summary>
        public int numberof { get; set; }

        /// <summary>
        /// Өрхийн тэргүүний нэр
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Дүүрэг
        /// </summary>
        public int districtid { get; set; }

        /// <summary>
        /// Хороо
        /// </summary>
        public int section { get; set; }

        /// <summary>
        /// Хаяг
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Утас
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Коучийн дугаар Хариуцсан коучийн нэр
        /// </summary>
        public int coachid { get; set; }

    }

    /// <summary>
    /// Өрхийн гишүүдийн мэдээлэл
    /// </summary>
    public class householdmember
    {
        /// <summary>
        /// Дэс дугаар
        /// </summary>
        public int memberid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Өрхийн гишүүний нэр 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Өрхийн тэргүүнтэй ямар хамааралтай болох
        /// </summary>
        public int relationshipid { get; set; }

        /// <summary>
        /// Нас
        /// </summary>
        public DateTime birthdate { get; set; }

        /// <summary>
        /// Хүйс 0-эр, 1-эм
        /// </summary>
        public int gender { get; set; }

        /// <summary>
        /// Гол оролцогч
        /// </summary>
        public bool isparticipant { get; set; }

        /// <summary>
        /// Одоо тантай хамт амьдарч байгаа юу?
        /// </summary>
        public bool istogether { get; set; }

        /// <summary>
        /// Боловсролын зэрэг
        /// </summary>
        public int educationdegreeid { get; set; }

        /// <summary>
        /// Хөдөлмөр эрхлэлтийн байдал
        /// </summary>
        public int employmentstatusid { get; set; }

        /// <summary>
        /// Эрүүл мэндийн байдал
        /// </summary>
        public int healthconditionid { get; set; }

    }


}
