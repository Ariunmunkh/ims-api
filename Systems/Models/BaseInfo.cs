﻿using System;

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
        public int? headid { get; set; }

        /// <summary>
        /// Дунд шатны хороо
        /// </summary>
        public int? committeeid { get; set; }

        /// <summary>
        /// Нэр
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// Төрөл
        /// </summary>
        public string? type { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн
    /// </summary>
    public class UVolunteer
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Дунд шатны хороо
        /// </summary>
        public int committeeid { get; set; }
        /// <summary>
        /// Төлөв
        /// </summary>
        public int status { get; set; }
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
        /// Дунд шатны хороо
        /// </summary>
        public int? committeeid { get; set; }
        /// <summary>
        /// Төлөв
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// Төрөл
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// Ургийн овог
        /// </summary>
        public string? familyname { get; set; }
        /// <summary>
        /// Өөрийн нэр
        /// </summary>
        public string? firstname { get; set; }
        /// <summary>
        /// Эцэг эхийн нэр
        /// </summary>
        public string? lastname { get; set; }
        /// <summary>
        /// Хүйс
        /// </summary>
        public int? gender { get; set; }
        /// <summary>
        /// Регистрийн дугаар
        /// </summary>
        public string? regno { get; set; }
        /// <summary>
        /// Мэргэжил
        /// </summary>
        public string? jobname { get; set; }
        /// <summary>
        /// Одоо эрхлэж буй ажил
        /// </summary>
        public string? employment { get; set; }
        /// <summary>
        /// Төрсөн огноо
        /// </summary>
        public DateTime? birthday { get; set; }
        /// <summary>
        /// Элссэн огноо /улаан загалмайд/
        /// </summary>
        public DateTime? joindate { get; set; }
        /// <summary>
        /// Утасны дугаар
        /// </summary>
        public string? phone { get; set; }
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
        /// Боловсролын түвшин
        /// </summary>
        public int? educationlevelid { get; set; }
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
        public string? address { get; set; }
        /// <summary>
        /// Төрсөн газар
        /// </summary>
        public string? birthplace { get; set; }
        /// <summary>
        /// facebook
        /// </summary>
        public string? facebook { get; set; }
        /// <summary>
        /// Хөгжлийн бэрхшээлтэй иргэн эсэх
        /// </summary>
        public bool isdisabled { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Зураг
    /// </summary>
    public class VolunteerImage
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int volunteerid { get; set; }

        /// <summary>
        /// Зураг
        /// </summary>
        public string? image { get; set; }
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
        public string? firstname { get; set; }
        /// <summary>
        /// Утасны дугаар
        /// </summary>
        public string? phone { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Сайн дурын ажлын мэдээлэл
    /// </summary>
    public class UVolunteerVoluntaryWork
    {
        /// <summary>
        /// Бүртгэлийн дугаар
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Төлөв
        /// </summary>
        public int? status { get; set; }
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
        /// Сайн дурын ажлын нэр
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// Сайн дурын ажлын төрөл
        /// </summary>
        public int? voluntaryworkid { get; set; }
        /// <summary>
        /// Төлөв
        /// </summary>
        public int? status { get; set; }
        /// <summary>
        /// Хугацаа
        /// </summary>
        public decimal? duration { get; set; }
        /// <summary>
        /// Эхлэх огноо
        /// </summary>
        public DateTime? begindate { get; set; }
        /// <summary>
        /// Дуусах огноо
        /// </summary>
        public DateTime? enddate { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string? note { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? image { get; set; }
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
        public int? trainingid { get; set; }
        /// <summary>
        /// Сургалтын нэр
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// Зохион байгуулагч
        /// </summary>
        public string? organizer { get; set; }
        /// <summary>
        /// Сургалт эхэлсэн огноо
        /// </summary>
        public DateTime? begindate { get; set; }
        /// <summary>
        /// Сургалт дууссан огноо
        /// </summary>
        public DateTime? enddate { get; set; }
        /// <summary>
        /// Сургалтын байршил
        /// </summary>
        public string? location { get; set; }
        /// <summary>
        /// Гэрчилгээтэй эсэх
        /// </summary>
        public bool? iscertificate { get; set; }
        /// <summary>
        /// Зураг
        /// </summary>
        public string? image { get; set; }
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
        public int? skillsid { get; set; }
        /// <summary>
        /// Ур чадварын түвшин
        /// </summary>
        public int? skillslevelid { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string? note { get; set; }
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
        public int? membershipid { get; set; }
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
        public string? note { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Боловсролын мэдээлэл
    /// </summary>
    public class VolunteerEducation
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
        /// Боловсролын түвшин
        /// </summary>
        public int educationlevelid { get; set; }

        /// <summary>
        /// Сургуулийн нэр
        /// </summary>
        public string? schoolname { get; set; }

        /// <summary>
        /// Төгссөн эсэх
        /// </summary>
        public bool? isend { get; set; }
        /// <summary>
        /// Курс/Анги
        /// </summary>
        public int classlevel { get; set; }

        /// <summary>
        /// Мэрэгжил
        /// </summary>
        public string? skill { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Эрхэлсэн ажил
    /// </summary>
    public class VolunteerEmployment
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
        /// Ажлын салбар
        /// </summary>
        public string? employment { get; set; }
        /// <summary>
        /// Ажлын газар
        /// </summary>
        public string? company { get; set; }
        /// <summary>
        /// Албан тушаал
        /// </summary>
        public string? job { get; set; }
        /// <summary>
        /// Эхэлсэн огноо
        /// </summary>
        public DateTime? begindate { get; set; }
        /// <summary>
        /// Дууссан огноо
        /// </summary>
        public DateTime? enddate { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string? note { get; set; }
    }

    /// <summary>
    /// Сайн дурын идэвхтэн Гадаад хэлний мэдлэг
    /// </summary>
    public class VolunteerLanguages
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
        /// Гадаад хэл
        /// </summary>
        public int languageid { get; set; }
        /// <summary>
        /// Гадаад хэл
        /// </summary>
        public int levelid { get; set; }
        /// <summary>
        /// Сурсан хугацаа /Жилээр/
        /// </summary>
        public int studyyear { get; set; }
        /// <summary>
        /// Түвшин шалгасан оноотой эсэх
        /// </summary>
        public bool? isscore { get; set; }
        /// <summary>
        /// Шалгалтын нэр
        /// </summary>
        public string? testname { get; set; }
        /// <summary>
        /// Шалгалтын оноо
        /// </summary>
        public int testscore { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string? note { get; set; }

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
        public int? assistanceid { get; set; }
        /// <summary>
        /// Төслийн нэр
        /// </summary>
        public string? projectname { get; set; }
        /// <summary>
        /// Огноо
        /// </summary>
        public DateTime? assistancedate { get; set; }
        /// <summary>
        /// Нэмэлт мэдээлэл
        /// </summary>
        public string? note { get; set; }
    }
}
