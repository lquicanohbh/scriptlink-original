using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTST.ScriptLinkService.Web
{
    public class Client
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string EpisodeNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string GenderCode { get; set; }
        public string GenderValue { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramValue { get; set; }
        public DateTime? AdmitDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string AdmittingPractitionerId { get; set; }
        public string AdmittingPractitionerName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string CountyCode { get; set; }
        public string CountyValue { get; set; }
        public string StateCode { get; set; }
        public string StateValue { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MaritalStatusCode { get; set; }
        public string MaritalStatusValue { get; set; }
        public string EducationCode { get; set; }
        public string EducationValue { get; set; }
        public string EmploymentStatusCode { get; set; }
        public string EmploymentStatusValue { get; set; }
        public DateTime? FinancialInvestigationDate { get; set; }
        public string FinancialInvestigationMedicareNumber { get; set; }
        public string FinancialInvestigationMedicaidNumber { get; set; }
        public string CommercialInsuranceCode { get; set; }
        public string CommercialInsuranceValue { get; set; }
        public string ManagedCareOrganizationCode { get; set; }
        public string ManagedCareOrganizationValue { get; set; }
        public string SlidingFeeScaleIncome { get; set; }
        public string SlidingFeeScaleNumberOfDependents { get; set; }
        public string SlidingFeeScaleFamilySize { get; set; }
        public bool IsPrimaryCare { get; set; }
        public override string ToString()
        {
            return "ClientId: " + this.ClientId +
                    "\nClientName: " + this.ClientName +
                    "\nEpisodeNumber: " + this.EpisodeNumber +
                    "\nDateOfBirth: " + (this.DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "") +
                    "\nGenderCode: " + this.GenderCode +
                    "\nGenderValue: " + this.GenderValue +
                    "\nProgramCode: " + this.ProgramCode +
                    "\nProgramValue: " + this.ProgramValue +
                    "\nAdmitDate: " + (this.AdmitDate.HasValue ? AdmitDate.Value.ToString("MM/dd/yyyy") : "") +
                    "\nDischargeDate: " + (this.DischargeDate.HasValue ? DischargeDate.Value.ToString("MM/dd/yyyy") : "") +
                    "\nAdmittingPractitionerId: " + this.AdmittingPractitionerId +
                    "\nAdmittingPractitionerName: " + this.AdmittingPractitionerName +
                    "\nSocialSecurityNumber: " + this.SocialSecurityNumber +
                    "\nAddress1: " + this.Address1 +
                    "\nAddress2: " + this.Address2 +
                    "\nZipCode: " + this.ZipCode +
                    "\nCity: " + this.City +
                    "\nCountyCode: " + this.CountyCode +
                    "\nCountyValue: " + this.CountyValue +
                    "\nStateCode: " + this.StateCode +
                    "\nStateValue: " + this.StateValue +
                    "\nHomePhone: " + this.HomePhone +
                    "\nWorkPhone: " + this.WorkPhone +
                    "\nMaritalStatusCode: " + this.MaritalStatusCode +
                    "\nMaritalStatusValue: " + this.MaritalStatusValue +
                    "\nEducationCode: " + this.EducationCode +
                    "\nEducationValue: " + this.EducationValue +
                    "\nEmploymentStatusCode: " + this.EmploymentStatusCode +
                    "\nEmploymentStatusValue: " + this.EmploymentStatusValue +
                    "\nFinancialInvestigationDate: " + (this.FinancialInvestigationDate.HasValue ? FinancialInvestigationDate.Value.ToString("MM/dd/yyyy") : "") +
                    "\nFinancialInvestigationMedicareNumber: " + this.FinancialInvestigationMedicareNumber +
                    "\nFinancialInvestigationMedicaidNumber: " + this.FinancialInvestigationMedicaidNumber +
                    "\nCommercialInsuranceCode: " + this.CommercialInsuranceCode +
                    "\nCommercialInsuranceValue: " + this.CommercialInsuranceValue +
                    "\nManagedCareOrganizationCode: " + this.ManagedCareOrganizationCode +
                    "\nManagedCareOrganizationValue: " + this.ManagedCareOrganizationValue +
                    "\nSlidingFeeScaleIncome: " + this.SlidingFeeScaleIncome +
                    "\nSlidingFeeScaleNumberOfDependents: " + this.SlidingFeeScaleNumberOfDependents +
                    "\nSlidingFeeScaleFamilySize: " + this.SlidingFeeScaleFamilySize;
        }
    }
}