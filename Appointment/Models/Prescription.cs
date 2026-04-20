using System;
using System.Collections.Generic;

namespace Appointment.Models

{
    public class PrescriptionViewModel
    {
        public int PrescriptionID { get; set; }
        public int AppointmentID { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Notes { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
