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
        public string educationlevel { get; set; }

        /// <summary>
        /// Хөдөлмөр эрхлэлтийн байдал
        /// </summary>
        public string employment { get; set; }

        /// <summary>
        /// Эрүүл мэндийн байдал
        /// </summary>
        public string health { get; set; }

    }

    /// <summary>
    /// Хөгжлийн бэрхшээл
    /// </summary>
    public class memberdisabled
    {
        /// <summary>
        /// Дэс дугаар
        /// </summary>
        public int memberdisabledid { get; set; }

        /// <summary>
        /// Өрхийн гишүүдийн дугаар
        /// </summary>
        public int memberid { get; set; }

        /// <summary>
        /// Хөгжлийн бэрхшээлтэй эсэх
        /// </summary>
        public bool isdisabled { get; set; }

        /// <summary>
        /// Ямар хэлбэрийн бэрхшээлтэй вэ?
        /// </summary>
        public string disabledtype { get; set; }

    }

    /// <summary>
    /// Боловсролын зэрэг
    /// </summary>
    public class membereducation
    {
        /// <summary>
        /// Дэс дугаар
        /// </summary>
        public int membereducationid { get; set; }

        /// <summary>
        /// Өрхийн гишүүдийн дугаар
        /// </summary>
        public int memberid { get; set; }

        /// <summary>
        /// боловсролын түвшин
        /// </summary>
        public string educationlevel { get; set; }

        /// <summary>
        /// сургуульд сурдаг уу
        /// </summary>
        public bool isstudied { get; set; }

        /// <summary>
        /// ямар сургуулийн хэддүгээр анги, курсэд сурч байна вэ?
        /// </summary>
        public string coursestudied { get; set; }

        /// <summary>
        /// сурдаг сургуулийн өмчийн хэлбэр ямар вэ?
        /// </summary>
        public string schoolownership { get; set; }

    }

    /// <summary>
    /// Эрүүл мэнд
    /// </summary>
    public class memberhealth
    {
        /// <summary>
        /// Дэс дугаар
        /// </summary>
        public int memberhealthid { get; set; }

        /// <summary>
        /// Өрхийн гишүүдийн дугаар
        /// </summary>
        public int memberid { get; set; }

        /// <summary>
        /// эрүүл мэндийн даатгалд хамрагдсан уу?
        /// </summary>
        public bool ishealthinsurance { get; set; }

        /// <summary>
        /// сүүлийн нэг сард өвдөж, гэмтсэн үү?
        /// </summary>
        public bool ishurt { get; set; }

        /// <summary>
        /// ямар өвчнөөр өвчилсөн бэ?
        /// </summary>
        public string diseased { get; set; }

        /// <summary>
        /// эмнэлэгт үйлчлүүлэхээр хандсан уу?
        /// </summary>
        public bool visitedhospital { get; set; }

        /// <summary>
        /// Аль шатлалын эмнэлэгт хандсан бэ?
        /// </summary>
        public string levelofhospital { get; set; }

        /// <summary>
        /// Сүүлийн 12 сарын хугацаанд баярсайхан эмнэлэгт хэвтэн эмчлүүлсэн үү?
        /// </summary>
        public string beenhospitalized { get; set; }
    }

    /// <summary>
    /// Эрүүл мэндийн урьдчилан сэргийлэлт
    /// </summary>
    public class healthprevention
    {
        /// <summary>
        /// Дэс дугаар
        /// </summary>
        public int healthpreventionid { get; set; }

        /// <summary>
        /// Өрхийн гишүүдийн дугаар
        /// </summary>
        public int memberid { get; set; }

        /// <summary>
        /// эрт илрүүлэг шинжилгээнд хамрагдсан уу?
        /// </summary>
        public bool istested { get; set; }

        /// <summary>
        /// үүлийн 12 сарын хугацаанд эрүүл мэндийн анхан шатны тусламж үйлчилгээг хэр давтамжтайгаар авсан бэ?
        /// </summary>
        public string servicereceived { get; set; }
    }

    /// <summary>
    /// Хөдөлмөр эрхлэлт
    /// </summary>
    public class memberemployment
    {
        /// <summary>
        /// Дэс дугаар
        /// </summary>
        public int memberemploymentid { get; set; }

        /// <summary>
        /// сүүлийн 7 хоногт ямар нэгэн ажил эрхэлсэн үү?
        /// </summary>
        public bool isemployment { get; set; }

        /// <summary>
        /// Сүүлийн 7 хоногт ажил эрхлээгүй үндсэн шалтгаанаа хэлнэ үү?
        /// </summary>
        public string noemploymentreason { get; set; }

        /// <summary>
        /// сүүлийн 7 хоногт ажил хайсан уу?
        /// </summary>
        public bool isfindwork { get; set; }

        /// <summary>
        /// Сүүлийн 7 хоногт ажил хайгаагүй шалтгаанаа хэлнэ үү?
        /// </summary>
        public string nofindworkreason { get; set; }

        /// <summary>
        /// сүүлийн 7 хоногт ажил эрхлээгүй хэдий ч ер нь сүүлийн 12 сард ямар нэгэн ажил хийсэн үү?
        /// </summary>
        public bool isanywork { get; set; }

        /// <summary>
        /// ямар ажил эрхэлсэн бэ?
        /// </summary>
        public string anyworkname { get; set; }

        /// <summary>
        /// ажлыг тайлбарла
        /// </summary>
        public string anyworknote { get; set; }

        /// <summary>
        /// ямар салбарт ажилласан бэ?
        /// </summary>
        public string industryworkname { get; set; }

        /// <summary>
        /// ажлыг 7 хоногт дунджаар хэдэн цаг хийдэг вэ?
        /// </summary>
        public string avgworktime { get; set; }

        /// <summary>
        /// долоо хоног тутмын ээлжийн ажил мөн үү?
        /// </summary>
        public string isshiftwork { get; set; }

        /// <summary>
        /// Таны ажилласан байгууллагын хариуцлагын хэлбэр юу вэ?
        /// </summary>
        public string organizationtype { get; set; }

        /// <summary>
        /// Та сүүлийн 12 сард ажиллаад мөнгөн ба мөнгөн бус хэлбэрээр цалин хөлс авсан уу?
        /// </summary>
        public bool isnotmoneysalary { get; set; }

        /// <summary>
        /// Та энэ ажлаасаа гадна сүүлийн 12 сард түр болон давхар ажил эрхэлсэн үү?
        /// </summary>
        public bool isdoublework { get; set; }
    }

}
