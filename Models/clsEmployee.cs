using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLRMGWEB.Models
{
    public class clsEmployee
    {
        public Nullable<Int64> nEmpId { get; set; }
        public string vEmpEmailId { get; set; }
        public string vEmpMobile { get; set; }
        public string vEmpName { get; set; }
        public string vEmpLocation { get; set; }
        public int nBSLTravelDesk { get; set; }
        public string vEmpPassword { get; set; }
        public string DOB { get; set; }
        public string vEmpDivision { get; set; }
        public bool bEmpActiveStatus { get; set; }
        public string vEmpType { get; set; }
        public string vEmpGrade { get; set; }
        public string vEmpDesignation { get; set; }
        public string vAadharNumber { get; set; }
        public string vPassportNumber { get; set; }
        public string PassportValid { get; set; }
        public string vDocAadhar { get; set; }
        public string vDocPassport { get; set; }
        public Int64 nL1ManagerCode { get; set; }
        public string vL1ManagerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string Confirmation { get; set; }
        public Int64 nL2ManagerCode { get; set; }
        public string vL2ManagerName { get; set; }
        public string EmpDept { get; set; }
        public string ConfirmationStatus { get; set; }
        public string TravelDeskDescription { get; set; }
        public string AppraisalFormStatus { get; set; }
        public string AppraisalCriteria { get; set; }
        public string BusinessDivision { get; set; }
        public string AssessmentYear { get; set; }
        public string EmpGender { get; set; }
        public string EmpRole { get; set; }
        public string QueryType { get; set; }
        public string vErrorMsg { get; set; }
    }



}