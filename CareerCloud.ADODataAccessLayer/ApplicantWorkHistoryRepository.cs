﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository : BaseADO,IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (ApplicantWorkHistoryPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                           ([Id]
                           ,[Applicant]
                           ,[Company_Name]
                           ,[Country_Code]
                           ,[Location]
                           ,[Job_Title]
                           ,[Job_Description]
                           ,[Start_Month]
                           ,[Start_Year]
                           ,[End_Month]
                           ,[End_Year])
                     VALUES
                           (@Id
                           ,@Applicant
                           ,@Company_Name
                           ,@Country_Code
                           ,@Location
                           ,@Job_Title
                           ,@Job_Description
                           ,@Start_Month
                           ,@Start_Year
                           ,@End_Month
                           ,@End_Year)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                cmd.Parameters.AddWithValue("@Location", item.Location);
                cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT [Id]
                              ,[Applicant]
                              ,[Company_Name]
                              ,[Country_Code]
                              ,[Location]
                              ,[Job_Title]
                              ,[Job_Description]
                              ,[Start_Month]
                              ,[Start_Year]
                              ,[End_Month]
                              ,[End_Year]
                              ,[Time_Stamp]
                          FROM [dbo].[Applicant_Work_History]";

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1000];
            int counter = 0;

            while (rdr.Read())
            {
                ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Applicant = rdr.GetGuid(1);
                poco.CompanyName = rdr.GetString(2);
                poco.CountryCode = rdr.GetString(3);
                poco.Location = rdr.GetString(4);
                poco.JobTitle = rdr.GetString(5);
                poco.JobDescription = rdr.GetString(6);
                poco.StartMonth = (short)rdr[7];
                poco.StartYear = (int)rdr[8];
                poco.EndMonth = (short)rdr[9];
                poco.EndYear = (int)rdr[10];
                poco.TimeStamp = (byte[])rdr[11];

                pocos[counter++] = poco;
            }
            conn.Close();

            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach(ApplicantWorkHistoryPoco item in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History] WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (ApplicantWorkHistoryPoco item in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                          SET [Applicant] = @Applicant
                           ,[Company_Name] = @Company_Name
                           ,[Country_Code] = @Country_Code
                           ,[Location] = @Location
                           ,[Job_Title] = @Job_Title
                           ,[Job_Description] = @Job_Description
                           ,[Start_Month] = @Start_Month
                           ,[Start_Year] = @Start_Year
                           ,[End_Month] = @End_Month
                           ,[End_Year] = @End_Year
                        Where [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                cmd.Parameters.AddWithValue("@Location", item.Location);
                cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
