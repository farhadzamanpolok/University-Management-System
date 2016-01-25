using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WindowsFormsApplication2
{

    static class DataAccess
    {
       
        public static readonly dbDataContext Db = new dbDataContext();
        private static readonly string ConnectionString = Db.Connection.ConnectionString;
        static SqlConnection _connection;
        private static SqlCommand _cmd;
        private static SqlDataAdapter _adp;
        private static DataSet _ds;
        public static SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(ConnectionString);
                    _connection.Open();

                    return _connection;
                }
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();

                    return _connection;
                }
                return _connection;
            }
        }

        public static DataSet GetDataSet(string sql)
        {
             _cmd = new SqlCommand(sql, Connection);
            _adp = new SqlDataAdapter(_cmd);

             _ds = new DataSet();
            _adp.Fill(_ds);
            
            return _ds;
        }

        public static void Submitdataset()
        {
           var cmBl= new SqlCommandBuilder(_adp);
            _adp.Update(_ds);
            Close();
        }
        public static DataTable GetDataTable(string sql)
        {
            var ds = GetDataSet(sql);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static int ExecuteSql(string sql)
        {
            var cmd = new SqlCommand(sql, Connection);
            return cmd.ExecuteNonQuery();
        }

        public static int ExecuteSql(string sql, SqlParameter p)
        {
            var cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.Add(p);
            return cmd.ExecuteNonQuery();
        }

        public static int Find_user(string username, string password, int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        {
                            var serial = from s in Db.students
                                         where s.student_id == username && s.password == password
                                         select s.student_id;

                            if (serial.Single() != null) return type;

                        }
                        break;
                    case 2:
                        {
                            var serial = from s in Db.faculties
                                         where s.faculty_Id == username && s.password == password
                                         select s.faculty_Id;
                            if (serial.Single() != null) return type;
                        }
                        break;
                    default:
                        {
                            var serial = from s in Db.admins
                                         where s.admin_Id == username && s.password == password
                                         select s.admin_Id;
                            if (serial.Single() != null) return type;
                        }
                        break;
                }
            }
            catch (Exception)
            {
                Close();
                return 0;
            }
            finally
            {
                Close();
            }
            return 0;
        }
        public static void Close()
        {
            Db.Connection.Close();
            Connection.Close();
            _connection.Close();
        }
        public static void Open()
        {
            Db.Connection.Open();
            
        }
    }

}