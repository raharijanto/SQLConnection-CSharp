using connectionSQL.Models;
using System;
using System.Data.SqlClient;

namespace connectionSQL
{
    class Program
    {
        SqlConnection connectSql;
        string connectionString =
            "Data Source=DESKTOP-QC1NOEP;" +
            "Initial Catalog=DBMCC001;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False";

        static void Main(string[] args)
        {
            Program progSql = new Program();

            Employees employees = new Employees()
            {
                //EmployeeId = 7,
                //EmployeeName = "Manpaitsumu",
                //IdJob = 4
            };

            //progSql.insert(employees);    // Uncomment all (EmployeeId, EmployeeName, IdJob) in employees class
            //progSql.update(employees);    // Uncomment only (EmployeeId, IdJob) in employees class
            //progSql.delete(employees);    // Uncomment only (EmployeeId) in employees class

            progSql.getAll();
            //progSql.getId(1);
        }

        // Query Get All Data
        void getAll()
        {
            connectSql = new SqlConnection(connectionString);
            SqlCommand commandSql = new SqlCommand();
            commandSql.Connection = connectSql;
            commandSql.CommandText = "select employees.employeename, jobs.jobname from employees join jobs on employees.idjob = jobs.jobid";
            try
            {
                connectSql.Open();
                using (SqlDataReader readData = commandSql.ExecuteReader())
                {
                    if (readData.HasRows)
                    {
                        while (readData.Read())
                        {
                            Console.WriteLine("Employee : " + readData[0]);
                            Console.WriteLine("Job : " + readData[1]);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    readData.Close();
                }
                connectSql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        // Query Get by Id
        void getId (int id)
        {
            connectSql = new SqlConnection(connectionString);
            SqlCommand commandSql = new SqlCommand();
            commandSql.Connection = connectSql;
            commandSql.CommandText = "select " +
                "employees.employeename, jobs.jobname " +
                "from " +
                    "employees " +
                "join " +
                    "jobs on employees.idjob = jobs.jobid " +
                "where employees.employeeid = @id";

            SqlParameter paramSql = new SqlParameter();
            paramSql.ParameterName = "@id";
            paramSql.Value = id;
            commandSql.Parameters.Add(paramSql);
            try
            {
                connectSql.Open();
                using (SqlDataReader readData = commandSql.ExecuteReader())
                {
                    if (readData.HasRows)
                    {
                        while (readData.Read())
                        {
                            Console.WriteLine("Employee : " + readData[0]);
                            Console.WriteLine("Job : " + readData[1]);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    readData.Close();
                }
                connectSql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        // Query Insert
        void insert (Employees employees)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();

                SqlCommand command = connect.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "insert into employees" +
                        "(employeeid, employeename, idjob)" +
                        "values (@employeeid, @employeename, @idjob)";

                    SqlParameter paramSQL = new SqlParameter();
                    paramSQL.ParameterName = "@employeeid";
                    paramSQL.Value = employees.EmployeeId;

                    SqlParameter paramSQL2 = new SqlParameter();
                    paramSQL2.ParameterName = "@employeename";
                    paramSQL2.Value = employees.EmployeeName;

                    SqlParameter paramSQL3 = new SqlParameter();
                    paramSQL3.ParameterName = "@idjob";
                    paramSQL3.Value = employees.IdJob;

                    command.Parameters.Add(paramSQL);
                    command.Parameters.Add(paramSQL2);
                    command.Parameters.Add(paramSQL3);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        // Query Update
        void update (Employees employees)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();

                SqlCommand command = connect.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "update employees " +
                        "set idjob = @idjob " +
                        "where employeeid = @employeeid";

                    SqlParameter paramSQL = new SqlParameter();
                    paramSQL.ParameterName = "@employeeid";
                    paramSQL.Value = employees.EmployeeId;

                    //SqlParameter paramSQL2 = new SqlParameter();
                    //paramSQL2.ParameterName = "@employeename";
                    //paramSQL2.Value = employees.EmployeeName;

                    SqlParameter paramSQL3 = new SqlParameter();
                    paramSQL3.ParameterName = "@idjob";
                    paramSQL3.Value = employees.IdJob;

                    command.Parameters.Add(paramSQL);
                    //command.Parameters.Add(paramSQL2);
                    command.Parameters.Add(paramSQL3);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        // Query Delete
        void delete (Employees employees)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();

                SqlCommand command = connect.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "delete from employees " +
                        "where employeeid = @employeeid";

                    SqlParameter paramSQL = new SqlParameter();
                    paramSQL.ParameterName = "@employeeid";
                    paramSQL.Value = employees.EmployeeId;

                    //SqlParameter paramSQL2 = new SqlParameter();
                    //paramSQL2.ParameterName = "@employeename";
                    //paramSQL2.Value = employees.EmployeeName;

                    //SqlParameter paramSQL3 = new SqlParameter();
                    //paramSQL3.ParameterName = "@idjob";
                    //paramSQL3.Value = employees.IdJob;

                    command.Parameters.Add(paramSQL);
                    //command.Parameters.Add(paramSQL2);
                    //command.Parameters.Add(paramSQL3);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }
    }
}
