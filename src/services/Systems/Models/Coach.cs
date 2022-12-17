using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Systems.Models
{
    /// <summary>
    /// Коуч
    /// </summary>
    public class coach
    {
        /// <summary>
        /// Коучийн дугаар
        /// </summary>
        public int coachid { get; set; }

        /// <summary>
        /// Коучийн нэр 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Утас
        /// </summary>
        public string phone { get; set; }

        //Ажиллаж буй дунд шатны хорооны нэр 
        //Хариуцсан хороод 
        //Хариуцсан бүлгүүд 
        //Хариуцсан өрхүүд

    }

    /// <summary>
    /// 
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

        //Дүүрэг
        //Хороо

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
        public string relative { get; set; }

        /// <summary>
        /// Нас
        /// </summary>
        public DateTime birthdate { get; set; }

        /// <summary>
        /// Хүйс 0-эр, 1-эм
        /// </summary>
        public int gender { get; set; }

        /// <summary>
        /// Одоо тантай хамт амьдарч байгаа юу?
        /// </summary>
        public bool istogether { get; set; }

        //Боловсролын зэрэг
        //Хөдөлмөр эрхлэлтийн байдал
        //Эрүүл мэндийн байдал

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


    /// <summary>zl
    /// Өрхийн айлчлалын мэдээлэл
    /// </summary>
    public class householdvisit
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int visitid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Айлчилсан огноо
        /// </summary>
        public DateTime visitdate { get; set; }

        //Айлчлалаар уулзсан өрхийн гишүүд 
        //Айлчилсан хүний нэр 

        /// <summary>
        /// Тайлбар
        /// </summary>
        public string note { get; set; }

    }

    //    Хамрагддаг бүлгийн мэдээлэл
    //Харьяалагддаг бүлгийн нэр   
    //Нэгж хувьцааны үнэ

    //    Хурлын ирцийн мэдээлэл
    //Бүлгийн хурал зохион байгуулагдсан огноо    
    //Бүлгийн хуралд оролцсон эсэх    
    //Худалдан авсан хувьцааны тоо

    //    Зээлийн мэдээлэл
    //Зээл авсан огноо 
    //Бүлгээс зээлсэн мөнгөн дүн 
    //Зээлийн зориулалт

    //    Зээлийн эргэн төлөлтийн мэдээлэл
    //Зээлийн эргэн төлөлт хийсэн огноо 
    //Эргэн төлсөн мөнгөн дүн 
    //Зээлийн үлдэгдэл

    //    Сургалт, үйл ажиллагаа
    //Огноо 
    //Сургалт, үйл ажиллагаа зохион байгуулсан байгууллагын нэр 
    //Сургалт, үйл ажиллагааны нэр    
    //Сургалтын үргэжилсэн хугацаа 
    //Сургалт, үйл ажиллагаанд хамрагдсан өрхийн гишүүний нэр

    //    Амьжиргаа сайжруулах үйл ажиллагааны мэдээлэл
    //Амьжиргаа сайжруулах төлөвлөгөө боловсруулсан огноо 
    //Өрхийн сонгосон аж ахуй 
    //Харьяалагдах дэд салбар

    //    Хөрөнгө оруулалтын мэдээлэл
    //Хөрөнгө хүлээн авсан огноо 
    //Хүлээн авсан хөрөнгийн нэр 
    //Тоо ширхэг 
    //Нэгжийн үнэ 
    //Нийт үнэ 
    //Тайлбар(марк, дугаар)

    //    Бусад тусламж, дэмжлэг
    //Тусламж, дэмжлэг хүлээн авсан огноо 
    //Хүлээн авсан тусламж дэмжлэг    
    //Тоо ширхэг  
    //Нэгжийн үнэ 
    //Нийт үнэ    
    //Дэмжлэг олгосон байгууллагын нэр

    //    Сургалт, үйл ажиллагаа
    //Огноо 
    //Холбон зуучилсан байгууллагын нэр 
    //Холбон зуучилсан үйлчилгээний нэр 
    //Үйлчилгээнд холбогдсон өрхийн гишүүний нэр

    //    Дунд шатны хорооны ерөнхий мэдээлэл
    //Дунд шатны хорооны нэр 
    //Хариуцсан коуч нар  
    //Хариуцсан хороод    
    //Хариуцсан бүлгүүд   
    //Хариуцсан өрхүүд

    //    Туршилтын хөтөлбөрийн ерөнхий мэдээлэл
    //Төсөл хэрэгжих дүүргүүд 
    //Нийт коуч 
    //Нийт хороо 
    //Нийт бүлэг 
    //Нийт өрх

}
