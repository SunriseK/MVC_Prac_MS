using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace MVC_Prac_MS.Models
{
    public class MyDataBase
    {
        public string connString = "User ID=sa;Password=Aa123456;Initial Catalog=MVCTest;Data Source=NB070525";
        SqlConnection conn = new SqlConnection();
        
        public void Connect()
        {
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        public void Disconnect()
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }

        public List<City> GetCityList()
        {
            try
            {
                Connect();

                string sql = "SELECT Id,City FROM dbo.City";
                SqlCommand cmd = new SqlCommand(sql, conn);
                List<City> list = new List<City>();

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

                
                return list;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        public List<Village> GetVillageList(string id)
        {
            try
            {
                Connect();

                string sql;
                if (id =="")
                {
                    sql=" SELECT VillageId, Village FROM Village";
                }
                else
                {
                    sql = $" SELECT VillageId, Village FROM Village WHERE CityId='{id}'";
                }
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                List<Village> list = new List<Village>();
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Village data = new Village();
                        data.VillageId = dr["VillageId"].ToString();
                        data.VillageName = dr["Village"].ToString();
                        list.Add(data);
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
            finally
            {

                Disconnect();
            }
        }

        public bool AddUserData(UserData data)
        {
            try
            {
                Connect();

                string id = Guid.NewGuid().ToString();
                string strSQL = @"INSERT INTO userdata (id, account, password, city, village, address)
                          VALUES (@id, @account, @password, @city, @village, @address)";

                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                cmd.Parameters.Add("@account", SqlDbType.NVarChar).Value = data.account;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = data.password1;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = data.city;
                cmd.Parameters.Add("@village", SqlDbType.NVarChar).Value = data.village;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = data.address;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public bool CheckUserData(string account, string password)
        {
            try
            {
                Connect();
                string strSQL = "SELECT 1 FROM userdata WHERE account = @account AND password = @password;";
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return false;
            }
            finally
            {
                Disconnect();
            }
        }


    }
}