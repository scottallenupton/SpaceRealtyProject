using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceRealty.Models;

namespace SpaceRealty.Repos
{
    public class UserRepository : IUserRepository
    {
        public string connString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=SpaceRealtyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection sqlConn;
        public UserRepository()
        {
            sqlConn = new SqlConnection(connString);
            sqlConn.Open();
        }
        public void CreateUser(Realtor realtor)
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "insert into Realtors(Id, FirstName, LastName, Email, Password, Username) values ('" + Guid.NewGuid().ToString("N") + "','" + realtor.firstName + "','" + realtor.lastName +
                    "','" + realtor.email + "','" + Encryptdata(realtor.password) + "','" + realtor.userName + "')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
        }

        public bool AuthenticateUser(Realtor realtor)
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from Realtors where Username = '" + realtor.userName + "'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader reader = cmd.ExecuteReader();
                bool returnVal = false;
                while (reader.Read())
                {
                    if (realtor.password == Decryptdata((string)reader["Password"]))
                        returnVal = true;
                    else
                        returnVal = false;
                }
                return returnVal;
            }
            else
            {
                return false;
            }
        }

        public Realtor RetrieveUser(int id)
        {
            throw new NotImplementedException();
        }

        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        //TODO: Dispose properly
        public void Dispose()
        {
        }
    }
}
