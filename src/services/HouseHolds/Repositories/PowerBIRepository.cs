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
    district.name districtname,
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
    district.name districtname,
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
    district.name districtname,
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
    district.name districtname,
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
    district.name districtname,
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
    district.name districtname,
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
    district.name districtname,
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
    district.name districtname,
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
    district.name districtname,
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
    district.name districtname,
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

    }
}