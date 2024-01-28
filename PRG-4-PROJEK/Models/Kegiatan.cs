using System;
using System.Data.SqlClient;

namespace PRG_4_PROJEK.Models
{
    public class Kegiatan
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
            
        public Kegiatan(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }


        public List<KegiatanModel> getAllData()
        {
            List<KegiatanModel> kegiatanList = new List<KegiatanModel>();
            try
            {
                string query = "select * from kegiatan";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KegiatanModel kegiatan = new KegiatanModel
                    {
                        id_kegiatan = Convert.ToInt32(reader["id_kegiatan"]),
                        deskripsi = reader["deskripsi"].ToString(),
                        kapasitas = Convert.ToInt32(reader["kapasitas"]),
                        tglmulai = reader["tgl_mulai"] == DBNull.Value ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("tgl_mulai")),
                        tglselesai = reader["tgl_selesai"] == DBNull.Value ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("tgl_selesai")),
                        status = reader["status"].ToString(),
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

     


        public KegiatanModel getData(int id_kegiatan)
        {
            KegiatanModel kegiatanModel = new KegiatanModel();
            try
            {
                Console.WriteLine("id : " + id_kegiatan);
                string query = "select * from kegiatan where id_kegiatan = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id_kegiatan);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                kegiatanModel.id_kegiatan = Convert.ToInt32(reader["id_kegiatan"]);
                kegiatanModel.deskripsi = reader["deskripsi"].ToString();
                kegiatanModel.kapasitas = Convert.ToInt32(reader["kapasitas"]);
                kegiatanModel.tglmulai = reader.GetDateTime(reader.GetOrdinal("tgl_mulai"));
                kegiatanModel.tglselesai = reader.GetDateTime(reader.GetOrdinal("tgl_selesai"));
                kegiatanModel.status = reader["status"].ToString();
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return kegiatanModel;
        }

        public void insertData(KegiatanModel kegiatanModel)
        {
            try
            {
                string query = "insert into kegiatan values(@p1, @p2, @p3, @p4, @p5)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", kegiatanModel.deskripsi);
                command.Parameters.AddWithValue("@p2", kegiatanModel.kapasitas);
                command.Parameters.AddWithValue("@p3", kegiatanModel.tglmulai);
                command.Parameters.AddWithValue("@p4", kegiatanModel.tglselesai);
                command.Parameters.AddWithValue("@p5", kegiatanModel.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void updateData(KegiatanModel kegiatanModel)
        {
            try
            {
                string query = "update kegiatan " +
                "set deskripsi = @p2, " +
                "kapasitas = @p3 " +
                "tgl_mulai = @p4 " +
                "tgl_selesai = @p5 " +
                "status = @p6 " +
                "where id_kegiatan = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", kegiatanModel.id_kegiatan);
                command.Parameters.AddWithValue("@p2", kegiatanModel.deskripsi);
                command.Parameters.AddWithValue("@p3", kegiatanModel.kapasitas);
                command.Parameters.AddWithValue("@p3", kegiatanModel.tglmulai);
                command.Parameters.AddWithValue("@p4", kegiatanModel.tglselesai);
                command.Parameters.AddWithValue("@p5", kegiatanModel.status);
                _connection.Open();
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
                string query = "delete from kegiatan where id_kegiatan = @p1";
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
