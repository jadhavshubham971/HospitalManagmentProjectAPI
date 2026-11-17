using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static HospitalManagmentProjetAPI.Models.HospitalMangemntClass;

namespace HospitalManagmentProjetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppoinmentController : ControllerBase
    {
        SqlConnection cn = new SqlConnection("data source=MSI\\SQLEXPRESS;initial catalog=HospitalMangmentSystem;integrated security=sspi;trustservercertificate=true");
        
        [HttpGet]
        public IActionResult GetAppointments()
        {
            cn.Open();
            string qry = "select * from appointment";
            SqlCommand cmd = new SqlCommand(qry, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Appointment> l = new List<Appointment>();
            while (dr.Read())
            {
                Appointment a = new Appointment();
                a.Aid = Convert.ToInt32(dr["aid"]);
                a.Pid = Convert.ToInt32(dr["pid"]);
                a.DrId = Convert.ToInt32(dr["dr_id"]);
                a.Schedule = Convert.ToDateTime(dr["schedule"]);
                a.Status = Convert.ToString(dr["status"]);
                l.Add(a);
            }

            if (l.Count == 0)
            {
                return NotFound("Appoinment Not Found");
            }
            return Ok(l);
        }
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            cn.Open();
            string qry = "select * from appointment where aid=@aid";
            SqlCommand cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@aid", id);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Appointment> l = new List<Appointment>();
            while (dr.Read())
            {
                Appointment a = new Appointment();
                a.Aid = Convert.ToInt32(dr["aid"]);
                a.Pid = Convert.ToInt32(dr["pid"]);
                a.DrId = Convert.ToInt32(dr["dr_id"]);
                a.Schedule = Convert.ToDateTime(dr["schedule"]);
                a.Status = Convert.ToString(dr["status"]);
                l.Add(a);
            }
            if (l.Count == 0)
            {
                return NotFound("Appoinment Not Found");
            }
            return Ok(l);
        }
        [HttpPost]
        public IActionResult InsertAppointment(int aid,int pid,int dr_id,string schedule,string status)
        {
            cn.Open();

            string qry = @"insert into appointment values(@aid,@pid,@drid,@schedule,@status)";

            SqlCommand cmd = new SqlCommand(qry, cn);

            cmd.Parameters.AddWithValue("@aid",aid);
            cmd.Parameters.AddWithValue("@pid",pid);
            cmd.Parameters.AddWithValue("@drid",dr_id);
            cmd.Parameters.AddWithValue("@schedule",schedule);
            cmd.Parameters.AddWithValue("@status",status);
            int c = cmd.ExecuteNonQuery();
            if (c != 0)
            {
                return Ok("insert successful");
            }
            else
            {
                return NotFound("not insert");
            }
        }
        [HttpPut]
        public IActionResult UpdateAppointment(int aid, int pid, int dr_id, string schedule, string status)
        {
            cn.Open();

            string qry = @"update appointment set pid=@pid,dr_id=@drid,schedule=@schedule,status=@status where aid=@aid";
            SqlCommand cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@aid", aid);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@drid", dr_id);
            cmd.Parameters.AddWithValue("@schedule", schedule);
            cmd.Parameters.AddWithValue("@status", status);
            int c = cmd.ExecuteNonQuery();
            if (c != 0)
            {
                return Ok("update successful");
            }
            else
            {
                return NotFound("not update");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            cn.Open();

            string qry = "delete from appointment where aid=@id";

            SqlCommand cmd = new SqlCommand(qry, cn);

            cmd.Parameters.AddWithValue("@id", id);

            int c = cmd.ExecuteNonQuery();

            if (c != 0)
            {
                return Ok("delete successful");
            }
            else
            {
                return NotFound("not delete");
            }
        }
    }  
}
