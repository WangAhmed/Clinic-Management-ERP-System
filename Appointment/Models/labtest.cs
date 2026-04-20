using System;
using System.Collections.Generic;

namespace Appointment.Models
{
    public class LabTestViewModel
    {
        public int LabTestID { get; set; }
        public string TestName { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public DateTime TestDate { get; set; }
        public decimal Price { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Expr1 { get; set; }
        public string Expr2 { get; set; }
        public int SpecialistID { get; set; }
        public string ChemicalsUsed { get; set; }
        public string LabProductsUsed { get; set; }
    }
}
