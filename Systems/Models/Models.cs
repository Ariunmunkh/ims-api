﻿using Newtonsoft.Json;


namespace Systems.Models
{
    /// <summary>
    /// Хэрэглэгч
    /// </summary>
    public class Tbluser
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int userid { get; set; }

        /// <summary>
        /// Нэвтрэх нэр
        /// </summary>
        public string? username { get; set; } = string.Empty;

        /// <summary>
        /// Имэйл
        /// </summary>
        public string? email { get; set; } = string.Empty;

        /// <summary>
        /// Нууц үг
        /// </summary>
        public string? password { get; set; } = string.Empty;

        /// <summary>
        /// Нууц үг
        /// </summary>
        [JsonIgnore]
        public string? encryptpass { get; set; } = string.Empty;

        /// <summary>
        /// Хэрэглэгчийн үүрэг 1 Admin, 2 Sub-admin, 3 Coach, 4 Others, 5 Volunteer
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
        public string? name { get; set; } = string.Empty;

        /// <summary>
        /// Хороодын дарга нарын нэр 
        /// </summary>
        public string? bossname { get; set; } = string.Empty;

        /// <summary>
        /// Утасны дугаар
        /// </summary>
        public string? phone { get; set; } = string.Empty;

        /// <summary>
        /// Байршил
        /// </summary>
        public string? location { get; set; } = string.Empty;
    }

    /// <summary>
    /// Сарын үйл ажиллагааны бичмэл мэдээлэл
    /// </summary>
    public class CommitteeReportInfo
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
        /// Бичмэл мэдээлэл
        /// </summary>
        public string? info { get; set; }

        /// <summary>
        /// Огноо
        /// </summary>
        public DateTime? infodate { get; set; }

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
        public CommitteeReportDtl[]? dtls { get; set; }
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
        public string? name { get; set; } = string.Empty;
        /// <summary>
        /// Санхүүжүүлэгч
        /// </summary>
        public string? funder { get; set; } = string.Empty;
        /// <summary>
        /// Төслийн товч мэдээлэл
        /// </summary>
        public string? note { get; set; } = string.Empty;
        /// <summary>
        /// Хүрсэн үр дүн
        /// </summary>
        public string? results { get; set; } = string.Empty;
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
        public string? c1_1 { get; set; } = string.Empty;
        /// <summary>
        /// Сум/Хорооны тоо
        /// </summary>
        public string? c1_2 { get; set; } = string.Empty;
        /// <summary>
        /// Нийслэлээс алслагдсан байдал /км/
        /// </summary>
        public string? c1_3 { get; set; } = string.Empty;
        /// <summary>
        /// Хүн амын тоо
        /// </summary>
        public string? c1_4 { get; set; } = string.Empty;
        /// <summary>
        /// Хүн амын тоо /0-5 нас/
        /// </summary>
        public string? c1_4_1 { get; set; } = string.Empty;
        /// <summary>
        /// Хүн амын тоо /6-17 нас/
        /// </summary>
        public string? c1_4_2 { get; set; } = string.Empty;
        /// <summary>
        /// Хүн амын тоо /18-25 нас/
        /// </summary>
        public string? c1_4_3 { get; set; } = string.Empty;
        /// <summary>
        /// Хүн амын тоо /26-45 нас/
        /// </summary>
        public string? c1_4_4 { get; set; } = string.Empty;
        /// <summary>
        /// Хүн амын тоо /46-60 нас/
        /// </summary>
        public string? c1_4_5 { get; set; } = string.Empty;
        /// <summary>
        /// Хүн амын тоо /61+ нас/
        /// </summary>
        public string? c1_4_6 { get; set; } = string.Empty;
        /// <summary>
        /// Хөдөлмөр эрхлэгч насны хүн амын тоо
        /// </summary>
        public string? c1_5 { get; set; } = string.Empty;
        /// <summary>
        /// Зорилтот бүлгийн хүн амын тоо
        /// </summary>
        public string? c1_6 { get; set; } = string.Empty;
        /// <summary>
        /// Өрх толгойлсон эрэгтэй
        /// </summary>
        public string? c1_6_1 { get; set; } = string.Empty;
        /// <summary>
        /// Өрх толгойлсон эмэгтэй
        /// </summary>
        public string? c1_6_2 { get; set; } = string.Empty;
        /// <summary>
        /// Хөгжлийн бэрхшээлтэй иргэн
        /// </summary>
        public string? c1_6_3 { get; set; } = string.Empty;
        /// <summary>
        /// Өндөр настан
        /// </summary>
        public string? c1_6_4 { get; set; } = string.Empty;
        /// <summary>
        /// Хагас өнчин хүүхдийн тоо
        /// </summary>
        public string? c1_6_5 { get; set; } = string.Empty;
        /// <summary>
        /// Бүтэн өнчин хүүхдийн тоо
        /// </summary>
        public string? c1_6_6 { get; set; } = string.Empty;
        /// <summary>
        /// Малчин өрхийн тоо
        /// </summary>
        public string? c1_7 { get; set; } = string.Empty;
        /// <summary>
        /// Малын тоо, толгой
        /// </summary>
        public string? c1_8 { get; set; } = string.Empty;
        /// <summary>
        /// Адуу
        /// </summary>
        public string? c1_8_1 { get; set; } = string.Empty;
        /// <summary>
        /// Тэмээ
        /// </summary>
        public string? c1_8_2 { get; set; } = string.Empty;
        /// <summary>
        /// Үхэр
        /// </summary>
        public string? c1_8_3 { get; set; } = string.Empty;
        /// <summary>
        /// Хонь
        /// </summary>
        public string? c1_8_4 { get; set; } = string.Empty;
        /// <summary>
        /// Ямаа
        /// </summary>
        public string? c1_8_5 { get; set; } = string.Empty;
        /// <summary>
        /// Төрийн өмчит аж ахуй нэгж байгууллагын тоо
        /// </summary>
        public string? c1_9 { get; set; } = string.Empty;
        /// <summary>
        /// Хувийн өмчит аж ахуй нэгж байгууллагын тоо
        /// </summary>
        public string? c1_10 { get; set; } = string.Empty;
        /// <summary>
        /// Орон нутагт үйл ажиллагаа явуулж буй Олон улсын байгууллагын тоо
        /// </summary>
        public string? c1_11 { get; set; } = string.Empty;
        /// <summary>
        /// Орон нутагт үйл ажиллагаа явуулж буй хүмүүнлэгийн байгууллагын тоо
        /// </summary>
        public string? c1_12 { get; set; } = string.Empty;
        /// <summary>
        /// Их, дээд сургуулийн тоо
        /// </summary>
        public string? c1_13 { get; set; } = string.Empty;
        /// <summary>
        /// Ерөнхий боловсролын сургуулийн тоо
        /// </summary>
        public string? c1_14 { get; set; } = string.Empty;
        /// <summary>
        /// Сургуулийн өмнөх боловсролын байгууллагын тоо
        /// </summary>
        public string? c1_15 { get; set; } = string.Empty;
        /// <summary>
        /// Жолооны курсын тоо
        /// </summary>
        public string? c1_16 { get; set; } = string.Empty;
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
        public string? c2_1 { get; set; } = string.Empty;
        /// <summary>
        /// Байгуулагдсан он, сар, өдөр
        /// </summary>
        public string? c2_2 { get; set; } = string.Empty;
        /// <summary>
        /// Хаяг, байршил
        /// </summary>
        public string? c2_3 { get; set; } = string.Empty;
        /// <summary>
        /// Утасны дугаар
        /// </summary>
        public string? c2_4 { get; set; } = string.Empty;
        /// <summary>
        /// И-мэйл хаяг
        /// </summary>
        public string? c2_5 { get; set; } = string.Empty;
        /// <summary>
        /// Үндсэн ажилтаны тоо
        /// </summary>
        public string? c2_6 { get; set; } = string.Empty;
        /// <summary>
        /// Гэрээт ажилтаны тоо
        /// </summary>
        public string? c2_7 { get; set; } = string.Empty;
        /// <summary>
        /// Бүртгэлтэй Сайн дурын идэвхтний тоо
        /// </summary>
        public string? c2_8 { get; set; } = string.Empty;
        /// <summary>
        /// Гишүүнчлэл
        /// </summary>
        public string? c2_9 { get; set; } = string.Empty;
        /// <summary>
        /// Гишүүн
        /// </summary>
        public string? c2_9_1 { get; set; } = string.Empty;
        /// <summary>
        /// Онцгой гишүүн
        /// </summary>
        public string? c2_9_2 { get; set; } = string.Empty;
        /// <summary>
        /// Мөнгөн гишүүн
        /// </summary>
        public string? c2_9_3 { get; set; } = string.Empty;
        /// <summary>
        /// Алтан гишүүн
        /// </summary>
        public string? c2_9_4 { get; set; } = string.Empty;
        /// <summary>
        /// Хүмүүнлэгийн гишүүн байгууллагын тоо
        /// </summary>
        public string? c2_10 { get; set; } = string.Empty;
        /// <summary>
        /// Хүүхэд залуучуудын хөдөлгөөний гишүүний тоо
        /// </summary>
        public string? c2_11 { get; set; } = string.Empty;
        /// <summary>
        /// Багачуудын улаан загалмайч
        /// </summary>
        public string? c2_11_1 { get; set; } = string.Empty;
        /// <summary>
        /// Өсвөрийн улаан загалмайч
        /// </summary>
        public string? c2_11_2 { get; set; } = string.Empty;
        /// <summary>
        /// Залуучуудын улаан загалмайч
        /// </summary>
        public string? c2_11_3 { get; set; } = string.Empty;
        /// <summary>
        /// Идэр улаан загалмайч
        /// </summary>
        public string? c2_11_4 { get; set; } = string.Empty;
        /// <summary>
        /// ДШХ-ны эзэмшлийн газартай эсэх
        /// </summary>
        public string? c2_12_1 { get; set; } = string.Empty;
        /// <summary>
        /// Эзэмшлийн газрын зориулалт
        /// </summary>
        public string? c2_12_2 { get; set; } = string.Empty;
        /// <summary>
        /// Талбайн хэмжээ
        /// </summary>
        public string? c2_12_3 { get; set; } = string.Empty;
        /// <summary>
        /// Хэрэв үгүй бол Газрын зориулалт
        /// </summary>
        public string? c2_12_4 { get; set; } = string.Empty;
        /// <summary>
        /// ДШХ-ны эзэмшлийн байртай эсэх
        /// </summary>
        public string? c2_13_1 { get; set; } = string.Empty;
        /// <summary>
        /// Эзэмшлийн байрны тоо
        /// </summary>
        public string? c2_13_2 { get; set; } = string.Empty;
        /// <summary>
        /// Талбайн хэмжээ
        /// </summary>
        public string? c2_13_3 { get; set; } = string.Empty;
        /// <summary>
        /// Өрөөний тоо
        /// </summary>
        public string? c2_13_4 { get; set; } = string.Empty;
        /// <summary>
        /// Хэрэв үгүй бол байрны зориулалт
        /// </summary>
        public string? c2_13_5 { get; set; } = string.Empty;
        /// <summary>
        /// ДШХ-ны эзэмшлийн агуулахтай эсэх
        /// </summary>
        public string? c2_14_1 { get; set; } = string.Empty;
        /// <summary>
        /// Ашиглалтанд орсон огноо
        /// </summary>
        public string? c2_14_2 { get; set; } = string.Empty;
        /// <summary>
        /// Талбайн хэмжээ
        /// </summary>
        public string? c2_14_3 { get; set; } = string.Empty;
        /// <summary>
        /// ДШХ-ны эзэмшлийн тээврийн хэрэгсэлтэй эсэх
        /// </summary>
        public string? c2_15_1 { get; set; } = string.Empty;
        /// <summary>
        /// Тээврийн хэрэгслийн тоо
        /// </summary>
        public string? c2_15_2 { get; set; } = string.Empty;
        /// <summary>
        /// Тээврийн хэрэгслийн тайлбар /Марк, ашигласан хугацаа, Монголд орж ирсэн огноо/
        /// </summary>
        public string? c2_15_3 { get; set; } = string.Empty;
        /// <summary>
        /// Бусад хөрөнгө /зөөврийн болон суурийн компьютер, принтер гэх мэтийг дурдах/
        /// </summary>
        public string? c2_16 { get; set; } = string.Empty;
        /// <summary>
        /// Анхан шатны хорооны талаарх мэдээлэл 
        /// </summary>
        public CommitteeInfoDtl[]? committeeinfodtl { get; set; }
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
        public string? name { get; set; } = string.Empty;
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
    public class Committeeactivity
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
        public string? c3_3 { get; set; } = string.Empty;
        /// <summary>
        /// Нөөц хөгжүүлэх, орлого нэмэгдүүлэх чиглэлээр хийгддэг үйл ажиллагаа /Үйл ажиллагааг жагсааж оруулах/
        /// </summary>
        public string? c3_4 { get; set; } = string.Empty;

        /// <summary>
        /// Байгууллагын хөгжлийн талаарх мэдээлэл 
        /// </summary>
        public Committeeactivitydtl[]? committeeactivitydtl { get; set; }
    }

    /// <summary>
    /// Байгууллагын хөгжлийн талаарх мэдээлэл 
    /// </summary>
    public class Committeeactivitydtl
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
        public string? name { get; set; } = string.Empty;
        /// <summary>
        /// Албан тушаал
        /// </summary>
        public string? job { get; set; } = string.Empty;
        /// <summary>
        /// Гишүүний төрөл
        /// </summary>
        public bool? type { get; set; }
    }

    /// <summary>
    /// АНХАН ШАТНЫ ХОРООДЫН МЭДЭЭЛЭЛ
    /// </summary>
    public class PrimaryStageInfo
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
        /// АНХАН ШАТНЫ ХОРООНЫ НЭР
        /// </summary>
        public string? c4_1 { get; set; }
        /// <summary>
        /// АНХАН ШАТНЫ ХОРООНЫ БАЙГУУЛАГДСАН ОГНОО
        /// </summary>
        public string? c4_2 { get; set; }
        /// <summary>
        /// СУМ/ХОРОО
        /// </summary>
        public string? c4_3_1 { get; set; }
        /// <summary>
        /// БАЙГУУЛЛАГА/ААН
        /// </summary>
        public string? c4_3_2 { get; set; }
        /// <summary>
        /// ТЭРГҮҮНИЙ ОВОГ, НЭР
        /// </summary>
        public string? c4_4 { get; set; }
        /// <summary>
        /// ХОЛБОГДОХ УТАСНЫ ДУГААР
        /// </summary>
        public string? c4_5 { get; set; }
        /// <summary>
        /// НАРИЙН БИЧГИЙН ДАРГЫН ОВОГ, НЭР
        /// </summary>
        public string? c4_6 { get; set; }
        /// <summary>
        /// ХОЛБОГДОХ УТАСНЫ ДУГААР
        /// </summary>
        public string? c4_7 { get; set; }
        /// <summary>
        /// ЭНГИЙН ГИШҮҮН
        /// </summary>
        public string? c4_8_1 { get; set; }
        /// <summary>
        /// ОНЦГОЙ ГИШҮҮН
        /// </summary>
        public string? c4_8_2 { get; set; }
        /// <summary>
        /// МӨНГӨН ГИШҮҮН
        /// </summary>
        public string? c4_8_3 { get; set; }
        /// <summary>
        /// АЛТАН ГИШҮҮН
        /// </summary>
        public string? c4_8_4 { get; set; }
        /// <summary>
        /// ХҮМҮҮНЛЭГИЙН ГИШҮҮН БАЙГУУЛЛАГЫН ТОО
        /// </summary>
        public string? c4_9 { get; set; }
        /// <summary>
        /// САЙН ДУРЫН ИДЭВХТНИЙ ТОО
        /// </summary>
        public string? c4_10 { get; set; }
        /// <summary>
        /// ХҮҮХЭД ЗАЛУУЧУУДЫН ГИШҮҮНИЙ ТОО
        /// </summary>
        public string? c4_11 { get; set; }
    }
}
