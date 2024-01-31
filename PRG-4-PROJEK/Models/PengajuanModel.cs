namespace PRG_4_PROJEK.Models
{
    public class PengajuanModel
    {

        public int id_pengajuan {  get; set; }
        public int id_daftarkegiatan { get; set; }
        public int jam_plus { get; set; }
        public string status { get; set; }
        public DateTime tanggal_pengajuan { get; set; }
        public string id_karyawan { get; set; }
        public string nim {  get; set; }
        public string? deskripsi_penolakan { get; set; }
    }
}
