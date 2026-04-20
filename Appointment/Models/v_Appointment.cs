using System;
using System.Collections.Generic;

namespace Appointment.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string StatusName { get; set; }
        public decimal Fee { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorContactNumber { get; set; }
        public string DoctorEmail { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientContactNumber { get; set; }
        public string PatientGender { get; set; }
        public DateTime PatientDateOfBirth { get; set; }
        public string PatientImage { get; set; }
        public string SpecialistName { get; set; }
        public int SpecialistID { get; set; }

        // Combine fields for a display-friendly property
        public string AppointmentDetails
        {
            get
            {
                return $"{AppointmentDate.ToShortDateString()} {AppointmentTime}";
            }
        }
    }
}
