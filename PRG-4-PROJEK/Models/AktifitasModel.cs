using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class AktifitasModel
    {
        [Required(ErrorMessage = "Mahasiswa Wajib Diisi")]
        public int id_aktifitas { get; set; }
        [Required(ErrorMessage = "Deskripsi Wajib Diisi")]
        public string deskripsi { get; set; }
        public string nim { get; set; }
        [Required(ErrorMessage = "Pilih Mahasiswa")]
        public string? nama { get; set; }
        public int? jp { get; set; }
        public int? jm { get; set; }

    }


}
