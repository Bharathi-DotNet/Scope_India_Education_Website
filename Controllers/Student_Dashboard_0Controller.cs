using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NuGet.Packaging;
using scope_project_2.Models;
using System.Collections;
using System.ComponentModel.DataAnnotations;

public class Student_Dashboard_0Controller : Controller
{

    [HttpGet]
    public IActionResult student_dashboard_0()
    {
        return View();
    }



    [HttpPost]
    public IActionResult student_dashboard_0(login log)
    {


        var email = log.email;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        con.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select first_name,last_name,gender,dob,email,phone,country,state,city,hobbies from register where email = @email";
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Connection = con;
        SqlDataReader dr = cmd.ExecuteReader();

        ArrayList arr = new ArrayList();
        while (dr.Read())
        {
            string[] ar = new string[20];
            ar[0] = dr["first_name"].ToString();
            ar[1] = dr["last_name"].ToString();
            ar[2] = dr["gender"].ToString();
            ar[3] = dr["dob"].ToString();
            ar[4] = dr["email"].ToString();
            ar[5] = dr["phone"].ToString();
            ar[6] = dr["country"].ToString();
            ar[7] = dr["state"].ToString();
            ar[8] = dr["city"].ToString();
            ar[9] = dr["hobbies"].ToString();

            arr.Add(ar);
        }

        ViewBag.arr = arr;
        return View("student_dashboard", "Student_Dashboard_0");
    }

   

}
