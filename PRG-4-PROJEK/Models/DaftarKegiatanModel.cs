using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class DaftarKegiatanModel
    {
        public int id_daftarkegiatan { get; set; }
        public int id_kegiatan { get; set; }
        public string nim { get; set; }
        public string? deskripsi_penolakan { get; set; }
        [Required(ErrorMessage = "Catatan Wajib Diisi")]
        public string? catatan { get; set; }
        public string status { get; set; }
    }
 
}
