using System;

namespace Systems.Models
{

    /// <summary>
    /// Сум, Дүүрэг
    /// </summary>
    public class district
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int districtid { get; set; }

        /// <summary>
        /// Нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// өрхийн тэргүүнтэй ямар холбоотой вэ
    /// </summary>
    public class relationship
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int relationshipid { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// өрхийн тэргүүн эсэх
        /// </summary>
        public bool ishead { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class dropdownitem
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// Өрхийн статус
    /// </summary>
    public class Systemstatus
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Өрхийн бүлэг
    /// </summary>
    public class householdgroup
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Коуч
        /// </summary>
        public int coachid { get; set; }

        /// <summary>
        /// Нэгж хувьцааны үнэ
        /// </summary>
        public decimal unitprice { get; set; }
    }

    /// <summary>
    /// Боловсролын зэрэг
    /// </summary>
    public class educationdegree
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Хөдөлмөр эрхлэлтийн байдал
    /// </summary>
    public class employmentstatus
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Эрүүл мэндийн байдал
    /// </summary>
    public class healthcondition
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Зээлийн зориулалт
    /// </summary>
    public class loanpurpose
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Сургалтын төрөл
    /// </summary>
    public class trainingtype
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Зохион байгуулагдсан сургалт, үйл ажиллагааны нэр
    /// </summary>
    public class trainingandactivity
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Сургалт, үйл ажиллагаа зохион байгуулсан байгууллагын нэр
    /// </summary>
    public class organization
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Харьяалагдах дэд салбар
    /// </summary>
    public class subbranch
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Хүлээн авсан хөрөнгийн төрөл
    /// </summary>
    public class assetreceivedtype
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Хүлээн авсан хөрөнгийн нэр
    /// </summary>
    public class assetreceived
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Хүлээн авсан дэмжлэгийн төрөл
    /// </summary>
    public class supportreceivedtype
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Дэмжлэг олгосон байгууллагын нэр
    /// </summary>
    public class sponsoringorganization
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Өрхийн үндсэн хэрэгцээ
    /// </summary>
    public class basicneeds
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }
    
    /// <summary>
    /// Холбон зуучилсан үйлчилгээний төрөл 
    /// </summary>
    public class mediatedservicetype
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Холбон зуучилсан байгууллагын нэр
    /// </summary>
    public class intermediaryorganization
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Холбон зуучилсан үйлчилгээний нэр
    /// </summary>
    public class proxyservice
    {
        /// <summary>
        /// дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// нэр
        /// </summary>
        public string name { get; set; }
    }

}
