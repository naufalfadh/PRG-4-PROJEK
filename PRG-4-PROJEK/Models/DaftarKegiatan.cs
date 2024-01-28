using System;
using System.Data.SqlClient;

namespace PRG_4_PROJEK.Models
{
    public class DaftarKegiatan
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
            
        public DaftarKegiatan(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }


        public List<DaftarKegiatanModel> getAllData()
        {
            List<DaftarKegiatanModel> daftarkegiatanList = new List<DaftarKegiatanModel>();
            try
            {
                string query = "select * from daftar_kegiatan";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DaftarKegiatanModel daftarkegiatan = new DaftarKegiatanModel
                    {
                        id_daftar_kegiatan = Convert.ToInt32(reader["id_daftar_kegiatan"]),
                        nim = reader["nim"].ToString(),
                        id_kegiatan = reader["id_kegiatan"].ToString(),
                        status = reader["status"].ToString(),
                    };

                    daftarkegiatanList.Add(daftarkegiatan);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return daftarkegiatanList;
        }

     


        public DaftarKegiatanModel getData(int id_daftar_kegiatan)
        {
            DaftarKegiatanModel daftarkegiatanModel = new DaftarKegiatanModel();
            try
            {
                Console.WriteLine("id : " + id_daftar_kegiatan);
                string query = "select * from daftar_kegiatan where id_daftar_kegiatan = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id_daftar_kegiatan);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                daftarkegiatanModel.id_daftar_kegiatan = Convert.ToInt32(reader["id_daftar_kegiatan"]);
                daftarkegiatanModel.nim = reader["nim"].ToString();
                daftarkegiatanModel.id_kegiatan = reader["id_kegiatan"].ToString();
                daftarkegiatanModel.status = reader["status"].ToString();
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return daftarkegiatanModel;
        }

        public void insertData(DaftarKegiatanModel daftarkegiatanModel)
        {
            try
            {
                string query = "insert into daftar_kegiatan values(@p1, @p2, @p3)";
                SqlCommand command = new SqlCommand(query, _connection);
              
                command.Parameters.AddWithValue("@p1", daftarkegiatanModel.nim);
                command.Parameters.AddWithValue("@p2", daftarkegiatanModel.id_kegiatan);
                command.Parameters.AddWithValue("@p3", daftarkegiatanModel.status);
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
                string query = "delete from daftar_kegiatan where id_daftar_kegiatan = @p1";
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
        public List<KegiatanModel> getAllDatakegiatan()
        {
            List<KegiatanModel> kegiatanList = new List<KegiatanModel>();
            try
            {
                string query = "SELECT * FROM kegiatan";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KegiatanModel kegiatan = new KegiatanModel
                    {
                        id_kegiatan = Convert.ToInt32(reader["id_kegiatan"]),
                        deskripsi = reader["deskripsi"].ToString(),
                    };
                    kegiatanList.Add(kegiatan);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return kegiatanList;
        }
    } 
}
