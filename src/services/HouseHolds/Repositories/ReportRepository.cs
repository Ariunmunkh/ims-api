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
    public class ReportRepository : IReportRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public ReportRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdLocation(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
    head.name headname,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender,
    TIMESTAMPDIFF(YEAR,head.birthdate,CURDATE()) headage,
    household.latitude,
    household.longitude
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdMember(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
case when householdmember.isparticipant = true then 'Мөн' else 'Биш' end isparticipant,
    householdmember.istogether,
    householdmember.relationshipid,
    relationship.name relationshipname,
    householdmember.educationdegreeid,
    COALESCE(educationdegree.name,'Боловсролын зэрэг хоосон') educationdegreename,
    householdmember.employmentstatusid,
    COALESCE(employmentstatus.name,'Хөдөлмөр эрхлэлтийн байдал хоосон') employmentstatusname,
    householdmember.healthconditionid,
    COALESCE(healthcondition.name,'Эрүүл мэндийн байдал хоосон') healthconditionname,      
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
       CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) < 25 THEN 'age24'
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) between 25 and 54 THEN 'age54'
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) > 54 THEN 'agemax'
        ELSE 'Хоосон'
    END agecategory,
       CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) between 18 and 55 THEN 1
        ELSE 0
    END employmentage,
       CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) < 6 THEN 1
        ELSE 0
    END kindergartenage
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.householdid = household.householdid
        LEFT JOIN
    relationship on relationship.relationshipid = householdmember.relationshipid
        left join
    educationdegree on educationdegree.id = householdmember.educationdegreeid
        left join
    employmentstatus on employmentstatus.id = householdmember.employmentstatusid
        left join
    healthcondition on healthcondition.id =  householdmember.healthconditionid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdVisit(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
    head.name headname,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender,
    TIMESTAMPDIFF(YEAR,head.birthdate,CURDATE()) headage,
    household.latitude,
    household.longitude,
    case
        when householdvisit.visitid is null then 'Айлчлалын мэдээлэл байхгүй'
        else 'Айлчлалын мэдээлэлтэй'
    end householdvisit,
    householdvisit.visitid,
    DATE_FORMAT(householdvisit.visitdate, '%Y-%m-%d %H:%i:%s') visitdate,
    householdvisit.memberid,
    visitmember.name visitmember,
    householdvisit.coachid,
    visitcoach.name visitcoach,
    householdvisit.note
FROM
    household
        left join
    householdvisit on householdvisit.householdid = household.householdid
        LEFT JOIN
    coach visitcoach ON visitcoach.coachid = householdvisit.coachid
        LEFT JOIN
    householdmember visitmember ON visitmember.memberid = householdvisit.memberid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdLoan(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
    head.name headname,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender,
    TIMESTAMPDIFF(YEAR,head.birthdate,CURDATE()) headage,
    household.latitude,
    household.longitude,
    case
        when loan.entryid is null then 'Зээл аваагүй'
        else 'Зээл авсан'
    end loan,
    loan.entryid,
    DATE_FORMAT(loan.loandate, '%Y-%m-%d %H:%i:%s') loandate,
    loan.amount,
    loan.loanpurposeid,
    loanpurpose.name loanpurposename
FROM
    household
        left join
    loan on loan.householdid = household.householdid
        left join 
    loanpurpose on loanpurpose.id = loan.loanpurposeid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdTraining(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
    head.name headname,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender,
    TIMESTAMPDIFF(YEAR,head.birthdate,CURDATE()) headage,
    household.latitude,
    household.longitude,
    case
        when training.entryid is null then 'Сургалтанд хамрагдаж байгаагүй'
        else 'Сургалтанд хамрагдсан'
    end training,
    DATE_FORMAT(training.trainingdate, '%Y-%m-%d %H:%i:%s') trainingdate,
    training.trainingtypeid,
    trainingtype.name trainingtypename,
    training.trainingandactivityid,
    trainingandactivity.name trainingandactivityname,
    training.organizationid,
    organization.name organizationname,
    training.duration,
    training.isjoin,
    training.memberid,
    joinmember.name joinname,
    case when joinmember.isparticipant = true then 'Мөн' else 'Биш' end isparticipant
FROM
    household
        left join
    training on training.householdid = household.householdid
        left join 
    trainingtype on trainingtype.id = training.trainingtypeid
        left join 
    trainingandactivity on trainingandactivity.id = training.trainingandactivityid
        left join 
    organization on organization.id = training.organizationid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    householdmember joinmember ON joinmember.memberid = training.memberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdImprovement(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
    head.name headname,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender,
    TIMESTAMPDIFF(YEAR,head.birthdate,CURDATE()) headage,
    household.latitude,
    household.longitude,
    case
        when improvement.entryid is null then 'Бизнесээ сонгоогүй'
        else 'Бизнесээ сонгосон'
    end improvement,
    DATE_FORMAT(improvement.plandate, '%Y-%m-%d %H:%i:%s') plandate,
    improvement.selectedfarm,
    improvement.subbranchid,
    subbranch.name subbranchname
FROM
    household
        left join
    improvement on improvement.householdid = household.householdid
        left join 
    subbranch on subbranch.id = improvement.subbranchid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdInvestment(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
    head.name headname,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender,
    TIMESTAMPDIFF(YEAR,head.birthdate,CURDATE()) headage,
    household.latitude,
    household.longitude,
    case
        when investment.entryid is null then 'Хөрөнгө хүлээн аваагүй'
        else 'Хөрөнгө хүлээн авсан'
    end investment,
    investment.assetreceivedid,
    assetreceived.name assetreceivedname,
    investment.assetreceivedtypeid,
    assetreceivedtype.name assetreceivedtypename,
    DATE_FORMAT(investment.investmentdate, '%Y-%m-%d %H:%i:%s') investmentdate,
    investment.note,
    investment.quantity,
    investment.totalprice,
    investment.unitprice
FROM
    household
        left join
    investment on investment.householdid = household.householdid
        left join 
    assetreceived on assetreceived.id = investment.assetreceivedid
        left join 
    assetreceivedtype on assetreceivedtype.id = investment.assetreceivedtypeid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdOthersupport(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
    head.name headname,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender,
    TIMESTAMPDIFF(YEAR,head.birthdate,CURDATE()) headage,
    household.latitude,
    household.longitude,
    case
        when othersupport.entryid is null then 'Дэмжлэг хүлээн аваагүй'
        else 'Дэмжлэг хүлээн авсан'
    end othersupport,
    othersupport.entryid,
    DATE_FORMAT(othersupport.supportdate, '%Y-%m-%d %H:%i:%s') supportdate,
    othersupport.supportreceivedtypeid,
    supportreceivedtype.name supportreceivedtypename,
    othersupport.name othersupportname, 
    othersupport.quantity,
    othersupport.unitprice,
    othersupport.totalprice,
    othersupport.sponsoringorganizationid,
    sponsoringorganization.name sponsoringorganizationname
FROM
    household
        left join
    othersupport on othersupport.householdid = household.householdid
        left join 
    supportreceivedtype on supportreceivedtype.id = othersupport.supportreceivedtypeid
        left join 
    sponsoringorganization on sponsoringorganization.id = othersupport.sponsoringorganizationid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdMediatedactivity(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    household.districtid,
    district.name districtname,
    household.districtid ||'-'|| household.section districtsection,
    household.section,
    COALESCE(coach.name,'Коучид харьяалагдаагүй өрх') coachname,
    household.coachid,
    household.householdgroupid,
    COALESCE(householdgroup.name,'Бүлэгт ороогүй өрх') householdgroupname,
    householdmember.name,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) age,
    head.name headname,
    case
        when head.gender = 0 then 'Эрэгтэй'
        when head.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end headgender,
    TIMESTAMPDIFF(YEAR,head.birthdate,CURDATE()) headage,
    household.latitude,
    household.longitude,
    case
        when mediatedactivity.entryid is null then 'Дэмжлэг хүлээн аваагүй'
        else 'Дэмжлэг хүлээн авсан'
    end mediatedactivity,
    mediatedactivity.entryid,
    DATE_FORMAT(mediatedactivity.mediateddate, '%Y-%m-%d %H:%i:%s') mediateddate,
    mediatedactivity.mediatedservicetypeid,
    mediatedservicetype.name mediatedservicetypename,
    mediatedactivity.intermediaryorganizationid,
    intermediaryorganization.name intermediaryorganizationname,
    mediatedactivity.proxyserviceid,
    proxyservice.name proxyservicename,
    mediatedactivity.memberid,
    servicemember.name servicemember
FROM
    household
        left join
    mediatedactivity on mediatedactivity.householdid = household.householdid
        left join 
    mediatedservicetype on mediatedservicetype.id = mediatedactivity.mediatedservicetypeid
        left join 
    intermediaryorganization on intermediaryorganization.id = mediatedactivity.intermediaryorganizationid
        left join 
    proxyservice on proxyservice.id = mediatedactivity.proxyserviceid
        left join 
    householdmember servicemember on servicemember.memberid = mediatedactivity.memberid
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember head ON head.memberid = household.headmemberid 
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
where household.status = @status");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseholdGenderCount(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(1) count
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid 
where household.status = @status
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(1) count
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
where household.status = @status
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetHouseholdGenderCountDistrict(int status, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(1) count
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
where household.status = @status
and household.districtid = @districtid
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(1) count
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetParticipantGenderCount(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid 
where household.status = @status
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
where household.status = @status
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetParticipantGenderCountDistrict(int status, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
where household.status = @status
and household.districtid = @districtid
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetParticipantAgeCount(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) < 25 or householdmember.birthdate is null THEN 1
        ELSE NULL
    END) age24,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) between 25 and 54 THEN 1
        ELSE NULL
    END) age54,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) > 54 THEN 1
        ELSE NULL
    END) agemax
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid 
where household.status = @status
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) < 25 or householdmember.birthdate is null THEN 1
        ELSE NULL
    END) age24,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) between 25 and 54 THEN 1
        ELSE NULL
    END) age54,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) > 54 THEN 1
        ELSE NULL
    END) agemax
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
where household.status = @status
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetParticipantAgeCountDistrict(int status, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) < 25 or householdmember.birthdate is null THEN 1
        ELSE NULL
    END) age24,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) between 25 and 54 THEN 1
        ELSE NULL
    END) age54,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) > 54 THEN 1
        ELSE NULL
    END) agemax
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
where household.status = @status
and household.districtid = @districtid
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) < 25 or householdmember.birthdate is null THEN 1
        ELSE NULL
    END) age24,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) between 25 and 54 THEN 1
        ELSE NULL
    END) age54,
    COUNT(CASE
        WHEN TIMESTAMPDIFF(YEAR,householdmember.birthdate,CURDATE()) > 54 THEN 1
        ELSE NULL
    END) agemax
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetParticipantDisabledCount(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        inner JOIN
    householdmember ON householdmember.memberid = household.memberid 
where household.status = @status
and householdmember.healthconditionid in (1)
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        inner JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
where household.status = @status
and householdmember.healthconditionid in (2)
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetParticipantDisabledCountDistrict(int status, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        inner JOIN
    householdmember ON householdmember.memberid = household.memberid
where household.status = @status
and household.districtid = @districtid
and householdmember.healthconditionid in (2)
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        inner JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
and householdmember.healthconditionid in (2)
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHouseheadGenderCount(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid 
where household.status = @status
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
        LEFT JOIN
    district ON district.districtid = household.districtid
where household.status = @status
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetHouseheadGenderCountDistrict(int status, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
where household.status = @status
and household.districtid = @districtid
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public MResult GetHousesingleGenderCount(int status)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid 
where household.status = @status
and not exists (select null from householdmember b where b.relationshipid = 2 and b.householdid = household.householdid group by b.householdid having count(1)>0)
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
where household.status = @status
and not exists (select null from householdmember b where b.relationshipid = 2 and b.householdid = household.householdid group by b.householdid having count(1)>0)
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetHousesingleGenderCountDistrict(int status, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
where household.status = @status
and household.districtid = @districtid
and not exists (select null from householdmember b where b.relationshipid = 2 and b.householdid = household.householdid group by b.householdid having count(1)>0)
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(CASE
        WHEN householdmember.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN householdmember.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN householdmember.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
and not exists (select null from householdmember b where b.relationshipid = 2 and b.householdid = household.householdid group by b.householdid having count(1)>0)
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public MResult GetHouseholdWorkingAgeCount(int status, int gender)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) BETWEEN 18 AND 55 
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) BETWEEN 18 AND 55
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetHouseholdWorkingAgeCountDistrict(int status, int gender, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
where household.status = @status
and household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) BETWEEN 18 AND 55 
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) BETWEEN 18 AND 55 
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public MResult GetHouseholdSchoolAgeCount(int status, int gender)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) BETWEEN 6 AND 17 
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) BETWEEN 6 AND 17 
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetHouseholdSchoolAgeCountDistrict(int status, int gender, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
where household.status = @status
and household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) BETWEEN 6 AND 17
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) BETWEEN 6 AND 17 
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public MResult GetHouseholdKindergartenAgeCount(int status, int gender)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) < 6
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
        LEFT JOIN
    district ON district.districtid = household.districtid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) < 6
GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetHouseholdKindergartenAgeCountDistrict(int status, int gender, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
where household.status = @status
and household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) < 6
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    COUNT(CASE
        WHEN member.gender = 0 THEN 1
        ELSE NULL
    END) male,
    COUNT(CASE
        WHEN member.gender = 1 THEN 1
        ELSE NULL
    END) female,
    COUNT(CASE
        WHEN member.gender IS NULL THEN 1
        ELSE NULL
    END) none
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.memberid
        LEFT JOIN
    householdmember member ON member.householdid = household.householdid
        AND member.memberid <> household.memberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
and household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
        AND TIMESTAMPDIFF(YEAR,
        member.birthdate,
        CURDATE()) < 6
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public MResult GetHousemenberAvg(int status, int gender)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    avg(household.numberof) avg
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)
     
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    avg(household.numberof) avg
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
        LEFT JOIN
    district ON district.districtid = household.districtid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)

GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetHousemenberAvgDistrict(int status, int gender, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    avg(household.numberof) avg
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
where household.status = @status
        AND household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    avg(household.numberof) avg
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
        AND household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public MResult GetDisabledAvg(int status, int gender)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    1 rowno,
    - 1 id,
    'Бүгд' name,
    avg(disabled.numberof) avg
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
        left join (select b.householdid, count(1) numberof from householdmember b where b.healthconditionid in (2) group by b.householdid)
    disabled on disabled.householdid = household.householdid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)
     
