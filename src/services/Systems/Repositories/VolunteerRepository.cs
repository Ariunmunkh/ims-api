﻿using BaseLibrary.LConnection;
using Connection.Model;
using Infrastructure;
using LConnection.Model;
using System;
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

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from volunteer");
                MResult result = connector.Execute(ref cmd, false);
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
countryid=@countryid,
divisionid=@divisionid,
districtid=@districtid,
address=@address,
isdisabled=@isdisabled,
updatedby=@updatedby) 
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
            cmd.AddParam("@countryid", DbType.Int32, request.countryid, ParameterDirection.Input);
            cmd.AddParam("@divisionid", DbType.Int32, request.divisionid, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, request.districtid, ParameterDirection.Input);
            cmd.AddParam("@address", DbType.String, request.address, ParameterDirection.Input);
            cmd.AddParam("@isdisabled", DbType.Boolean, request.isdisabled, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
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
        /// <returns></returns>
        public MResult GetEmergencyContactList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    emergencycontact.*
FROM
    emergencycontact
ORDER BY emergencycontact.firstname");
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
updatedby=@updatedby) 
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
        /// <returns></returns>
        public MResult GetVolunteerVoluntaryWorkList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteervoluntarywork.*
FROM
    volunteervoluntarywork
ORDER BY volunteervoluntarywork.updated");
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
updatedby=@updatedby) 
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
        /// <returns></returns>
        public MResult GetVolunteerTrainingList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteertraining.*
FROM
    volunteertraining
ORDER BY volunteertraining.updated");
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
trainingdate,
location,
duration,
note,
updatedby)
values
  (@id,
@volunteerid,
@trainingid,
@trainingdate,
@location,
@duration,
@note,
@updatedby) 
ON DUPLICATE KEY UPDATE 
trainingid=@trainingid,
trainingdate=@trainingdate,
location=@location,
duration=@duration,
note=@note,
updatedby=@updatedby) 
updated = current_timestamp");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@volunteerid", DbType.Int32, request.volunteerid, ParameterDirection.Input);
            cmd.AddParam("@trainingid", DbType.Int32, request.trainingid, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GetVolunteerSkillsList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteerskills.*
FROM
    volunteerskills
ORDER BY volunteerskills.updated");
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
updatedby=@updatedby) 
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
        /// <returns></returns>
        public MResult GetVolunteerMembershipList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteermembership.*
FROM
    volunteermembership
ORDER BY volunteermembership.updated");
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
updatedby=@updatedby) 
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

        #region VolunteerAssistance
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetVolunteerAssistanceList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    volunteerassistance.*
FROM
    volunteerassistance
ORDER BY volunteerassistance.firstname");
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
updatedby=@updatedby) 
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