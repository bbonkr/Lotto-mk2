using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LottoMk2.Model;
using Tools.Data;
using Tools.Log;

namespace LottoMk2.Helper
{
    public class DatabaseHelper
    {
        public string DefaultConnectionName
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultConnectionName"];
            }
        }

        public string DefaultConnection
        {
            get
            {
                //ConfigurationManager.ConnectionStrings[this.DefaultConnectionName].ConnectionString = this.GetConnectionString();
                //return ConfigurationManager.ConnectionStrings[this.DefaultConnectionName].ConnectionString;

                return this.GetConnectionString();
            }
        }

        public void Init()
        {
            try
            {
                string dbFilePath = this.GetDbFilePath();

                string strPathDatabaseDirectory = Path.GetDirectoryName(dbFilePath);
                string strPathDatabaseFile = Path.GetFileName(dbFilePath);

                if (!File.Exists(dbFilePath))
                {
                    if (!Directory.Exists(strPathDatabaseDirectory))
                    {
                        Directory.CreateDirectory(strPathDatabaseDirectory);
                    }

                    SQLiteConnection.CreateFile(dbFilePath);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(this.GetType(), ex);
                DebugHelper.WriteLine(ex);
            }
        }

        public bool TestConnection()
        {
            return this.TestConnection(this.DefaultConnection);
        }

        /// <summary>
        /// 데이터 베이스 파일 연결 테스트
        /// </summary>
        /// <param name="filePath">SQLite 파일 경로</param>
        /// <returns></returns>
        public bool TestConnection(string filePath)
        {
            string strConnectionString = this.GetConnectionString(filePath);

            using (SQLiteConnection connection = new SQLiteConnection(strConnectionString))
            {
                try
                {
                    connection.Open();

                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                    DebugHelper.WriteLine(ex);
                    return false;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public bool TestTable()
        {
            DataService dao = new DataService();
            dao.SetConnectionString(this.GetConnectionString());
            dao.AddRequestService(CommandType.Text, "select * from lotto", new ParameterCollection(), ExecuteType.ExecuteQuery);
            List<ResponseService> responses = dao.Execute();
            if (dao.HasError)
            {
                Logger.Error(dao.GetType(), dao.ErrorMessage);
                DebugHelper.WriteLine(dao.ErrorMessage);
                return false;
            }
            else
            {
                if (responses[0].DataSet.Tables.Count == 0)
                {
                    DebugHelper.WriteLine("Table is not exists.");
                }
                return responses[0].DataSet.Tables.Count > 0;
            }
        }

        public bool CreateTable()
        {
            string createTable = String.Empty;
            createTable = "Create table Lotto ( ";
            createTable += "     Id int primary key not null ";
            createTable += "   , Dt text not null ";
            createTable += "   , Num1 int not null ";
            createTable += "   , Num2 int not null ";
            createTable += "   , Num3 int not null ";
            createTable += "   , Num4 int not null ";
            createTable += "   , Num5 int not null ";
            createTable += "   , Num6 int not null ";
            createTable += "   , NumBonus int not null ";
            createTable += "    )";

            DataService dao = new DataService();
            dao.SetConnectionString(this.GetConnectionString());
            dao.AddRequestService(CommandType.Text, createTable, new ParameterCollection(), ExecuteType.ExecuteNonQuery);
            List<ResponseService> responses = dao.Execute();
            if (dao.HasError)
            {
                Logger.Error(dao.GetType(), dao.ErrorMessage);
                DebugHelper.WriteLine(dao.ErrorMessage);
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Lotto> CheckLotto(bool incBonusNumber, params int[] numbers)
        {
            List<Lotto> resultSet = new List<Lotto>();
            string inCondition = String.Empty;  // in 조건에 사용될 컬럼이름
            inCondition = "num1 , num2, num3, num4, num5, num6";
            if (incBonusNumber)
            {
                inCondition += ", numBonus";
            }

            ParameterCollection parameters = new ParameterCollection();

            string select = @"select * from lotto where 1 = 1";
            for (int i = 0; i < numbers.Length; i++)
            {
                select += String.Format(" and @num{0} in ({1})", i + 1, inCondition);
                parameters.Add(String.Format("@num{0}", i + 1), numbers[i]);
            }
            select += " order by Id desc";

            DataService dao = new DataService();
            dao.SetConnectionString(this.GetConnectionString());
            dao.AddRequestService(CommandType.Text, select, parameters, ExecuteType.ExecuteQuery);
            List<ResponseService> responses = dao.Execute();
            if (dao.HasError)
            {
                Logger.Error(dao.GetType(), dao.ErrorMessage);
                DebugHelper.WriteLine(dao.ErrorMessage);
            }
            else
            {
                if (responses[0].DataSet.Tables.Count == 0)
                {
                    DebugHelper.WriteLine("Table is not exists.");
                }
                else
                {
                    resultSet = responses[0].DataSet.Tables[0].Select().Select(r => new Lotto()
                    {
                        Id = Convert.ToInt32(r["Id"]),
                        Dt = String.Format("{0}", r["Dt"]),
                        Num1 = Convert.ToInt32(r["Num1"]),
                        Num2 = Convert.ToInt32(r["Num2"]),
                        Num3 = Convert.ToInt32(r["Num3"]),
                        Num4 = Convert.ToInt32(r["Num4"]),
                        Num5 = Convert.ToInt32(r["Num5"]),
                        Num6 = Convert.ToInt32(r["Num6"]),
                        NumBonus = Convert.ToInt32(r["NumBonus"]),
                    }).ToList();
                }
            }

            return resultSet;
        }

        public bool Save(Lotto item)
        {
            string save = "";

            if (this.GetData(item.Id, String.Empty, String.Empty).Count > 0)
            {
                // update
                save += "update lotto ";
                save += "   set Dt = @dt";
                save += "     , Num1 = @num1";
                save += "     , Num2 = @num2";
                save += "     , Num3 = @num3";
                save += "     , Num4 = @num4";
                save += "     , Num5 = @num5";
                save += "     , Num6 = @num6";
                save += "     , NumBonus = @numBonus";
                save += " where Id   = @id";
            }
            else
            {
                // insert
                save += "insert into lotto (";
                save += "            Id";
                save += "          , Dt";
                save += "          , Num1";
                save += "          , Num2";
                save += "          , Num3";
                save += "          , Num4";
                save += "          , Num5";
                save += "          , Num6";
                save += "          , NumBonus";
                save += "          ) ";
                save += "     values ";
                save += "          ( @id";
                save += "          , @dt";
                save += "          , @num1";
                save += "          , @num2";
                save += "          , @num3";
                save += "          , @num4";
                save += "          , @num5";
                save += "          , @num6";
                save += "          , @numBonus";
                save += "          )";
            }

            ParameterCollection parameters = new ParameterCollection();
            parameters.Add("@id", item.Id);
            parameters.Add("@dt", item.Dt);
            parameters.Add("@num1", item.Num1);
            parameters.Add("@num2", item.Num2);
            parameters.Add("@num3", item.Num3);
            parameters.Add("@num4", item.Num4);
            parameters.Add("@num5", item.Num5);
            parameters.Add("@num6", item.Num6);
            parameters.Add("@numBonus", item.NumBonus);

            DataService dao = new DataService();
            dao.SetConnectionString(this.GetConnectionString());
            dao.AddRequestService(CommandType.Text, save, parameters, ExecuteType.ExecuteNonQuery);
            List<ResponseService> responses = dao.Execute();
            if (dao.HasError)
            {
                Logger.Error(dao.GetType(), dao.ErrorMessage);
                DebugHelper.WriteLine(dao.ErrorMessage);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Delete(Lotto item)
        {
            string delete = "";

            if (this.GetData(item.Id, String.Empty, String.Empty).Count > 0)
            {
                // update
                delete += "delete from lotto ";
                delete += " where Id   = @id";
            }
            else
            {
                return false;
            }

            ParameterCollection parameters = new ParameterCollection();
            parameters.Add("@id", item.Id);
            //parameters.Add("@dt", item.Dt);
            //parameters.Add("@num1", item.Num1);
            //parameters.Add("@num1", item.Num2);
            //parameters.Add("@num1", item.Num3);
            //parameters.Add("@num1", item.Num4);
            //parameters.Add("@num1", item.Num5);
            //parameters.Add("@num1", item.Num6);

            DataService dao = new DataService();
            dao.SetConnectionString(this.GetConnectionString());
            dao.AddRequestService(CommandType.Text, delete, parameters, ExecuteType.ExecuteNonQuery);
            List<ResponseService> responses = dao.Execute();
            if (dao.HasError)
            {
                Logger.Error(dao.GetType(), dao.ErrorMessage);
                DebugHelper.WriteLine(dao.ErrorMessage);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteAll()
        {
            string delete = "delete from lotto ";
            ParameterCollection parameters = new ParameterCollection();

            DataService dao = new DataService();
            dao.SetConnectionString(this.GetConnectionString());
            dao.AddRequestService(CommandType.Text, delete, parameters, ExecuteType.ExecuteNonQuery);
            List<ResponseService> responses = dao.Execute();
            if (dao.HasError)
            {
                Logger.Error(dao.GetType(), dao.ErrorMessage);
                DebugHelper.WriteLine(dao.ErrorMessage);
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Lotto> GetData(int? id, string startDt, string endDt)
        {
            List<Lotto> resultSet = new List<Lotto>();

            ParameterCollection parameters = new ParameterCollection();

            string select = @"select * from lotto where 1 = 1";
            if (id.HasValue)
            {
                select += " and id = @id";
                parameters.Add("@id", id.Value);
            }

            if (!String.IsNullOrEmpty(startDt) && !String.IsNullOrEmpty(endDt))
            {
                select += " and Dt between @startDt and @endDt";
                parameters.Add("@startDt", startDt);
                parameters.Add("@endDt", endDt);
            }

            select += " order by Id desc";

            DataService dao = new DataService();
            dao.SetConnectionString(this.GetConnectionString());
            dao.AddRequestService(CommandType.Text, select, parameters, ExecuteType.ExecuteQuery);
            List<ResponseService> responses = dao.Execute();
            if (dao.HasError)
            {
                Logger.Error(dao.GetType(), dao.ErrorMessage);
                DebugHelper.WriteLine(dao.ErrorMessage);
            }
            else
            {
                if (responses[0].DataSet.Tables.Count == 0)
                {
                    DebugHelper.WriteLine("No data.");
                }
                else
                {
                    resultSet = responses[0].DataSet.Tables[0].Select().Select(r => new Lotto()
                    {
                        Id = Convert.ToInt32(r["Id"]),
                        Dt = String.Format("{0}", r["Dt"]),
                        Num1 = Convert.ToInt32(r["Num1"]),
                        Num2 = Convert.ToInt32(r["Num2"]),
                        Num3 = Convert.ToInt32(r["Num3"]),
                        Num4 = Convert.ToInt32(r["Num4"]),
                        Num5 = Convert.ToInt32(r["Num5"]),
                        Num6 = Convert.ToInt32(r["Num6"]),
                        NumBonus = Convert.ToInt32(r["NumBonus"])
                    }).ToList();
                }
            }

            return resultSet;
        }

        public string GetDbFilePath()
        {
            string dbFileName = "Lotto.db";
            string dbDirectory = "data";
            FrmMain frm = new FrmMain();

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), frm.CompanyName, frm.ProductName, dbDirectory, dbFileName);

            return path;
        }

        private string GetConnectionString()
        {
            string connectionString = String.Empty;

            string filePath = this.GetDbFilePath();
            connectionString = this.GetConnectionString(filePath);

            return connectionString;
        }

        private string GetConnectionString(string filePath)
        {
            string connectionString = String.Empty;

            connectionString = String.Format("Data Source={0}", filePath);

            return connectionString;
        }
    }
}