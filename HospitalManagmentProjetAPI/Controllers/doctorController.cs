using HospitalManagmentProjetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static HospitalManagmentProjetAPI.Models.HospitalMangemntClass;

namespace HospitalManagmentProjetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class doctorController : ControllerBase
    {
        SqlConnection cn = new SqlConnection("data source=MSI\\SQLEXPRESS;initial catalog=HospitalMangmentSystem;integrated security=sspi;trustservercertificate=true");
        [HttpGet("{id}")]
        public IActionResult GetData(int id)
        {
                cn.Open();
                string qry = "select * from doctore where dr_id=@id";
                SqlCommand cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                List<Doctor> l = new List<Doctor>();
                while (dr.Read())
                {
                    Doctor d = new Doctor();
                    d.DrId = Convert.ToInt32(dr["dr_id"]);
                    d.DeptId = Convert.ToInt32(dr["dept_id"]);
                    d.Name = Convert.ToString(dr["name"]);
                    d.Specialty = Convert.ToString(dr["specialty"]);
                    l.Add(d);
                }
                cn.Close();
                if (l.Count == 0)
                {
                    return NotFound("Doctor Not Found");
                }
                return Ok(l);
        }
        [HttpGet]
        public IActionResult selectdoctore()
        {
            cn.Open();
            string qry = "select * from doctore";
            SqlCommand cmd = new SqlCommand(qry, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Doctor> l = new List<Doctor>();
            while (dr.Read())
            {
                Doctor d = new Doctor();
                d.DrId = Convert.ToInt32(dr["dr_id"]);
                d.DeptId = Convert.ToInt32(dr["dept_id"]);
                d.Name = Convert.ToString(dr["name"]);
                d.Specialty = Convert.ToString(dr["specialty"]);
                l.Add(d);
            }
            cn.Close();
            if (l.Count == 0)
            {
                return NotFound("Doctor Not Found");
            }
            return Ok(l);
        }
        [HttpPost]
        public IActionResult InsertDoctor(int drid,int deptid,string name,string specialty)
        {
            cn.Open();

            string qry = "insert into doctore values(@drid,@deptid,@name,@specialty)";

            SqlCommand cmd = new SqlCommand(qry, cn);

            cmd.Parameters.AddWithValue("@drid",drid);
            cmd.Parameters.AddWithValue("@deptid",deptid);
            cmd.Parameters.AddWithValue("@name",name);
            cmd.Parameters.AddWithValue("@specialty",specialty);
            int c=cmd.ExecuteNonQuery();
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
        public IActionResult updateDoctor(int drid, int deptid, string name, string specialty)
        {
            cn.Open();

            string qry = "update doctore set dept_id=@deptid,name=@name,specialty=@specialty where dr_id=@drid";

            SqlCommand cmd = new SqlCommand(qry, cn);

            cmd.Parameters.AddWithValue("@drid", drid);
            cmd.Parameters.AddWithValue("@deptid", deptid);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@specialty", specialty);
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
        [HttpDelete("{drid}")]
        public IActionResult deletedata(int drid)
        {
            cn.Open();
            string qry = "delete from doctore where dr_id=@drid";
            SqlCommand cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@drid", drid);
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

