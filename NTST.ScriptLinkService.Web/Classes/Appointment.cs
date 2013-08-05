using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTST.ScriptLinkService.Web.Classes
{
    public partial class Appointment
    {
        public string AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string EpisodeNumber { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramValue { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string StatusCode { get; set; }
        public string StatusValue { get; set; }
        public string ScheduledServiceCode { get; set; }
        public string ScheduledServiceValue { get; set; }
        public string MissedVisitServiceCode { get; set; }
        public string MissedVisitServiceValue { get; set; }
        public string PostedCode { get; set; }
        public string PostedValue { get; set; }
        public string CrosstabCount { get; set; }
        public string Notes { get; set; }
    }
}