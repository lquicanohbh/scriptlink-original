using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using NTST.ScriptLinkService.Objects;
using NTST.ScriptLinkService.Web.Repositories;
using NTST.ScriptLinkService.Web.Classes;
using System.Threading;

namespace NTST.ScriptLinkService.Web
{
    /// <summary>
    /// Summary description for ScriptLinkService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ScriptLinkServiceComplete : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetVersion()
        {
            return "Version 1.0";
        }
        [WebMethod]
        public OptionObject RunScript(OptionObject optionObject, String scriptName)
        {
            var currentTime = DateTime.Now.ToString("hh:mm tt");
            OptionObject returnOptionObject = new OptionObject();
            //initialize the option object
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;

            switch (scriptName.Split(',')[0])
            {
                //case "ViewOptionObject":
                //    //create a page to view the option object and launch the URL
                //    var fileName = CreateHTMLPage.GetHTMLPage(optionObject);
                //    returnOptionObject.ErrorCode = 5;
                //    returnOptionObject.ErrorMesg = "http://" + Context.Request.Url.Host + System.Web.VirtualPathUtility.GetDirectory(Context.Request.RawUrl) + fileName;
                //    break;
                case "MakeTimeRequired":
                    returnOptionObject = MakeTimeRequired(optionObject, false);
                    break;
                case "MakeTimeRequiredInpatient":
                    returnOptionObject = MakeTimeRequired(optionObject, true);
                    break;
                case "SourceOfAdmission":
                    returnOptionObject = SourceOfAdmission(optionObject);
                    break;
                case "SocialSecurityDefault":
                    returnOptionObject = SocialSecurityDefault(optionObject, true);
                    break;
                case "SocialSecurityDefaultDemo":
                    returnOptionObject = SocialSecurityDefault(optionObject, false);
                    break;
                case "CheckDiagnosis":
                    returnOptionObject = CheckDiagnosis(optionObject, scriptName);
                    break;
                case "Discharge":
                    returnOptionObject = Discharge(optionObject);
                    break;
                case "CheckSubscriberPolicy":
                    returnOptionObject = CheckSubscriberPolicy(optionObject);
                    break;
                case "CheckSubscriberMedicaid":
                    returnOptionObject = CheckSubscriberMedicaid(optionObject);
                    break;
                case "CheckSubscriberMedicare":
                    returnOptionObject = CheckSubscriberMedicare(optionObject);
                    break;
                case "CheckNoteType":
                    returnOptionObject = CheckNoteType(optionObject);
                    break;
                case "DisabilityAlert":
                    returnOptionObject = DisabilityAlert(optionObject);
                    break;
                case "TreatmentPlan":
                    returnOptionObject = TreatmentPlan(optionObject);
                    break;
                case "ServiceNeedsIntensityAssessment":
                    returnOptionObject = ServiceNeedsIntensityAssessment(optionObject);
                    break;
                case "DefaultMGAFScore":
                    returnOptionObject = DefaultMGAFScore(optionObject);
                    break;
                case "ChartLocationOptionLoad":
                    returnOptionObject = ChartLocation(optionObject, true);
                    break;
                case "ChartLocation":
                    returnOptionObject = ChartLocation(optionObject, false);
                    break;
                case "MapReferral":
                    returnOptionObject = MapReferral(optionObject, false);
                    break;
                case "ReferralDirections":
                    returnOptionObject = MapReferral(optionObject, true);
                    break;
                case "LoadDisclaimers":
                    returnOptionObject = LoadDisclaimers(optionObject);
                    break;
                case "DefaultCheckinInventory":
                    returnOptionObject = DefaultPractitioner(optionObject, true);
                    break;
                case "DefaultCheckoutInventory":
                    returnOptionObject = DefaultPractitioner(optionObject, false);
                    break;
                case "AppointmentStatus":
                    returnOptionObject = AppointmentStatus(optionObject, true);
                    break;
                case "StaffAppointmentStatus":
                    returnOptionObject = AppointmentStatus(optionObject, false);
                    break;
                case "EmailAuthorizationNotification":
                    returnOptionObject = EmailAuthorizationNotification(optionObject);
                    break;
                case "CheckFSROutcome":
                    returnOptionObject = CheckFSROutcome(optionObject);
                    break;
                case "CheckFSRDraft":
                    returnOptionObject = CheckFSRDraft(optionObject);
                    break;
                case "CheckFSRStatus":
                    returnOptionObject = CheckFSRStatus(optionObject);
                    break;
                case "CheckDaysWorked":
                    returnOptionObject = CheckDaysWorked(optionObject);
                    break;
                case "EmailVoidPN":
                    returnOptionObject = EmailVoidPN(optionObject, currentTime);
                    break;
                case "AddDurationOutpatient":
                    returnOptionObject = AddDuration(optionObject, false);
                    break;
                case "AddDurationInpatient":
                    returnOptionObject = AddDuration(optionObject, true);
                    break;
                case "MakeEpisodeRequired":
                    returnOptionObject = MakeEpisodeRequired(optionObject);
                    break;
                case "UpdateUser":
                    returnOptionObject = UpdateUser(optionObject);
                    break;
                case "DefaultAssessmentInfo":
                    returnOptionObject = DefaultAssessmentInfo(optionObject);
                    break;
                case "DefaultSubstanceAbuse":
                    returnOptionObject = DefaultSubstanceAbuse(optionObject);
                    break;
                case "DefaultDiagnosticAssessment":
                    returnOptionObject = DefaultDiagnosticAssessment(optionObject);
                    break;
                case "DefaultResidentialCaregiver":
                    returnOptionObject = DefaultResidentialCaregiver(optionObject);
                    break;
                case "DefaultCrisisServices":
                    returnOptionObject = DefaultCrisisServices(optionObject);
                    break;
                case "DefaultPsychosocial":
                    returnOptionObject = DefaultPsychosocial(optionObject);
                    break;
                case "RemoveCaseload":
                    returnOptionObject = RemoveCaseload(optionObject);
                    break;
                case "CheckOverlapPsyEval":
                    returnOptionObject = CheckOverlap(optionObject, "PsyEval");
                    break;
                case "CheckOverlapOPN":
                    returnOptionObject = CheckOverlap(optionObject, "OPN");
                    break;
                case "CheckOverlapPsyHist":
                    returnOptionObject = CheckOverlap(optionObject, "PsyHist");
                    break;
                case "CheckOverlapTXPN":
                    returnOptionObject = CheckOverlap(optionObject, "TXPN");
                    break;
                case "CheckOverlapMedNote":
                    returnOptionObject = CheckOverlap(optionObject, "MedNote");
                    break;
                case "AddDurationAndTime":
                    returnOptionObject = AddDurationAndTime(optionObject, scriptName);
                    break;
                case "Required":
                    returnOptionObject = MakeFieldsRequired(optionObject, scriptName);
                    break;
                case "MakeSubscriberPolicyRequired":
                    returnOptionObject = MakeSubscriberPolicyRequired(optionObject);
                    break;
                case "ProgramTransfer":
                    returnOptionObject = ProgramTransfer(optionObject, scriptName);
                    break;
                case "PreAdmitCompleted":
                    returnOptionObject = PreAdmitCompleted(optionObject, scriptName);
                    break;
                case "AppointmentEditDetails":
                    returnOptionObject = AppointmentEditDetails(optionObject);
                    break;
                default:
                    break;
            }
            //}
            return returnOptionObject;
        }
        private OptionObject PreAdmitCompleted(OptionObject optionObject, string scriptName)
        {
            var returnOptionObject = new OptionObject();
            var Client = getClientInfoByClientEpisode(optionObject.EntityID, optionObject.EpisodeNumber.ToString());
            var westBrowardPrograms = ConfigurationManager.AppSettings["WestBrowardPrograms"].ToString().Split(',').ToList();
            var housingPrograms = ConfigurationManager.AppSettings["HousingPrograms"].ToString().Split(',').ToList();
            if (Client != null)
            {
                if (westBrowardPrograms.Contains(Client.ProgramCode))
                {
                    sendEmail(
                        ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                        "Pre-admit/Admit for (" + Client.ProgramCode + ") " + Client.ProgramValue + " completed.",
                        Client.ToString(),
                        new List<string> { ConfigurationManager.AppSettings["SMTPCreateSWTicket"].ToString() },
                        new List<string>(),
                        ConfigurationManager.AppSettings["westBrowardAdmitTicketCreation"].ToString().Split(',').ToList());
                }
                else if (housingPrograms.Contains(Client.ProgramCode))
                {
                    sendEmail(
                        ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                        "Pre-admit/Admit for (" + Client.ProgramCode + ") " + Client.ProgramValue + " completed.",
                        Client.ToString(),
                        new List<string> { ConfigurationManager.AppSettings["SMTPCreateSWTicket"].ToString() },
                        new List<string>(),
                        ConfigurationManager.AppSettings["housingAdmitTicketCreation"].ToString().Split(',').ToList());
                }
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;
        }
        private Client getClientInfoByClientEpisode(string ClientId, string EpisodeNumber)
        {
            var Client = new Client();
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            #region commandText
            var commandText = "SELECT TOP(1) epa.PATID, " +
                                "epa.patient_name," +
                                "epa.EPISODE_NUMBER," +
                                "epa.c_date_of_birth," +
                                "epa.patient_gender_code," +
                                "epa.patient_gender_value," +
                                "epa.program_code," +
                                "epa.program_value," +
                                "epa.preadmit_admission_date," +
                                "epa.date_of_discharge," +
                                "epa.admit_practitioner_id," +
                                "epa.admit_practitioner_name," +
                                "epa.c_ss_number," +
                                "demo.patient_add_street_1," +
                                "demo.patient_add_street_2," +
                                "demo.patient_add_zipcode," +
                                "demo.patient_add_city," +
                                "demo.patient_add_county_code," +
                                "demo.patient_add_county_value," +
                                "demo.patient_add_state_code," +
                                "demo.patient_add_state_value," +
                                "demo.patient_home_phone," +
                                "demo.patient_work_phone," +
                                "demo.marital_status_code," +
                                "demo.marital_status_value," +
                                "demo.education_code," +
                                "demo.education_value," +
                                "demo.employment_status_code," +
                                "demo.employment_status_value," +
                                "fin.fin_invest_eff_date," +
                                "fin.medicare_number," +
                                "fin.medicaid_number," +
                                "fin.bho_code," +
                                "fin.bho_value," +
                                "fin.mco_code," +
                                "fin.mco_value," +
                                "fin.sliding_fee_income," +
                                "fin.sliding_fee_num_of_depend," +
                                "fin.sliding_fee_family_size " +
                                "FROM SYSTEM.view_episode_summary_admit epa " +
                                "INNER JOIN SYSTEM.patient_current_demographics demo " +
                                "ON epa.PATID = demo.PATID " +
                                "AND epa.FACILITY = demo.FACILITY " +
                                "LEFT OUTER JOIN SYSTEM.call_intake_financial_inv fin " +
                                "ON epa.PATID = fin.PATID " +
                                "AND epa.FACILITY = fin.FACILITY " +
                                "WHERE epa.PATID=? AND epa.EPISODE_NUMBER=?" +
                                "ORDER BY fin.fin_invest_eff_date DESC";
            #endregion
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        dbcommand.Parameters.Add(new OdbcParameter("PATID", ClientId));
                        dbcommand.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", EpisodeNumber));
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                #region ReadInValues
                                Client.ClientId = reader["PATID"].ToString();
                                Client.ClientName = reader["patient_name"].ToString();
                                Client.EpisodeNumber = reader["EPISODE_NUMBER"].ToString();
                                Client.DateOfBirth = getNullableDatetime(reader, "c_date_of_birth");
                                Client.GenderCode = reader["patient_gender_code"].ToString();
                                Client.GenderValue = reader["patient_gender_value"].ToString();
                                Client.ProgramCode = reader["program_code"].ToString();
                                Client.ProgramValue = reader["program_value"].ToString();
                                Client.AdmitDate = getNullableDatetime(reader, "preadmit_admission_date");
                                Client.DischargeDate = getNullableDatetime(reader, "date_of_discharge");
                                Client.AdmittingPractitionerId = reader["admit_practitioner_id"].ToString();
                                Client.AdmittingPractitionerName = reader["admit_practitioner_name"].ToString();
                                Client.SocialSecurityNumber = reader["c_ss_number"].ToString();
                                Client.Address1 = reader["patient_add_street_1"].ToString();
                                Client.Address2 = reader["patient_add_street_2"].ToString();
                                Client.ZipCode = reader["patient_add_zipcode"].ToString();
                                Client.City = reader["patient_add_city"].ToString();
                                Client.CountyCode = reader["patient_add_county_code"].ToString();
                                Client.CountyValue = reader["patient_add_county_value"].ToString();
                                Client.StateCode = reader["patient_add_state_code"].ToString();
                                Client.StateValue = reader["patient_add_state_value"].ToString();
                                Client.HomePhone = reader["patient_home_phone"].ToString();
                                Client.WorkPhone = reader["patient_work_phone"].ToString();
                                Client.MaritalStatusCode = reader["marital_status_code"].ToString();
                                Client.MaritalStatusValue = reader["marital_status_value"].ToString();
                                Client.EducationCode = reader["education_code"].ToString();
                                Client.EducationValue = reader["education_value"].ToString();
                                Client.EmploymentStatusCode = reader["employment_status_code"].ToString();
                                Client.EmploymentStatusValue = reader["employment_status_value"].ToString();
                                Client.FinancialInvestigationDate = getNullableDatetime(reader, "fin_invest_eff_date");
                                Client.FinancialInvestigationMedicareNumber = reader["medicare_number"].ToString();
                                Client.FinancialInvestigationMedicaidNumber = reader["medicaid_number"].ToString();
                                Client.CommercialInsuranceCode = reader["bho_code"].ToString();
                                Client.CommercialInsuranceValue = reader["bho_value"].ToString();
                                Client.ManagedCareOrganizationCode = reader["mco_code"].ToString();
                                Client.ManagedCareOrganizationValue = reader["mco_value"].ToString();
                                Client.SlidingFeeScaleIncome = reader["sliding_fee_income"].ToString();
                                Client.SlidingFeeScaleNumberOfDependents = reader["sliding_fee_num_of_depend"].ToString();
                                Client.SlidingFeeScaleFamilySize = reader["sliding_fee_family_size"].ToString();
                                #endregion
                            }
                            else
                            {
                                Client = null;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }
            return Client;
        }
        private DateTime? getNullableDatetime(OdbcDataReader reader, string columnName)
        {
            int x = reader.GetOrdinal(columnName);
            return reader.IsDBNull(x) ? (DateTime?)null : reader.GetDateTime(x);
        }
        private OptionObject ProgramTransfer(OptionObject optionObject, string scriptName)
        {
            var returnOptionObject = new OptionObject();
            var clientField = new FieldObject { FieldNumber = "8" };
            var episodeField = new FieldObject { FieldNumber = "8.01" };
            var currentProgramField = new FieldObject { FieldNumber = "7.99" };
            var currentTreatmentSettingField = new FieldObject { FieldNumber = "8.02" };
            var programField = new FieldObject { FieldNumber = "8.03" };
            var treatmentSettingField = new FieldObject { FieldNumber = "8.04" };
            var treatmentServiceField = new FieldObject { FieldNumber = "8.05" };
            var rrgField = new FieldObject { FieldNumber = "8.06" };
            var dateField = new FieldObject { FieldNumber = "7.97" };
            var transferTypeField = new FieldObject { FieldNumber = "7.98" };
            var fields = new List<FieldObject>();
            var program = new Program();

            try
            {
                switch (scriptName.Split(',')[1])
                {
                    case "EpisodeField":
                        program = getProgramInfoByClientEpisode(optionObject.EntityID,
                                optionObject.Forms[0].CurrentRow.Fields.First(f => f.FieldNumber.Equals(episodeField.FieldNumber)).FieldValue);
                        currentProgramField.FieldValue = program.ProgramCode;
                        currentTreatmentSettingField.FieldValue = program.TreatmentSettingCode;
                        fields.Add(currentProgramField);
                        fields.Add(currentTreatmentSettingField);
                        fields.ForEach(f => f.Enabled = "0");
                        programField.Enabled = "1";
                        fields.Add(programField);
                        break;
                    case "ProgramField":
                        program = getProgramInfoByProgramId(optionObject.Forms[0].CurrentRow.Fields.First(f => f.FieldNumber.Equals(programField.FieldNumber)).FieldValue);
                        treatmentSettingField.FieldValue = program.TreatmentSettingCode;
                        treatmentServiceField.FieldValue = program.TreatmentServiceCode;
                        rrgField.FieldValue = program.RRGCode;
                        fields.Add(treatmentSettingField);
                        fields.Add(treatmentServiceField);
                        fields.Add(rrgField);
                        fields.ForEach(f => f.Enabled = "0");
                        break;
                    case "OnLoad":
                        clientField.FieldValue = optionObject.EntityID;
                        fields.Add(clientField);
                        fields.Add(currentProgramField);
                        fields.Add(currentTreatmentSettingField);
                        fields.Add(treatmentServiceField);
                        fields.Add(treatmentSettingField);
                        fields.Add(rrgField);
                        fields.Add(programField);
                        fields.ForEach(f => f.Enabled = "0");
                        break;
                    case "PreFile":
                        var programTransferObj = new NTST.ScriptLinkService.Web.ProgramTransfer.ProgramTransferObject();
                        var SystemCode = ConfigurationManager.AppSettings["SystemCode"].ToString();
                        var Username = ConfigurationManager.AppSettings["Username"].ToString();
                        var Password = ConfigurationManager.AppSettings["Password"].ToString();

                        foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
                        {
                            if (field.FieldNumber.Equals(dateField.FieldNumber))
                                dateField.FieldValue = field.FieldValue;
                            if (field.FieldNumber.Equals(episodeField.FieldNumber))
                                episodeField.FieldValue = field.FieldValue;
                            if (field.FieldNumber.Equals(programField.FieldNumber))
                                programField.FieldValue = field.FieldValue;
                            if (field.FieldNumber.Equals(transferTypeField.FieldNumber))
                                transferTypeField.FieldValue = field.FieldValue;
                        }
                        programTransferObj.DateOfTransfer = DateTime.Parse(dateField.FieldValue);
                        programTransferObj.DateOfTransferSpecified = true;
                        programTransferObj.Program = programField.FieldValue;
                        programTransferObj.TimeOfTransfer = DateTime.Now.ToString("hh:mm tt");
                        if (!transferTypeField.FieldValue.Equals(String.Empty))
                            programTransferObj.TypeOfTransfer = transferTypeField.FieldValue;
                        var programTransfer = new NTST.ScriptLinkService.Web.ProgramTransfer.ProgramTransfer();
                        var response = programTransfer.TransferProgram(SystemCode, Username, Password, programTransferObj, optionObject.EntityID, episodeField.FieldValue);
                        returnOptionObject.ErrorCode = response.Status == 1 ? 4 : 1;
                        returnOptionObject.ErrorMesg = response.Status == 1 ? "Client transfer was successful. View Report?" :
                                                                    "Client transfer could not be completed, please try again.\nIf this problem persists contact your system administrator.\r\n\r\nError Message: " +
                                                                    response.Message;

                        if (response.Status != 1)
                            sendEmail(ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                                    "Program transfer failed",
                                    "Client ID: " + optionObject.EntityID +
                                    "\nEpisode #: " + episodeField.FieldValue +
                                    "\nEntered By: " + optionObject.OptionUserId +
                                    "\nEntered Date: " + DateTime.Now.ToString("MM/dd/yyyy") +
                                    "\nEntered Time: " + DateTime.Now.ToString("hh:mm tt") +
                                    "\nError Message: " + response.Message,
                                    new List<string> { ConfigurationManager.AppSettings["UpdateUserEmail"].ToString() });
                        break;
                    default:
                        break;
                }
                if (fields.Count > 0)
                {
                    var formObject = new FormObject
                    {
                        FormId = optionObject.Forms[0].FormId,
                        CurrentRow = new RowObject
                        {
                            ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId,
                            RowAction = "EDIT",
                            RowId = optionObject.Forms[0].CurrentRow.RowId,
                            Fields = fields.ToArray()
                        },
                    };
                    var forms = new FormObject[1];
                    forms[0] = formObject;
                    returnOptionObject.Forms = forms;
                }
            }
            catch (Exception e)
            {
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;
        }
        private Program getProgramInfoByClientEpisode(string ClientId, string EpisodeNumber)
        {
            var program = new Program();
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT TOP(1) table_program_definition.program_code, " +
                                "table_program_definition.program_value, " +
                                "table_program_definition.program_X_tx_setting_code, " +
                                "table_program_definition.program_X_tx_setting_value, " +
                                "table_program_definition.program_X_RRG_code, " +
                                "table_program_definition.program_X_RRG_value, " +
                                "table_program_definition.program_X_tx_service_code, " +
                                "table_program_definition.program_X_tx_service_value " +
                                "FROM SYSTEM.view_episode_summary_current " +
                                "INNER JOIN SYSTEM.table_program_definition " +
                                "ON view_episode_summary_current.program_code = table_program_definition.program_code " +
                                "AND view_episode_summary_current.FACILITY = table_program_definition.FACILITY " +
                                "WHERE view_episode_summary_current.PATID = ? " +
                                "AND view_episode_summary_current.EPISODE_NUMBER = ? ";
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        dbcommand.Parameters.Add(new OdbcParameter("PATID", ClientId));
                        dbcommand.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", EpisodeNumber));
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                program.ProgramCode = reader["program_code"].ToString();
                                program.ProgramValue = reader["program_value"].ToString();
                                program.RRGCode = reader["program_X_RRG_code"].ToString();
                                program.RRGValue = reader["program_X_RRG_value"].ToString();
                                program.TreatmentServiceCode = reader["program_X_tx_service_code"].ToString();
                                program.TreatmentServiceValue = reader["program_X_tx_service_value"].ToString();
                                program.TreatmentSettingCode = reader["program_X_tx_setting_code"].ToString();
                                program.TreatmentSettingValue = reader["program_X_tx_setting_value"].ToString();
                            }
                            else
                            {
                                program = null;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }
            return program;
        }
        private Program getProgramInfoByProgramId(string ProgramId)
        {
            var program = new Program();
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT TOP(1) table_program_definition.program_code, " +
                                "table_program_definition.program_value, " +
                                "table_program_definition.program_X_tx_setting_code, " +
                                "table_program_definition.program_X_tx_setting_value, " +
                                "table_program_definition.program_X_RRG_code, " +
                                "table_program_definition.program_X_RRG_value, " +
                                "table_program_definition.program_X_tx_service_code, " +
                                "table_program_definition.program_X_tx_service_value " +
                                "FROM SYSTEM.table_program_definition " +
                                "WHERE table_program_definition.program_code = ? ";
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        dbcommand.Parameters.Add(new OdbcParameter("program_code", ProgramId));
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                program.ProgramCode = reader["program_code"].ToString();
                                program.ProgramValue = reader["program_value"].ToString();
                                program.RRGCode = reader["program_X_RRG_code"].ToString();
                                program.RRGValue = reader["program_X_RRG_value"].ToString();
                                program.TreatmentServiceCode = reader["program_X_tx_service_code"].ToString();
                                program.TreatmentServiceValue = reader["program_X_tx_service_value"].ToString();
                                program.TreatmentSettingCode = reader["program_X_tx_setting_code"].ToString();
                                program.TreatmentSettingValue = reader["program_X_tx_setting_value"].ToString();
                            }
                            else
                            {
                                program = null;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }
            return program;
        }
        private OptionObject RemoveCaseload(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject
            {
                EntityID = optionObject.EntityID,
                OptionId = optionObject.OptionId,
                SystemCode = optionObject.SystemCode,
                Facility = optionObject.Facility
            };
            List<CaseloadRecord> listOfRecords = new List<CaseloadRecord>();
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            const string commandText = "SELECT addc.add_client, " +
                                            "addc.caseload_type, " +
                                            "addc.CUSTHNBW_UID " +
                                            "FROM SYSTEM.PM_OTHER_CASELOAD addc " +
                                            "LEFT OUTER JOIN SYSTEM.PM_OTHER_CASELOAD remc " +
                                            "ON addc.PATID = remc.PATID " +
                                            "AND addc.EPISODE_NUMBER = remc.EPISODE_NUMBER " +
                                            "AND addc.add_client = remc.remove_client " +
                                            "AND addc.FACILITY = remc.FACILITY " +
                                            "WHERE addc.add_client IS NOT NULL " +
                                            "AND addc.PATID=? " +
                                            "AND addc.EPISODE_NUMBER=? " +
                                            "GROUP BY addc.add_client " +
                                            "HAVING COUNT(DISTINCT addc.CUSTHNBW_UID) > COUNT(DISTINCT remc.CUSTHNBW_UID) ";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var dbparameter1 = new OdbcParameter("PATID", optionObject.EntityID);
                    dbcommand.Parameters.Add(dbparameter1);
                    var dbparameter2 = new OdbcParameter("EPISODE_NUMBER", optionObject.EpisodeNumber.ToString());
                    dbcommand.Parameters.Add(dbparameter2);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOfRecords.Add(RemoveCaseloadRecord(optionObject.EntityID,
                                optionObject.EpisodeNumber.ToString(),
                                reader["caseload_type"].ToString(),
                                reader["add_client"].ToString()));
                        }
                    }
                }
                connection.Close();
            }
            if (listOfRecords.Count() > 0)
                AddDCIRecord(listOfRecords);
            return returnOptionObject;
        }
        private CaseloadRecord RemoveCaseloadRecord(String PATID, String Episode, String CaseloadType, String RemoveClient)
        {
            var caseloadRecord = new CaseloadRecord();
            caseloadRecord.PATID = PATID;
            caseloadRecord.Episode = Episode;
            caseloadRecord.DateOfAssignment = DateTime.Now;
            caseloadRecord.TimeOfAssignment = DateTime.Now;
            caseloadRecord.CaseloadType = (CaseloadTypeEnum)Enum.Parse(typeof(CaseloadTypeEnum), CaseloadType);
            //caseloadRecord.RecordAction = RecordActionEnum.Add;
            caseloadRecord.Question = QuestionEnum.RemovePractitioner;
            caseloadRecord.RemoveClient = RemoveClient;
            caseloadRecord.Comments = "Auto-generated discharge";
            //caseloadRecord.uniqueIdentifier = CreateUniqueIdentifier(UniqueIdentifier);
            return caseloadRecord;
        }
        private String AddDCIRecord(List<CaseloadRecord> tempList)
        {
            string xmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                                        "<option>" +
                                        "<optionidentifier>USER26</optionidentifier>";
            string xmlFooter = "</option>";
            var SystemCode = ConfigurationManager.AppSettings["SystemCode"].ToString();
            var Username = ConfigurationManager.AppSettings["Username"].ToString();
            var Password = ConfigurationManager.AppSettings["Password"].ToString();
            string record = "";
            long filewarnings = 1;
            bool filewarningsSpecified = true;
            string resultStream = "";
            string recordStream = "";
            recordStream = xmlHeader;
            foreach (var tempRecord in tempList)
            {
                recordStream += tempRecord.ToString();
            }
            recordStream += xmlFooter;
            var test = new DCIImport.DCIImport();
            var result = test.ImportRecord(SystemCode, Username, Password, record, filewarnings, filewarningsSpecified, recordStream, resultStream);
            return result;
        }
        private OptionObject DefaultPsychosocial(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject
            {
                EntityID = optionObject.EntityID,
                OptionId = optionObject.OptionId,
                SystemCode = optionObject.SystemCode,
                Facility = optionObject.Facility
            };
            var clinician = new FieldObject { FieldNumber = "126.56" };
            var status = new FieldObject { FieldNumber = "141", FieldValue = "F" };
            var aDate = new FieldObject { FieldNumber = "126.55" };
            var aTime = new FieldObject { FieldNumber = "134.7", FieldValue = DateTime.Now.ToString("hh:mm tt") };

            var fields = new FieldObject[1];
            fields[0] = aTime;
            var currentRow = new RowObject();
            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";
            var formObject = new FormObject { FormId = "129" };
            formObject.CurrentRow = currentRow;

            var forms = new FormObject[2];
            forms[0] = formObject;

            var fields1 = new FieldObject[1];
            fields1[0] = status;
            var currentRow1 = new RowObject();
            currentRow1.Fields = fields1;
            currentRow1.RowId = optionObject.Forms[4].CurrentRow.RowId;
            currentRow1.ParentRowId = optionObject.Forms[4].CurrentRow.ParentRowId;
            currentRow1.RowAction = "EDIT";
            var formObject1 = new FormObject { FormId = "133" };
            formObject1.CurrentRow = currentRow1;
            forms[1] = formObject1;
            returnOptionObject.Forms = forms;
            return returnOptionObject;
        }
        private OptionObject DefaultCrisisServices(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject
            {
                EntityID = optionObject.EntityID,
                OptionId = optionObject.OptionId,
                SystemCode = optionObject.SystemCode,
                Facility = optionObject.Facility
            };
            var clinician = new FieldObject { FieldNumber = "141.11" };
            var status = new FieldObject { FieldNumber = "140.91", FieldValue = "F" };
            var aDate = new FieldObject { FieldNumber = "131.69" };
            var aTime = new FieldObject { FieldNumber = "136.72", FieldValue = DateTime.Now.ToString("hh:mm tt") };

            var fields = new FieldObject[1];
            fields[0] = aTime;
            var currentRow = new RowObject();
            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";
            var formObject = new FormObject { FormId = "147" };
            formObject.CurrentRow = currentRow;

            var forms = new FormObject[2];
            forms[0] = formObject;

            var fields1 = new FieldObject[1];
            fields1[0] = status;
            var currentRow1 = new RowObject();
            currentRow1.Fields = fields1;
            currentRow1.RowId = optionObject.Forms[2].CurrentRow.RowId;
            currentRow1.ParentRowId = optionObject.Forms[2].CurrentRow.ParentRowId;
            currentRow1.RowAction = "EDIT";
            var formObject1 = new FormObject { FormId = "149" };
            formObject1.CurrentRow = currentRow1;
            forms[1] = formObject1;
            returnOptionObject.Forms = forms;
            return returnOptionObject;
        }
        private OptionObject DefaultResidentialCaregiver(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject
            {
                EntityID = optionObject.EntityID,
                OptionId = optionObject.OptionId,
                SystemCode = optionObject.SystemCode,
                Facility = optionObject.Facility
            };
            var clinician = new FieldObject { FieldNumber = "141.26" };
            var status = new FieldObject { FieldNumber = "141.27", FieldValue = "F" };
            var aDate = new FieldObject { FieldNumber = "128.19" };
            var aTime = new FieldObject { FieldNumber = "141.25" };
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(aDate.FieldNumber))
                    aDate.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(aTime.FieldNumber))
                    aTime.FieldValue = field.FieldValue;
            }
            if (!aDate.FieldValue.Equals(String.Empty))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
                const string commandText = "SELECT TOP(1) t.staff_member_id, " +
                                            "tempTbl.Data_Entry_Time " +
                                            "FROM SYSTEM.Residential_Caregiver_Note tempTbl " +
                                            "INNER JOIN SYSTEM.RADplus_users t " +
                                            "ON tempTbl.Data_Entry_User_Id = t.USERID " +
                                            "AND tempTbl.FACILITY = t.FACILITY " +
                                            "WHERE tempTbl.PATID=? " +
                                            "AND tempTbl.EPISODE_NUMBER=? " +
                                            "AND tempTbl.Assessment_Date=? " +
                                            "AND tempTbl.Draft_PA_Final IS NULL";
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        var dbparameter1 = new OdbcParameter("PATID", optionObject.EntityID);
                        dbcommand.Parameters.Add(dbparameter1);
                        var dbparameter2 = new OdbcParameter("EPISODE_NUMBER", optionObject.EpisodeNumber);
                        dbcommand.Parameters.Add(dbparameter2);
                        var dbparameter3 = new OdbcParameter("Assessment_Date", DateTime.Parse(aDate.FieldValue).ToString("yyyy-MM-dd"));
                        dbcommand.Parameters.Add(dbparameter3);
                        //var dbparameter4 = new OdbcParameter("Draft_PA_Final", "F");
                        //dbcommand.Parameters.Add(dbparameter4);
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clinician.FieldValue = reader["staff_member_id"].ToString();
                                aTime.FieldValue = reader["Data_Entry_Time"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
                if (!clinician.FieldValue.Equals(String.Empty))
                {
                    var fields = new FieldObject[3];
                    fields[0] = clinician;
                    fields[1] = aTime;
                    fields[2] = status;
                    var currentRow = new RowObject();
                    currentRow.Fields = fields;
                    currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
                    currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
                    currentRow.RowAction = "EDIT";
                    var formObject = new FormObject { FormId = "138" };
                    formObject.CurrentRow = currentRow;
                    var forms = new FormObject[1];
                    forms[0] = formObject;
                    returnOptionObject.Forms = forms;
                }
            }
            return returnOptionObject;
        }
        private OptionObject DefaultDiagnosticAssessment(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject
            {
                EntityID = optionObject.EntityID,
                OptionId = optionObject.OptionId,
                SystemCode = optionObject.SystemCode,
                Facility = optionObject.Facility
            };
            var clinician = new FieldObject { FieldNumber = "141.13" };
            var status = new FieldObject { FieldNumber = "141.15", FieldValue = "F" };
            var aDate = new FieldObject { FieldNumber = "133.83" };
            var aTime = new FieldObject { FieldNumber = "141.14" };
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(aDate.FieldNumber))
                    aDate.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(aTime.FieldNumber))
                    aTime.FieldValue = field.FieldValue;
            }
            if (!aDate.FieldValue.Equals(String.Empty))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
                const string commandText = "SELECT t.staff_member_id, " +
                                            "tempTbl.Data_Entry_Time, " +
                                            "COUNT(t.staff_member_id) as num_of_records " +
                                            "FROM SYSTEM.Diagnostic_Assessment1 tempTbl " +
                                            "INNER JOIN SYSTEM.RADplus_users t " +
                                            "ON tempTbl.Data_Entry_User_Id = t.USERID " +
                                            "AND tempTbl.FACILITY = t.FACILITY " +
                                            "WHERE tempTbl.PATID=? " +
                                            "AND tempTbl.EPISODE_NUMBER=? " +
                                            "AND tempTbl.Assessment_Date=? ";
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        var dbparameter1 = new OdbcParameter("PATID", optionObject.EntityID);
                        dbcommand.Parameters.Add(dbparameter1);
                        var dbparameter2 = new OdbcParameter("EPISODE_NUMBER", optionObject.EpisodeNumber);
                        dbcommand.Parameters.Add(dbparameter2);
                        var dbparameter3 = new OdbcParameter("Assessment_Date", DateTime.Parse(aDate.FieldValue).ToString("yyyy-MM-dd"));
                        dbcommand.Parameters.Add(dbparameter3);
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read() && reader["num_of_records"].ToString().Equals("1"))
                            {
                                clinician.FieldValue = reader["staff_member_id"].ToString();
                                aTime.FieldValue = reader["Data_Entry_Time"].ToString();
                            }
                            else
                            {
                                clinician.FieldValue = "";
                                aTime.FieldValue = "";
                            }
                        }
                    }
                    connection.Close();
                }
                var fields = new FieldObject[3];
                fields[0] = clinician;
                fields[1] = aTime;
                fields[2] = status;
                var currentRow = new RowObject();
                currentRow.Fields = fields;
                currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
                currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
                currentRow.RowAction = "EDIT";
                var formObject = new FormObject { FormId = "153" };
                formObject.CurrentRow = currentRow;
                var forms = new FormObject[1];
                forms[0] = formObject;
                returnOptionObject.Forms = forms;
            }
            return returnOptionObject;
        }
        private OptionObject DefaultSubstanceAbuse(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject
            {
                EntityID = optionObject.EntityID,
                OptionId = optionObject.OptionId,
                SystemCode = optionObject.SystemCode,
                Facility = optionObject.Facility
            };
            var clinician = new FieldObject { FieldNumber = "5.14" };
            var status = new FieldObject { FieldNumber = "141.36", FieldValue = "F" };
            var aDate = new FieldObject { FieldNumber = "5.13" };
            var aTime = new FieldObject { FieldNumber = "5.18" };
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(aDate.FieldNumber))
                    aDate.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(aTime.FieldNumber))
                    aTime.FieldValue = field.FieldValue;
            }
            if (!aDate.FieldValue.Equals(String.Empty) && !aTime.FieldValue.Equals(String.Empty))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
                const string commandText = "SELECT t.staff_member_id " +
                                            "FROM SYSTEM.Substance_Abuse_Assessment tempTbl " +
                                            "INNER JOIN SYSTEM.RADplus_users t " +
                                            "ON tempTbl.Data_Entry_User_Id = t.USERID " +
                                            "AND tempTbl.FACILITY = t.FACILITY " +
                                            "WHERE tempTbl.PATID=? " +
                                            "AND tempTbl.EPISODE_NUMBER=? " +
                                            "AND tempTbl.Assessing_Date=? " +
                                            "AND tempTbl.Assessment_Time=? ";
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        var dbparameter1 = new OdbcParameter("PATID", optionObject.EntityID);
                        dbcommand.Parameters.Add(dbparameter1);
                        var dbparameter2 = new OdbcParameter("EPISODE_NUMBER", optionObject.EpisodeNumber);
                        dbcommand.Parameters.Add(dbparameter2);
                        var dbparameter3 = new OdbcParameter("Assessment_Date", DateTime.Parse(aDate.FieldValue).ToString("yyyy-MM-dd"));
                        dbcommand.Parameters.Add(dbparameter3);
                        var dbparameter4 = new OdbcParameter("Assessment_Time", aTime.FieldValue);
                        dbcommand.Parameters.Add(dbparameter4);
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clinician.FieldValue = reader["staff_member_id"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
                var fields = new FieldObject[2];
                fields[0] = clinician;
                fields[1] = status;
                var currentRow = new RowObject();
                currentRow.Fields = fields;
                currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
                currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
                currentRow.RowAction = "EDIT";
                var formObject = new FormObject { FormId = "17" };
                formObject.CurrentRow = currentRow;
                var forms = new FormObject[1];
                forms[0] = formObject;
                returnOptionObject.Forms = forms;
            }
            return returnOptionObject;
        }
        private OptionObject DefaultAssessmentInfo(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject
            {
                EntityID = optionObject.EntityID,
                OptionId = optionObject.OptionId,
                SystemCode = optionObject.SystemCode,
                Facility = optionObject.Facility
            };
            var clinician = new FieldObject { FieldNumber = "114.80" };
            var status = new FieldObject { FieldNumber = "141.01", FieldValue = "F" };
            var aDate = new FieldObject { FieldNumber = "114.79" };
            var aTime = new FieldObject { FieldNumber = "114.81" };
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(aDate.FieldNumber))
                    aDate.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(aTime.FieldNumber))
                    aTime.FieldValue = field.FieldValue;
            }
            if (!aDate.FieldValue.Equals(String.Empty) && !aTime.FieldValue.Equals(String.Empty))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
                const string commandText = "SELECT t.staff_member_id " +
                                            "FROM SYSTEM.Mental_Status_Assessment tempTbl " +
                                            "INNER JOIN SYSTEM.RADplus_users t " +
                                            "ON tempTbl.Data_Entry_User_Id = t.USERID " +
                                            "AND tempTbl.FACILITY = t.FACILITY " +
                                            "WHERE tempTbl.PATID=? " +
                                            "AND tempTbl.EPISODE_NUMBER=? " +
                                            "AND tempTbl.Assessment_Date=? " +
                                            "AND tempTbl.Assessment_Time=? ";
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        var dbparameter1 = new OdbcParameter("PATID", optionObject.EntityID);
                        dbcommand.Parameters.Add(dbparameter1);
                        var dbparameter2 = new OdbcParameter("EPISODE_NUMBER", optionObject.EpisodeNumber);
                        dbcommand.Parameters.Add(dbparameter2);
                        var dbparameter3 = new OdbcParameter("Assessment_Date", DateTime.Parse(aDate.FieldValue).ToString("yyyy-MM-dd"));
                        dbcommand.Parameters.Add(dbparameter3);
                        var dbparameter4 = new OdbcParameter("Assessment_Time", aTime.FieldValue);
                        dbcommand.Parameters.Add(dbparameter4);
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clinician.FieldValue = reader["staff_member_id"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
                var fields = new FieldObject[2];
                fields[0] = clinician;
                fields[1] = status;
                var currentRow = new RowObject();
                currentRow.Fields = fields;
                currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
                currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
                currentRow.RowAction = "EDIT";
                var formObject = new FormObject { FormId = "90" };
                formObject.CurrentRow = currentRow;
                var forms = new FormObject[1];
                forms[0] = formObject;
                returnOptionObject.Forms = forms;
            }
            return returnOptionObject;
        }
        private OptionObject UpdateUser(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject();
            string staffName = null;
            string staffCredentials = null;
            string userId = null;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            const string commandText = "SELECT t.USERID, " +
                                        "s.name, s.prac_credentials_code FROM SYSTEM.RADplus_users t " +
                                        "INNER JOIN SYSTEM.staff_current_demographics s " +
                                        "ON t.staff_member_id = s.STAFFID " +
                                        "AND t.FACILITY = s.FACILITY " +
                                        "WHERE s.STAFFID=?";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var dbparameter = new OdbcParameter("STAFFID", optionObject.EntityID);
                    dbcommand.Parameters.Add(dbparameter);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staffName = reader["name"].ToString();
                            staffCredentials = reader["prac_credentials_code"].ToString();
                            userId = reader["USERID"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            String systemCode = ConfigurationManager.AppSettings["SystemCode"].ToString();
            String username = ConfigurationManager.AppSettings["Username"].ToString();
            String password = ConfigurationManager.AppSettings["Password"].ToString();
            var userObject = new UserManagement.UserDefinitionObject();
            userObject.UserDescription = formatUserDescription(staffName, staffCredentials);
            userObject.UserId = userId;
            var updateUser = new UserManagement.WebServices();
            var response = updateUser.UpdateUser(systemCode, username, password, userObject);
            if (response.Status != 1)
                mailUserUpdateFailure("USERID: " + userId + "\n" +
                                        "STAFFID: " + optionObject.EntityID + "\n" +
                                        "STAFF CREDENTIALS: " + staffCredentials + "\n" +
                                        "Updated at: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "\n" +
                                        "Updated by: " + optionObject.OptionUserId + "\n" +
                                        "Error message: " + response.Message + "\n");
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;
        }
        private void mailUserUpdateFailure(String body)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString());
            mailMessage.To.Clear();
            mailMessage.To.Add(ConfigurationManager.AppSettings["UpdateUserEmail"].ToString());
            mailMessage.Subject = "Unable to update user credentials";
            mailMessage.Body = body;
            smtpClient.Send(mailMessage);
            mailMessage.Dispose();
        }
        private String formatUserDescription(String staffName, String credentials)
        {
            const string quote = "\"";
            var tempStaffName = staffName.Substring(staffName.IndexOf(",") + 1) + " " + staffName.Substring(0, staffName.IndexOf(","));
            if (credentials.Equals((String.Empty)))
                return quote + tempStaffName + quote;
            return quote + tempStaffName + ", " + credentials.Replace("&", ", ") + quote;
        }
        private OptionObject MakeEpisodeRequired(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject();
            var episode = new FieldObject { FieldNumber = "12001", Required = "1" };
            var fields = new FieldObject[1];
            fields[0] = episode;
            var currentRow = new RowObject();
            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";
            var formObject = new FormObject { FormId = "12001" };
            formObject.CurrentRow = currentRow;
            var forms = new FormObject[1];
            forms[0] = formObject;
            returnOptionObject.Forms = forms;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;

        }
        private OptionObject EmailVoidPN(OptionObject optionObject, String currentTime)
        {
            string serviceCode = null;
            string serviceValue = null;
            string programCode = null;
            string programValue = null;
            string duration = null;
            string startTime = null;
            string endTime = null;
            var dateOfService = new DateTime();

            var voidDataEntryDate = new DateTime();
            string voidDataEntryTime = null;
            string voidDataEntryUserId = null;
            string voidNoteType = null;
            string voidReasonValue = null;
            string voidComments = null;
            string voidDataEntryByName = null;
            string joinToTxHistory = null;
            var dataEntryDate = new DateTime();
            string dataEntryTime = null;
            string dataEntryUserId = null;

            var clientID = new FieldObject { FieldNumber = "12000" };
            var episode = new FieldObject { FieldNumber = "12001" };
            var reasonForVoid = new FieldObject { FieldNumber = "12011" };
            var comments = new FieldObject { FieldNumber = "12012" };

            var returnOptionObject = new OptionObject();
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(clientID.FieldNumber))
                    clientID.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(episode.FieldNumber))
                    episode.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(reasonForVoid.FieldNumber))
                    reasonForVoid.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(comments.FieldNumber))
                    comments.FieldValue = field.FieldValue;
            }
            if (!clientID.FieldValue.Equals(String.Empty) && !episode.FieldValue.Equals(String.Empty) && !reasonForVoid.FieldValue.Equals(String.Empty))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
                var commandText = "SELECT t.note_type_value, t.data_entry_date, t.data_entry_user_id, t.data_entry_time, t.JOIN_TO_TX_HISTORY, " +
                                    "t.reason_for_void_value, t.void_comments, t.void_data_entry_by, t.void_data_entry_date, t.void_data_entry_time, t.void_data_entry_user_id " +
                                    "FROM SYSTEM.cw_notes_voided t WHERE t.PATID=? AND t.EPISODE_NUMBER=? " +
                                    "AND t.void_data_entry_date=? AND (t.void_data_entry_time=? OR t.void_data_entry_time=?) AND t.void_data_entry_user_id=? " +
                                    "AND t.reason_for_void_code=?";
                var connectionString2 = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
                var commandText2 = "SELECT t.SERVICE_CODE, t.v_service_value, t.program_code, t.program_value, t.duration, t.start_time, t.end_time, t.date_of_service " +
                                    "FROM SYSTEM.billing_tx_history t WHERE (t.PATID=? AND t.EPISODE_NUMBER=?) AND ((t.data_entry_date=? AND t.data_entry_time=? AND data_entry_user_id=?) " +
                                    "OR (t.JOIN_TO_TX_HISTORY=?))";

                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        var parameter1 = new OdbcParameter("PATID", clientID.FieldValue);
                        dbcommand.Parameters.Add(parameter1);
                        var parameter2 = new OdbcParameter("EPISODE_NUMBER", episode.FieldValue);
                        dbcommand.Parameters.Add(parameter2);
                        var parameter3 = new OdbcParameter("void_data_entry_date", DateTime.Now.ToString("yyyy-MM-dd"));
                        dbcommand.Parameters.Add(parameter3);
                        var parameter4 = new OdbcParameter("void_data_entry_time", currentTime);
                        dbcommand.Parameters.Add(parameter4);
                        var parameter5 = new OdbcParameter("void_data_entry_time", DateTime.Parse(currentTime).AddMinutes(-1).ToString("hh:mm tt"));
                        dbcommand.Parameters.Add(parameter5);
                        var parameter6 = new OdbcParameter("void_data_entry_user_id", optionObject.OptionUserId);
                        dbcommand.Parameters.Add(parameter6);
                        var parameter7 = new OdbcParameter("reason_for_void_code", reasonForVoid.FieldValue);
                        dbcommand.Parameters.Add(parameter7);

                        using (var reader = dbcommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataEntryDate = DateTime.Parse(reader["data_entry_date"].ToString());
                                dataEntryTime = reader["data_entry_time"].ToString();
                                dataEntryUserId = reader["data_entry_user_id"].ToString();
                                voidDataEntryDate = DateTime.Parse(reader["void_data_entry_date"].ToString());
                                voidDataEntryTime = reader["void_data_entry_time"].ToString();
                                voidDataEntryUserId = reader["void_data_entry_user_id"].ToString();
                                voidNoteType = reader["note_type_value"].ToString();
                                voidReasonValue = reader["reason_for_void_value"].ToString();
                                voidComments = reader["void_comments"].ToString();
                                voidDataEntryByName = reader["void_data_entry_by"].ToString();
                                joinToTxHistory = reader["JOIN_TO_TX_HISTORY"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
                if (dataEntryDate != null && dataEntryTime != null && dataEntryUserId != null)
                {
                    using (var connection = new OdbcConnection(connectionString2))
                    {
                        connection.Open();
                        using (var dbcommand = new OdbcCommand(commandText2, connection))
                        {
                            var parameter1 = new OdbcParameter("PATID", clientID.FieldValue);
                            dbcommand.Parameters.Add(parameter1);
                            var parameter2 = new OdbcParameter("EPISODE_NUMBER", episode.FieldValue);
                            dbcommand.Parameters.Add(parameter2);
                            var parameter3 = new OdbcParameter("data_entry_date", dataEntryDate.ToString("yyyy-MM-dd"));
                            dbcommand.Parameters.Add(parameter3);
                            var parameter4 = new OdbcParameter("data_entry_time", dataEntryTime);
                            dbcommand.Parameters.Add(parameter4);
                            var parameter5 = new OdbcParameter("entry_user_id", dataEntryUserId);
                            dbcommand.Parameters.Add(parameter5);
                            var parameter6 = new OdbcParameter("JOIN_TO_TX_HISTORY", joinToTxHistory);
                            dbcommand.Parameters.Add(parameter6);
                            using (var reader = dbcommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    serviceCode = reader["SERVICE_CODE"].ToString();
                                    serviceValue = reader["v_service_value"].ToString();
                                    programCode = reader["program_code"].ToString();
                                    programValue = reader["program_value"].ToString();
                                    duration = reader["duration"].ToString();
                                    startTime = reader["start_time"].ToString();
                                    endTime = reader["end_time"].ToString();
                                    dateOfService = DateTime.Parse(reader["date_of_service"].ToString());
                                }
                            }
                        }
                        connection.Close();
                    }
                    sendEmail(ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                                "Progress note voided.",
                                "Client ID: " + clientID.FieldValue + "\n" +
                                "Episode #: " + episode.FieldValue + "\n" +
                                "Service: (" + serviceCode + ") " + serviceValue + "\n" +
                                "Program: (" + programCode + ") " + programValue + "\n" +
                                "Service date: " + dateOfService.ToString("MM/dd/yyyy") + "\n" +
                                "Duration: " + duration + "\n" +
                                "Start Time: " + startTime + "\n" +
                                "End Time: " + endTime + "\n" +
                                "Reason for void: " + voidReasonValue + "\n" +
                                "Void by: " + voidDataEntryByName + "\n" +
                                "Void date/time: " + voidDataEntryDate.ToString("MM/dd/yyyy") + " " + voidDataEntryTime + "\n" +
                                "Void comments: " + voidComments + "\n",
                                ConfigurationManager.AppSettings["ProgressNoteVoidedRecipients"].ToString().Split(',').ToList(),
                                new List<string>(),
                                ConfigurationManager.AppSettings["AssignToBilling"].ToString().Split(',').ToList());
                }
                else
                {
                    sendEmail(ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                                "Progress note voided.",
                                "NOTE WAS VOIDED, BUT THE FOLLOWING INFORMATION MIGHT BE INCOMPLETE.\n" +
                                "PLEASE MAKE THIS VOID A PRIORITY, AND IF POSSIBLE CONTACT THE PERSON WHO VOIDED THE NOTE.\n" +
                                "Client ID: " + clientID.FieldValue + "\n" +
                                "Episode #: " + episode.FieldValue + "\n" +
                                "Service: (" + serviceCode + ") " + serviceValue + "\n" +
                                "Program: (" + programCode + ") " + programValue + "\n" +
                                "Service date: " + dateOfService.ToString("MM/dd/yyyy") + "\n" +
                                "Duration: " + duration + "\n" +
                                "Start Time: " + startTime + "\n" +
                                "End Time: " + endTime + "\n" +
                                "Reason for void: " + voidReasonValue + "\n" +
                                "Void by: " + optionObject.OptionUserId + "\n" +
                                "Void date/time: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "\n" +
                                "Void comments: " + comments.FieldValue + "\n",
                                ConfigurationManager.AppSettings["ProgressNoteVoidedRecipients"].ToString().Split(',').ToList(),
                                new List<string>(),
                                ConfigurationManager.AppSettings["AssignToBilling"].ToString().Split(',').ToList());
                }
            }
            else
            {
                sendEmail(ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                            "Progress note voided.",
                            "NOTE WAS VOIDED, BUT THE FOLLOWING INFORMATION MIGHT BE INCOMPLETE.\n" +
                            "PLEASE MAKE THIS VOID A PRIORITY, AND IF POSSIBLE CONTACT THE PERSON WHO VOIDED THE NOTE.\n" +
                            "Client ID: " + clientID.FieldValue + "\n" +
                            "Episode #: " + episode.FieldValue + "\n" +
                            "Service: (" + serviceCode + ") " + serviceValue + "\n" +
                            "Program: (" + programCode + ") " + programValue + "\n" +
                            "Service date: " + dateOfService.ToString("MM/dd/yyyy") + "\n" +
                            "Duration: " + duration + "\n" +
                            "Start Time: " + startTime + "\n" +
                            "End Time: " + endTime + "\n" +
                            "Reason for void: " + voidReasonValue + "\n" +
                            "Void by: " + optionObject.OptionUserId + "\n" +
                            "Void date/time: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "\n" +
                            "Void comments: " + comments.FieldValue + "\n",
                            ConfigurationManager.AppSettings["ProgressNoteVoidedRecipients"].ToString().Split(',').ToList(),
                            new List<string>(),
                            ConfigurationManager.AppSettings["AssignToBilling"].ToString().Split(',').ToList());
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;
        }
        private OptionObject CheckDaysWorked(OptionObject optionObject)
        {
            var daysWorked = new FieldObject { FieldNumber = "54016" };
            var primaryIncome = new FieldObject { FieldNumber = "54008" };// salary = 1
            var returnOptionObject = new OptionObject();
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (daysWorked.FieldNumber.Equals(field.FieldNumber))
                    daysWorked.FieldValue = field.FieldValue;
                if (primaryIncome.FieldNumber.Equals(field.FieldNumber))
                    primaryIncome.FieldValue = field.FieldValue;
            }
            if (daysWorked.FieldValue.Equals("00") && primaryIncome.FieldValue.Equals("1"))
            {
                returnOptionObject.ErrorCode = 3;
                returnOptionObject.ErrorMesg = "You have selected <b>0</b> days worked (in the last 30 days), but the client's primary income source is <b>Salary</b>.\n " +
                                                "If this is correct then continue, if not please verify how many days the client has worked in the last 30 days.";

            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            if (optionObject.OptionStaffId.Equals("003819"))
                returnOptionObject.ErrorMesg = returnOptionObject.ErrorMesg + "\n\nLlyan, we need more INDIAN SPICE!!!\n\n";
            return returnOptionObject;
        }
        private OptionObject CheckFSRStatus(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject();
            var MHOStatus = new FieldObject { FieldNumber = "54003" };
            const string draft = "1";
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(MHOStatus.FieldNumber))
                    MHOStatus.FieldValue = field.FieldValue;
            }
            if (MHOStatus.FieldValue.Equals(draft))
            {
                returnOptionObject.ErrorCode = 4;
                returnOptionObject.ErrorMesg = "The current Mental Health Outcome is in <b>Draft</b>.\n" +
                                                "If left in <b>Draft</b> mode, the MHO will not be reported to the state.\n" +
                                                "<b>Do you wish to continue?</b>";
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            if (optionObject.OptionStaffId.Equals("003819"))
                returnOptionObject.ErrorMesg = returnOptionObject.ErrorMesg + "\n\nLlyan, we need more INDIAN SPICE!!!\n\n";
            return returnOptionObject;
        }
        private OptionObject CheckFSRDraft(OptionObject optionObject)
        {
            const string draft = "1";
            var EvaluationDate = new FieldObject { FieldNumber = "54004" };
            var returnOptionObject = new OptionObject();
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(EvaluationDate.FieldNumber))
                    EvaluationDate.FieldValue = field.FieldValue;
            }
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
            string commandText = EvaluationDate.FieldValue.Equals(String.Empty) ?
                                    "SELECT t.FMO_uniqueid, t.evaluation_date FROM CWSSF.florida_fsr_mh_outcomes t " +
                                        "WHERE t.PATID=? AND t.mh_outcomes_status_code=?" :
                                    "SELECT t.FMO_uniqueid, t.evaluation_date FROM CWSSF.florida_fsr_mh_outcomes t " +
                                       "WHERE t.PATID=? AND t.mh_outcomes_status_code=? AND t.evaluation_date<>?";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    dbcommand.Parameters.Add(new OdbcParameter("PATID", optionObject.EntityID));
                    dbcommand.Parameters.Add(new OdbcParameter("mh_outcomes_status_code", draft));
                    if (!EvaluationDate.FieldValue.Equals(String.Empty))
                        dbcommand.Parameters.Add(new OdbcParameter("evaluation_date", (DateTime.Parse(EvaluationDate.FieldValue)).ToString("yyyy-MM-dd")));
                    using (var reader = dbcommand.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            returnOptionObject.ErrorCode = 1;
                            returnOptionObject.ErrorMesg +=
                                "There are other Mental Health Outcome(s) <b>(" + DateTime.Parse(reader["evaluation_date"].ToString()).ToString("MM/dd/yyyy") + ")</b> in <b>draft</b> for this client, " +
                                "they must be finalized before completing another outcome.\n";
                        }
                    }
                }
                connection.Close();
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            if (optionObject.OptionStaffId.Equals("003819"))
                returnOptionObject.ErrorMesg = returnOptionObject.ErrorMesg + "\n\nLlyan, we need more INDIAN SPICE!!!\n\n";
            return returnOptionObject;
        }
        private OptionObject CheckFSROutcome(OptionObject optionObject)
        {
            var returnOptionObject = new OptionObject();
            var purpOfEval = new FieldObject { FieldNumber = "54001" };
            foreach (var fieldObject in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (fieldObject.FieldNumber.Equals(purpOfEval.FieldNumber))
                    purpOfEval.FieldValue = fieldObject.FieldValue;
            }
            if (purpOfEval.FieldValue.Equals("2") ||
                purpOfEval.FieldValue.Equals("3") ||
                purpOfEval.FieldValue.Equals("4"))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
                const string commandText = "SELECT t.FMO_uniqueid FROM CWSSF.florida_fsr_mh_outcomes t " +
                                           "WHERE t.PATID=? ";
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        var parameter1 = new OdbcParameter("PATID", optionObject.EntityID);
                        dbcommand.Parameters.Add(parameter1);
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                returnOptionObject.ErrorCode = 1;
                                returnOptionObject.ErrorMesg =
                                    "The <b>first</b> Mental Health Outcome in Avatar must be a purpose code <b>1 " +
                                    "(Admission to Provider)</b>.";
                            }
                        }
                    }
                    connection.Close();
                }
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            if (optionObject.OptionStaffId.Equals("003819"))
                returnOptionObject.ErrorMesg = returnOptionObject.ErrorMesg + "\n\nLlyan, we need more INDIAN SPICE!!!\n\n";
            return returnOptionObject;
        }
        private OptionObject EmailAuthorizationNotification(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            string guarantorID;
            var noteStatus = new FieldObject { FieldNumber = "50010" };
            var serviceCode = new FieldObject { FieldNumber = "51001" };
            var serviceDate = new FieldObject { FieldNumber = "51011" };
            var serviceDuration = new FieldObject { FieldNumber = "51003" };
            foreach (var form in optionObject.Forms)
            {
                foreach (var field in form.CurrentRow.Fields)
                {
                    if (field.FieldNumber.Equals(noteStatus.FieldNumber))
                        noteStatus.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(serviceCode.FieldNumber))
                        serviceCode.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(serviceDate.FieldNumber))
                        serviceDate.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(serviceDuration.FieldNumber))
                        serviceDuration.FieldValue = field.FieldValue;
                }
            }
            if (noteStatus.FieldValue.Equals("F"))
            {
                var service = new Service();
                service = GetServiceJustEntered(optionObject.EntityID, optionObject.EpisodeNumber.ToString(),
                                                DateTime.Parse(serviceDate.FieldValue), serviceCode.FieldValue,
                                                optionObject.OptionStaffId);
                try
                {
                    if (service != null && guarantorAndServiceNeedAuth(service))
                        if (!isAuthValid(service))
                            sendEmail(ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                                "Service for (" + service.Patid + ") " + service.PatientName + " provided without proper authorization",
                                service.ToString(),
                                ConfigurationManager.AppSettings["AuthEmailRecipients"].ToString().Split(',').ToList());
                }
                catch (Exception e)
                {
                }
                var svcCodeList = ConfigurationManager.AppSettings["ServiceCodesToExclude"].ToString().Split(',').ToList();
                if (Convert.ToInt32(serviceDuration.FieldValue) > 300 && svcCodeList.IndexOf(serviceCode.FieldValue) < 0)
                {
                    sendEmail(ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                        "Service with a duration of " + serviceDuration.FieldValue + " entered.",
                        "Client ID: " + optionObject.EntityID +
                        "\nEpisode #: " + optionObject.EpisodeNumber +
                        "\nGuarantor ID: " + service.GuarantorId +
                        "\nService Code: " + serviceCode.FieldValue +
                        "\nService Date: " + serviceDate.FieldValue +
                        "\nStaff ID: " + optionObject.OptionStaffId,
                        new List<string> { ConfigurationManager.AppSettings["SMTPCreateSWTicket"].ToString() },
                        new List<string>(),
                        ConfigurationManager.AppSettings["AssignToBilling"].ToString().Split(',').ToList());

                }
                //var clientDiagnosisObj = previousDiagnosisExist(optionObject);
                //if (clientDiagnosisObj == null || (clientDiagnosisObj.DateOfDiagnosis.CompareTo(DateTime.Parse(serviceDate.FieldValue))) > 0)
                //{
                //    var emailBody = "Client ID: " + optionObject.EntityID +
                //        "\nEpisode #: " + optionObject.EpisodeNumber +
                //        "\nGuarantor ID: " + guarantorID +
                //        "\nService Code: " + serviceCode.FieldValue +
                //        "\nService Date: " + serviceDate.FieldValue +
                //        "\nStaff ID: " + optionObject.OptionStaffId;
                //    if (clientDiagnosisObj != null)
                //    {
                //        emailBody += "\n\nDiagnosis On File: " +
                //                    "\nDiagnosis: " + clientDiagnosisObj.DiagnosisAxisI1 +
                //                    "\nDate: " + clientDiagnosisObj.DateOfDiagnosis.ToString("MM/dd/yyyy") +
                //                    ",  Time: " + clientDiagnosisObj.TimeOfDiagnosis +
                //                    "\nDiagnosis Type: " + clientDiagnosisObj.TypeOfDiagnosis +
                //                    "\nDiagnosing Practitioner: " + clientDiagnosisObj.DiagnosingPractitioner;
                //    }
                //    else
                //    {
                //        emailBody += "\n\nNo Diagnosis On File.";
                //    }
                //    sendEmail(ConfigurationManager.AppSettings["SMTPFromEmailAddress"].ToString(),
                //        "Service with no Diagnosis entered.",
                //        emailBody,
                //        new List<string> { ConfigurationManager.AppSettings["SMTPCreateSWTicket"].ToString() },
                //        new List<string>(),
                //        new List<string>{ConfigurationManager.AppSettings["appSWAnywhere1"].ToString(),
                //                            ConfigurationManager.AppSettings["appSWAnywhere2"].ToString(),
                //                            ConfigurationManager.AppSettings["appSWAnywhere3"].ToString()});
                //}
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private Service GetServiceJustEntered(string Patid, string EpisodeNumber, DateTime ServiceDate, string ServiceCode, string StaffId)
        {
            var service = new Service();
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            #region CommandText
            var commandText = "SELECT TOP(1) billing_tx_history.cost_of_service, " +
                                    "billing_tx_history.date_of_service, " +
                                    "billing_tx_history.duration, " +
                                    "billing_tx_history.end_time, " +
                                    "billing_tx_history.EPISODE_NUMBER, " +
                                    "billing_tx_history.FACILITY, " +
                                    "billing_tx_history.JOIN_TO_TX_HISTORY, " +
                                    "billing_tx_history.location_code, " +
                                    "billing_tx_history.location_value, " +
                                    "billing_tx_history.NOT_uniqueid, " +
                                    "billing_tx_history.ORIG_JOIN_TO_TX_HISTORY, " +
                                    "billing_tx_history.PATID, " +
                                    "billing_guar_table.GUARANTOR_ID, " +
                                    "billing_guar_table.guarantor_name, " +
                                    "billing_tx_history.program_code, " +
                                    "billing_tx_history.program_value, " +
                                    "billing_tx_history.program_X_RRG_code, " +
                                    "billing_tx_history.program_X_RRG_value, " +
                                    "billing_tx_history.PROVIDER_ID, " +
                                    "billing_tx_history.SERVICE_CODE, " +
                                    "billing_tx_history.start_time, " +
                                    "billing_tx_history.units_of_service, " +
                                    "billing_tx_history.v_patient_name, " +
                                    "billing_tx_history.v_PROVIDER_NAME, " +
                                    "billing_tx_history.v_service_value " +
                                    "FROM SYSTEM.billing_tx_history " +
                                    "INNER JOIN SYSTEM.billing_guar_table " +
                                    "ON billing_tx_history.primary_guarantor = billing_guar_table.GUARANTOR_ID " +
                                    "AND billing_tx_history.FACILITY = billing_guar_table.FACILITY " +
                                    "WHERE billing_tx_history.PATID=? " +
                                    "AND billing_tx_history.EPISODE_NUMBER=? " +
                                    "AND billing_tx_history.SERVICE_CODE=? " +
                                    "AND billing_tx_history.PROVIDER_ID=? " +
                                    "AND billing_tx_history.date_of_service=? " +
                                    "AND (billing_tx_history.data_entry_time=? OR billing_tx_history.data_entry_time=?)";
            #endregion
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        dbcommand.Parameters.Add(new OdbcParameter("PATID", Patid));
                        dbcommand.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", EpisodeNumber));
                        dbcommand.Parameters.Add(new OdbcParameter("SERVICE_CODE", ServiceCode));
                        dbcommand.Parameters.Add(new OdbcParameter("PROVIDER_ID", StaffId));
                        dbcommand.Parameters.Add(new OdbcParameter("date_of_service", ServiceDate.ToString("yyyy-MM-dd")));
                        dbcommand.Parameters.Add(new OdbcParameter("data_entry_time", DateTime.Now.ToString("hh:mm tt")));
                        dbcommand.Parameters.Add(new OdbcParameter("data_entry_time", DateTime.Now.AddMinutes(-1).ToString("hh:mm tt")));
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                #region ReadInValues
                                service.ServiceCost = reader["cost_of_service"].ToString();
                                service.ServiceDate = DateTime.Parse(reader["date_of_service"].ToString());
                                service.Duration = reader["duration"].ToString();
                                service.StartTime = reader["start_time"].ToString();
                                service.EndTime = reader["end_time"].ToString();
                                service.EpisodeNumber = reader["EPISODE_NUMBER"].ToString();
                                service.Facility = reader["FACILITY"].ToString();
                                service.JoinToTxHistory = reader["JOIN_TO_TX_HISTORY"].ToString();
                                service.LocationCode = reader["location_code"].ToString();
                                service.LocationValue = reader["location_value"].ToString();
                                service.NotUniqueId = reader["NOT_uniqueid"].ToString();
                                service.OrigJoinToTxHistory = reader["ORIG_JOIN_TO_TX_HISTORY"].ToString();
                                service.Patid = reader["PATID"].ToString();
                                service.PatientName = reader["v_patient_name"].ToString();
                                service.GuarantorId = reader["GUARANTOR_ID"].ToString();
                                service.GuarantorName = reader["guarantor_name"].ToString();
                                service.ProgramCode = reader["program_code"].ToString();
                                service.ProgramValue = reader["program_value"].ToString();
                                service.ProgramRRGCode = reader["program_X_RRG_code"].ToString();
                                service.ProgramRRGValue = reader["program_X_RRG_value"].ToString();
                                service.StaffId = reader["PROVIDER_ID"].ToString();
                                service.StaffName = reader["v_PROVIDER_NAME"].ToString();
                                service.ServiceCode = reader["SERVICE_CODE"].ToString();
                                service.ServiceValue = reader["v_service_value"].ToString();
                                service.UnitsOfService = reader["units_of_service"].ToString();
                                #endregion
                            }
                            else
                            {
                                service = null;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }
            return service;
        }
        private bool guarantorAndServiceNeedAuth(Service service)
        {

            List<String> List202 = new List<String>() { "203", "300", "3000", "301", "3001", "307", "313", "315", "316", "400", "4000", "4001" };

            List<String> List207_208_210 = new List<String>() { "203", "3000", "3001", "300", "301", "307", "313", "315", "316", "400", "401", "402", "403", "404", "405", "4000", "4001" };
            List<String> List212 = new List<String>() { "400", "401", "402", "403", "404", "405", "300", "3000", "301", "3001", "307", "313", "315", "316", "203" };
            List<String> List213_406 = new List<String>() { "200", "201", "202", "203", "3000", "3001", "300", "301", "307", "313", "315", "316", "400", "401", "402", "4000", "4001" };
            switch (service.GuarantorId)
            {
                case "202": return List202.Contains(service.ServiceCode);
                case "205": return true;
                case "207": return List207_208_210.Contains(service.ServiceCode);
                case "208": return List207_208_210.Contains(service.ServiceCode);
                case "210": return List207_208_210.Contains(service.ServiceCode);
                case "212": return List212.Contains(service.ServiceCode);
                case "213": return List213_406.Contains(service.ServiceCode);
                case "302": return true;
                case "303": return true;
                case "401": return true;
                case "403": return true;
                case "406": return List213_406.Contains(service.ServiceCode);
                case "701": return true;
                default: return false;
            }
        }
        private bool isAuthValid(Service service)
        {
            bool temp = true;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT history_managed_care_auths.auth_start_date " +
                                "FROM SYSTEM.history_managed_care_auths " +
                                "WHERE history_managed_care_auths.PATID=? " +
                                "AND history_managed_care_auths.GUARANTOR_ID=? " +
                                "AND history_managed_care_auths.auth_start_date<=? " +
                                "AND history_managed_care_auths.auth_end_date>=? " +
                                "AND (history_managed_care_auths.rem_units IS NULL OR CAST(history_managed_care_auths.rem_units as DOUBLE)>0) " +
                                "AND (history_managed_care_auths.rem_visits IS NULL OR CAST(history_managed_care_auths.rem_visits as DOUBLE)>0) " +
                                "AND history_managed_care_auths.service_codes_all like ?";
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        dbcommand.Parameters.Add(new OdbcParameter("PATID", service.Patid));
                        dbcommand.Parameters.Add(new OdbcParameter("GUARANTOR_ID", service.GuarantorId));
                        dbcommand.Parameters.Add(new OdbcParameter("auth_start_date", service.ServiceDate.ToString("yyyy-MM-dd")));
                        dbcommand.Parameters.Add(new OdbcParameter("auth_end_date", service.ServiceDate.ToString("yyyy-MM-dd")));
                        dbcommand.Parameters.Add(new OdbcParameter("service_codes_all", "%" + service.ServiceCode + "%"));
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                                temp = true;
                            else
                                temp = false;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }
            return temp;
        }
        private OptionObject DefaultPractitioner(OptionObject optionObject, Boolean isCheckIn)
        {
            OptionObject returnOptionObject = new OptionObject();
            int numOfFields = 0;
            var Staff = new FieldObject();
            Boolean StaffChanged = false;
            var Date = new FieldObject();
            Boolean DateChanged = false;
            var Time = new FieldObject();
            Boolean TimeChanged = false;
            if (isCheckIn)
            {
                Staff.FieldNumber = "130.71";
                Date.FieldNumber = "130.49";
                Time.FieldNumber = "130.68";
            }
            else
            {
                Staff.FieldNumber = "130.74";
                Date.FieldNumber = "130.5";
                Time.FieldNumber = "130.69";
            }
            foreach (var field in optionObject.Forms[2].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(Staff.FieldNumber))
                    Staff.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(Date.FieldNumber))
                    Date.FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(Staff.FieldNumber))
                    Time.FieldValue = field.FieldValue;
            }
            if (Staff.FieldValue.Equals(String.Empty))
            {
                Staff.FieldValue = optionObject.OptionStaffId;
                StaffChanged = true;
                numOfFields++;
            }
            if (Date.FieldValue.Equals(String.Empty))
            {
                Date.FieldValue = DateTime.Now.ToString("MM/dd/yyyy");
                DateChanged = true;
                numOfFields++;
            }
            if (Time.FieldValue.Equals(String.Empty))
            {
                Time.FieldValue = DateTime.Now.ToString("HHmm");
                TimeChanged = true;
                numOfFields++;
            }
            var fields = new FieldObject[numOfFields];
            int pointer = 0;
            if (StaffChanged)
                fields[pointer++] = Staff;
            if (DateChanged)
                fields[pointer++] = Date;
            if (TimeChanged)
                fields[pointer++] = Time;
            var forms = new FormObject[1];
            var formObject = new FormObject();
            var currentRow = new RowObject();
            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[2].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[2].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";
            #region OnlyMultiIteration
            //int i = 0;
            //if (optionObject.Forms[2].OtherRows != null)
            //{
            //    var otherRows = new RowObject[optionObject.Forms[2].OtherRows.Length];
            //    foreach (var row in optionObject.Forms[2].OtherRows)
            //    {
            //        var tempFields = new FieldObject[1];
            //        var tempField = new FieldObject();
            //        tempField.FieldNumber = "130.71";
            //        tempField.FieldValue = optionObject.OptionStaffId;
            //        tempFields[0] = tempField;
            //        var tempRow = new RowObject();
            //        tempRow.Fields = tempFields;
            //        tempRow.RowId = row.RowId;
            //        tempRow.ParentRowId = row.ParentRowId;
            //        tempRow.RowAction = "EDIT";
            //        otherRows[i++] = tempRow;
            //    }
            //    formObject.OtherRows = otherRows;
            //}
            #endregion
            formObject.CurrentRow = currentRow;
            formObject.FormId = "951";
            formObject.MultipleIteration = true;
            forms[0] = formObject;
            returnOptionObject.Forms = forms;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private String getMissedVisitCode(String AppointmentStatus)
        {
            String MissedVisitCode = String.Empty;
            switch (AppointmentStatus)
            {
                case "1"://scheduled
                    MissedVisitCode = "950";
                    break;
                case "2"://re-schedule>24
                    MissedVisitCode = "954";
                    break;
                case "3"://walk-in
                    MissedVisitCode = "950";
                    break;
                case "4"://missed visit/new note written
                    MissedVisitCode = "955";
                    break;
                case "5"://re-schedule<24
                    MissedVisitCode = "957";
                    break;
                case "6"://canceled < 24
                    MissedVisitCode = "952";
                    break;
                case "7"://canceled > 24
                    MissedVisitCode = "956";
                    break;
                case "8"://clinician canceled appointment
                    MissedVisitCode = "953";
                    break;
                case "9"://did not show
                    MissedVisitCode = "951";
                    break;
                default:
                    break;
            }
            return MissedVisitCode;
        }
        private OptionObject AppointmentEditDetails(OptionObject optionObject)
        {
            var systemCode = ConfigurationManager.AppSettings["SystemCode"].ToString();
            var username = ConfigurationManager.AppSettings["Username"].ToString();
            var password = ConfigurationManager.AppSettings["Password"].ToString();
            string appointmentStatusList = "&2&4&5&6&7&8&9&";
            var returnOptionObject = new OptionObject();
            var ClientField = new FieldObject { FieldNumber = "10010" };
            var EpisodeNumberField = new FieldObject { FieldNumber = "12345" };
            var AppointmentDateField = new FieldObject { FieldNumber = "22000" };
            var StaffField = new FieldObject { FieldNumber = "10003" };
            var AppointmentStartTimeField = new FieldObject { FieldNumber = "10107" };
            var AppointmentStatusField = new FieldObject { FieldNumber = "10005" };
            try
            {
                foreach (var field in optionObject.Forms.ElementAt(0).CurrentRow.Fields)
                {
                    if (field.FieldNumber.Equals(ClientField.FieldNumber))
                        ClientField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(EpisodeNumberField.FieldNumber))
                        EpisodeNumberField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(AppointmentDateField.FieldNumber))
                        AppointmentDateField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(StaffField.FieldNumber))
                        StaffField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(AppointmentStartTimeField.FieldNumber))
                        AppointmentStartTimeField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(AppointmentStatusField.FieldNumber))
                        AppointmentStatusField.FieldValue = field.FieldValue;
                }
                if (appointmentStatusList.IndexOf(AppointmentStatusField.FieldValue) >= 0)
                {
                    var appointments = AppointmentRepository.GetAppointment(ClientField.FieldValue,
                        EpisodeNumberField.FieldValue,
                        DateTime.Parse(AppointmentDateField.FieldValue),
                        StaffField.FieldValue,
                        DateTime.Parse(AppointmentStartTimeField.FieldValue));
                    var WebSvcAppointmentObj = new AppointmentScheduling.AppointmentSchedulingObject();
                    var WebSvcAppointment = new AppointmentScheduling.AppointmentScheduling();
                    var SingleAppointment = new Appointment();
                    SingleAppointment = appointments.ElementAt(0);
                    WebSvcAppointmentObj.ApptDate = SingleAppointment.AppointmentDate;
                    WebSvcAppointmentObj.ApptDateSpecified = true;
                    WebSvcAppointmentObj.ApptStatus = SingleAppointment.StatusCode;
                    WebSvcAppointmentObj.ClientID = SingleAppointment.ClientId;
                    WebSvcAppointmentObj.Episode = Convert.ToInt64(SingleAppointment.EpisodeNumber);
                    WebSvcAppointmentObj.EpisodeSpecified = true;
                    WebSvcAppointmentObj.Site = SingleAppointment.SiteId;
                    WebSvcAppointmentObj.ApptStartTime = SingleAppointment.StartTime;
                    WebSvcAppointmentObj.ApptEndTime = SingleAppointment.EndTime;
                    WebSvcAppointmentObj.ServiceCode = SingleAppointment.ScheduledServiceCode;
                    WebSvcAppointmentObj.Program = SingleAppointment.ProgramCode;
                    WebSvcAppointmentObj.NumberOfClients = "1";
                    WebSvcAppointmentObj.ApptNotes = SingleAppointment.Notes;
                    if (appointmentStatusList.IndexOf(SingleAppointment.StatusCode) >= 0)
                    {
                        WebSvcAppointmentObj.MissedVisitSvcCode = getMissedVisitCode(SingleAppointment.StatusCode);
                        WebSvcAppointmentObj.MissedVisit = "X";
                    }
                    var response = WebSvcAppointment.UpdateAppointment(systemCode,
                        username,
                        password,
                        WebSvcAppointmentObj,
                        SingleAppointment.StaffId,
                        SingleAppointment.AppointmentId);
                }
            }
            catch (Exception e)
            {
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private OptionObject AppointmentStatus(OptionObject optionObject, Boolean isClientSchedulingScreen)
        {
            String appointmentStatusList = "&2&4&5&6&7&8&9&";
            OptionObject returnOptionObject = new OptionObject();
            FieldObject AppointmentStatus = new FieldObject();
            AppointmentStatus.FieldNumber = "10005";//same
            FieldObject MissedVisit = new FieldObject();
            MissedVisit.FieldNumber = "10101";//same
            FieldObject MissedVisitCode = new FieldObject();
            MissedVisitCode.FieldNumber = "10102";//same
            FieldObject CoPractitioner = new FieldObject();
            CoPractitioner.FieldNumber = "10007";
            foreach (var form in optionObject.Forms)
            {
                foreach (var field in form.CurrentRow.Fields)
                {
                    if (AppointmentStatus.FieldNumber.Equals(field.FieldNumber))
                        AppointmentStatus.FieldValue = field.FieldValue;
                    if (CoPractitioner.FieldNumber.Equals(field.FieldNumber))
                        CoPractitioner.FieldValue = field.FieldValue;
                    //if (MissedVisit.FieldNumber.Equals(field.FieldNumber))
                    //    MissedVisit.FieldValue = field.FieldValue;
                }
            }
            if (appointmentStatusList.IndexOf(AppointmentStatus.FieldValue) != -1)
            {
                MissedVisitCode.FieldValue = getMissedVisitCode(AppointmentStatus.FieldValue);
                MissedVisitCode.Required = "0";
                MissedVisitCode.Enabled = "0";
                MissedVisit.Required = "0";
                MissedVisit.Enabled = "0";
                MissedVisit.FieldValue = "X";
            }
            else
            {
                MissedVisitCode.FieldValue = String.Empty;
                MissedVisitCode.Required = "0";
                MissedVisitCode.Enabled = "0";
                MissedVisit.Required = "0";
                MissedVisit.Enabled = "0";
                MissedVisit.FieldValue = String.Empty;
            }
            var fields = new FieldObject[2];
            var forms = new FormObject[1];
            var formObject = new FormObject();
            var currentRow = new RowObject();
            fields[0] = MissedVisit;
            fields[1] = MissedVisitCode;
            currentRow.Fields = fields;
            currentRow.RowId = isClientSchedulingScreen ? optionObject.Forms[0].CurrentRow.RowId : optionObject.Forms[0].CurrentRow.RowId;
            currentRow.ParentRowId = isClientSchedulingScreen ? optionObject.Forms[0].CurrentRow.ParentRowId : optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";
            formObject.FormId = isClientSchedulingScreen ? "610" : "610";
            formObject.CurrentRow = currentRow;
            forms[0] = formObject;
            //var fields2 = new FieldObject[1];
            //var formObject2 = new FormObject();
            //var currentRow2 = new RowObject();
            //CoPractitioner.FieldValue = "000082";
            //fields2[0] = CoPractitioner;
            //currentRow2.Fields = fields2;
            //currentRow2.RowId = optionObject.Forms[0].CurrentRow.RowId;
            //currentRow2.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            //currentRow2.RowAction = "EDIT";
            //formObject2.FormId = "600";
            //formObject2.CurrentRow = currentRow2;
            //forms[1] = formObject2;
            returnOptionObject.Forms = forms;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private OptionObject LoadDisclaimers(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            #region General Disclaimer
            FieldObject GeneralDisclaimer = new FieldObject();
            GeneralDisclaimer.FieldNumber = "130.62";
            GeneralDisclaimer.Lock = "1";
            GeneralDisclaimer.Enabled = "0";
            GeneralDisclaimer.Required = "0";
            GeneralDisclaimer.FieldValue = "Personal property will be held no longer than 7 days after discharge date. " +
                                            "At that time, HBH personnel will dispose of any property not retrieved, " +
                                            "at their discretion. Above is a correct listing of my personal effects and belongings, " +
                                            "which HBH will not be held responsible for and hereby I take full responsibility for keeping in my possession.";
            #endregion
            #region CSU Disclaimer  Retain
            FieldObject CSUDisclaimerRetain = new FieldObject();
            CSUDisclaimerRetain.FieldNumber = "130.65";
            CSUDisclaimerRetain.Lock = "1";
            CSUDisclaimerRetain.Enabled = "0";
            CSUDisclaimerRetain.Required = "0";
            CSUDisclaimerRetain.FieldValue = "All valuables such as wallets, money, jewelry, etc. will be logged separately to ensure safekeeping. " +
                                                "(Shoe laces, articles of clothing with strings, belts, duffel-type bags, perfumes, etc.  " +
                                                "are to be secured in the property room according to Bed #).  " +
                                                "These items will be retrieved upon Discharge from Unit.\r\n\r\n" +
                                                "Above is a correct listing of my personal effects and belongings, which I hereby place in custody of HBH. " +
                                                "By signing below, I hereby relieve HBH and its employees for any loss or damage.";
            #endregion
            #region CSU Disclaimer Return
            FieldObject CSUDisclaimerReturn = new FieldObject();
            CSUDisclaimerReturn.FieldNumber = "130.66";
            CSUDisclaimerReturn.Lock = "1";
            CSUDisclaimerReturn.Enabled = "0";
            CSUDisclaimerReturn.Required = "0";
            CSUDisclaimerReturn.FieldValue = "I hereby acknowledge that all personal property deposited at HBH has been returned to me.";
            #endregion

            var fieldsDisclaimerForm = new FieldObject[1];
            fieldsDisclaimerForm[0] = GeneralDisclaimer;
            var fieldsCSUForm = new FieldObject[2];
            fieldsCSUForm[0] = CSUDisclaimerRetain;
            fieldsCSUForm[1] = CSUDisclaimerReturn;
            var forms = new FormObject[2];
            #region Disclaimer Form / 1st Tab
            var DisclaimerForm = new FormObject();
            var DisclaimerCurrentRow = new RowObject();
            DisclaimerCurrentRow.Fields = fieldsDisclaimerForm;
            DisclaimerCurrentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
            DisclaimerCurrentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            DisclaimerCurrentRow.RowAction = "EDIT";
            DisclaimerForm.CurrentRow = DisclaimerCurrentRow;
            DisclaimerForm.FormId = "949";
            forms[0] = DisclaimerForm;
            #endregion
            #region CSU Form / 2nd Tab
            var CSUForm = new FormObject();
            var CSUCurrentRow = new RowObject();
            CSUCurrentRow.Fields = fieldsCSUForm;
            CSUCurrentRow.RowId = optionObject.Forms[1].CurrentRow.RowId;
            CSUCurrentRow.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
            CSUCurrentRow.RowAction = "EDIT";
            CSUForm.CurrentRow = CSUCurrentRow;
            CSUForm.FormId = "953";
            forms[1] = CSUForm;
            #endregion
            returnOptionObject.Forms = forms;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;

        }
        private String getClientAddress(String PATID)
        {
            String clientAddress = String.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT patient_current_demographics.PATID, " +
                                "patient_current_demographics.patient_name, " +
                                "patient_current_demographics.patient_add_street_1, " +
                                "patient_current_demographics.patient_add_city, " +
                                "patient_current_demographics.patient_add_state_code,  " +
                                "patient_current_demographics.patient_add_zipcode  " +
                                "FROM SYSTEM.patient_current_demographics " +
                                "WHERE patient_current_demographics.PATID=? ";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var parameter1 = new OdbcParameter("PATID", PATID);
                    dbcommand.Parameters.Add(parameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            clientAddress = reader["patient_add_street_1"].ToString() + ", " +
                                                reader["patient_add_city"].ToString() + ", " +
                                                reader["patient_add_state_code"].ToString() + " " +
                                                reader["patient_add_zipcode"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            return clientAddress;
        }
        private String getSiteAddress(String SiteID)
        {
            String referralAddress = String.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT appt_site_registration.SITEID, " +
                                "appt_site_registration.site_name, " +
                                "appt_site_registration.site_addr_street_1, " +
                                "appt_site_registration.site_addr_city, " +
                                "appt_site_registration.site_addr_state, " +
                                "appt_site_registration.site_addr_zip_code " +
                                "FROM SYSTEM.appt_site_registration appt_site_registration " +
                                "WHERE appt_site_registration.SITEID=? ";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var parameter1 = new OdbcParameter("SITEID", SiteID);
                    dbcommand.Parameters.Add(parameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            referralAddress = reader["site_addr_street_1"].ToString() + ", " +
                                                reader["site_addr_city"].ToString() + ", " +
                                                reader["site_addr_state"].ToString() + " " +
                                                reader["site_addr_zip_code"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            return referralAddress;
        }
        private String getReferralAddress(String ReferralCode)
        {
            String referralAddress = String.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT table_referral_sources.REF_SOURCE_CODE, " +
                                "table_referral_sources.ref_source_add_str1, " +
                                "table_referral_sources.ref_source_add_str2, " +
                                "table_referral_sources.ref_source_add_zip, " +
                                "table_referral_sources.ref_source_add_city, " +
                                "table_referral_sources.ref_source_add_state " +
                                "FROM SYSTEM.table_referral_sources table_referral_sources " +
                                "WHERE table_referral_sources.REF_SOURCE_CODE=? ";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var parameter1 = new OdbcParameter("REF_SOURCE_CODE", ReferralCode);
                    dbcommand.Parameters.Add(parameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            referralAddress = reader["ref_source_add_str1"].ToString() + ", " +
                                                reader["ref_source_add_city"].ToString() + ", " +
                                                reader["ref_source_add_state"].ToString() + " " +
                                                reader["ref_source_add_zip"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            return referralAddress;
        }
        private OptionObject MapReferral(OptionObject optionObject, Boolean getDirections)
        {
            OptionObject returnOptionObject = new OptionObject();
            FieldObject PlaceReferredTo = new FieldObject();
            PlaceReferredTo.FieldNumber = "130.87";
            FieldObject CurrentSite = new FieldObject();
            CurrentSite.FieldNumber = "130.86";
            FieldObject DirectionsFrom = new FieldObject();
            DirectionsFrom.FieldNumber = "130.97";
            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (PlaceReferredTo.FieldNumber.Equals(field.FieldNumber))
                    PlaceReferredTo.FieldValue = field.FieldValue;
                if (CurrentSite.FieldNumber.Equals(field.FieldNumber))
                    CurrentSite.FieldValue = field.FieldValue;
                if (DirectionsFrom.FieldNumber.Equals(field.FieldNumber))
                    DirectionsFrom.FieldValue = field.FieldValue;
            }
            if (!PlaceReferredTo.FieldValue.Equals("9999"))
            {
                if (getDirections)
                {
                    if (DirectionsFrom.FieldValue.Equals("1"))
                    {
                        returnOptionObject.ErrorCode = 5;
                        returnOptionObject.ErrorMesg = "http://maps.google.com/maps?saddr=" + getSiteAddress(CurrentSite.FieldValue) + "&daddr=" + getReferralAddress(PlaceReferredTo.FieldValue.ToString());
                    }
                    else
                    {
                        returnOptionObject.ErrorCode = 5;
                        returnOptionObject.ErrorMesg = "http://maps.google.com/maps?saddr=" + getClientAddress(optionObject.EntityID) + "&daddr=" + getReferralAddress(PlaceReferredTo.FieldValue.ToString());
                    }
                }
                else
                {
                    returnOptionObject.ErrorCode = 5;
                    returnOptionObject.ErrorMesg = "http://maps.google.com/?q=" + getReferralAddress(PlaceReferredTo.FieldValue.ToString());
                }
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private OptionObject ChartLocation(OptionObject optionObject, Boolean isCallFromOptionLoad)
        {
            OptionObject returnOptionObject = new OptionObject();
            FieldObject ChartDropdown = new FieldObject();
            ChartDropdown.FieldNumber = "626.1";
            FieldObject ChartTextField = new FieldObject();
            ChartTextField.FieldNumber = "148";
            foreach (var form in optionObject.Forms)
            {
                foreach (var field in form.CurrentRow.Fields)
                {
                    if (ChartDropdown.FieldNumber.Equals(field.FieldNumber))
                        ChartDropdown.FieldValue = field.FieldValue;
                }
            }
            if (isCallFromOptionLoad)
            {
                ChartTextField.Enabled = "0";
                ChartTextField.FieldValue = getChartLocation(ChartDropdown.FieldValue) + " - " + optionObject.EntityID;
            }
            else
            {
                ChartTextField.FieldValue = getChartLocation(ChartDropdown.FieldValue) + " - " + optionObject.EntityID;
            }
            var fields = new FieldObject[1];
            var forms = new FormObject[1];
            var formObject = new FormObject();
            var currentRow = new RowObject();
            fields[0] = ChartTextField;
            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";

            formObject.FormId = "14";//510 for admission screen
            formObject.CurrentRow = currentRow;
            forms[0] = formObject;
            //returnOptionObject.ErrorCode = 3;
            //returnOptionObject.ErrorMesg = ChartTextField.FieldValue;
            returnOptionObject.Forms = forms;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;

        }
        private String getChartLocation(String dictionaryCode)
        {
            String chartLocationValue = String.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT dictionaries_patient.dictionary_value " +
                                "FROM SYSTEM.dictionaries_patient " +
                                "WHERE dictionaries_patient.field_number=? " +
                                "AND dictionaries_patient.dictionary_code=? ";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var parameter1 = new OdbcParameter("field_number", "626.1");
                    dbcommand.Parameters.Add(parameter1);
                    var parameter2 = new OdbcParameter("dictionary_code", dictionaryCode);
                    dbcommand.Parameters.Add(parameter2);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            chartLocationValue = reader["dictionary_value"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            return chartLocationValue;
        }
        private String getMGAFSCore(String PATID, String Episode)
        {
            String MGAFSCore = String.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
            var commandText = "SELECT cw_patient_notes_supp_1.ss_note_integer_4  " +
                                "FROM SYSTEM.cw_patient_notes_supp_1  " +
                                "WHERE cw_patient_notes_supp_1.PATID=?  " +
                                "AND cw_patient_notes_supp_1.EPISODE_NUMBER=? " +
                                "AND cw_patient_notes_supp_1.ss_note_integer_4 IS NOT NULL " +
                                "GROUP BY cw_patient_notes_supp_1.PATID " +
                                "ORDER BY cw_patient_notes_supp_1.data_entry_date DESC, cw_patient_notes_supp_1.data_entry_time DESC ";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var parameter1 = new OdbcParameter("PATID", PATID);
                    dbcommand.Parameters.Add(parameter1);
                    var parameter2 = new OdbcParameter("EPISODE_NUMBER", Episode);
                    dbcommand.Parameters.Add(parameter2);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MGAFSCore = reader["ss_note_integer_4"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            return MGAFSCore;
        }
        private OptionObject DefaultMGAFScore(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            FieldObject MGAF = new FieldObject();
            MGAF.FieldNumber = "7051.4";
            foreach (var form in optionObject.Forms)
            {
                foreach (var field in form.CurrentRow.Fields)
                {
                    if (field.FieldNumber.Equals(MGAF.FieldNumber))
                        MGAF.FieldValue = field.FieldValue;
                }
            }
            if (MGAF.FieldValue.Equals(String.Empty))
                MGAF.FieldValue = getMGAFSCore(optionObject.EntityID, optionObject.EpisodeNumber.ToString());
            var fields = new FieldObject[1];
            var forms = new FormObject[1];
            var formObject = new FormObject();
            var currentRow = new RowObject();
            fields[0] = MGAF;
            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[1].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";

            formObject.FormId = "21008";
            formObject.CurrentRow = currentRow;
            forms[0] = formObject;
            returnOptionObject.Forms = forms;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private OptionObject ServiceNeedsIntensityAssessment(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            int highCount = 0;
            int medCount = 0;
            int lowCount = 0;

            FieldObject genSvcNeedsHigh = new FieldObject();
            genSvcNeedsHigh.FieldNumber = "140.85";
            FieldObject genSvcNeedsMed = new FieldObject();
            genSvcNeedsMed.FieldNumber = "140.86";
            FieldObject genSvcNeedsLow = new FieldObject();
            genSvcNeedsLow.FieldNumber = "140.87";
            FieldObject incEduEmpHigh = new FieldObject();
            incEduEmpHigh.FieldNumber = "140.88";
            FieldObject incEduEmpMed = new FieldObject();
            incEduEmpMed.FieldNumber = "140.89";
            FieldObject incEduEmpLow = new FieldObject();
            incEduEmpLow.FieldNumber = "140.9";

            FieldObject[] generalServicesNeeds = new FieldObject[9];
            generalServicesNeeds[0] = new FieldObject("138.74");
            generalServicesNeeds[1] = new FieldObject("140.64");
            generalServicesNeeds[2] = new FieldObject("140.65");
            generalServicesNeeds[3] = new FieldObject("140.66");
            generalServicesNeeds[4] = new FieldObject("140.63");
            generalServicesNeeds[5] = new FieldObject("140.68");
            generalServicesNeeds[6] = new FieldObject("140.7");
            generalServicesNeeds[7] = new FieldObject("140.72");
            generalServicesNeeds[8] = new FieldObject("140.74");

            FieldObject[] incEduEmp = new FieldObject[4];
            incEduEmp[0] = new FieldObject("140.76");
            incEduEmp[1] = new FieldObject("140.78");
            incEduEmp[2] = new FieldObject("140.8");
            incEduEmp[3] = new FieldObject("140.82");

            foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(generalServicesNeeds[0].FieldNumber))
                    generalServicesNeeds[0].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(generalServicesNeeds[1].FieldNumber))
                    generalServicesNeeds[1].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(generalServicesNeeds[2].FieldNumber))
                    generalServicesNeeds[2].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(generalServicesNeeds[3].FieldNumber))
                    generalServicesNeeds[3].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(generalServicesNeeds[4].FieldNumber))
                    generalServicesNeeds[4].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(generalServicesNeeds[5].FieldNumber))
                    generalServicesNeeds[5].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(generalServicesNeeds[6].FieldNumber))
                    generalServicesNeeds[6].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(generalServicesNeeds[7].FieldNumber))
                    generalServicesNeeds[7].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(generalServicesNeeds[8].FieldNumber))
                    generalServicesNeeds[8].FieldValue = field.FieldValue;
            }
            foreach (var field in optionObject.Forms[1].CurrentRow.Fields)
            {
                if (field.FieldNumber.Equals(incEduEmp[0].FieldNumber))
                    incEduEmp[0].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(incEduEmp[1].FieldNumber))
                    incEduEmp[1].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(incEduEmp[2].FieldNumber))
                    incEduEmp[2].FieldValue = field.FieldValue;
                if (field.FieldNumber.Equals(incEduEmp[3].FieldNumber))
                    incEduEmp[3].FieldValue = field.FieldValue;
            }
            foreach (var field in generalServicesNeeds)
            {
                if (!field.FieldValue.Equals(String.Empty))
                {
                    if (field.FieldValue.Equals("1"))
                        lowCount++;
                    else if (field.FieldValue.Equals("2"))
                        medCount++;
                    else
                        highCount++;
                }
            }
            genSvcNeedsLow.FieldValue = lowCount.ToString();
            genSvcNeedsMed.FieldValue = medCount.ToString();
            genSvcNeedsHigh.FieldValue = highCount.ToString();
            lowCount = 0;
            medCount = 0;
            highCount = 0;
            foreach (var field in incEduEmp)
            {
                if (!field.FieldValue.Equals(String.Empty))
                {
                    if (field.FieldValue.Equals("1"))
                        lowCount++;
                    else if (field.FieldValue.Equals("2"))
                        medCount++;
                    else
                        highCount++;
                }
            }
            incEduEmpLow.FieldValue = lowCount.ToString();
            incEduEmpMed.FieldValue = medCount.ToString();
            incEduEmpHigh.FieldValue = highCount.ToString();
            var fields1 = new FieldObject[3];
            fields1[0] = genSvcNeedsLow;
            fields1[1] = genSvcNeedsMed;
            fields1[2] = genSvcNeedsHigh;
            var fields2 = new FieldObject[3];
            fields2[0] = incEduEmpLow;
            fields2[1] = incEduEmpMed;
            fields2[2] = incEduEmpHigh;
            var forms = new FormObject[2];
            var formObject1 = new FormObject();
            var formObject2 = new FormObject();
            var currentRow1 = new RowObject();
            var currentRow2 = new RowObject();
            currentRow1.Fields = fields1;
            currentRow1.RowId = optionObject.Forms[0].CurrentRow.RowId;
            currentRow1.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow1.RowAction = "EDIT";
            formObject1.FormId = "169";
            formObject1.CurrentRow = currentRow1;
            forms[0] = formObject1;
            currentRow2.Fields = fields2;
            currentRow2.RowId = optionObject.Forms[1].CurrentRow.RowId;
            currentRow2.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
            currentRow2.RowAction = "EDIT";
            formObject2.FormId = "175";
            formObject2.CurrentRow = currentRow2;
            forms[1] = formObject2;
            returnOptionObject.Forms = forms;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private Int32 getRatingScore(FieldObject fieldObject)
        {
            Int32 score = 0;
            switch (fieldObject.FieldValue)
            {
                case "1":
                    score = 2;
                    break;
                case "2":
                    score = 1;
                    break;
                case "3":
                    score = 0;
                    break;
                case "4":
                    score = -1;
                    break;
                case "5":
                    score = -2;
                    break;
                case "6":
                    score = -3;
                    break;
                default:
                    break;
            }
            return score;
        }
        private String getTreatmentPlanName(String planNameCode)
        {
            String planNameValue = null;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBCCWS"].ConnectionString;
            var commandText = "SELECT cw_dictionaries.dictionary_code, cw_dictionaries.dictionary_value " +
                                "FROM SYSTEM.cw_dictionaries " +
                                "WHERE cw_dictionaries.field_number=? AND cw_dictionaries.dictionary_code=?";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var parameter1 = new OdbcParameter("field_number", "52134.1");
                    dbcommand.Parameters.Add(parameter1);
                    var parameter2 = new OdbcParameter("dictionary_code", planNameCode);
                    dbcommand.Parameters.Add(parameter2);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            planNameValue = reader["dictionary_value"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            return planNameValue;
        }
        private OptionObject TreatmentPlan(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            FieldObject planNameDict = new FieldObject();
            planNameDict.FieldNumber = "52134";
            FieldObject planName = new FieldObject();
            planName.FieldNumber = "52002";
            foreach (var form in optionObject.Forms)
            {
                foreach (var field in form.CurrentRow.Fields)
                {
                    if (field.FieldNumber.Equals(planNameDict.FieldNumber))
                        planNameDict.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(planName.FieldNumber))
                        planName.FieldValue = field.FieldValue;
                }
            }
            planName.Enabled = "0";
            planName.FieldValue = getTreatmentPlanName(planNameDict.FieldValue);
            var fields = new FieldObject[1];
            var forms = new FormObject[1];
            var formObject = new FormObject();
            var currentRow = new RowObject();
            fields[0] = planName;
            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";

            formObject.FormId = "52001";
            formObject.CurrentRow = currentRow;
            forms[0] = formObject;
            returnOptionObject.Forms = forms;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private OptionObject DisabilityAlert(OptionObject optionObject)
        {
            //form id is 54000
            //only 1 form id
            OptionObject returnOptionObject = new OptionObject();
            FieldObject primaryIncomeSource = new FieldObject();//disability dictionary code is 4
            primaryIncomeSource.FieldNumber = "54008";
            FieldObject disabilityIncome = new FieldObject();//yes is 1, no is 0
            disabilityIncome.FieldNumber = "54011";

            foreach (var form in optionObject.Forms)
            {
                foreach (var field in form.CurrentRow.Fields)
                {
                    if (field.FieldNumber.Equals(disabilityIncome.FieldNumber))
                        disabilityIncome.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(primaryIncomeSource.FieldNumber))
                        primaryIncomeSource.FieldValue = field.FieldValue;
                }
            }
            if (disabilityIncome.FieldValue.Equals("1"))
            {
                returnOptionObject.ErrorCode = 3;
                returnOptionObject.ErrorMesg = "The client receives Psychiatric Disability Income.";
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private OptionObject CheckNoteType(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            FieldObject homeVisit = new FieldObject();
            homeVisit.FieldNumber = "7003.3";
            FieldObject noteType = new FieldObject();
            noteType.FieldNumber = "10751";
            foreach (var form in optionObject.Forms)
            {
                foreach (var currentField in form.CurrentRow.Fields)
                {
                    if (currentField.FieldNumber.Equals(noteType.FieldNumber))
                        noteType.FieldValue = currentField.FieldValue;
                }
            }
            if (!noteType.FieldValue.Equals("15"))
            {
                homeVisit.Required = "0";
                homeVisit.Enabled = "0";
                var fields = new FieldObject[1];
                var forms = new FormObject[1];
                var formObject = new FormObject();
                var currentRow = new RowObject();
                fields[0] = homeVisit;
                currentRow.Fields = fields;
                currentRow.RowId = optionObject.Forms[1].CurrentRow.RowId;
                currentRow.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
                currentRow.RowAction = "EDIT";

                formObject.FormId = "21005";
                formObject.CurrentRow = currentRow;
                forms[0] = formObject;
                returnOptionObject.Forms = forms;
            }
            else
            {
                homeVisit.Required = "1";
                homeVisit.Enabled = "1";
                var fields = new FieldObject[1];
                var forms = new FormObject[1];
                var formObject = new FormObject();
                var currentRow = new RowObject();
                fields[0] = homeVisit;
                currentRow.Fields = fields;
                currentRow.RowId = optionObject.Forms[1].CurrentRow.RowId;
                currentRow.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
                currentRow.RowAction = "EDIT";

                formObject.FormId = "21005";
                formObject.CurrentRow = currentRow;
                forms[0] = formObject;
                returnOptionObject.Forms = forms;
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private Boolean checkUserRole(String UserID)
        {
            Boolean applyScriptLinkRules = true;
            var connectionString1 = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText1 = "SELECT RADplus_users.USERID, " +
                                "RADplus_users.USERROLE " +
                                "FROM SYSTEM.RADplus_users RADplus_users " +
                                "WHERE RADplus_users.USERID=? " +
                                "AND (RADplus_users.USERROLE LIKE '%&ARBILLING&%' " +
                                "OR RADplus_users.USERROLE LIKE '%&ITALL&%') ";
            using (var connection1 = new OdbcConnection(connectionString1))
            {
                connection1.Open();
                using (var dbcommand = new OdbcCommand(commandText1, connection1))
                {
                    var parameter1 = new OdbcParameter("USERID", UserID);
                    dbcommand.Parameters.Add(parameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            applyScriptLinkRules = false;
                        }
                    }
                }
                connection1.Close();
            }
            return applyScriptLinkRules;
        }
        private OptionObject checkFieldFormat(FieldObject fieldObject, OptionObject optionObject, String fieldName)
        {
            if (fieldObject.FieldValue.Equals(optionObject.EntityID))
            {
                optionObject.ErrorCode = 1;
                optionObject.ErrorMesg = "The " + fieldName + " cannot be the Client's ID.\n" +
                                        "Please enter the correct information, or if the " + fieldName + " is not applicable for this guarantor leave it blank.";
            }
            else if (fieldObject.FieldValue.Length < 9 && fieldObject.FieldValue.Length > 0)
            {
                optionObject.ErrorCode = 1;
                optionObject.ErrorMesg = "The " + fieldName + " cannot be less than 10 characters in length.\n" +
                                        "Please check that the number entered is the correct number, or if the " + fieldName + " is not applicable for this guarantor leave it blank.";
            }
            else if (fieldObject.FieldValue.IndexOf(' ') != -1)
            {
                optionObject.ErrorCode = 1;
                optionObject.ErrorMesg = "The " + fieldName + " cannot contain any spaces.\n" +
                                        "Please check that the number entered is the correct number, or if the " + fieldName + " is not applicable for this guarantor leave it blank.";
            }
            return optionObject;
        }
        private OptionObject CheckSubscriberPolicy(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            FieldObject policyNumField = new FieldObject();
            policyNumField.FieldNumber = "263";
            foreach (var form in optionObject.Forms)
            {
                foreach (var currentField in form.CurrentRow.Fields)
                {
                    if (currentField.FieldNumber.Equals(policyNumField.FieldNumber))
                        policyNumField.FieldValue = currentField.FieldValue;
                }
            }
            if (checkUserRole(optionObject.OptionUserId))
                checkFieldFormat(policyNumField, returnOptionObject, "Subscriber Policy Number");
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private OptionObject CheckSubscriberMedicaid(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            FieldObject policyNumField = new FieldObject();
            policyNumField.FieldNumber = "263";
            FieldObject medicaidField = new FieldObject();
            medicaidField.FieldNumber = "1057";
            FieldObject guarantorID = new FieldObject();
            guarantorID.FieldNumber = "680";
            foreach (var form in optionObject.Forms)
            {
                foreach (var currentField in form.CurrentRow.Fields)
                {
                    if (currentField.FieldNumber.Equals(policyNumField.FieldNumber))
                        policyNumField.FieldValue = currentField.FieldValue;
                    if (currentField.FieldNumber.Equals(medicaidField.FieldNumber))
                        medicaidField.FieldValue = currentField.FieldValue;
                    if (currentField.FieldNumber.Equals(guarantorID.FieldNumber))
                        guarantorID.FieldValue = currentField.FieldValue;
                }
            }
            if (checkUserRole(optionObject.OptionUserId))
            {
                checkFieldFormat(medicaidField, returnOptionObject, "Subscriber Medicaid Number");
                if (returnOptionObject.ErrorCode != 1)
                {
                    String financialClassCode = checkFinancialCLass(guarantorID);
                    if (financialClassCode.Equals("3"))//medicaid class
                    {
                        policyNumField.FieldValue = medicaidField.FieldValue;
                        var fields = new FieldObject[1];
                        var forms = new FormObject[1];
                        var formObject = new FormObject();
                        var currentRow = new RowObject();
                        fields[0] = policyNumField;

                        currentRow.Fields = fields;
                        currentRow.RowId = optionObject.Forms[1].CurrentRow.RowId;
                        currentRow.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
                        currentRow.RowAction = "EDIT";

                        formObject.FormId = "3001";
                        formObject.CurrentRow = currentRow;
                        formObject.MultipleIteration = true;
                        forms[0] = formObject;
                        returnOptionObject.Forms = forms;
                    }
                }
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private OptionObject CheckSubscriberMedicare(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            FieldObject policyNumField = new FieldObject();
            policyNumField.FieldNumber = "263";
            FieldObject medicareField = new FieldObject();
            medicareField.FieldNumber = "818";
            FieldObject guarantorID = new FieldObject();
            guarantorID.FieldNumber = "680";
            foreach (var form in optionObject.Forms)
            {
                foreach (var currentField in form.CurrentRow.Fields)
                {
                    if (currentField.FieldNumber.Equals(policyNumField.FieldNumber))
                        policyNumField.FieldValue = currentField.FieldValue;
                    if (currentField.FieldNumber.Equals(medicareField.FieldNumber))
                        medicareField.FieldValue = currentField.FieldValue;
                    if (currentField.FieldNumber.Equals(guarantorID.FieldNumber))
                        guarantorID.FieldValue = currentField.FieldValue;
                }
            }
            if (checkUserRole(optionObject.OptionUserId))
            {
                checkFieldFormat(medicareField, returnOptionObject, "Subscriber Medicare Number");
                if (returnOptionObject.ErrorCode != 1)
                {
                    String financialClassCode = checkFinancialCLass(guarantorID);
                    if (financialClassCode.Equals("8"))//medicaid class
                    {
                        policyNumField.FieldValue = medicareField.FieldValue;
                        var fields = new FieldObject[1];
                        var forms = new FormObject[1];
                        var formObject = new FormObject();
                        var currentRow = new RowObject();
                        fields[0] = policyNumField;

                        currentRow.Fields = fields;
                        currentRow.RowId = optionObject.Forms[1].CurrentRow.RowId;
                        currentRow.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
                        currentRow.RowAction = "EDIT";

                        formObject.FormId = "3001";
                        formObject.CurrentRow = currentRow;
                        formObject.MultipleIteration = true;
                        forms[0] = formObject;
                        returnOptionObject.Forms = forms;
                    }
                }
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            return returnOptionObject;
        }
        private String checkFinancialCLass(FieldObject fieldObject)
        {
            String financialClassCode = "-1";
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT billing_guar_table.GUARANTOR_ID, " +
                                "billing_guar_table.guarantor_name, " +
                                "billing_guar_table.financial_class_code, " +
                                "billing_guar_table.financial_class_value " +
                                "FROM SYSTEM.billing_guar_table billing_guar_table " +
                                "WHERE billing_guar_table.GUARANTOR_ID=?";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var parameter1 = new OdbcParameter("GUARANTOR_ID", fieldObject.FieldValue);
                    dbcommand.Parameters.Add(parameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            financialClassCode = reader["financial_class_code"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            return financialClassCode;
        }
        private OptionObject Discharge(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            String patientName = null;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT patient_name FROM SYSTEM.patient_current_demographics " +
                                "WHERE PATID=?";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var dbparameter1 = new OdbcParameter("PATID", optionObject.EntityID);
                    dbcommand.Parameters.Add(dbparameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patientName = reader["patient_name"].ToString();
                        }
                    }
                }
                connection.Close();
            }
            returnOptionObject.ErrorCode = 4;
            returnOptionObject.ErrorMesg = "<p>You are filing a discharge for Client " +
                                            "<b>(" + optionObject.EntityID + ") " + patientName + "</b> " +
                                            "from <b>Episode # " + optionObject.EpisodeNumber + ".</b></p><br>" +
                                            "<p>Please make sure this information is correct, because once filed all future appointments will be deleted from the system " +
                                            "and cannot be recovered.</p><br>" +
                                            "<b><p style=\"text-align:center;size:20px;\" >Do you wish to continue?</p></b>";
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;

            return returnOptionObject;
        }
        private OptionObject CheckDiagnosis(OptionObject optionObject, string scriptName)
        {
            OptionObject returnOptionObject = new OptionObject();
            var fieldList = scriptName.Split(',').ToList().Skip(1);
            try
            {
                var dateField = new FieldObject { FieldNumber = fieldList.ElementAt(0) };
                foreach (var form in optionObject.Forms)
                {
                    foreach (var field in form.CurrentRow.Fields)
                    {
                        if (field.FieldNumber.Equals(dateField.FieldNumber))
                            dateField.FieldValue = field.FieldValue;
                    }
                }
                if (!IsPrimaryCareEpisode(optionObject.EntityID, optionObject.EpisodeNumber))
                {
                    var clientDiagnosisObj = previousDiagnosisExist(optionObject);
                    if (clientDiagnosisObj == null)
                    {
                        returnOptionObject.ErrorCode = 3;
                        returnOptionObject.ErrorMesg = "This client does not have a diagnosis on file.\n" +
                                                        "Progress notes will not bill properly without a diagnosis.\n" +
                                                        "Please create a diagnosis for this client.\n" +
                                                        "If your supervisor verifies that your program/guarantor does not require a diagnosis," +
                                                        "you must enter the diagnosis of 799.9 (Diagnosis Deferred).";
                    }
                    else if ((clientDiagnosisObj.DateOfDiagnosis.CompareTo(DateTime.Parse(dateField.FieldValue))) > 0)
                    {
                        returnOptionObject.ErrorCode = 2;
                        returnOptionObject.ErrorMesg = "The service date entered is before the diagnosis date on file: " +
                                                        "\n\nDiagnosis: " + clientDiagnosisObj.DiagnosisAxisI1 +
                                                        "\nDate: " + clientDiagnosisObj.DateOfDiagnosis.ToString("MM/dd/yyyy") +
                                                        ",  Time: " + clientDiagnosisObj.TimeOfDiagnosis +
                                                        "\nDiagnosis Type: " + clientDiagnosisObj.TypeOfDiagnosis +
                                                        "\nDiagnosing Practitioner: " + clientDiagnosisObj.DiagnosingPractitioner +
                                                        "\n\nThis service will <b>not</b> bill if the date is before the diagnosis date.";
                    }
                }
            }
            catch (Exception e)
            {
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            if (optionObject.OptionStaffId.Equals("003819"))
                returnOptionObject.ErrorMesg = returnOptionObject.ErrorMesg + "\n\nLlyan, we need more INDIAN SPICE!!!\n\n";
            return returnOptionObject;
        }

        private bool IsPrimaryCareEpisode(string p, double p_2)
        {
            bool returnValue = false;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            #region SQLQuery
            var commandText = "SELECT primary_care_pgm_code FROM SYSTEM.episode_history ep " +
                "INNER JOIN SYSTEM.table_program_definition prog " +
                "ON ep.program_code = prog.program_code " +
                "AND ep.FACILITY = prog.FACILITY " +
                "WHERE ep.PATID=? " +
                "AND ep.EPISODE_NUMBER=? ";
            #endregion
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    dbcommand.Parameters.Add(new OdbcParameter("PATID", p));
                    dbcommand.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", p_2.ToString()));
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader["primary_care_pgm_code"].ToString().Equals("Y"))
                            returnValue = true;
                    }
                }
            }
            return returnValue;
        }
        private OptionObject SocialSecurityDefault(OptionObject optionObject, Boolean isAdmissionScreen)
        {
            OptionObject returnOptionObject = new OptionObject();
            String field6Value = null;
            String field90Value = null;
            String field171Value = null;

            foreach (var form in optionObject.Forms)
            {
                foreach (var currentField in form.CurrentRow.Fields)
                {
                    if (currentField.FieldNumber.Equals("6"))
                        field6Value = currentField.FieldValue;
                    if (currentField.FieldNumber.Equals("90"))
                        field90Value = currentField.FieldValue;
                    if (currentField.FieldNumber.Equals("171"))
                        field171Value = currentField.FieldValue;
                }
            }

            var socialSecurityField = new FieldObject();
            var addressField = new FieldObject();
            var lastNameField = new FieldObject();
            var form510Fields = new FieldObject[1];
            var form523Fields = new FieldObject[2];
            var form14Fields = new FieldObject[3];
            var admissionForms = new FormObject[2];
            var demoForms = new FormObject[1];
            var form510 = new FormObject();
            var form523 = new FormObject();
            var form14 = new FormObject();
            var currentRow1 = new RowObject();
            var currentRow2 = new RowObject();

            #region Invalid Social Security Number
            socialSecurityField.FieldNumber = "6";
            if (field6Value != "999-99-9999")
            {
                socialSecurityField.FieldValue = field6Value;
            }
            else
            {
                socialSecurityField.FieldValue = "888-88-8888";
                returnOptionObject.ErrorCode = 3;
                returnOptionObject.ErrorMesg = "The Social Security Number entered is not a valid number for Florida State Reporting.\n" +
                                                "The number entered will be replaced by 888-88-8888." +
                                                "If you know the correct Social Security Number, please go back and enter the appropriate information";
            }
            #endregion

            #region Invalid Character in Address Field
            addressField.FieldNumber = "90";
            if (field90Value.IndexOf("#") == -1)
            {
                addressField.FieldValue = field90Value;
            }
            else
            {
                addressField.FieldValue = field90Value.Replace("#", "");
                returnOptionObject.ErrorCode = 3;
                returnOptionObject.ErrorMesg = "The address entered contains an invalid character ('#') for billing purposes.\n" +
                                                "The character will be removed from the address.";
            }
            #endregion

            #region Invalid Character in Name Field
            lastNameField.FieldNumber = "171";
            if (field171Value.IndexOf("'") == -1 && field171Value.IndexOf("-") == -1)
            {
                lastNameField.FieldValue = field171Value;
            }
            else
            {
                lastNameField.FieldValue = field171Value.Replace("'", "");
                lastNameField.FieldValue = lastNameField.FieldValue.Replace("-", "");
                returnOptionObject.ErrorCode = 3;
                returnOptionObject.ErrorMesg = "The Last Name field contains an invalid character for billing purposes.\n" +
                                                "The character will be removed from the client's last name.";
            }
            #endregion

            if (isAdmissionScreen)
            {
                form510Fields[0] = socialSecurityField;
                form523Fields[0] = addressField;
                form523Fields[1] = lastNameField;

                currentRow1.Fields = form510Fields;
                currentRow1.RowId = optionObject.Forms[0].CurrentRow.RowId;
                currentRow1.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
                currentRow1.RowAction = "EDIT";
                form510.FormId = "510";
                form510.CurrentRow = currentRow1;
                admissionForms[0] = form510;

                currentRow2.Fields = form523Fields;
                currentRow2.RowId = optionObject.Forms[1].CurrentRow.RowId;
                currentRow2.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
                currentRow2.RowAction = "EDIT";
                form523.FormId = "523";
                form523.CurrentRow = currentRow2;
                admissionForms[1] = form523;
                returnOptionObject.Forms = admissionForms;
            }
            else
            {
                form14Fields[0] = socialSecurityField;
                form14Fields[1] = addressField;
                form14Fields[2] = lastNameField;

                currentRow1.Fields = form14Fields;
                currentRow1.RowId = optionObject.Forms[0].CurrentRow.RowId;
                currentRow1.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
                currentRow1.RowAction = "EDIT";
                form14.FormId = "14";
                form14.CurrentRow = currentRow1;
                demoForms[0] = form14;
                returnOptionObject.Forms = demoForms;
            }

            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;

            return returnOptionObject;
        }
        private OptionObject SourceOfAdmission(OptionObject optionObject)
        {
            OptionObject returnOptionObject = new OptionObject();
            String field158Value = null;

            foreach (var form in optionObject.Forms)
            {
                foreach (var currentField in form.CurrentRow.Fields)
                {
                    if (currentField.FieldNumber.Equals("158"))
                        field158Value = currentField.FieldValue;
                }
            }

            var field1 = new FieldObject();
            var fields = new FieldObject[1];
            var forms = new FormObject[1];
            var formObject = new FormObject();
            var currentRow = new RowObject();

            field1.FieldNumber = "158";
            field1.FieldValue = field158Value;
            field1.Required = "1";
            fields[0] = field1;

            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";

            formObject.FormId = "510";
            formObject.CurrentRow = currentRow;
            forms[0] = formObject;
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.Forms = forms;

            return returnOptionObject;
        }
        private OptionObject MakeTimeRequired(OptionObject optionObject, Boolean isInpatientNote)
        {
            OptionObject returnOptionObject = new OptionObject();
            String field3003Value = "";
            String field3004Value = "";

            foreach (var form in optionObject.Forms)
            {
                foreach (var currentField in form.CurrentRow.Fields)
                {
                    if (currentField.FieldNumber.Equals("3003"))
                        field3003Value = currentField.FieldValue;
                    if (currentField.FieldNumber.Equals("3004"))
                        field3004Value = currentField.FieldValue;
                }
            }

            var field1 = new FieldObject();
            var field2 = new FieldObject();
            var fields = new FieldObject[2];
            var forms = new FormObject[1];
            var formObject = new FormObject();
            var currentRow = new RowObject();

            field1.FieldNumber = "3003";
            field1.Required = "1";
            field1.FieldValue = field3003Value;
            fields[0] = field1;
            field2.FieldNumber = "3004";
            field2.Required = "1";
            field2.FieldValue = field3004Value;
            field2.Enabled = ConfigurationManager.AppSettings["enableEndTime"].ToString().Equals("true") ? "1" : "0";
            fields[1] = field2;

            currentRow.Fields = fields;
            currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
            currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
            currentRow.RowAction = "EDIT";

            if (isInpatientNote)
                formObject.FormId = "7000";
            else
                formObject.FormId = "7001";
            formObject.CurrentRow = currentRow;
            forms[0] = formObject;

            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            returnOptionObject.Forms = forms;

            return returnOptionObject;
        }
        private OptionObject AddDuration(OptionObject optionObject, Boolean isInpatientNote)
        {
            var returnOptionObject = new OptionObject();
            var duration = new FieldObject { FieldNumber = "51003" };
            var startTime = new FieldObject { FieldNumber = "3003" };
            var endTime = new FieldObject { FieldNumber = "3004" };
            var serviceCode = new FieldObject { FieldNumber = "51001" };
            try
            {
                foreach (var field in optionObject.Forms[0].CurrentRow.Fields)
                {
                    if (field.FieldNumber.Equals(duration.FieldNumber))
                        duration.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(endTime.FieldNumber))
                        endTime.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(startTime.FieldNumber))
                        startTime.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(serviceCode.FieldNumber))
                        serviceCode.FieldValue = field.FieldValue;
                }
                if (!duration.FieldValue.Equals(String.Empty))
                {
                    if (duration.FieldValue.Equals("0"))
                    {
                        returnOptionObject.ErrorCode = 1;
                        returnOptionObject.ErrorMesg = "The note duration cannot be equal to zero.\r\nPlease check and enter the correct duration.";
                    }
                    else if (Convert.ToInt32(duration.FieldValue) > 0)
                    {
                        if (Convert.ToInt32(duration.FieldValue) > 120)
                        {
                            returnOptionObject.ErrorCode = 3;
                            returnOptionObject.ErrorMesg = "The service duration is more than 120 minutes, please make sure this is correct before proceeding.";
                        }
                        if (!startTime.FieldValue.Equals(String.Empty))
                        {
                            endTime.FieldValue = DateTime.Parse(startTime.FieldValue).AddMinutes(Convert.ToDouble(duration.FieldValue)).ToString("hh:mm tt");
                            var fields = new FieldObject[1];
                            fields[0] = endTime;
                            var currentRow = new RowObject();
                            currentRow.Fields = fields;
                            currentRow.RowId = optionObject.Forms[0].CurrentRow.RowId;
                            currentRow.ParentRowId = optionObject.Forms[0].CurrentRow.ParentRowId;
                            currentRow.RowAction = "EDIT";
                            var formObject = new FormObject();
                            if (isInpatientNote)
                                formObject.FormId = "7000";
                            else
                                formObject.FormId = "7001";
                            formObject.CurrentRow = currentRow;
                            var forms = new FormObject[1];
                            forms[0] = formObject;
                            returnOptionObject.Forms = forms;
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;

            return returnOptionObject;
        }
        private OptionObject CheckOverlap(OptionObject optionObject, String OptionID)
        {
            var returnOptionObject = new OptionObject();
            var dateField = new FieldObject();
            var startTimeField = new FieldObject();
            var durationField = new FieldObject();
            var serviceCodeField = new FieldObject();
            var formStatusField = new FieldObject();
            String message = null;
            switch (OptionID)
            {
                case "PsyEval":
                    dateField.FieldNumber = "127.4";
                    startTimeField.FieldNumber = "127.42";
                    durationField.FieldNumber = "142.93";
                    serviceCodeField.FieldNumber = "142.92";
                    formStatusField.FieldNumber = "141.24";
                    break;
                case "OPN":
                    dateField.FieldNumber = "51011";
                    startTimeField.FieldNumber = "3003";
                    durationField.FieldNumber = "51003";
                    serviceCodeField.FieldNumber = "51001";
                    formStatusField.FieldNumber = "50010";
                    break;
                case "PsyHist":
                    dateField.FieldNumber = "126.55";
                    startTimeField.FieldNumber = "134.7";
                    durationField.FieldNumber = "142.98";
                    serviceCodeField.FieldNumber = "142.96";
                    formStatusField.FieldNumber = "141";
                    break;
                case "TXPN":
                    dateField.FieldNumber = "144.08";
                    startTimeField.FieldNumber = "144.09";
                    durationField.FieldNumber = "144.1";
                    serviceCodeField.FieldNumber = "144.11";
                    formStatusField.FieldNumber = "144.16";
                    break;
                case "MedNote":
                    dateField.FieldNumber = "151.95";
                    startTimeField.FieldNumber = "152.01";
                    durationField.FieldNumber = "152.32";
                    serviceCodeField.FieldNumber = "152.3";
                    formStatusField.FieldNumber = "152.36";
                    break;
                default:
                    break;
            }
            foreach (var form in optionObject.Forms)
            {
                foreach (var field in form.CurrentRow.Fields)
                {
                    if (field.FieldNumber.Equals(dateField.FieldNumber))
                        dateField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(startTimeField.FieldNumber))
                        startTimeField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(durationField.FieldNumber))
                        durationField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(serviceCodeField.FieldNumber))
                        serviceCodeField.FieldValue = field.FieldValue;
                    if (field.FieldNumber.Equals(formStatusField.FieldNumber))
                        formStatusField.FieldValue = field.FieldValue;
                }
            }
            if (formStatusField.FieldValue.Equals("F"))
            {
                try
                {
                    if (!isSkipDuplicateCheck(serviceCodeField.FieldValue))
                    {
                        message = isClientOverlap(optionObject.EntityID, DateTime.Parse(dateField.FieldValue).ToString("yyyy-MM-dd"), startTimeField.FieldValue, durationField.FieldValue);
                        //message += isStaffOverlap(optionObject.OptionStaffId, DateTime.Parse(dateField.FieldValue).ToString("yyyy-MM-dd"), startTimeField.FieldValue, durationField.FieldValue);
                    }
                }
                catch (Exception e)
                {
                }
                if (message != null)
                {
                    returnOptionObject.ErrorCode = 4;
                    returnOptionObject.ErrorMesg += "The current service being entered is overlapping with the following service(s): \n\n" +
                                                    message + "\n" +
                                                    "Do you wish to continue filing? (y/n)";
                }
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;

            return returnOptionObject;
        }
        private String isClientOverlap(String PATID, String Date, String StartTime, String Duration)
        {
            String temp = null;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            #region SQLQuery
            var commandText = "SELECT billing_tx_history.date_of_service, " +
                                "billing_tx_history.duration, " +
                                "dateadd('mi',billing_tx_history.duration,cast(billing_tx_history.start_time as DATETIME)) as end_time, " +
                                "billing_tx_history.PROVIDER_ID, " +
                                "billing_tx_history.SERVICE_CODE, " +
                                "cast(billing_tx_history.start_time as DATETIME) as start_time, " +
                                "billing_tx_history.v_PROVIDER_NAME, " +
                                "billing_tx_history.PATID, " +
                                "billing_tx_history.v_patient_name," +
                                "billing_tx_history.v_service_value " +
                                "FROM SYSTEM.billing_tx_history " +
                                "INNER JOIN SYSTEM.Service_Code_Overlap_Setup " +
                                "ON billing_tx_history.SERVICE_CODE = Service_Code_Overlap_Setup.Service_Code " +
                                "AND billing_tx_history.FACILITY = Service_Code_Overlap_Setup.FACILITY " +
                                "WHERE billing_tx_history.date_of_service=? " +
                                "AND billing_tx_history.PATID=? " +
                                "AND billing_tx_history.JOIN_TO_TX_HISTORY = billing_tx_history.ORIG_JOIN_TO_TX_HISTORY " +
                                "AND Service_Code_Overlap_Setup.Skip_Overlapping_Check= 'N' " +
                                "AND ( " +
                                "(dateadd('mi',1,cast('" + StartTime + "' as DATETIME)) BETWEEN cast(billing_tx_history.start_time as DATETIME) AND dateadd('mi',billing_tx_history.duration,cast(billing_tx_history.start_time as DATETIME))) " +
                                "OR (dateadd('mi'," + Duration + "-1,cast('" + StartTime + "' as DATETIME)) BETWEEN cast(billing_tx_history.start_time as DATETIME) AND dateadd('mi',billing_tx_history.duration,cast(billing_tx_history.start_time as DATETIME))) " +
                                "OR (cast('" + StartTime + "' as DATETIME) < cast(billing_tx_history.start_time as DATETIME) AND dateadd('mi'," + Duration + ",cast('" + StartTime + "' as DATETIME)) > dateadd('mi',billing_tx_history.duration,cast(billing_tx_history.start_time as DATETIME))) " +
                                ") " +
                                "GROUP BY billing_tx_history.ID ";
            #endregion
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var dbparameter2 = new OdbcParameter("date_of_service", Date);
                    dbcommand.Parameters.Add(dbparameter2);
                    var dbparameter1 = new OdbcParameter("PATID", PATID);
                    dbcommand.Parameters.Add(dbparameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read() && i < 3)
                        {
                            temp += "Client: (" + reader["PATID"].ToString().PadLeft(8, '0') + ") " + reader["v_patient_name"].ToString() + "\n" +
                                    "Service Date: " + DateTime.Parse(reader["date_of_service"].ToString()).ToString("MM/dd/yyyy") + "\n" +
                                    "Service Type: (" + reader["SERVICE_CODE"].ToString() + ") " + reader["v_service_value"].ToString() + "\n" +
                                    "Practitioner: (" + reader["PROVIDER_ID"].ToString() + ") " + reader["v_PROVIDER_NAME"].ToString() + "\n" +
                                    "Start Time: " + DateTime.Parse(reader["start_time"].ToString()).ToString("hh:mm tt") + "\n" +
                                    "End Time: " + DateTime.Parse(reader["end_time"].ToString()).ToString("hh:mm tt") + "\n" +
                                    "Duration: " + reader["duration"].ToString() + "\n\n";
                            i++;
                        }
                    }
                }
                connection.Close();
            }
            return temp;
        }
        private String isStaffOverlap(String StaffID, String Date, String StartTime, String Duration)
        {
            String temp = null;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            #region SQLQuery
            var commandText = "SELECT billing_tx_history.date_of_service, " +
                                "billing_tx_history.duration, " +
                                "dateadd('mi',billing_tx_history.duration,cast(billing_tx_history.start_time as DATETIME)) as end_time, " +
                                "billing_tx_history.PROVIDER_ID, " +
                                "billing_tx_history.SERVICE_CODE, " +
                                "cast(billing_tx_history.start_time as DATETIME) as start_time, " +
                                "billing_tx_history.v_PROVIDER_NAME, " +
                                "billing_tx_history.PATID, " +
                                "billing_tx_history.v_patient_name," +
                                "billing_tx_history.v_service_value " +
                                "FROM SYSTEM.billing_tx_history " +
                                "INNER JOIN SYSTEM.Service_Code_Overlap_Setup " +
                                "ON billing_tx_history.SERVICE_CODE = Service_Code_Overlap_Setup.Service_Code " +
                                "AND billing_tx_history.FACILITY = Service_Code_Overlap_Setup.FACILITY " +
                                "WHERE billing_tx_history.date_of_service=? " +
                                "AND billing_tx_history.PROVIDER_ID=? " +
                                "AND billing_tx_history.JOIN_TO_TX_HISTORY = billing_tx_history.ORIG_JOIN_TO_TX_HISTORY " +
                                "AND Service_Code_Overlap_Setup.Skip_Overlapping_Check= 'N' " +
                                "AND ( " +
                                "(dateadd('mi',1,cast('" + StartTime + "' as DATETIME)) BETWEEN cast(billing_tx_history.start_time as DATETIME) AND dateadd('mi',billing_tx_history.duration,cast(billing_tx_history.start_time as DATETIME))) " +
                                "OR (dateadd('mi'," + Duration + "-1,cast('" + StartTime + "' as DATETIME)) BETWEEN cast(billing_tx_history.start_time as DATETIME) AND dateadd('mi',billing_tx_history.duration,cast(billing_tx_history.start_time as DATETIME))) " +
                                "OR (cast('" + StartTime + "' as DATETIME) < cast(billing_tx_history.start_time as DATETIME) AND dateadd('mi'," + Duration + ",cast('" + StartTime + "' as DATETIME)) > dateadd('mi',billing_tx_history.duration,cast(billing_tx_history.start_time as DATETIME))) " +
                                ") " +
                                "GROUP BY billing_tx_history.ID ";
            #endregion
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var dbparameter2 = new OdbcParameter("date_of_service", Date);
                    dbcommand.Parameters.Add(dbparameter2);
                    var dbparameter1 = new OdbcParameter("PROVIDER_ID", StaffID);
                    dbcommand.Parameters.Add(dbparameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read() && i < 3)
                        {
                            temp += "Client: (" + reader["PATID"].ToString().PadLeft(8, '0') + ") " + reader["v_patient_name"].ToString() + "\n" +
                                    "Service Date: " + DateTime.Parse(reader["date_of_service"].ToString()).ToString("MM/dd/yyyy") + "\n" +
                                    "Service Type: (" + reader["SERVICE_CODE"].ToString() + ") " + reader["v_service_value"].ToString() + "\n" +
                                    "Practitioner: (" + reader["PROVIDER_ID"].ToString() + ") " + reader["v_PROVIDER_NAME"].ToString() + "\n" +
                                    "Start Time: " + DateTime.Parse(reader["start_time"].ToString()).ToString("hh:mm tt") + "\n" +
                                    "End Time: " + DateTime.Parse(reader["end_time"].ToString()).ToString("hh:mm tt") + "\n" +
                                    "Duration: " + reader["duration"].ToString() + "\n\n";
                            i++;
                        }
                    }
                }
                connection.Close();
            }
            return temp;
        }
        private Boolean isSkipDuplicateCheck(String ServiceCode)
        {
            Boolean isSkipDuplicateCheck = false;
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            #region SQLQuery
            var commandText = "SELECT count(Service_Code_Overlap_Setup.Service_Code) as num_of_recs " +
                                "FROM SYSTEM.Service_Code_Overlap_Setup " +
                                "WHERE Service_Code_Overlap_Setup.Service_Code=? " +
                                "AND Service_Code_Overlap_Setup.Skip_Overlapping_Check= 'N' " +
                                "AND Service_Code_Overlap_Setup.PATID='1006474' " +
                                "AND Service_Code_Overlap_Setup.EPISODE_NUMBER='3' ";
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (var dbcommand = new OdbcCommand(commandText, connection))
                {
                    var dbparameter1 = new OdbcParameter("Service_Code", ServiceCode);
                    dbcommand.Parameters.Add(dbparameter1);
                    using (var reader = dbcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["num_of_recs"].ToString().Equals("0"))
                            {
                                isSkipDuplicateCheck = true;
                            }
                        }
                    }
                }
                connection.Close();
            }
            #endregion
            return isSkipDuplicateCheck;
        }
        private OptionObject AddDurationAndTime(OptionObject optionObject, String ScriptName)
        {
            String[] temp = ScriptName.Split(',');
            var returnOptionObject = new OptionObject();
            var formObject = new FormObject();
            var startTimeField = new FieldObject();
            var endTimeField = new FieldObject();
            var durationField = new FieldObject();
            try
            {
                formObject.FormId = temp[1];
                startTimeField.FieldNumber = temp[2];
                endTimeField.FieldNumber = temp[3];
                durationField.FieldNumber = temp[4];
                foreach (var form in optionObject.Forms)
                {
                    foreach (var field in form.CurrentRow.Fields)
                    {
                        if (field.FieldNumber.Equals(startTimeField.FieldNumber))
                            startTimeField.FieldValue = field.FieldValue;
                        if (field.FieldNumber.Equals(endTimeField.FieldNumber))
                            endTimeField.FieldValue = field.FieldValue;
                        if (field.FieldNumber.Equals(durationField.FieldNumber))
                            durationField.FieldValue = field.FieldValue;
                    }
                }
                if (temp[5].Equals("Dur"))
                {
                    durationField.FieldValue = ChangeDuration(DateTime.Parse(startTimeField.FieldValue), DateTime.Parse(endTimeField.FieldValue)).TotalMinutes.ToString();
                }
                else
                {
                    endTimeField.FieldValue = ChangeEndTime(DateTime.Parse(startTimeField.FieldValue), Double.Parse(durationField.FieldValue)).ToString("hh:mm tt");
                }
                var fields = new FieldObject[1];
                fields[0] = temp[5].Equals("Dur") ? durationField : endTimeField;
                var currentRow = new RowObject();
                currentRow.Fields = fields;
                foreach (var form in optionObject.Forms)
                {
                    if (form.FormId.Equals(formObject.FormId))
                    {
                        currentRow.RowId = form.CurrentRow.RowId;
                        currentRow.ParentRowId = form.CurrentRow.ParentRowId;
                    }
                }
                currentRow.RowAction = "EDIT";
                formObject.CurrentRow = currentRow;
                var forms = new FormObject[1];
                forms[0] = formObject;
                returnOptionObject.Forms = forms;
            }
            catch (Exception e)
            {
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;
        }
        private DateTime ChangeEndTime(DateTime startTime, Double duration)
        {
            return startTime.AddMinutes(duration);
        }
        private TimeSpan ChangeDuration(DateTime startTime, DateTime endTime)
        {
            return endTime - startTime;
        }
        private OptionObject MakeFieldsRequired(OptionObject optionObject, string scriptName)
        {
            var returnOptionObject = new OptionObject();
            scriptName = scriptName.Remove(0, scriptName.IndexOf(',') + 1);
            var tempFormList = scriptName.Split(';').ToList();
            var formList = new List<FormObject>();
            try
            {
                foreach (var tempForm in tempFormList)
                {
                    formList.Add(getFormObjectByFormId(optionObject, tempForm.Split(',').ElementAt(0)));
                    addFieldsToForm(tempForm.Split(',').Skip(1).ToList(), formList.First(x => x.FormId == tempForm.Split(',').ElementAt(0)), optionObject);
                }
                returnOptionObject.Forms = formList.ToArray();
            }
            catch (Exception e)
            {
                //returnOptionObject.ErrorCode = 3;
                //returnOptionObject.ErrorMesg = "An error has occurred "+;
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;
        }
        private void addFieldsToForm(List<string> fields, FormObject returnFormObject, OptionObject optionObject)
        {
            var tempFieldList = new List<FieldObject>();
            foreach (var tempField in fields)
            {
                tempFieldList.Add(new FieldObject { FieldNumber = tempField, FieldValue = getFieldValueByNumber(optionObject, returnFormObject.FormId, tempField), Required = "1" });
            }
            returnFormObject.CurrentRow.Fields = tempFieldList.ToArray();
        }
        private FormObject getFormObjectByFormId(OptionObject fullOptionObject, string formId)
        {
            var tempForm = new FormObject();
            foreach (var form in fullOptionObject.Forms)
            {
                if (formId.Equals(form.FormId))
                {
                    tempForm.CurrentRow = new RowObject();
                    tempForm.CurrentRow.ParentRowId = form.CurrentRow.ParentRowId;
                    tempForm.CurrentRow.RowId = form.CurrentRow.RowId;
                    tempForm.CurrentRow.RowAction = "EDIT";
                    tempForm.FormId = formId;
                }
            }
            return tempForm;
        }
        private string getFieldValueByNumber(OptionObject fullOptionObject, string formId, string fieldNumber)
        {
            return getFieldValueByNumber(fullOptionObject.Forms.FirstOrDefault(f => f.FormId == formId), fieldNumber);
        }
        private string getFieldValueByNumber(FormObject fullFormObject, string fieldNumber)
        {
            return fullFormObject.CurrentRow.Fields.FirstOrDefault(f => f.FieldNumber == fieldNumber).FieldValue;
        }
        private OptionObject UpdateDiagnosis(OptionObject optionObject, string scriptName)
        {
            /* scriptname delimited by commas as follows:
             * #1 : scriptName
             * #2 : draft/final/PA field
             * #3 : draft/final/PA value
             * #4 : date field
             * #5 : time field
             * #6 : axis I dx field
             * #7 : diagnosing practitioner field
             */
            var returnOptionObject = new OptionObject();
            try
            {
                var fieldList = scriptName.Split(',').ToList().Skip(1);
                var noteStatusField = new FieldObject { FieldNumber = fieldList.ElementAt(0) };
                var statusNeeded = fieldList.ElementAt(1);
                var dateField = new FieldObject { FieldNumber = fieldList.ElementAt(2) };
                var timeField = new FieldObject { FieldNumber = fieldList.ElementAt(3) };
                var axisIdx = new FieldObject { FieldNumber = fieldList.ElementAt(4) };
                var practitioner = new FieldObject { FieldNumber = fieldList.ElementAt(5) };

                var clientDiagnosisObj = new ClientDiagnosis.ClientDiagnosisObject();
                foreach (var form in optionObject.Forms)
                {
                    foreach (var field in form.CurrentRow.Fields)
                    {
                        if (field.FieldNumber.Equals(dateField.FieldNumber))
                            clientDiagnosisObj.DateOfDiagnosis = DateTime.Parse(field.FieldValue);
                        if (field.FieldNumber.Equals(timeField.FieldNumber))
                            clientDiagnosisObj.TimeOfDiagnosis = field.FieldValue;
                        if (field.FieldNumber.Equals(axisIdx.FieldNumber))
                            clientDiagnosisObj.DiagnosisAxisI1 = field.FieldValue;
                        if (field.FieldNumber.Equals(practitioner.FieldNumber))
                            clientDiagnosisObj.DiagnosingPractitioner = field.FieldValue;
                        if (field.FieldNumber.Equals(noteStatusField.FieldNumber))
                            noteStatusField.FieldValue = field.FieldValue;
                    }
                }
                if (noteStatusField.FieldValue.Equals(statusNeeded))
                {
                    clientDiagnosisObj.TypeOfDiagnosis = previousDiagnosisExist(optionObject) == null ? "A" : "U";
                    clientDiagnosisObj.DateOfDiagnosisSpecified = true;
                    clientDiagnosisObj.PrincipalDiagnosis = clientDiagnosisObj.DiagnosisAxisI1;
                    returnOptionObject.ErrorCode = 4;
                    if (clientDiagnosisObj.TypeOfDiagnosis.Equals("U"))
                    {
                        returnOptionObject.ErrorMesg = "The Diagnosis entered will be added to the client's record " +
                            "and become the client's new Diagnosis";
                    }
                }
            }
            catch (Exception e)
            {
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;
        }
        private ClientDiagnosis.ClientDiagnosisObject previousDiagnosisExist(OptionObject optionObject)
        {
            var dxObj = new ClientDiagnosis.ClientDiagnosisObject();
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            var commandText = "SELECT TOP(1) history_diagnosis.diagnosis_type_value, " +
                                "history_diagnosis.diagnosis_type_code," +
                                "history_diagnosis.date_of_diagnosis, " +
                                "history_diagnosis.time_of_diagnosis," +
                                "history_diagnosis.axis_I_diag_code_1," +
                                "history_diagnosis.axis_I_diag_value_1," +
                                "history_diagnosis.principal_diagnosis_code," +
                                "history_diagnosis.principal_diagnosis_value," +
                                "history_diagnosis.diagnosing_practitioner_code," +
                                "history_diagnosis.diagnosing_practitioner_value " +
                                "FROM SYSTEM.history_diagnosis " +
                                "WHERE PATID=? AND EPISODE_NUMBER=? " +
                                "ORDER BY history_diagnosis.date_of_diagnosis ASC";
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        var dbparameter1 = new OdbcParameter("PATID", optionObject.EntityID);
                        dbcommand.Parameters.Add(dbparameter1);
                        var dbparameter2 = new OdbcParameter("EPISODE_NUMBER", optionObject.EpisodeNumber);
                        dbcommand.Parameters.Add(dbparameter2);
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dxObj.TypeOfDiagnosis = "(" + reader["diagnosis_type_code"].ToString() + ") " + reader["diagnosis_type_value"].ToString();
                                dxObj.DateOfDiagnosis = DateTime.Parse(reader["date_of_diagnosis"].ToString());
                                dxObj.TimeOfDiagnosis = reader["time_of_diagnosis"].ToString();
                                dxObj.DiagnosisAxisI1 = "(" + reader["axis_I_diag_code_1"].ToString() + ") " + reader["axis_I_diag_value_1"].ToString();
                                dxObj.PrincipalDiagnosis = "(" + reader["principal_diagnosis_code"].ToString() + ") " + reader["principal_diagnosis_value"].ToString();
                                dxObj.DiagnosingPractitioner = "(" + reader["diagnosing_practitioner_code"].ToString() + ") " + reader["diagnosing_practitioner_value"].ToString();
                            }
                            else
                            {
                                dxObj = null;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }
            return dxObj;
        }
        private void sendEmail(string Sender, string Subject, string Body, List<string> Recipients, List<string> CCRecipients, List<string> SpiceworksCommands)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = ConfigurationManager.AppSettings["SMTPServer"].ToString();
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
                mailMessage.From = new MailAddress(Sender);
                mailMessage.To.Clear();
                Recipients.ForEach(x => mailMessage.To.Add(x));
                if (CCRecipients.Count > 0)
                    CCRecipients.ForEach(x => mailMessage.CC.Add(x));
                mailMessage.Subject = Subject;
                mailMessage.Body = Body;
                if (SpiceworksCommands.Count > 0)
                {
                    mailMessage.Body += "\n";
                    foreach (var x in SpiceworksCommands)
                    {
                        mailMessage.Body += "\n" + x;
                    }
                }
                smtpClient.Send(mailMessage);
                mailMessage.Dispose();
            }
            catch (Exception e)
            {
            }
        }
        private void sendEmail(string Sender, string Subject, string Body, List<string> Recipients, List<string> CCRecipients)
        {
            sendEmail(Sender, Subject, Body, Recipients, CCRecipients, new List<string>());
        }
        private void sendEmail(string Sender, string Subject, string Body, List<string> Recipients)
        {
            sendEmail(Sender, Subject, Body, Recipients, new List<string>(), new List<string>());
        }
        private OptionObject MakeSubscriberPolicyRequired(OptionObject optionObject)
        {
            var subsPolicyNumField = new FieldObject { FieldNumber = "263" };
            var guarantorID = new FieldObject { FieldNumber = "680" };
            var guarantorList = ConfigurationManager.AppSettings["GuarantorsToMakeRequired"].ToString().Split(',').ToList();
            var returnOptionObject = new OptionObject();
            var currentRow = new RowObject();
            var formObject = new FormObject();
            var otherRows = new List<RowObject>();
            try
            {
                if (guarantorList.IndexOf(optionObject.Forms[1].CurrentRow.Fields.First(f => f.FieldNumber == guarantorID.FieldNumber).FieldValue) >= 0)
                {
                    var returnFieldSubPolicy = new FieldObject
                    {
                        FieldNumber = subsPolicyNumField.FieldNumber,
                        FieldValue = optionObject.Forms[1].CurrentRow.Fields.First(f => f.FieldNumber == subsPolicyNumField.FieldNumber).FieldValue,
                        Required = "1"
                    };
                    currentRow.ParentRowId = optionObject.Forms[1].CurrentRow.ParentRowId;
                    currentRow.RowAction = "EDIT";
                    currentRow.RowId = optionObject.Forms[1].CurrentRow.RowId;

                    var crFields = new FieldObject[1];
                    crFields[0] = returnFieldSubPolicy;
                    currentRow.Fields = crFields;

                    formObject.FormId = optionObject.Forms[1].FormId;
                    formObject.CurrentRow = currentRow;
                }
                if (optionObject.Forms[1].OtherRows != null && optionObject.Forms[1].OtherRows.Length > 0)
                {

                    formObject.MultipleIteration = optionObject.Forms[1].MultipleIteration;
                    foreach (var otherRow in optionObject.Forms[1].OtherRows)
                    {
                        if (guarantorList.IndexOf(otherRow.Fields.First(f => f.FieldNumber == guarantorID.FieldNumber).FieldValue) >= 0)
                        {
                            var tempField = new FieldObject
                            {
                                FieldNumber = subsPolicyNumField.FieldNumber,
                                FieldValue = otherRow.Fields.First(f => f.FieldNumber == subsPolicyNumField.FieldNumber).FieldValue,
                                Required = "1"
                            };
                            var orFields = new FieldObject[1];
                            orFields[0] = tempField;
                            var tempOtherRow = new RowObject
                            {
                                ParentRowId = otherRow.ParentRowId,
                                RowAction = "EDIT",
                                RowId = otherRow.RowId,
                                Fields = orFields
                            };
                            otherRows.Add(tempOtherRow);
                        }
                    }
                }
                formObject.OtherRows = otherRows.ToArray();
                var rForms = new FormObject[1];
                rForms[0] = formObject;
                returnOptionObject.Forms = rForms;
            }
            catch (Exception e)
            {
            }
            returnOptionObject.EntityID = optionObject.EntityID;
            returnOptionObject.OptionId = optionObject.OptionId;
            returnOptionObject.SystemCode = optionObject.SystemCode;
            returnOptionObject.Facility = optionObject.Facility;
            return returnOptionObject;
        }
        private bool isPractitionerInCategory(string StaffID, string PractitionerCategory)
        {
            bool returnValue = false;
            var connectionString = ConfigurationManager.ConnectionStrings["ODBC"].ConnectionString;
            var commandText = "SELECT TOP (1) demo.STAFFID " +
                                "FROM SYSTEM.staff_current_demographics " +
                                "WHERE demo.STAFFID=? " +
                                "AND demo.practitioner_category_value like ? ";
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        dbcommand.Parameters.Add(new OdbcParameter("STAFFID", StaffID));
                        dbcommand.Parameters.Add(new OdbcParameter("practitioner_category_value", "%" + PractitionerCategory.ToUpper() + "%"));
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            if (reader.Read())
                                returnValue = true;
                            else
                                returnValue = false;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }
            return returnValue;
        }
    }
}