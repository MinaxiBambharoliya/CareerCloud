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
    public class SecurityLoginsLogRepository : BaseADO, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (SecurityLoginsLogPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                               ([Id]
                               ,[Login]
                               ,[Source_IP]
                               ,[Logon_Date]
                               ,[Is_Succesful])
                         VALUES
                               (@Id
                               ,@Login
                               ,@Source_IP
                               ,@Logon_Date
                               ,@Is_Succesful)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {

            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT [Id]
                              ,[Login]
                              ,[Source_IP]
                              ,[Logon_Date]
                              ,[Is_Succesful]
                          FROM [dbo].[Security_Logins_Log]";

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[10000];
            int counter = 0;

            while (rdr.Read())
            {
                SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Login = rdr.GetGuid(1);
                poco.SourceIP = rdr.GetString(2);
                poco.LogonDate = (DateTime)rdr[3];
                poco.IsSuccesful = rdr.GetBoolean(4);

                pocos[counter++] = poco;
            }
            conn.Close();

            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {

            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (SecurityLoginsLogPoco item in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Log]
                         WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn
            };

            foreach (SecurityLoginsLogPoco item in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                               SET [Login] = @Login
                               ,[Source_IP] = @Source_IP
                               ,[Logon_Date] = @Logon_Date
                               ,[Is_Succesful] = @Is_Succesful
                         WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
