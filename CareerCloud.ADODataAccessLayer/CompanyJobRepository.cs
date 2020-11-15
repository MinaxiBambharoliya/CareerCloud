﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobRepository : BaseADO, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (CompanyJobPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                               ([Id]
                               ,[Company]
                               ,[Profile_Created]
                               ,[Is_Inactive]
                               ,[Is_Company_Hidden])
                         VALUES
                               (@Id
                               ,@Company
                               ,@Profile_Created
                               ,@Is_Inactive
                               ,@Is_Company_Hidden)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT [Id]
                              ,[Company]
                              ,[Profile_Created]
                              ,[Is_Inactive]
                              ,[Is_Company_Hidden]
                              ,[Time_Stamp]
                          FROM [dbo].[Company_Jobs]";

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            CompanyJobPoco[] pocos = new CompanyJobPoco[10000];
            int counter = 0;

            while (rdr.Read())
            {
                CompanyJobPoco poco = new CompanyJobPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Company = rdr.GetGuid(1);
                poco.ProfileCreated = (DateTime)rdr[2];
                poco.IsInactive = (bool)rdr[3];
                poco.IsCompanyHidden = rdr.GetBoolean(4);
                poco.TimeStamp = (byte[])rdr[5];

                pocos[counter++] = poco;
            }
            conn.Close();

            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (CompanyJobPoco item in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs]
                        WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (CompanyJobPoco item in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Company_Jobs]
                               SET [Company] = @Company
                               ,[Profile_Created] = @Profile_Created
                               ,[Is_Inactive] = @Is_Inactive
                               ,[Is_Company_Hidden] = @Is_Company_Hidden
                        WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
