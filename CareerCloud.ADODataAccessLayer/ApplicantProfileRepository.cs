using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfiguration config = configBuilder.Build();
            SqlConnection conn = new SqlConnection(config.GetConnectionString("ConnectionString"));
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantProfilePoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                       ([Id]
                       ,[Login]
                       ,[Current_Salary]
                       ,[Current_Rate]
                       ,[Currency]
                       ,[Country_Code]
                       ,[State_Province_Code]
                       ,[Street_Address]
                       ,[City_Town]
                       ,[Zip_Postal_Code])
                 VALUES
                       (@Id
                       ,@Login
                       ,@Current_Salary
                       ,@Current_Rate
                       ,@Currency
                       ,@Country_Code
                       ,@State_Province_Code
                       ,@Street_Address
                       ,@City_Town
                       ,@Zip_Postal_Code)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                cmd.Parameters.AddWithValue("@Currency", item.Currency);
                cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                cmd.Parameters.AddWithValue("@City_Town", item.City);
                cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
