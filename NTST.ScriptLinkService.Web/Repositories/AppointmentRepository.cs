using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using NTST.ScriptLinkService.Web.Classes;

namespace NTST.ScriptLinkService.Web.Repositories
{
    public class AppointmentRepository
    {
        public static List<Appointment> GetAppointment(string ClientId)
        {
            return QueryAppointmentTable(ClientId, String.Empty, null, string.Empty, null, "Client");
        }
        public static List<Appointment> GetAppointment(string ClientId, string Episode)
        {
            return QueryAppointmentTable(ClientId, Episode, null, string.Empty, null, "Episode");
        }
        public static List<Appointment> GetAppointment(string ClientId, string Episode, DateTime? FormDate)
        {
            return QueryAppointmentTable(ClientId, Episode, FormDate, String.Empty, null, "Date");
        }
        public static List<Appointment> GetAppointment(string ClientId, string Episode, DateTime? FormDate, string StaffId)
        {
            return QueryAppointmentTable(ClientId, Episode, FormDate, StaffId, null, "Staff");
        }
        public static List<Appointment> GetAppointment(string ClientId, string Episode, DateTime? FormDate, string StaffId, DateTime? FormTime)
        {
            return QueryAppointmentTable(ClientId, Episode, FormDate, StaffId, FormTime, "Time");
        }
        private static List<Appointment> QueryAppointmentTable(string ClientId, string Episode, DateTime? FormDate, string StaffId, DateTime? FormTime, string QueryType)
        {
            var AppointmentList = new List<Appointment>();
            var connectionString = ConfigurationManager.ConnectionStrings["CacheODBC"].ConnectionString;
            #region commandText
            var commandText = "SELECT appt_data.appointment_date as AppointmentDate," +
                                    "appt_data.appointment_start_time as StartTime," +
                                    "appt_data.appointment_end_time as EndTime," +
                                    "appt_data.ID as AppointmentId," +
                                    "appt_data.PATID as ClientId," +
                                    "appt_data.patient_name as ClientName," +
                                    "appt_data.EPISODE_NUMBER as EpisodeNumber," +
                                    "appt_data.STAFFID as StaffId," +
                                    "appt_data.staff_name as StaffName," +
                                    "appt_data.program_code as ProgramCode," +
                                    "appt_data.program_value as ProgramValue," +
                                    "appt_data.status_code as StatusCode," +
                                    "appt_data.status_value as StatusValue," +
                                    "appt_data.SERVICE_CODE as ScheduledServiceCode," +
                                    "appt_data.service_description as ScheduledServiceValue," +
                                    "appt_data.missed_visit_service_code as MissedVisitServiceCode," +
                                    "appt_data.missed_visit_service_desc as MissedVisitServiceValue," +
                                    "appt_data.SITEID as SiteId,"+
                                    "appt_data.site_name as SiteName,"+
                                    "appt_data.posted_code as PostedCode," +
                                    "appt_data.posted_value as PostedValue," +
                                    "appt_data.appointment_notes as Notes " +
                                    "FROM SYSTEM.appt_data ";
            #endregion
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (var dbcommand = new OdbcCommand(commandText, connection))
                    {
                        switch (QueryType)
                        {
                            case "Client":
                                commandText += "WHERE appt_data.PATID=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("PATID", ClientId));
                                break;
                            case "Episode":
                                commandText += "WHERE appt_data.PATID=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("PATID", ClientId));
                                commandText += "AND appt_data.EPISODE_NUMBER=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", Episode));
                                break;
                            case "Date":
                                commandText += "WHERE appt_data.PATID=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("PATID", ClientId));
                                commandText += "AND appt_data.EPISODE_NUMBER=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", Episode));
                                commandText += "AND appt_data.appointment_date=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("appointment_date", FormDate.Value.ToString("yyyy-MM-dd")));
                                break;
                            case "Staff":
                                commandText += "WHERE appt_data.PATID=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("PATID", ClientId));
                                commandText += "AND appt_data.EPISODE_NUMBER=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", Episode));
                                commandText += "AND appt_data.appointment_date=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("appointment_date", FormDate.Value.ToString("yyyy-MM-dd")));
                                commandText += "AND appt_data.STAFFID=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("STAFFID", StaffId));
                                break;
                            case "Time":
                                commandText += "WHERE appt_data.PATID=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("PATID", ClientId));
                                commandText += "AND appt_data.EPISODE_NUMBER=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", Episode));
                                commandText += "AND appt_data.appointment_date=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("appointment_date", FormDate.Value.ToString("yyyy-MM-dd")));
                                commandText += "AND appt_data.STAFFID=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("STAFFID", StaffId));
                                commandText += "AND appt_data.appointment_start_time=? ";
                                dbcommand.Parameters.Add(new OdbcParameter("appointment_start_time", FormTime.Value.ToString("hh:mm tt")));
                                break;
                            default:
                                break;
                        }
                        dbcommand.CommandText = commandText;
                        using (var reader = dbcommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                #region ReadInValues
                                var appointment = new Appointment();
                                appointment.AppointmentDate = reader.GetDate(reader.GetOrdinal("AppointmentDate"));
                                appointment.AppointmentId = reader["AppointmentId"].ToString();
                                appointment.ClientId = reader["ClientId"].ToString();
                                appointment.ClientName = reader["ClientName"].ToString();
                                appointment.EpisodeNumber = reader["EpisodeNumber"].ToString();
                                appointment.EndTime = reader["EndTime"].ToString();
                                appointment.MissedVisitServiceCode = reader["MissedVisitServiceCode"].ToString();
                                appointment.MissedVisitServiceValue = reader["MissedVisitServiceValue"].ToString();
                                appointment.Notes = reader["Notes"].ToString();
                                appointment.PostedCode = reader["PostedCode"].ToString();
                                appointment.PostedValue = reader["PostedValue"].ToString();
                                appointment.ProgramCode = reader["ProgramCode"].ToString();
                                appointment.ProgramValue = reader["ProgramValue"].ToString();
                                appointment.ScheduledServiceCode = reader["ScheduledServiceCode"].ToString();
                                appointment.ScheduledServiceValue = reader["ScheduledServiceValue"].ToString();
                                appointment.StaffId = reader["StaffId"].ToString();
                                appointment.StaffName = reader["StaffName"].ToString();
                                appointment.StartTime = reader["StartTime"].ToString();
                                appointment.StatusCode = reader["StatusCode"].ToString();
                                appointment.StatusValue = reader["StatusValue"].ToString();
                                appointment.SiteId = reader["SiteId"].ToString();
                                appointment.SiteName = reader["SiteName"].ToString();
                                AppointmentList.Add(appointment);
                                #endregion
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return AppointmentList;
        }
    }
}