using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class KaryawanModel
    {
        [Required(ErrorMessage = "NPK harus diisi.")]
        public string npk { get; set; }

        [Required(ErrorMessage = "Nama harus diisi.")]
        public string nama { get; set; }

        [Required(ErrorMessage = "Password harus diisi.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Jenis kelamin harus dipilih.")]
        public string jk { get; set; }
        [Required(ErrorMessage = "Role harus dipilih.")]
        public string role { get; set; }
        public string status { get; set; }
    }

}
