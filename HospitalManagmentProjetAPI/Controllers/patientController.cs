using HospitalManagmentProjetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HospitalManagmentProjetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class patientController : ControllerBase
    {
        SqlConnection cn = new SqlConnection("data source=MSI\\SQLEXPRESS;initial catalog=HospitalMangmentSystem;integrated security=sspi;trustservercertificate=true");
        [HttpGet("{id}")]
        public IActionResult GetData(int id)
        {
            cn.Open();
            string qry = "select * from patient where pid=@pid";
            SqlCommand cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@pid", id);
            SqlDataReader dr = cmd.ExecuteReader();
            List<HospitalMangemntClass> l = new List<HospitalMangemntClass>();
            while (dr.Read())
            {
                HospitalMangemntClass h = new HospitalMangemntClass();
                h.Pid = Convert.ToInt32(dr["pid"]);
                h.PName = Convert.ToString(dr["pname"]);
                h.Birthdate = Convert.ToDateTime(dr["birthdate"]);
                h.Phoneno = Convert.ToInt32(dr["phoneno"]);
                l.Add(h);
            }
            cn.Close();
            if (l.Count == 0)
            {
                return BadRequest("add proper value");
            }
            return Ok(l);
        }

        [HttpGet]
        public IActionResult getall()
        {
            cn.Open();
            string qry = "select * from patient";
            SqlCommand cmd = new SqlCommand(qry, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<HospitalMangemntClass> l = new List<HospitalMangemntClass>();
            while (dr.Read())
            {
                HospitalMangemntClass h = new HospitalMangemntClass();
                h.Pid = Convert.ToInt32(dr["pid"]);
                h.PName = Convert.ToString(dr["pname"]);
                h.Birthdate = Convert.ToDateTime(dr["birthdate"]);
                h.Phoneno = Convert.ToInt32(dr["phoneno"]);
                l.Add(h);
            }
            cn.Close();
            if (l.Count == 0)
            {
                return BadRequest("add proper value");
            }
            return Ok(l);
        }
        [HttpPost]
        public IActionResult insert(int pid, string pname, DateTime birthdate, int phoneno)
        {
            cn.Open();
            string qry = "insert into patient values (@pid,@pname,@birthdate,@phoneno)";
            SqlCommand cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@birthdate", birthdate);
            cmd.Parameters.AddWithValue("@phoneno", phoneno);
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
        [HttpDelete("{pid}")]
        public IActionResult deletedata(int pid)
        {
            cn.Open();
            string qry = "delete from patient where pid=@pid";
            SqlCommand cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@pid", pid);
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
        [HttpPut]
        public IActionResult updatedata(int pid, string pname, DateTime birthdate, int phoneno)
        {
            cn.Open();
            string qry = "update patient set pname=@pname,birthdate=@birthdate,phoneno=@phoneno where pid=@pid";
            SqlCommand cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@birthdate", birthdate);
            cmd.Parameters.AddWithValue("@phoneno", phoneno);
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
        [HttpPatch]
        public IActionResult updatevalue(int pid, string pname)
        {
            cn.Open();
            string qry = "update patient set pname=@pname where pid=@pid";
            SqlCommand cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@pname", pname);
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
    }
}
