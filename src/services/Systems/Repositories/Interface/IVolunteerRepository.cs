using Connection.Model;
using Systems.Models;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVolunteerRepository
    {
        #region Volunteer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetVolunteerList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetVolunteer(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetVolunteer(Volunteer request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteVolunteer(int id);
        #endregion

        #region EmergencyContact
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetEmergencyContactList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetEmergencyContact(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetEmergencyContact(EmergencyContact request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteEmergencyContact(int id);
        #endregion

        #region VolunteerVoluntaryWork
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetVolunteerVoluntaryWorkList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetVolunteerVoluntaryWork(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetVolunteerVoluntaryWork(VolunteerVoluntaryWork request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteVolunteerVoluntaryWork(int id);
        #endregion

        #region VolunteerTraining
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetVolunteerTrainingList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetVolunteerTraining(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetVolunteerTraining(VolunteerTraining request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteVolunteerTraining(int id);
        #endregion

        #region VolunteerSkills
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetVolunteerSkillsList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetVolunteerSkills(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetVolunteerSkills(VolunteerSkills request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteVolunteerSkills(int id);
        #endregion

        #region VolunteerMembership
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetVolunteerMembershipList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetVolunteerMembership(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetVolunteerMembership(VolunteerMembership request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteVolunteerMembership(int id);
        #endregion

        #region VolunteerAssistance
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetVolunteerAssistanceList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetVolunteerAssistance(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetVolunteerAssistance(VolunteerAssistance request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteVolunteerAssistance(int id);
        #endregion

    }
}