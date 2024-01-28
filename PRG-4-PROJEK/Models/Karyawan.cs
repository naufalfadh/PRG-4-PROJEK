using System;
using System.Data.SqlClient;

namespace PRG_4_PROJEK.Models
{
    public class Karyawan
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
            
        public Karyawan(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }

        public KaryawanModel getDataByUsernamePassword(string npk, string password)
        {
            KaryawanModel karyawanModel = new KaryawanModel();
            try
            {
                string query = "SELECT * from karyawan where id_karyawan = @p1 AND password = @p2";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", npk);
                command.Parameters.AddWithValue("@p2", password);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    karyawanModel.npk = reader["id_karyawan"].ToString();
                    karyawanModel.nama = reader["nama"].ToString();
                    karyawanModel.jk = reader["jenis_kelamin"].ToString();
                    karyawanModel.password = reader["password"].ToString();
                    karyawanModel.role = reader["role"].ToString();
                    karyawanModel.status = reader["status"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally 
            {
                _connection.Close();
            }
            return karyawanModel;
        }

        public List<KaryawanModel> getAllData()
        {
            List<KaryawanModel> karyawanList = new List<KaryawanModel>();
            try
            {
                string query = "select * from karyawan";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KaryawanModel karyawan= new KaryawanModel
                    {
                        npk = reader["id_karyawan"].ToString(),
                        nama = reader["nama"].ToString(),
                        password = reader["password"].ToString(),
                        jk = reader["jenis_kelamin"].ToString(),
                        role = reader["role"].ToString(),
                        status = reader["status"].ToString(),
                    };
                    karyawanList.Add(karyawan);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return karyawanList;
        }

        public KaryawanModel getData(string npk)
        {
            KaryawanModel karyawanModel = new KaryawanModel();
            try
            {
                string query = "select * from karyawan where id_karyawan = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", npk);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                karyawanModel.npk = reader["id_karyawan"].ToString();
                karyawanModel.nama = reader["nama"].ToString();
                karyawanModel.password = reader["password"].ToString();
                karyawanModel.jk = reader["jenis_kelamin"].ToString();
                karyawanModel.role = reader["role"].ToString();
                karyawanModel.status = reader["status"].ToString();
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return karyawanModel;
        }

        public void insertData(KaryawanModel karyawanModel)
        {
            try
            {
                string query = "insert into karyawan values(@p1, @p2, @p3, @p4, @p5, @p6)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", karyawanModel.npk);
                command.Parameters.AddWithValue("@p2", karyawanModel.nama);
                command.Parameters.AddWithValue("@p3", karyawanModel.password);
                command.Parameters.AddWithValue("@p4", karyawanModel.jk);
                command.Parameters.AddWithValue("@p5", karyawanModel.role);
                command.Parameters.AddWithValue("@p6", karyawanModel.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void updateData(KaryawanModel karyawanModel)
        {
            try
            {
                string query = "update karyawan " +
                "set nama = @p2, " +
                "password = @p3, " +
                "jenis_kelamin = @p4, " +
                "role = @p5, " +
                "status = @p6 " +
                "where id_karyawan = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", karyawanModel.npk);
                command.Parameters.AddWithValue("@p2", karyawanModel.nama);
                command.Parameters.AddWithValue("@p3", karyawanModel.password);
                command.Parameters.AddWithValue("@p4", karyawanModel.jk);
                command.Parameters.AddWithValue("@p5", karyawanModel.role);
                command.Parameters.AddWithValue("@p6", karyawanModel.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void deleteData(int id)
        {
            try
            {
                string query = "delete from karyawan where id_karyawan = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public bool IsNPKAlreadyExists(string npk)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM karyawan WHERE id_karyawan = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", npk);
                _connection.Open();

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
    } 
}
