using CareerCloud.DataAccessLayer;
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
    public class ApplicantResumeRepository : BaseADO,IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };
            foreach (ApplicantResumePoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                    ([Id]
                    ,[Applicant]
                    ,[Resume]
                    ,[Last_Updated])
                VALUES
                   (@Id
                   ,@Applicant
                   ,@Resume
                   ,@Last_Updated)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Resume", item.Resume);
                cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);

                conn.Open(); 
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT [Id]
                              ,[Applicant]
                              ,[Resume]
                              ,[Last_Updated]
                          FROM [dbo].[Applicant_Resumes]";

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            ApplicantResumePoco[] pocos = new ApplicantResumePoco[1000];
            int counter = 0;

            while (rdr.Read())
            {
                ApplicantResumePoco poco = new ApplicantResumePoco();
                poco.Id = rdr.GetGuid(0);
                poco.Applicant = rdr.GetGuid(1);
                poco.Resume = rdr.GetString(2);
                poco.LastUpdated = rdr.IsDBNull(3)?(DateTime?)null : (DateTime?)rdr.GetDateTime(3);

                pocos[counter++] = poco;
            }
            conn.Close();

            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach(ApplicantResumePoco item in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes] WHERE [Id]=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };
            foreach (ApplicantResumePoco item in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                    SET [Applicant] = @Applicant
                    ,[Resume] = @Resume
                    ,[Last_Updated] = @Last_Updated
                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Resume", item.Resume);
                cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