UNION SELECT 
    2 rowno,
    household.districtid rowno,
    district.name,
    avg(disabled.numberof) avg
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
        left join (select b.householdid, count(1) numberof from householdmember b where b.healthconditionid in (2) group by b.householdid)
    disabled on disabled.householdid = household.householdid
        LEFT JOIN
    district ON district.districtid = household.districtid
WHERE
    household.status = @status
        AND (householdmember.gender = @gender or -1 = @gender)

GROUP BY household.districtid , district.name) tbl order by rowno, id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetDisabledAvgDistrict(int status, int gender, int districtid)
        {
            Hashtable retdata = new Hashtable();

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select * from (SELECT 
    household.section id,
    household.section ||' хороо' name,
    avg(disabled.numberof) avg
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
        left join (select b.householdid, count(1) numberof from householdmember b where b.healthconditionid in (2) group by b.householdid)
    disabled on disabled.householdid = household.householdid
where household.status = @status
        AND household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
GROUP BY household.section) tbl order by id");
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, gender, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("section", result.retdata);

            cmd.CommandText(@"select * from (SELECT 
    household.coachid id,
    COALESCE(coach.name,'Коучгүй') name,
    avg(disabled.numberof) avg
FROM
    household
        LEFT JOIN
    householdmember ON householdmember.memberid = household.headmemberid
        left join (select b.householdid, count(1) numberof from householdmember b where b.healthconditionid in (2) group by b.householdid)
    disabled on disabled.householdid = household.householdid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where household.status = @status
        AND household.districtid = @districtid
        AND (householdmember.gender = @gender or -1 = @gender)
GROUP BY household.coachid, coach.name) tbl order by id");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            retdata.Add("coach", result.retdata);

            return new MResult { retdata = retdata };
        }

    }
}