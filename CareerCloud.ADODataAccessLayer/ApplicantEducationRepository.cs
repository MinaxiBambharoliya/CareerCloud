using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : BaseADO,IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (ApplicantEducationPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                       ([Id]
                       ,[Applicant]
                       ,[Major]
                       ,[Certificate_Diploma]
                       ,[Start_Date]
                       ,[Completion_Date]
                       ,[Completion_Percent])
                            VALUES
                       (@Id
                       ,@Applicant
                       ,@Major
                       ,@Certificate_Diploma
                       ,@Start_Date
                       ,@Completion_Date
                       ,@Completion_Percent)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Major", item.Major);
                cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT [Id]
                          ,[Applicant]
                          ,[Major]
                          ,[Certificate_Diploma]
                          ,[Start_Date]
                          ,[Completion_Date]
                          ,[Completion_Percent]
                          ,[Time_Stamp]
                      FROM [dbo].[Applicant_Educations]";

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];
            int counter = 0;

            while (rdr.Read())
            {
                ApplicantEducationPoco poco = new ApplicantEducationPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Applicant = rdr.GetGuid(1);
                poco.Major = rdr.GetString(2);
                poco.CertificateDiploma = rdr.GetString(3);
                poco.StartDate = (DateTime?)rdr[4];
                poco.CompletionDate = (DateTime?)rdr[5];
                poco.CompletionPercent = (byte?)rdr[6];
                poco.TimeStamp = (byte[])rdr[7];

                pocos[counter++] = poco;
            }
            conn.Close();

            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {

            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };


            foreach (ApplicantEducationPoco item in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Educations] WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                
                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };


            foreach (ApplicantEducationPoco item in items)
            {
               // cmd.CommandType = System.Data.CommandType.StoredProcedure

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations]
               SET [Applicant] = @Applicant
                  ,[Major] = @Major
                  ,[Certificate_Diploma] = @Certificate_Diploma
                  ,[Start_Date] = @Start_Date
                  ,[Completion_Date] = @Completion_Date
                  ,[Completion_Percent] = @Completion_Percent
                WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Major", item.Major);
                cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
