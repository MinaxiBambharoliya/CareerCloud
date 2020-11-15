using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : BaseADO, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (CompanyProfilePoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                           ([Id]
                           ,[Registration_Date]
                           ,[Company_Website]
                           ,[Contact_Phone]
                           ,[Contact_Name]
                           ,[Company_Logo])
                     VALUES
                           (@Id
                           ,@Registration_Date
                           ,@Company_Website
                           ,@Contact_Phone
                           ,@Contact_Name
                           ,@Company_Logo)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone); 
                cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {

            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT [Id]
                              ,[Registration_Date]
                              ,[Company_Website]
                              ,[Contact_Phone]
                              ,[Contact_Name]
                              ,[Company_Logo]
                              ,[Time_Stamp]
                          FROM [dbo].[Company_Profiles]";

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            CompanyProfilePoco[] pocos = new CompanyProfilePoco[1000];
            int counter = 0;

            while (rdr.Read())
            {
                CompanyProfilePoco poco = new CompanyProfilePoco();
                poco.Id = rdr.GetGuid(0);
                poco.RegistrationDate = rdr.GetDateTime(1);
                poco.CompanyWebsite = rdr.IsDBNull(2) ? null : rdr.GetString(2);
                poco.ContactPhone = rdr.IsDBNull(3) ? null : rdr.GetString(3);
                poco.ContactName = rdr.IsDBNull(4) ? null : rdr.GetString(4);
                poco.CompanyLogo = rdr.IsDBNull(5) ? (byte[])null : (byte[])rdr[5];
                poco.TimeStamp = (byte[])rdr[6];

                pocos[counter++] = poco;
            }
            conn.Close();

            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (CompanyProfilePoco item in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
                     WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (CompanyProfilePoco item in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                           SET [Registration_Date] = @Registration_Date
                           ,[Company_Website] = @Company_Website
                           ,[Contact_Phone] = @Contact_Phone
                           ,[Contact_Name] = @Contact_Name
                           ,[Company_Logo] = @Company_Logo
                     WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
