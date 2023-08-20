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

    /// <summary>
    /// Орон нутгийн талаарх мэдээлэл
    /// </summary>
    public class LocalInfo
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Дунд шатны хороо
        /// </summary>
        public int committeeid { get; set; }
        /// <summary>
        /// Аймаг/Дүүргийн нэр
        /// </summary>
        public string c1_1 { get; set; }
        /// <summary>
        /// Сум/Хорооны тоо
        /// </summary>
        public string c1_2 { get; set; }
        /// <summary>
        /// Нийслэлээс алслагдсан байдал /км/
        /// </summary>
        public string c1_3 { get; set; }
        /// <summary>
        /// Хүн амын тоо
        /// </summary>
        public string c1_4 { get; set; }
        /// <summary>
        /// Хүн амын тоо /0-5 нас/
        /// </summary>
        public string c1_4_1 { get; set; }
        /// <summary>
        /// Хүн амын тоо /6-17 нас/
        /// </summary>
        public string c1_4_2 { get; set; }
        /// <summary>
        /// Хүн амын тоо /18-25 нас/
        /// </summary>
        public string c1_4_3 { get; set; }
        /// <summary>
        /// Хүн амын тоо /26-45 нас/
        /// </summary>
        public string c1_4_4 { get; set; }
        /// <summary>
        /// Хүн амын тоо /46-60 нас/
        /// </summary>
        public string c1_4_5 { get; set; }
        /// <summary>
        /// Хүн амын тоо /61+ нас/
        /// </summary>
        public string c1_4_6 { get; set; }
        /// <summary>
        /// Хөдөлмөр эрхлэгч насны хүн амын тоо
        /// </summary>
        public string c1_5 { get; set; }
        /// <summary>
        /// Зорилтот бүлгийн хүн амын тоо
        /// </summary>
        public string c1_6 { get; set; }
        /// <summary>
        /// Өрх толгойлсон эрэгтэй
        /// </summary>
        public string c1_6_1 { get; set; }
        /// <summary>
        /// Өрх толгойлсон эмэгтэй
        /// </summary>
        public string c1_6_2 { get; set; }
        /// <summary>
        /// Хөгжлийн бэрхшээлтэй иргэн
        /// </summary>
        public string c1_6_3 { get; set; }
        /// <summary>
        /// Өндөр настан
        /// </summary>
        public string c1_6_4 { get; set; }
        /// <summary>
        /// Хагас өнчин хүүхдийн тоо
        /// </summary>
        public string c1_6_5 { get; set; }
        /// <summary>
        /// Бүтэн өнчин хүүхдийн тоо
        /// </summary>
        public string c1_6_6 { get; set; }
        /// <summary>
        /// Малчин өрхийн тоо
        /// </summary>
        public string c1_7 { get; set; }
        /// <summary>
        /// Малын тоо, толгой
        /// </summary>
        public string c1_8 { get; set; }
        /// <summary>
        /// Адуу
        /// </summary>
        public string c1_8_1 { get; set; }
        /// <summary>
        /// Тэмээ
        /// </summary>
        public string c1_8_2 { get; set; }
        /// <summary>
        /// Үхэр
        /// </summary>
        public string c1_8_3 { get; set; }
        /// <summary>
        /// Хонь
        /// </summary>
        public string c1_8_4 { get; set; }
        /// <summary>
        /// Ямаа
        /// </summary>
        public string c1_8_5 { get; set; }
        /// <summary>
        /// Төрийн өмчит аж ахуй нэгж байгууллагын тоо
        /// </summary>
        public string c1_9 { get; set; }
        /// <summary>
        /// Хувийн өмчит аж ахуй нэгж байгууллагын тоо
        /// </summary>
        public string c1_10 { get; set; }
        /// <summary>
        /// Орон нутагт үйл ажиллагаа явуулж буй Олон улсын байгууллагын тоо
        /// </summary>
        public string c1_11 { get; set; }
        /// <summary>
        /// Орон нутагт үйл ажиллагаа явуулж буй хүмүүнлэгийн байгууллагын тоо
        /// </summary>
        public string c1_12 { get; set; }
        /// <summary>
        /// Их, дээд сургуулийн тоо
        /// </summary>
        public string c1_13 { get; set; }
        /// <summary>
        /// Ерөнхий боловсролын сургуулийн тоо
        /// </summary>
        public string c1_14 { get; set; }
        /// <summary>
        /// Сургуулийн өмнөх боловсролын байгууллагын тоо
        /// </summary>
        public string c1_15 { get; set; }
        /// <summary>
        /// Жолооны курсын тоо
        /// </summary>
        public string c1_16 { get; set; }
    }

    /// <summary>
    /// Дунд шатны хорооны талаарх мэдээлэл
    /// </summary>
    public class CommitteeInfo
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Дунд шатны хороо
        /// </summary>
        public int committeeid { get; set; }
        /// <summary>
        /// Дунд шатны хорооны нэр
        /// </summary>
        public string c2_1 { get; set; }
        /// <summary>
        /// Байгуулагдсан он, сар, өдөр
        /// </summary>
        public string c2_2 { get; set; }
        /// <summary>
        /// Хаяг, байршил
        /// </summary>
        public string c2_3 { get; set; }
        /// <summary>
        /// Утасны дугаар
        /// </summary>
        public string c2_4 { get; set; }
        /// <summary>
        /// И-мэйл хаяг
        /// </summary>
        public string c2_5 { get; set; }
        /// <summary>
        /// Үндсэн ажилтаны тоо
        /// </summary>
        public string c2_6 { get; set; }
        /// <summary>
        /// Гэрээт ажилтаны тоо
        /// </summary>
        public string c2_7 { get; set; }
        /// <summary>
        /// Бүртгэлтэй Сайн дурын идэвхтний тоо
        /// </summary>
        public string c2_8 { get; set; }
        /// <summary>
        /// Гишүүнчлэл
        /// </summary>
        public string c2_9 { get; set; }
        /// <summary>
        /// Гишүүн
        /// </summary>
        public string c2_9_1 { get; set; }
        /// <summary>
        /// Онцгой гишүүн
        /// </summary>
        public string c2_9_2 { get; set; }
        /// <summary>
        /// Мөнгөн гишүүн
        /// </summary>
        public string c2_9_3 { get; set; }
        /// <summary>
        /// Алтан гишүүн
        /// </summary>
        public string c2_9_4 { get; set; }
        /// <summary>
        /// Хүмүүнлэгийн гишүүн байгууллагын тоо
        /// </summary>
        public string c2_10 { get; set; }
        /// <summary>
        /// Хүүхэд залуучуудын хөдөлгөөний гишүүний тоо
        /// </summary>
        public string c2_11 { get; set; }
        /// <summary>
        /// Багачуудын улаан загалмайч
        /// </summary>
        public string c2_11_1 { get; set; }
        /// <summary>
        /// Өсвөрийн улаан загалмайч
        /// </summary>
        public string c2_11_2 { get; set; }
        /// <summary>
        /// Залуучуудын улаан загалмайч
        /// </summary>
        public string c2_11_3 { get; set; }
        /// <summary>
        /// Идэр улаан загалмайч
        /// </summary>
        public string c2_11_4 { get; set; }
        /// <summary>
        /// ДШХ-ны эзэмшлийн газартай эсэх
        /// </summary>
        public string c2_12_1 { get; set; }
        /// <summary>
        /// Эзэмшлийн газрын зориулалт
        /// </summary>
        public string c2_12_2 { get; set; }
        /// <summary>
        /// Талбайн хэмжээ
        /// </summary>
        public string c2_12_3 { get; set; }
        /// <summary>
        /// Хэрэв үгүй бол Газрын зориулалт
        /// </summary>
        public string c2_12_4 { get; set; }
        /// <summary>
        /// ДШХ-ны эзэмшлийн байртай эсэх
        /// </summary>
        public string c2_13_1 { get; set; }
        /// <summary>
        /// Эзэмшлийн байрны тоо
        /// </summary>
        public string c2_13_2 { get; set; }
        /// <summary>
        /// Талбайн хэмжээ
        /// </summary>
        public string c2_13_3 { get; set; }
        /// <summary>
        /// Өрөөний тоо
        /// </summary>
        public string c2_13_4 { get; set; }
        /// <summary>
        /// Хэрэв үгүй бол байрны зориулалт
        /// </summary>
        public string c2_13_5 { get; set; }
        /// <summary>
        /// ДШХ-ны эзэмшлийн агуулахтай эсэх
        /// </summary>
        public string c2_14_1 { get; set; }
        /// <summary>
        /// Ашиглалтанд орсон огноо
        /// </summary>
        public string c2_14_2 { get; set; }
        /// <summary>
        /// Талбайн хэмжээ
        /// </summary>
        public string c2_14_3 { get; set; }
        /// <summary>
        /// ДШХ-ны эзэмшлийн тээврийн хэрэгсэлтэй эсэх
        /// </summary>
        public string c2_15_1 { get; set; }
        /// <summary>
        /// Тээврийн хэрэгслийн тоо
        /// </summary>
        public string c2_15_2 { get; set; }
        /// <summary>
        /// Тээврийн хэрэгслийн тайлбар /Марк, ашигласан хугацаа, Монголд орж ирсэн огноо/
        /// </summary>
        public string c2_15_3 { get; set; }
        /// <summary>
        /// Бусад хөрөнгө /зөөврийн болон суурийн компьютер, принтер гэх мэтийг дурдах/
        /// </summary>
        public string c2_16 { get; set; }
        /// <summary>
        /// Анхан шатны хорооны талаарх мэдээлэл 
        /// </summary>
        public CommitteeInfoDtl[] committeeinfodtl { get; set; }
    }

    /// <summary>
    /// Анхан шатны хорооны талаарх мэдээлэл 
    /// </summary>
    public class CommitteeInfoDtl
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Дунд шатны хороо
        /// </summary>
        public int committeeid { get; set; }
        /// <summary>
        /// Сум/Хороо дахь анхан шатны хорооны нэр
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Тэмдэг
        /// </summary>
        public bool? isnote { get; set; }
        /// <summary>
        /// Банкны данс
        /// </summary>
        public bool? isbank { get; set; }
    }

    /// <summary>
    /// ҮЙЛ АЖИЛЛАГААНЫ ТАЛААРХ МЭДЭЭЛЭЛ
    /// </summary>
    public class committeeactivity
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Дунд шатны хороо
        /// </summary>
        public int committeeid { get; set; }
        /// <summary>
        /// Хэрэгжүүлж байсан төслийн нэр, хугацаа, үндсэн үйл ажиллагаа, үр дүнгийн тухай 2-3 өгүүлбэрт багтаах /2020 оноос хойшхи/
        /// </summary>
        public string c3_3 { get; set; }
        /// <summary>
        /// Нөөц хөгжүүлэх, орлого нэмэгдүүлэх чиглэлээр хийгддэг үйл ажиллагаа /Үйл ажиллагааг жагсааж оруулах/
        /// </summary>
        public string c3_4 { get; set; }

        /// <summary>
        /// Байгууллагын хөгжлийн талаарх мэдээлэл 
        /// </summary>
        public committeeactivitydtl[] committeeactivitydtl { get; set; }
    }

    /// <summary>
    /// Байгууллагын хөгжлийн талаарх мэдээлэл 
    /// </summary>
    public class committeeactivitydtl
    {
        /// <summary>
        /// Дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Дунд шатны хороо
        /// </summary>
        public int committeeid { get; set; }
        /// <summary>
        /// Овог, нэр
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Албан тушаал
        /// </summary>
        public string job { get; set; }
        /// <summary>
        /// Гишүүний төрөл
        /// </summary>
        public bool? type { get; set; }
    }

}
