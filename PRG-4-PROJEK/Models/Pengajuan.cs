using System.Data.SqlClient;

namespace PRG_4_PROJEK.Models
{
    public class Pengajuan
    {

        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public Pengajuan(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }


        public int GetTotalPengajuan()
        {
            List<PengajuanModel> pengajuanList = getAllData();
            int totalPengajuan = pengajuanList.Count;
            return totalPengajuan;
        }
        public List<PengajuanModel> getAllData()
        {
            List<PengajuanModel> pengajuanList = new List<PengajuanModel>();
            try
            {
                string query = "SELECT * from pengajuan";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PengajuanModel pengajuan = new PengajuanModel
                    {
                        id_pengajuan = Convert.ToInt32(reader["id_pengajuan"]),
                        
                    };
                    pengajuanList.Add(pengajuan);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return pengajuanList;
        }
        public PengajuanModel getData(int id_daftar_kegiatan)
        {
            PengajuanModel pengajuanModel = new PengajuanModel();
            try
            {
                Console.WriteLine("id_pendaftaran : " + id_daftar_kegiatan);
                string query = "SELECT * FROM pendaftaran JOIN kegiatan ON pendaftaran.id_kegiatan = kegiatan.id_kegiatan JOIN mahasiswa ON pendaftaran.nim = mahasiswa.nim where id_daftarkegiatan = "+id_daftar_kegiatan;
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id_daftar_kegiatan);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                pengajuanModel.id_daftarkegiatan = Convert.ToInt32(reader["id_daftarkegiatan"]);
                pengajuanModel.status = reader["nim"].ToString();
                pengajuanModel.nim = reader["nim"].ToString();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pengajuanModel;
        }

        public void AccJP(PengajuanModel pengajuanModel)
        {
            try
            {

                string ppp = "SELECT id_daftarkegiatan FROM pendaftaran WHERE id_daftarkegiatan = @p1";
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
                command.Parameters.AddWithValue("@p5", pengajuanModel.id_karyawan);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();

                string query1 = "UPDATE pendaftaran SET status = 'Pengajuan Diterima' WHERE id_daftarkegiatan = @p1";
                using SqlCommand command1 = new SqlCommand(query1, _connection);
                command1.Parameters.AddWithValue("@p1", pengajuanModel.id_daftarkegiatan);
                _connection.Open();
                command1.ExecuteNonQuery();
                _connection.Close();

                string query2 = "UPDATE mahasiswa SET jam_plus = jam_plus + @p2 WHERE nim = @p1";
                using SqlCommand command2 = new SqlCommand(query2, _connection);
                command2.Parameters.AddWithValue("@p1", pengajuanModel.nim);
                command2.Parameters.AddWithValue("@p2", pengajuanModel.jam_plus);
                _connection.Open();
                command2.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("srt pengajuan : " + ex.Message);
            }
        }
    }
}
