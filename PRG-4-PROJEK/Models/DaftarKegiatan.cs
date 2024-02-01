using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Data;

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
                string query = "select * from pendaftaran";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DaftarKegiatanModel daftarkegiatan = new DaftarKegiatanModel
                    {
                        id_daftarkegiatan = Convert.ToInt32(reader["id_daftarkegiatan"]),
                        nim = reader["nim"].ToString(),
                        id_kegiatan = Convert.ToInt32(reader["id_kegiatan"]),
                        deskripsi_penolakan = reader["deskripsi_penolakan"].ToString(),
                        catatan = reader["catatan"].ToString(),
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

        public List<DaftarKegiatanModel> getAllDataP()
        {
            List<DaftarKegiatanModel> daftarkegiatanList = new List<DaftarKegiatanModel>();
            try
            {
                string query = "select * from pendaftaran WHERE status = 'Menunggu Persetujuan PIC'";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DaftarKegiatanModel daftarkegiatan = new DaftarKegiatanModel
                    {
                        id_daftarkegiatan = Convert.ToInt32(reader["id_daftarkegiatan"]),
                        nim = reader["nim"].ToString(),
                        id_kegiatan = Convert.ToInt32(reader["id_kegiatan"]),
                        deskripsi_penolakan = reader["deskripsi_penolakan"].ToString(),
                        catatan = reader["catatan"].ToString(),
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

        public List<DaftarKegiatanModel> getAllDataJP()
        {
            List<DaftarKegiatanModel> daftarkegiatanList = new List<DaftarKegiatanModel>();
            try
            {
                string query = "select * from pendaftaran WHERE status = 'Menunggu Proses Pengajuan'";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DaftarKegiatanModel daftarkegiatan = new DaftarKegiatanModel
                    {
                        id_daftarkegiatan = Convert.ToInt32(reader["id_daftarkegiatan"]),
                        nim = reader["nim"].ToString(),
                        id_kegiatan = Convert.ToInt32(reader["id_kegiatan"]),
                        deskripsi_penolakan = reader["deskripsi_penolakan"].ToString(),
                        catatan = reader["catatan"].ToString(),
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

        public List<DaftarKegiatanModel> getAllDataJP(int id)
        {
            List<DaftarKegiatanModel> daftarkegiatanList = new List<DaftarKegiatanModel>();
            try
            {
                string query = "select * from pendaftaran WHERE id_daftarkegiatan = "+id;
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DaftarKegiatanModel daftarkegiatan = new DaftarKegiatanModel
                    {
                        id_daftarkegiatan = Convert.ToInt32(reader["id_daftarkegiatan"]),
                        nim = reader["nim"].ToString(),
                        id_kegiatan = Convert.ToInt32(reader["id_kegiatan"]),
                        deskripsi_penolakan = reader["deskripsi_penolakan"].ToString(),
                        catatan = reader["catatan"].ToString(),
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
            DaftarKegiatanModel pengajuanModel = new DaftarKegiatanModel();
            try
            {
                Console.WriteLine("id_pendaftaran : " + id_daftar_kegiatan);
                string query = "SELECT * FROM pendaftaran JOIN kegiatan ON pendaftaran.id_kegiatan = kegiatan.id_kegiatan JOIN mahasiswa ON pendaftaran.nim = mahasiswa.nim";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id_daftar_kegiatan);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                pengajuanModel.id_daftarkegiatan = Convert.ToInt32(reader["id_daftarkegiatan"]);
                pengajuanModel.nim = reader["nim"].ToString();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pengajuanModel;
        }



        public void insertData(DaftarKegiatanModel daftarkegiatanModel)
        {
            try
            {
                string query = "insert into pendaftaran values(@p1, @p2, @p3, @p4, @p5)";
                SqlCommand command = new SqlCommand(query, _connection);
              
                command.Parameters.AddWithValue("@p1", daftarkegiatanModel.nim);
                command.Parameters.AddWithValue("@p2", daftarkegiatanModel.id_kegiatan);
                command.Parameters.AddWithValue("@p3", daftarkegiatanModel.deskripsi_penolakan ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@p4", daftarkegiatanModel.catatan ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@p5", daftarkegiatanModel.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("srt pend : "+ex.Message);
            }
        }

        public void deleteData(int id)
        {
            try
            {
                string query = "UPDATE pendaftaran SET status='Dibatalkan' where id_daftarkegiatan = @p1";
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

        public void TolakJP(int id,string deskripsi_penolakan)
        {
            try
            {
                string query = "UPDATE pendaftaran SET status='Ditolak', deskripsi_penolakan = @p2 WHERE id_daftarkegiatan = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                command.Parameters.AddWithValue("@p2", deskripsi_penolakan);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AccPend(int id)
        {
            try
            {
                //Ambil id kegiatan
                string ppp = "SELECT id_kegiatan FROM pendaftaran WHERE id_daftarkegiatan = @p1";
                SqlCommand commands = new SqlCommand(ppp, _connection);
                commands.Parameters.AddWithValue("@p1", id);
                _connection.Open();

                int idkegiatan = (int)commands.ExecuteScalar();
                _connection.Close();

                Console.WriteLine(id);
                string query = "UPDATE pendaftaran SET status = @p2 WHERE id_daftarkegiatan = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                command.Parameters.AddWithValue("@p2", "Disetujui");
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();

                //Mengurangi kapasitas
                string query1 = "UPDATE kegiatan SET kapasitas = kapasitas - 1 WHERE id_kegiatan = @p1";
                using SqlCommand command1 = new SqlCommand( query1, _connection);
                command1.Parameters.AddWithValue("@p1", idkegiatan);
                _connection.Open();
                command1.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("pend : "+ex.Message);
            }
        }

        public void PengajuanJP(int id)
        {
            try
            {

                Console.WriteLine(id);
                string query = "UPDATE pendaftaran SET status = @p2 WHERE id_daftarkegiatan = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                command.Parameters.AddWithValue("@p2", "Menunggu Proses Pengajuan");
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("pend : " + ex.Message);
            }
        }

        public List<KegiatanModel> getAllDatakegiatan()
        {
            List<KegiatanModel> kegiatanList = new List<KegiatanModel>();
            try
            {
                string query = "SELECT * FROM kegiatan WHERE status='Sedang Dikerjakan'";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KegiatanModel kegiatan = new KegiatanModel
                    {
                        id_kegiatan = Convert.ToInt32(reader["id_kegiatan"]),
                        deskripsi = reader["deskripsi"].ToString(),
                        kapasitas = Convert.ToInt32(reader["kapasitas"].ToString()),
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
        public void AccJP(PengajuanModel pengajuanModel)
        {
            try
            {

                string ppp = "SELECT id_kegiatan FROM pendaftaran WHERE id_daftarkegiatan = @p1";
                SqlCommand commands = new SqlCommand(ppp, _connection);
                commands.Parameters.AddWithValue("@p1", pengajuanModel.id_daftarkegiatan);
                _connection.Open();
                int idkegiatan = (int)commands.ExecuteScalar();
                _connection.Close();
                Console.WriteLine("id pend : " + idkegiatan);

                string query = "INSERT INTO pengajuan VALUES(@p1, @p2, @p3, GETDATE(), @p5)";
                SqlCommand command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@p1", pengajuanModel.id_daftarkegiatan);
                command.Parameters.AddWithValue("@p2", pengajuanModel.jam_plus);
                command.Parameters.AddWithValue("@p3", pengajuanModel.status);
                command.Parameters.AddWithValue("@p5", "1");

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();

                string query1 = "UPDATE pendaftaran SET status = 'Pengajuan Diterima' WHERE id_kegiatan = @p1";
                using SqlCommand command1 = new SqlCommand(query1, _connection);
                command1.Parameters.AddWithValue("@p1", idkegiatan);
                _connection.Open();
                command1.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("srt pengajuan : " + ex.Message);
            }

        }

        public bool CheckIfAlreadyRegistered(int id_kegiatan)
        {
            try
            {
                // Query untuk memeriksa apakah id_kegiatan sudah terdaftar sebelumnya
                string query = "SELECT COUNT(*) FROM pendaftaran WHERE id_kegiatan = @id_kegiatan";

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@id_kegiatan", id_kegiatan);
                    _connection.Open();
                    int count = (int)command.ExecuteScalar(); 

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false; 
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close(); 
                }
            }
        }

    }
}
