using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RegAppBuisnessLogicLayer
{



    #region MainDataUpdation
    public partial class DatabaseUpdation
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd;


        #region insertIntoTableUserRegistraion
        public string insertIntoTableUserRegistration(string UserName, string ID, string Email, string Password)
        {



            cmd = new SqlCommand("insert into User_Registration values('" + UserName + "','" + ID + "','" + Email + "','" + Password + "')");

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();

            string str = "data inserted";
            conn.Close();
            return str;


        }

        #endregion


        #region checkUserLogin




        public bool checkUserLogin(string username, string password)
        {



            try
            {
                string qry = "SELECT * FROM User_Registration";
                SqlCommand cmd = new SqlCommand(qry, conn);
                SqlDataReader dr;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["User_Email"].ToString() == username && dr["User_Password"].ToString() == password)
                    {
                        /*Response.Write(dr["UserName"] + "");
                        sawa.Username = dr["UserName"].ToString();
                        sawa.Role = dr["Role"].ToString();
                        sawa.RoleId = int.Parse(dr["RoleId"] + "");*/
                        return true;
                    }
                    conn.Close();
                }

                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                //  Response.Write(ex.ToString());
                ex.ToString();

                return false;
            }




        }

        #endregion




        #region firsttimelogin

        public bool isFirstTimeLogin(string username)
        {
            cmd = new SqlCommand("select IsFirstTimeLogin from User_Registration where User_Email='" + username + "'");
            conn.Open();
            cmd.Connection = conn;
            string isFirstTimeLogin1 = cmd.ExecuteScalar().ToString();

            bool isFirstTimeLogin = Convert.ToBoolean(isFirstTimeLogin1);
            conn.Close();
            return isFirstTimeLogin;
        }


        public void changepassfirsttime(string username, string newPassword, string confirmNewPassword)
        {





            if (newPassword == confirmNewPassword)
            {

                cmd = new SqlCommand("UPDATE User_Registration SET  IsFirstTimeLogin= 1,User_Password='" + newPassword + "' WHERE  User_Email= '" + username + "'; ");
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }


        }
        #endregion




        #region forgotpassword
        public string forgotPassword(string username)
        {
            cmd = new SqlCommand("select User_Email from User_Registration where User_Email='" + username + "'");
            cmd.Connection = conn;
            conn.Open();
            string returnUsername = cmd.ExecuteScalar().ToString();
            conn.Close();
            return returnUsername;

        }
        #endregion


        #region RandompasswordGeneration
        string userIDFromReg;
        public string randomOTP(string ID,string rOTP,string username)
        {
            

            string insert = "INSERT INTO OTP ([User_ID], [User_Email], [OTP]) VALUES(@userId, @userEmail, @OTP)";
            cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@userId", ID);
            cmd.Parameters.AddWithValue("@OTP", rOTP);
            cmd.Parameters.AddWithValue("@userEmail", username);
            //cmd = new SqlCommand("insert into OTP values( User_ID='"+ID+"' , User_Email='"+username+ "',  OTP='" + rOTP + "') where User_Email='" + username+"'",conn);
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            
            conn.Close();
            return rOTP;

        }



        public string methodForCookie(string username)
        {

            cmd = new SqlCommand("select User_ID from User_Registration where User_Email='"+username+"'");
            cmd.Connection = conn;
            conn.Open();

            userIDFromReg = cmd.ExecuteScalar().ToString();
            conn.Close();
            return userIDFromReg;
        }



        #endregion
    }


    #endregion






}

