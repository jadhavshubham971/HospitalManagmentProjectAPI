namespace HospitalManagmentProjetAPI.Models
{
    public class HospitalMangemntClass
    {
        internal int pid;
        internal string? pname;
        internal DateTime birthdate;
        internal int Phoneno;
        internal string? specialty;
        internal string? name;
        internal int dept_id;
        internal int dr_id;

        public DateTime Birthdate { get; internal set; }
        public string? PName { get; internal set; }
        public int Pid { get; internal set; }

        public class Department
        {
            public int DeptId { get; set; }
            public string DeptName { get; set; }
        }

        public class Doctor
        {
            public int DrId { get; set; }
            public int DeptId { get; set; }
            public string Name { get; set; }
            public string Specialty { get; set; }
        }

        public class Patient
        {
            public int Pid { get; set; }
            public string PName { get; set; }
            public DateTime BirthDate { get; set; }
            public string PhoneNo { get; set; }
        }

        public class Medication
        {
            public int Mid { get; set; }
            public string MName { get; set; }
        }

        public class Appointment
        {
            public int Aid { get; set; }
            public int Pid { get; set; }
            public int DrId { get; set; }
            public DateTime Schedule { get; set; }
            public string Status { get; set; }
        }

        public class Prescription
        {
            public int PerId { get; set; }
            public int Pid { get; set; }
            public int Mid { get; set; }
            public string Dosage { get; set; }
            public DateTime Prescribed { get; set; }
        }
    }
}

