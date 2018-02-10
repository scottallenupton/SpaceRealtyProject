using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SpaceRealty.Models;

namespace SpaceRealty.Repos
{
    public class PropertyRepository : IPropertyRepository
    {
        public string connString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=SpaceRealtyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection sqlConn;
        public PropertyRepository()
        {
            sqlConn = new SqlConnection(connString);
            sqlConn.Open();
        }
        public void CreateHouse(House house)
        {        
            if(sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "insert into Houses(MLS, Street1, Street2) values ('" + house.MLSNum + "','" + house.Street1 + "','" + house.Street2 + "')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteHouse(int MLSNum)
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "delete from Houses where MLS = '" + MLSNum + "'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
        }

        public List<House> PopulateHouses()
        {
            List<House> Properties = new List<House>();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from Houses";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    House newProperty = new House();
                    newProperty.MLSNum = (int)reader["MLS"];
                    newProperty.Street1 = (string)reader["Street1"];
                    newProperty.Street2 = (string)reader["Street2"];
                    Properties.Add(newProperty);
                }
            }
            return Properties;
        }

        public void EditHouse(House house)
        {
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "update Houses(MLS, Street1, Street2) values ('" + house.MLSNum + "','" + house.Street1 + "','" + house.Street2 + "')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
        }

        //TODO: Properly Dispose
        public void Dispose()
        {
            sqlConn.Close();
        }
    }
}
