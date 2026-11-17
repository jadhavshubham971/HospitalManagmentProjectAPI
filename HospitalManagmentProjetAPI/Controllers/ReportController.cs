using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HospitalManagmentProjetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
            SqlConnection cn = new SqlConnection(
                "Data Source=MSI\\SQLEXPRESS;Initial Catalog=HospitalMangmentSystem;Integrated Security=True;TrustServerCertificate=True");
            [HttpGet("AppointmentDetails")]
        public IActionResult AppointmentDetails()
            {
                cn.Open();
                string qry = @"select a.aid,p.pname as PatientName,
                           d.name as DoctorName,
                           a.schedule,a.status
                           from appointment a
                           inner join patient p on a.pid=p.pid
                           inner join doctore d on a.dr_id=d.dr_id";

                SqlCommand cmd = new SqlCommand(qry, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                List<object> l = new List<object>();
                while (dr.Read())
                {
                    l.Add(new
                    {
                        Aid = dr["aid"],
                        PatientName = dr["PatientName"],
                        DoctorName = dr["DoctorName"],
                        Schedule = dr["schedule"],
                        Status = dr["status"]
                    });
                }
            if (l.Count > 0)
            {
                return Ok(l);
            }
            else
            {
                return NotFound("No Records Found");
            }
        }
         [HttpGet("DoctorDepartment")]
        public IActionResult DoctorDepartment()
        {
                cn.Open();
                string qry = @"select d.dr_id,d.name,d.specialty,dp.deptname from doctore d inner join department dp on d.dept_id=dp.dept_id";
                SqlCommand cmd = new SqlCommand(qry, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                List<object> l = new List<object>();
                while (dr.Read())
                {
                    l.Add(new
                    {
                        DrId = dr["dr_id"],
                        Name = dr["name"],
                        Specialty = dr["specialty"],
                        Department = dr["deptname"]
                    });
                }
            if (l.Count > 0)
            {
                return Ok(l);
            }
            else
            {
                return NotFound("No Records Found");
            }
        }
          [HttpGet("PrescriptionDetails")]
        public IActionResult PrescriptionDetails()
            {
                cn.Open();
                string qry = @"select pr.per_id,p.pname,
                           m.mname,pr.dosage,
                           pr.prescibed
                           from prescription pr
                           inner join patient p
                           on pr.pid=p.pid
                           inner join medication m
                           on pr.mid=m.mid";

                SqlCommand cmd = new SqlCommand(qry, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                List<object> l = new List<object>();
                while (dr.Read())
                {
                    l.Add(new
                    {
                        PrescriptionId = dr["per_id"],
                        PatientName = dr["pname"],
                        Medicine = dr["mname"],
                        Dosage = dr["dosage"],
                        Prescribed = dr["prescibed"]
                    });
                }
            if (l.Count > 0)
            {
                return Ok(l);
            }
            else
            {
                return NotFound("No Records Found");
            }
        }

         [HttpGet("HospitalReport")]
        public IActionResult HospitalReport()
        {
                cn.Open();
                string qry = @"SELECT p.pname,
                           d.name AS DoctorName,
                           dp.deptname,
                           a.schedule,
                           m.mname,
                           pr.dosage
                           FROM patient p
                           INNER JOIN appointment a
                           ON p.pid = a.pid
                           INNER JOIN doctore d
                           ON a.dr_id = d.dr_id
                           INNER JOIN department dp
                           ON d.dept_id = dp.dept_id
                           INNER JOIN prescription pr
                           ON p.pid = pr.pid
                           INNER JOIN medication m
                           ON pr.mid = m.mid";

                SqlCommand cmd = new SqlCommand(qry, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                List<object> l = new List<object>();
                while (dr.Read())
                {
                    l.Add(new
                    {
                        PatientName = dr["pname"],
                        DoctorName = dr["DoctorName"],
                        Department = dr["deptname"],
                        AppointmentDate = dr["schedule"],
                        Medicine = dr["mname"],
                        Dosage = dr["dosage"]
                    });
                }
            if (l.Count > 0)
            {
                return Ok(l);
            }
            else
            {
                return NotFound("No Records Found");
            }
        }
        
    }
}

