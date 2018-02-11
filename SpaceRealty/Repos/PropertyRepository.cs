using System;
using System.Collections.Generic;
using System.Data;
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
            //TODO: Data validation
            //Insert new house into database
            if(sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "insert into Houses(MLS, Street1, Street2, City, State, ZipCode, Neighborhood, SalesPrice, DateListed, Bedrooms, Bathrooms," +
                    "GarageSize, SquareFeet, LotSize, Description) values ('" + house.MLSNum + "','" + house.Street1 + "','" + house.Street2 +
                    "','" + house.City + "','" + house.State + "','" + house.ZipCode + "','" + house.Neighborhood + "','" + house.SalesPrice + "','" + house.DateListed +
                    "','" + house.Bedrooms + "','" + house.Bathrooms + "','" + house.GarageSize + "','" + house.SquareFeet + "','" + house.LotSize + "','" + house.Description + "')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
            //If theres a photo added, import photo into photo database
            if (house.PhotoData != null)
                CreatePhoto(house.PhotoData, house.MLSNum);
        }

        public void DeleteHouse(int MLSNum)
        {
            //Delete house and photos from database
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "delete from Houses where MLS = " + MLSNum;
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();

                query = "delete from Pictures where MLSNum = " + MLSNum;
                cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
        }

        public List<House> PopulateHouses()
        {
            //Create House objects
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
                    if(reader["Street1"] != DBNull.Value)
                        newProperty.Street1 = (string)reader["Street1"];
                    if (reader["Street2"] != DBNull.Value)
                        newProperty.Street2 = (string)reader["Street2"];
                    if (reader["City"] != DBNull.Value)
                        newProperty.City = (string)reader["City"];
                    if (reader["State"] != DBNull.Value)
                        newProperty.State = (string)reader["State"];
                    if (reader["ZipCode"] != DBNull.Value)
                        newProperty.ZipCode = (int)reader["ZipCode"];
                    if (reader["Neighborhood"] != DBNull.Value)
                        newProperty.Neighborhood = (string)reader["Neighborhood"];
                    if (reader["SalesPrice"] != DBNull.Value)
                        newProperty.SalesPrice = (decimal)reader["SalesPrice"];
                    if (reader["DateListed"] != DBNull.Value)
                        newProperty.DateListed = (DateTime)reader["DateListed"];
                    if (reader["Bedrooms"] != DBNull.Value)
                        newProperty.Bedrooms = (int)reader["Bedrooms"];
                    if (reader["Bathrooms"] != DBNull.Value)
                        newProperty.Bathrooms = (decimal)reader["Bathrooms"];
                    if (reader["GarageSize"] != DBNull.Value)
                        newProperty.GarageSize = (int)reader["GarageSize"];
                    if (reader["SquareFeet"] != DBNull.Value)
                        newProperty.SquareFeet = (int)reader["SquareFeet"];
                    if (reader["LotSize"] != DBNull.Value)
                        newProperty.LotSize = (int)reader["LotSize"];
                    if (reader["Description"] != DBNull.Value)
                        newProperty.Description = (string)reader["Description"];
                    Properties.Add(newProperty);
                }
            }
            return Properties;
        }

        public void EditHouse(House house)
        {
            //TODO: Data validation
            //Update house in the database
            if (sqlConn.State == System.Data.ConnectionState.Open && house.MLSNum != 0)
            {
                string query = "update Houses set Street1 = '" + house.Street1 + "', Street2 = '" + house.Street2 + "', City = '" + house.City + "', State = '" +
                    house.State + "', ZipCode = " + house.ZipCode + ", Neighborhood = '" + house.Neighborhood + "', SalesPrice = " + house.SalesPrice + ", DateListed = '" +
                    house.DateListed.ToShortDateString() + "', Bedrooms = " + house.Bedrooms + ", Bathrooms = " + house.Bathrooms + ", GarageSize = " + house.GarageSize + ", SquareFeet = " +
                    house.SquareFeet + ", LotSize = " + house.LotSize + ", Description = '" + house.Description + "' where MLS = " + house.MLSNum;
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
            //If theres a photo, add photo in the database
            if (house.PhotoData != null)
                CreatePhoto(house.PhotoData, house.MLSNum);
        }

        public void CreatePhoto(byte[] photo, int MLSNum)
        {
            //Create photo in the database
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "insert into Pictures(Id, PhotoData, MLSNum) values ('" + Guid.NewGuid().ToString("N") + "', @byteArray, '" + MLSNum + "')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.Parameters.Add("@byteArray", SqlDbType.VarBinary).Value = photo;
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> SelectPhotos(int MLSNum)
        {
            //Select photos for this house object
            List<string> Photos = new List<string>();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from Pictures where MLSNum = " + MLSNum;
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["PhotoData"] != DBNull.Value)
                        Photos.Add(Convert.ToBase64String(((byte[])reader["PhotoData"]), 0, ((byte[])reader["PhotoData"]).Length));
                }
            }
            return Photos;
        }

        //TODO: Properly Dispose
        public void Dispose()
        {
            sqlConn.Close();
        }

    }
}
