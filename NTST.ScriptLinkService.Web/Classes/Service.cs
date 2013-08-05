using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTST.ScriptLinkService.Web
{
    public class Service
    {
        public string ServiceCost { get; set; }
        public DateTime ServiceDate { get; set; }
        public string Duration { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EpisodeNumber { get; set; }
        public string Facility { get; set; }
        public string JoinToTxHistory { get; set; }
        public string LocationCode { get; set; }
        public string LocationValue { get; set; }
        public string NotUniqueId { get; set; }
        public string OrigJoinToTxHistory { get; set; }
        public string Patid { get; set; }
        public string PatientName { get; set; }
        public string GuarantorId { get; set; }
        public string GuarantorName { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramValue { get; set; }
        public string ProgramRRGCode { get; set; }
        public string ProgramRRGValue { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceValue { get; set; }
        public string UnitsOfService { get; set; }

        public override string ToString()
        {
            return "Patid: " + this.Patid +
                    "\nPatientName: " + this.PatientName +
                    "\nEpisodeNumber: " + this.EpisodeNumber +
                    "\nGuarantorId: " + this.GuarantorId +
                    "\nGuarantorName: " + this.GuarantorName +
                    "\nServiceDate: " + this.ServiceDate.ToString("MM/dd/yyyy") +
                    "\nDuration: " + this.Duration +
                    "\nStartTime: " + this.StartTime +
                    "\nEndTime: " + this.EndTime +
                    "\nStaffId: " + this.StaffId +
                    "\nStaffName: " + this.StaffName +
                    "\nServiceCode: " + this.ServiceCode +
                    "\nServiceValue: " + this.ServiceValue +
                    "\nProgramCode: " + this.ProgramCode +
                    "\nProgramValue: " + this.ProgramValue +
                    "\nProgramRRGCode: " + this.ProgramRRGCode +
                    "\nProgramRRGValue: " + this.ProgramRRGValue +
                    "\nLocationCode: " + this.LocationCode +
                    "\nLocationValue: " + this.LocationValue +
                    "\nUnitsOfService: " + this.UnitsOfService +
                    "\nServiceCost: " + this.ServiceCost;
        }
    }
}