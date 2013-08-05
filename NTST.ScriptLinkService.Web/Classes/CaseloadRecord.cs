using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTST.ScriptLinkService.Web
{
    public class CaseloadRecord
    {
        public String uniqueIdentifier;
        public String PATID;
        public String Episode;
        public String Comments;
        public QuestionEnum Question;
        public CaseloadTypeEnum CaseloadType;
        public DateTime DateOfAssignment;
        public DateTime TimeOfAssignment;
        public String AddClient;
        public String RemoveClient;
        public RecordActionEnum RecordAction;

        public String ToString()
        {
            return "<optiondata>" +
                                            "<PATID>" + this.PATID + "</PATID>" +
                                            "<EPISODE_NUMBER>" + this.Episode + "</EPISODE_NUMBER>" +
                                            "<SYSTEM.PM_OTHER_CASELOAD>" +
                //"<rows.reference>" +
                //    "<unique_identifier>" + this.uniqueIdentifier + "</unique_identifier>" +
                //    "<add_edit_delete>" + (char)this.RecordAction + "</add_edit_delete>" +
                //"</rows.reference>" +
                //"<add_client>" + this.AddClient + "</add_client>" +
                                                "<remove_client>" + this.RemoveClient + "</remove_client>" +
                                                "<date_of_assignment>" + this.DateOfAssignment.ToString("yyyy-MM-dd") + "</date_of_assignment>" +
                                                "<time_of_assignment>" + this.TimeOfAssignment.ToString("hh:mm tt") + "</time_of_assignment>" +
                                                "<caseload_type>" + (int)this.CaseloadType + "</caseload_type>" +
                                                "<comments>" + this.Comments + "</comments>" +
                                                "<Question>" + (int)this.Question + "</Question>" +
                                            "</SYSTEM.PM_OTHER_CASELOAD>" +
                                        "</optiondata>";
        }
        public String GetDataString()
        {
            String returnString = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                                "<option>" +
                                        "<optionidentifier>USER26</optionidentifier>" +
                                        "<optiondata>" +
                                            "<PATID>" + this.PATID + "</PATID>" +
                                            "<EPISODE_NUMBER>" + this.Episode + "</EPISODE_NUMBER>" +
                                            "<SYSTEM.PM_OTHER_CASELOAD>" +
                //"<rows.reference>" +
                //    "<unique_identifier>" + this.uniqueIdentifier + "</unique_identifier>" +
                //    "<add_edit_delete>" + (char)this.RecordAction + "</add_edit_delete>" +
                //"</rows.reference>" +
                //"<add_client>" + this.AddClient + "</add_client>" +
                                                "<remove_client>" + this.RemoveClient + "</remove_client>" +
                                                "<date_of_assignment>" + this.DateOfAssignment.ToString("yyyy-MM-dd") + "</date_of_assignment>" +
                                                "<time_of_assignment>" + this.TimeOfAssignment.ToString("hh:mm tt") + "</time_of_assignment>" +
                                                "<caseload_type>" + (int)this.CaseloadType + "</caseload_type>" +
                                                "<comments>" + this.Comments + "</comments>" +
                                                "<Question>" + (int)this.Question + "</Question>" +
                                            "</SYSTEM.PM_OTHER_CASELOAD>" +
                                        "</optiondata>" +
                                    "</option>";
            return returnString;
        }
    }
    public enum RecordActionEnum
    {
        Add = 'A',
        Edit = 'E',
        Delete = 'D'
    }
    public enum QuestionEnum
    {
        AddPractitioner = 1,
        RemovePractitioner = 2
    }
    public enum CaseloadTypeEnum
    {
        ARNP = 1,
        BehavioralTech = 2,
        CaseManager = 3,
        JobCoach = 4,
        Nurse = 5,
        ParentAdvocate = 6,
        PeerAdvocate = 7,
        PSRCounselor = 8,
        Psychiatrist = 9,
        ResidentialTech = 10,
        Therapist = 11
    }
}