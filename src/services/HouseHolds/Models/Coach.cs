using System;

namespace HouseHolds.Models
{
    /// <summary>
    /// Төслийн ерөнхий мэдээлэл
    /// </summary>
    public class project
    {
        /// <summary>
        /// Төслийн дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Төслийн нэр
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Төслийн удирдагчийн нэр
        /// </summary>
        public string leadername { get; set; }

        /// <summary>
        /// Утас
        /// </summary>
        public string leaderphone { get; set; }

        /// <summary>
        /// Төслийн байршил
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// Төсөл хэрэгжих хороо/сум
        /// </summary>
        public string implementation { get; set; }
    }

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

        /// <summary>
        /// Төсөл
        /// </summary>
        public int? projectid { get; set; }

        /// <summary>
        /// Сум, Дүүрэг
        /// </summary>
        public int? districtid { get; set; }

        /// <summary>
        /// Хороо
        /// </summary>
        public string[] section { get; set; }
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

        /// <summary>
        /// Айлчлалаар уулзсан өрхийн гишүүд 
        /// </summary>
        public int memberid { get; set; }

        /// <summary>
        /// Холбон зуучилсан үйлчилгээний төрөл
        /// </summary>
        public int[] mediatedservicetypeid { get; set; }

        /// <summary>
        /// Өрхийн үндсэн хэрэгцээ
        /// </summary>
        public int[] basicneedsid { get; set; }

        /// <summary>
        /// Үндсэн хэрэгцээний тайлбар
        /// </summary>
        public string basicneedsnote { get; set; }

        /// <summary>
        /// Өрхийн орлого, зарлагын бүртгэлээ тогтмол хөтөлсөн эсэх
        /// </summary>
        public bool? incomeexpenditurerecord { get; set; }

        /// <summary>
        /// Өрхийн хөгжлийн төлөвлөгөө боловсруулсан эсэх
        /// </summary>
        public bool? developmentplan { get; set; }

        /// <summary>
        /// Айлчилсан хүний нэр /Коучийн дугаар/
        /// </summary>
        public int coachid { get; set; }

