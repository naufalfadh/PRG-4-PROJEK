using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    using System.ComponentModel.DataAnnotations;

    public class KegiatanModel
    {
        public int id_kegiatan { get; set; }

        [Required(ErrorMessage = "Deskripsi harus diisi.")]
        public string deskripsi { get; set; }

        [Required(ErrorMessage = "Kapasitas harus diisi.")]
        public int kapasitas { get; set; }

        [Required(ErrorMessage = "Tanggal Mulai harus diisi.")]
        [Display(Name = "Tanggal Mulai")]
        [DataType(DataType.Date)]
        public DateTime tglmulai { get; set; }

        [Required(ErrorMessage = "Tanggal Selesai harus diisi.")]
        [Display(Name = "Tanggal Selesai")]
        [DataType(DataType.Date)]
        public DateTime tglselesai { get; set; }

        public string status { get; set; }


    }


}
