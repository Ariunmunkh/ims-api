using BaseLibrary.LConnection;
using Connection.Model;
using LConnection.Model;
using System.Data;
using HouseHolds.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class PowerBIRepository : IPowerBIRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public PowerBIRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdCount()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname
FROM
    household
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = 1");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdMember1855Count()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    member.memberid,
    member.name membername,
    TIMESTAMPDIFF(YEAR,member.birthdate, CURDATE()) memberage,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender
FROM
    household
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = 1 AND TIMESTAMPDIFF(YEAR,member.birthdate, CURDATE()) BETWEEN 18 AND 55");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdMember617Count()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    member.memberid,
    member.name membername,
    TIMESTAMPDIFF(YEAR,member.birthdate, CURDATE()) memberage,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender
FROM
    household
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = 1 AND TIMESTAMPDIFF(YEAR,member.birthdate, CURDATE()) BETWEEN 6 AND 17");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdMember5Count()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    member.memberid,
    member.name membername,
    TIMESTAMPDIFF(YEAR,member.birthdate, CURDATE()) memberage,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender
FROM
    household
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = 1 AND TIMESTAMPDIFF(YEAR,member.birthdate, CURDATE()) < 6");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdMemberSingleCount()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    head.memberid headid,
    head.name headname,
    TIMESTAMPDIFF(YEAR,head.birthdate, CURDATE()) headage,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender
FROM
    household
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = 1 
and not exists (select null from householdmember b where b.relationshipid = 2 and b.householdid = household.householdid group by b.householdid having count(1)>0)");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdMemberAvg()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    member.memberid,
    member.name membername,
    TIMESTAMPDIFF(YEAR,member.birthdate, CURDATE()) memberage,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender
FROM
    household
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = 1");
            return connector.Execute(ref cmd, false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdParticipant()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,

    household.coachid,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,

    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate, CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = 1");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdParticipantDisabled()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    householdmember.healthconditionid,
    healthcondition.name healthconditionname
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    healthcondition ON healthcondition.id = householdmember.healthconditionid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1
        AND householdmember.healthconditionid IN (2 , 3, 4, 5)");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdNeeds()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    householdvisit.mediatedservicetypeid,
    mediatedservicetype.name mediatedservicetypename
FROM
    household
        inner JOIN
    householdvisit ON householdvisit.householdid = household.householdid
        LEFT JOIN
    mediatedservicetype ON mediatedservicetype.id = householdvisit.mediatedservicetypeid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdServices()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    householdvisit.mediatedservicetypeid,
    COALESCE(mediatedservicetype.name,'Хэрэгцээ бүртгээгүй') mediatedservicetypename
FROM
    household
        inner JOIN
    mediatedactivity ON mediatedactivity.householdid = household.householdid
        LEFT JOIN
    mediatedservicetype ON mediatedservicetype.id = mediatedactivity.mediatedservicetypeid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1");
            return connector.Execute(ref cmd, false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdBusiness()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    improvement.businessid,
    COALESCE(business.name,'Бизнесээ сонгоогүй') businessname
FROM
    household
        left JOIN
    improvement ON improvement.householdid = household.householdid
        LEFT JOIN
    business ON business.id = improvement.businessid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdBusinessType()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    improvement.subbranchid,
    case 
        when improvement.subbranchid in (3, 4, 5) then subbranch.name 
        when improvement.subbranchid in (3, 4, 5) then 'Бусад' 
        else 'Бизнесээ сонгоогүй'
    end subbranchname
FROM
    household
        left JOIN
    improvement ON improvement.householdid = household.householdid
        LEFT JOIN
    subbranch ON subbranch.id = improvement.subbranchid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1");
            return connector.Execute(ref cmd, false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdInvestment()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname
FROM
    household
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1 and exists(select null from investment where investment.householdid = household.householdid)");
            return connector.Execute(ref cmd, false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdInvestmentPrice()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    investment.totalprice
FROM
    household
        inner JOIN
    investment ON investment.householdid = household.householdid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1");
            return connector.Execute(ref cmd, false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdLivelihoodTraining()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    DATE_FORMAT(training.trainingdate, '%Y-%m-%d %H:%i:%s') trainingdate
FROM
    household
       inner JOIN
    training ON training.householdid = household.householdid
        LEFT JOIN
    householdmember ON householdmember.memberid = training.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1 and training.trainingtypeid = 3");
            return connector.Execute(ref cmd, false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdTechnicalSkillsTraining()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    DATE_FORMAT(training.trainingdate, '%Y-%m-%d %H:%i:%s') trainingdate
FROM
    household
       inner JOIN
    training ON training.householdid = household.householdid
        LEFT JOIN
    householdmember ON householdmember.memberid = training.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1 and training.trainingtypeid = 1");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdImprovement()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    improvement.subbranchid,
    COALESCE(subbranch.name,'Бизнесээ сонгоогүй') subbranchname
FROM
    household
        left JOIN
    improvement ON improvement.householdid = household.householdid
        LEFT JOIN
    subbranch ON subbranch.id = improvement.subbranchid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdBasicFinancialTraining()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender,
    DATE_FORMAT(training.trainingdate, '%Y-%m-%d %H:%i:%s') trainingdate
FROM
    household
       inner JOIN
    training ON training.householdid = household.householdid
        LEFT JOIN
    householdmember ON householdmember.memberid = training.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1 and training.trainingtypeid = 6");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdHouseholdgroup()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.districtid,
    COALESCE(district.name,'Дүүрэг сонгоогүй өрх') districtname,
    household.section,
    household.coachid,
    COALESCE(coach.name, 'Коучид харьяалагдаагүй өрх') coachname,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.memberid,
    householdmember.name membername,
    TIMESTAMPDIFF(YEAR,
        householdmember.birthdate,
        CURDATE()) memberage,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end membergender
FROM
    household
        left JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
WHERE
    household.status = 1");
            return connector.Execute(ref cmd, false);
        }

    }
}