        /// <summary>
        /// Тайлбар
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// Шийдвэрлэсэн байдал, авах арга хэмжээ
        /// </summary>
        public string decisionandaction { get; set; }

    }

    //    Хамрагддаг бүлгийн мэдээлэл
    //Харьяалагддаг бүлгийн нэр   
    //Нэгж хувьцааны үнэ

    /// <summary>
    /// Хурлын ирцийн мэдээлэл
    /// </summary>
    public class meetingattendance
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int entryid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Бүлгийн хурал зохион байгуулагдсан огноо
        /// </summary>
        public DateTime meetingdate { get; set; }

        /// <summary>
        /// Бүлгийн хуралд оролцсон эсэх
        /// </summary>
        public bool isjoin { get; set; }

        /// <summary>
        /// Худалдан авсан хувьцааны тоо
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Нэг хувьцааны үнэ
        /// </summary>
        public decimal unitprice { get; set; }

        /// <summary>
        /// Хуримтлуулсан мөнгөн дүн
        /// </summary>
        public decimal amount { get; set; }
    }

    /// <summary>
    /// Зээлийн мэдээлэл
    /// </summary>
    public class loan
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int entryid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Зээл авсан огноо 
        /// </summary>
        public DateTime loandate { get; set; }

        /// <summary>
        /// Бүлгээс зээлсэн мөнгөн дүн 
        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// Зээлийн зориулалт
        /// </summary>
        public int loanpurposeid { get; set; }

        /// <summary>
        /// Зээлийн зориулалтын тайлбар
        /// </summary>
        public string loanpurposenote { get; set; }
    }

    /// <summary>
    /// Зээлийн эргэн төлөлтийн мэдээлэл
    /// </summary>
    public class loanrepayment
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int entryid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Зээлийн эргэн төлөлт хийсэн огноо 
        /// </summary>
        public DateTime repaymentdate { get; set; }

        /// <summary>
        /// Эргэн төлсөн мөнгөн дүн 
        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// Зээлийн үлдэгдэл
        /// </summary>
        public decimal balance { get; set; }

    }


    /// <summary>
    /// Сургалт, үйл ажиллагаа
    /// </summary>
    public class training
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int entryid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Огноо
        /// </summary>
        public DateTime trainingdate { get; set; }

        /// <summary>
        /// Сургалтын үндсэн чиглэл
        /// </summary>
        public int trainingcategoryid { get; set; }

        /// <summary>
        /// Сургалтын төрөл
        /// </summary>
        public int trainingtypeid { get; set; }

        /// <summary>
        /// Сургалтын нэр
        /// </summary>
        public int trainingandactivityid { get; set; }

        /// <summary>
        /// Сургалтын явагдсан хэлбэр
        /// </summary>
        public int formoftrainingid { get; set; }

        /// <summary>
        /// Сургалт өгсөн байгууллага/ажилтан
        /// </summary>
        public int organizationid { get; set; }

        /// <summary>
        /// Сургалтын үргэжилсэн хугацаа
        /// </summary>
        public decimal duration { get; set; }

        /// <summary>
        /// Өрхөөс уг сургалтад хамрагдсан эсэх
        /// </summary>
        public bool isjoin { get; set; }

        /// <summary>
        /// Сургалт, үйл ажиллагаанд хамрагдсан өрхийн гишүүний нэр
        /// </summary>
        public int memberid { get; set; }
    }

    /// <summary>
    /// Амьжиргаа сайжруулах үйл ажиллагааны мэдээлэл
    /// </summary>
    public class improvement
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int entryid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Амьжиргаа сайжруулах төлөвлөгөө боловсруулсан огноо
        /// </summary>
        public DateTime plandate { get; set; }

        /// <summary>
        /// Өрхийн сонгосон аж ахуй
        /// </summary>
        public int businessid { get; set; }

        /// <summary>
        /// Харьяалагдах дэд салбар
        /// </summary>
        public int subbranchid { get; set; }

    }

    /// <summary>
    /// Хөрөнгө оруулалтын мэдээлэл
    /// </summary>
    public class investment
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int entryid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Хөрөнгө хүлээн авсан огноо
        /// </summary>
        public DateTime investmentdate { get; set; }

        /// <summary>
        /// Хүлээн авсан хөрөнгийн төрөл
        /// </summary>
        public int assetreceivedtypeid { get; set; }

        /// <summary>
        /// Хүлээн авсан хөрөнгийн нэр
        /// </summary>
        public int assetreceivedid { get; set; }

        /// <summary>
        /// Тоо ширхэг
        /// </summary>
        public decimal quantity { get; set; }

        /// <summary>
        /// Нэгжийн үнэ
        /// </summary>
        public decimal unitprice { get; set; }

        /// <summary>
        /// Нийт үнэ
        /// </summary>
        public decimal totalprice { get; set; }

        /// <summary>
        /// Тайлбар(марк, дугаар)
        /// </summary>
        public string note { get; set; }
    }

    /// <summary>
    /// Бусад тусламж, дэмжлэг
    /// </summary>
    public class othersupport
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int entryid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Тусламж, дэмжлэг хүлээн авсан огноо
        /// </summary>
        public DateTime supportdate { get; set; }

        /// <summary>
        /// Хүлээн авсан дэмжлэгийн төрөл
        /// </summary>
        public int supportreceivedtypeid { get; set; }

        /// <summary>
        /// Хүлээн авсан тусламж дэмжлэг
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Тоо ширхэг
        /// </summary>
        public decimal quantity { get; set; }

        /// <summary>
        /// Нэгжийн үнэ
        /// </summary>
        public decimal unitprice { get; set; }

        /// <summary>
        /// Нийт үнэ
        /// </summary>
        public decimal totalprice { get; set; }

        /// <summary>
        /// Дэмжлэг олгосон байгууллагын нэр
        /// </summary>
        public int sponsoringorganizationid { get; set; }
    }

    /// <summary>
    /// Холбон зуучилсан үйл ажиллагаа
    /// </summary>
    public class mediatedactivity
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int entryid { get; set; }

        /// <summary>
        /// Өрхийн дугаар
        /// </summary>
        public int householdid { get; set; }

        /// <summary>
        /// Үйлчилгээ авсан огноо
        /// </summary>
        public DateTime mediateddate { get; set; }

        /// <summary>
        /// Үйлчилгээний төрөл
        /// </summary>
        public int mediatedservicetypeid { get; set; }

        /// <summary>
        /// Үйлчилгээний нэр
        /// </summary>
        public int proxyserviceid { get; set; }

        /// <summary>
        /// Үйлчилгээ үзүүлсэн байгууллага / ажилтан 
        /// </summary>
        public int intermediaryorganizationid { get; set; }

        /// <summary>
        /// Үйлчилгээнд холбогдсон өрхийн гишүүний нэр
        /// </summary>
        public int memberid { get; set; }
    }

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
