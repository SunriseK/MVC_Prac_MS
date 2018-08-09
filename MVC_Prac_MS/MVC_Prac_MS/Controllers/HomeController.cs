using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Prac_MS.Models;
using System.Data.SqlClient;
using System.Data;

namespace MVC_Prac_MS.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            DateTime date = DateTime.Now;
            Student data = new Student();
            List<Student> list = new List<Student>();
            list.Add(new Student("1", "小明", 80));
            list.Add(new Student("2", "小華", 70));
            list.Add(new Student("3", "小英", 60));
            list.Add(new Student("4", "小李", 50));
            list.Add(new Student("5", "小張", 90));

            ViewBag.Date = date;
            ViewBag.Student = data;
            ViewBag.List = list;

            return View();
        }

        public ActionResult Index2()
        {
            DateTime date = DateTime.Now;
            ViewBag.Date = date;

            Student data = new Student("1", "小明", 99);

            return View(data);
        }

        public ActionResult Index3()
        {
            DateTime date = DateTime.Now;
            ViewBag.Date = date;

            Student data = new Student("1", "小明", 99);

            return View(data);
        }

        public ActionResult Transcripts(string id, string name, int score)
        {
            Student data = new Student(id, name, score);
            return View(data);
        }

        //[HttpPost]
        //public ActionResult Transcripts(FormCollection post)
        //{
        //    string id = post["id"];
        //    string name = post["name"];
        //    int score = Convert.ToInt32(post["score"]);

        //    Student data = new Student(id, name, score);
        //    return View(data);
        //}
        [HttpPost]
        public ActionResult Transcripts(Student model)
        {
            string id = model.id;
            string name = model.name;
            int score = model.score;

            Student data = new Student(id, name, score);
            return View(data);
        }

        //string connString = "Server=localhost;Database=MVCTest;User ID=sa;Password=Aa123456;Application Name=MVC_TEST";
        string connString = "User ID=sa;Password=Aa123456;Initial Catalog=MVCTest;Data Source=NB070525";
        //string connString = "User ID=b2bsa;Password=@dvantech!;Initial Catalog=CurationPool;Data Source=ACLSTNR12";
        SqlConnection conn = new SqlConnection();

        public ActionResult DBTest()
        {
            conn.ConnectionString = connString;
            string sql = "SELECT Id,City FROM dbo.City";
            //string sql = "SELECT[ID],[Name],[Salary],[BirtyDay],[MyPicture],[Email] FROM[dbo].[TEST_MASK]";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dt);

            ViewBag.DT = dt;

            return View();
        }

        public ActionResult DBTest2()
        {
            conn.ConnectionString = connString;
            string sql = "SELECT Id,City FROM dbo.City";
            SqlCommand cmd = new SqlCommand(sql, conn);
            List<City> list = new List<City>();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    City city = new City();
                    city.CityId = dr["id"].ToString();
                    city.CityName = dr["city"].ToString();
                    list.Add(city);
                }
            }

            if (conn.State != ConnectionState.Closed)
                conn.Close();

            ViewBag.List = list;
            return View();
        }

    }
}