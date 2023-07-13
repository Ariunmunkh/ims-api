using BaseLibrary.LConnection;
using Connection.Model;
using Infrastructure;
using LConnection.Model;
using System;
using System.Collections;
using System.Data;
using Systems.Models;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public VolunteerRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        #region Volunteer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetVolunteerList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteer.*
FROM
    volunteer
ORDER BY volunteer.firstname");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteer(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteer.*
FROM
    volunteer
WHERE
    volunteer.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteer(Volunteer request)
        {
            MCommand cmd = connector.PopCommand();
            MResult result;
            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteer");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteer
  (id,
familyname,
firstname,
lastname,
gender,
regno,
birthday,
phone,
isvolunteer,
isblooddonor,
bloodgroupid,
educationlevelid,
countryid,
divisionid,
districtid,
address,
isdisabled,
updatedby)
values
  (@id,
@familyname,
@firstname,
@lastname,
@gender,
@regno,
@birthday,
@phone,
@isvolunteer,
@isblooddonor,
@bloodgroupid,
@educationlevelid,
@countryid,
@divisionid,
@districtid,
@address,
@isdisabled,
@updatedby) 
ON DUPLICATE KEY UPDATE 
familyname=@familyname,
firstname=@firstname,
lastname=@lastname,
gender=@gender,
regno=@regno,
birthday=@birthday,
phone=@phone,
isvolunteer=@isvolunteer,
isblooddonor=@isblooddonor,
bloodgroupid=@bloodgroupid,
educationlevelid=@educationlevelid,
countryid=@countryid,
divisionid=@divisionid,
districtid=@districtid,
address=@address,
isdisabled=@isdisabled,
updatedby=@updatedby, 
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@familyname", DbType.String, request.familyname, ParameterDirection.Input);
            cmd.AddParam("@firstname", DbType.String, request.firstname, ParameterDirection.Input);
            cmd.AddParam("@lastname", DbType.String, request.lastname, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, request.gender, ParameterDirection.Input);
            cmd.AddParam("@regno", DbType.String, request.regno, ParameterDirection.Input);
            cmd.AddParam("@birthday", DbType.DateTime, request.birthday, ParameterDirection.Input);
            cmd.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
            cmd.AddParam("@isvolunteer", DbType.Boolean, request.isvolunteer, ParameterDirection.Input);
            cmd.AddParam("@isblooddonor", DbType.Boolean, request.isblooddonor, ParameterDirection.Input);
            cmd.AddParam("@bloodgroupid", DbType.Int32, request.bloodgroupid, ParameterDirection.Input);
            cmd.AddParam("@educationlevelid", DbType.Int32, request.educationlevelid, ParameterDirection.Input);
            cmd.AddParam("@countryid", DbType.Int32, request.countryid, ParameterDirection.Input);
            cmd.AddParam("@divisionid", DbType.Int32, request.divisionid, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, request.districtid, ParameterDirection.Input);
            cmd.AddParam("@address", DbType.String, request.address, ParameterDirection.Input);
            cmd.AddParam("@isdisabled", DbType.Boolean, request.isdisabled, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            result = connector.Execute(ref cmd, true);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = new Hashtable { { "volunteerid", request.id } } };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteer(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from volunteer where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region EmergencyContact
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetEmergencyContactList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
relationship.name relationship,
    emergencycontact.*
FROM
    emergencycontact
left join relationship on relationship.id = emergencycontact.relationshipid
where emergencycontact.volunteerid = @id
ORDER BY emergencycontact.firstname");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetEmergencyContact(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    emergencycontact.*
FROM
    emergencycontact
WHERE
    emergencycontact.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetEmergencyContact(EmergencyContact request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from emergencycontact");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO emergencycontact
  (id,
volunteerid,
relationshipid,
firstname,
phone,
updatedby)
values
  (@id,
@volunteerid,
@relationshipid,
@firstname,
@phone,
@updatedby) 
ON DUPLICATE KEY UPDATE 
relationshipid=@relationshipid,
firstname=@firstname,
phone=@phone,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@relationshipid", DbType.Int32, request.relationshipid, ParameterDirection.Input);
            cmd.AddParam("@firstname", DbType.String, request.firstname, ParameterDirection.Input);
            cmd.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteEmergencyContact(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from emergencycontact where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region VolunteerVoluntaryWork
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerVoluntaryWorkList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
voluntarywork.name voluntarywork,
    volunteervoluntarywork.*
FROM volunteervoluntarywork
left join voluntarywork 
on voluntarywork.id = volunteervoluntarywork.voluntaryworkid
where volunteervoluntarywork.volunteerid = @id
ORDER BY volunteervoluntarywork.updated");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerVoluntaryWork(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteervoluntarywork.*
FROM
    volunteervoluntarywork
WHERE
    volunteervoluntarywork.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteerVoluntaryWork(VolunteerVoluntaryWork request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteervoluntarywork");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteervoluntarywork
  (id,
volunteerid,
voluntaryworkid,
duration,
voluntaryworkdate,
note,
updatedby)
values
  (@id,
@volunteerid,
@voluntaryworkid,
@duration,
@voluntaryworkdate,
@note,
@updatedby) 
ON DUPLICATE KEY UPDATE 
voluntaryworkid=@voluntaryworkid,
duration=@duration,
voluntaryworkdate=@voluntaryworkdate,
note=@note,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@voluntaryworkid", DbType.Int32, request.voluntaryworkid, ParameterDirection.Input);
            cmd.AddParam("@duration", DbType.Int32, request.duration, ParameterDirection.Input);
            cmd.AddParam("@voluntaryworkdate", DbType.DateTime, request.voluntaryworkdate, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteerVoluntaryWork(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from volunteervoluntarywork where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region VolunteerTraining
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerTrainingList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
training.name training,
    volunteertraining.*
FROM
    volunteertraining
left join training on training.id = volunteertraining.trainingid
where volunteertraining.volunteerid = @id
ORDER BY volunteertraining.updated");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerTraining(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteertraining.*
FROM
    volunteertraining
WHERE
    volunteertraining.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteerTraining(VolunteerTraining request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteertraining");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteertraining
  (id,
volunteerid,
trainingid,
levelid,
trainingdate,
location,
duration,
note,
updatedby)
values
  (@id,
@volunteerid,
@trainingid,
@levelid,
@trainingdate,
@location,
@duration,
@note,
@updatedby) 
ON DUPLICATE KEY UPDATE 
trainingid=@trainingid,
levelid=@levelid,
trainingdate=@trainingdate,
location=@location,
duration=@duration,
note=@note,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@trainingid", DbType.Int32, request.trainingid, ParameterDirection.Input);
            cmd.AddParam("@levelid", DbType.Int32, request.levelid, ParameterDirection.Input);
            cmd.AddParam("@trainingdate", DbType.DateTime, request.trainingdate, ParameterDirection.Input);
            cmd.AddParam("@location", DbType.String, request.location, ParameterDirection.Input);
            cmd.AddParam("@duration", DbType.Int32, request.duration, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteerTraining(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from volunteertraining where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region VolunteerSkills
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerSkillsList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteerskills.*
FROM
    volunteerskills
where volunteerskills.volunteerid = @id
ORDER BY volunteerskills.updated");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerSkills(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteerskills.*
FROM
    volunteerskills
WHERE
    volunteerskills.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteerSkills(VolunteerSkills request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteerskills");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteerskills
  (id,
volunteerid,
skillsid,
skillslevelid,
note,
updatedby)
values
  (@id,
@volunteerid,
@skillsid,
@skillslevelid,
@note,
@updatedby) 
ON DUPLICATE KEY UPDATE 
skillsid=@skillsid,
skillslevelid=@skillslevelid,
note=@note,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@skillsid", DbType.Int32, request.skillsid, ParameterDirection.Input);
            cmd.AddParam("@skillslevelid", DbType.Int32, request.skillslevelid, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteerSkills(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from volunteerskills where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region VolunteerMembership
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerMembershipList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteermembership.*
FROM
    volunteermembership
where volunteermembership.volunteerid = @id
ORDER BY volunteermembership.updated");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerMembership(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteermembership.*
FROM
    volunteermembership
WHERE
    volunteermembership.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteerMembership(VolunteerMembership request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteermembership");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteermembership
  (id,
volunteerid,
membershipid,
begindate,
enddate,
note,
updatedby)
values
  (@id,
@volunteerid,
@membershipid,
@begindate,
@enddate,
@note,
@updatedby) 
ON DUPLICATE KEY UPDATE 
membershipid=@membershipid,
begindate=@begindate,
enddate=@enddate,
note=@note,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@membershipid", DbType.Int32, request.membershipid, ParameterDirection.Input);
            cmd.AddParam("@begindate", DbType.DateTime, request.begindate, ParameterDirection.Input);
            cmd.AddParam("@enddate", DbType.DateTime, request.enddate, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteerMembership(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from volunteermembership where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region VolunteerEducation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerEducationList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
educationlevel.name educationlevel,
    volunteereducation.*
FROM
    volunteereducation
left join educationlevel on educationlevel.id = volunteereducation.educationlevelid
where volunteereducation.volunteerid = @id
ORDER BY volunteereducation.updated desc");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerEducation(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteereducation.*
FROM
    volunteereducation
WHERE
    volunteereducation.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteerEducation(VolunteerEducation request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteereducation");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteereducation
  (id,
volunteerid,
educationlevelid,
schoolname,
isend,
classlevel,
skill,
updatedby)
values
  (@id,
@volunteerid,
@educationlevelid,
@schoolname,
@isend,
@classlevel,
@skill,
@updatedby) 
ON DUPLICATE KEY UPDATE 
educationlevelid=@educationlevelid,
schoolname=@schoolname,
isend=@isend,
classlevel=@classlevel,
skill=@skill,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@educationlevelid", DbType.Int32, request.educationlevelid, ParameterDirection.Input);
            cmd.AddParam("@schoolname", DbType.String, request.schoolname, ParameterDirection.Input);
            cmd.AddParam("@isend", DbType.Boolean, request.isend, ParameterDirection.Input);
            cmd.AddParam("@classlevel", DbType.Int32, request.classlevel, ParameterDirection.Input);
            cmd.AddParam("@skill", DbType.String, request.skill, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteerEducation(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from volunteereducation where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region VolunteerEmployment
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerEmploymentList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteeremployment.*
FROM
    volunteeremployment
where volunteeremployment.volunteerid = @id
ORDER BY volunteeremployment.updated desc");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerEmployment(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteeremployment.*
FROM
    volunteeremployment
WHERE
    volunteeremployment.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteerEmployment(VolunteerEmployment request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteeremployment");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteeremployment
  (id,
volunteerid,
employment,
company,
job,
begindate,
enddate,
note,
updatedby)
values
  (@id,
@volunteerid,
@employment,
@company,
@job,
@begindate,
@enddate,
@note,
@updatedby) 
ON DUPLICATE KEY UPDATE 
employment=@employment,
company=@company,
job=@job,
begindate=@begindate,
enddate=@enddate,
note=@note,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@employment", DbType.String, request.employment, ParameterDirection.Input);
            cmd.AddParam("@company", DbType.String, request.company, ParameterDirection.Input);
            cmd.AddParam("@job", DbType.String, request.job, ParameterDirection.Input);
            cmd.AddParam("@begindate", DbType.DateTime, request.begindate, ParameterDirection.Input);
            cmd.AddParam("@enddate", DbType.DateTime, request.enddate, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteerEmployment(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from volunteeremployment where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region VolunteerLanguages
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerLanguagesList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
languages.name languages,
    volunteerlanguages.*
FROM
    volunteerlanguages
left join languages on languages.id = volunteerlanguages.languageid
where volunteerlanguages.volunteerid = @id
ORDER BY volunteerlanguages.updated desc");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerLanguages(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteerlanguages.*
FROM
    volunteerlanguages
WHERE
    volunteerlanguages.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteerLanguages(VolunteerLanguages request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteerlanguages");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteerlanguages
  (id,
volunteerid,
languageid,
levelid,
studyyear,
isscore,
testname,
testscore,
note,
updatedby)
values
  (@id,
@volunteerid,
@languageid,
@levelid,
@studyyear,
@isscore,
@testname,
@testscore,
@note,
@updatedby) 
ON DUPLICATE KEY UPDATE 
languageid=@languageid,
levelid=@levelid,
studyyear=@studyyear,
isscore=@isscore,
testname=@testname,
testscore=@testscore,
note=@note,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@languageid", DbType.Int32, request.languageid, ParameterDirection.Input);
            cmd.AddParam("@levelid", DbType.Int32, request.levelid, ParameterDirection.Input);
            cmd.AddParam("@studyyear", DbType.Int32, request.studyyear, ParameterDirection.Input);
            cmd.AddParam("@isscore", DbType.Boolean, request.isscore, ParameterDirection.Input);
            cmd.AddParam("@testname", DbType.String, request.testname, ParameterDirection.Input);
            cmd.AddParam("@testscore", DbType.Int32, request.testscore, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteerLanguages(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from volunteerlanguages where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

        #region VolunteerAssistance
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerAssistanceList(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteerassistance.*
FROM
    volunteerassistance
where volunteerassistance.volunteerid = @id
ORDER BY volunteerassistance.firstname");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetVolunteerAssistance(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteerassistance.*
FROM
    volunteerassistance
WHERE
    volunteerassistance.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetVolunteerAssistance(VolunteerAssistance request)
        {
            MCommand cmd = connector.PopCommand();

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteerassistance");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"INSERT INTO volunteerassistance
  (id,
volunteerid,
assistanceid,
projectname,
assistancedate,
note,
updatedby)
values
  (@id,
@volunteerid,
@assistanceid,
@projectname,
@assistancedate,
@note,
@updatedby) 
ON DUPLICATE KEY UPDATE 
assistanceid=@assistanceid,
projectname=@projectname,
assistancedate=@assistancedate,
note=@note,
updatedby=@updatedby,
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@assistanceid", DbType.Int32, request.assistanceid, ParameterDirection.Input);
            cmd.AddParam("@projectname", DbType.String, request.projectname, ParameterDirection.Input);
            cmd.AddParam("@assistancedate", DbType.DateTime, request.assistancedate, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteVolunteerAssistance(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from VolunteerAssistance where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
        #endregion

    }
}