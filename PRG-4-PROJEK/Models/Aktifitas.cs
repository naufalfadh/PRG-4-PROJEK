using System;
using System.Data.SqlClient;

namespace PRG_4_PROJEK.Models
{
    public class Aktifitas
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
            
        public Aktifitas(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }


        public List<AktifitasModel> getAllData()
        {
            List<AktifitasModel> aktifitasList = new List<AktifitasModel>();
            try
            {
                string query = "select * from aktifitas";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AktifitasModel aktifitas = new AktifitasModel
                    {
                        id_aktifitas = Convert.ToInt32(reader["id_aktifitas"]),
                        deskripsi = reader["deskripsi"].ToString(),
                        nim = reader["nim"].ToString(),
                        nama = reader["nama"].ToString(),
                        jp = Convert.ToInt32(reader["jam_plus"]),
                        jm = Convert.ToInt32(reader["jam_minus"]),
                    };

                    aktifitasList.Add(aktifitas);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return aktifitasList;
        }

        public List<MahasiswaModel> getAllDatamahasiswa()
        {
            List<MahasiswaModel> mahasiswaList = new List<MahasiswaModel>();
            try
            {
                string query = "SELECT * from mahasiswa";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MahasiswaModel mahasiswa = new MahasiswaModel
                    {
                        nim = reader["nim"].ToString(),
                        nama = reader["nama"].ToString(),
                    };
                    mahasiswaList.Add(mahasiswa);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mahasiswaList;
        }
        public void insertData(AktifitasModel aktifitasModel)
        {
            try
            {
                string query = "INSERT INTO aktifitas VALUES(@deskripsi, @nim, @nama, @jp, @jm)";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@deskripsi", aktifitasModel.deskripsi);
                    command.Parameters.AddWithValue("@nim", aktifitasModel.nim);
                    command.Parameters.AddWithValue("@nama", aktifitasModel.nama);
                    command.Parameters.AddWithValue("@jp", aktifitasModel.jp ?? (object)DBNull.Value); // Use DBNull if jp is null
                    command.Parameters.AddWithValue("@jm", aktifitasModel.jm ?? (object)DBNull.Value); // Use DBNull if jm is null

                    _connection.Open();
                    command.ExecuteNonQuery();
                }

                // Reset query variable
                query = "";

                // Update Jam_Plus or Jam_Minus in mahasiswa table
                if (aktifitasModel.jp != null)
                {
                    query = "UPDATE mahasiswa SET jam_plus = jam_plus + @jam_plus WHERE nim = @nim";
                }
                else if (aktifitasModel.jm != null)
                {
                    query = "UPDATE mahasiswa SET jam_minus = jam_minus + @jam_minus WHERE nim = @nim";
                }

                if (!string.IsNullOrEmpty(query))
                {
                    using (SqlCommand updateCommand = new SqlCommand(query, _connection))
                    {
                        if (aktifitasModel.jp != null || aktifitasModel.jm != null)
                        {
                            if (aktifitasModel.jp != null)
                            {
                                updateCommand.Parameters.AddWithValue("@jam_plus", aktifitasModel.jp);
                            }
                            else
                            {
                                updateCommand.Parameters.AddWithValue("@jam_minus", aktifitasModel.jm);
                            }
                            updateCommand.Parameters.AddWithValue("@nim", aktifitasModel.nim);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                }

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
                string query = "delete from mahasiswa where id_aktifitas = @p1";
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
    } 
}
