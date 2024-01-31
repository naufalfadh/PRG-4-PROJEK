using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class MahasiswaModel
    {
        [Required(ErrorMessage = "NIM harus diisi.")]
        public string nim { get; set; }
        [Required(ErrorMessage = "Nama harus diisi.")]
        public string nama { get; set; }
        [Required(ErrorMessage = "RFID harus diisi.")]
        public string rfid { get; set; }
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "PIN harus terdiri dari 6 angka.")]
        [Required(ErrorMessage = "PIN harus diisi.")]
        public string pin { get; set; }
        [Required(ErrorMessage = "Jenis Kelamin harus diisi.")]
        public string jk { get; set; }
        public int? jp { get; set; }
        public int? jm { get; set; }
        public string? softskil { get; set; }
        public string status { get; set; }
       
    }

    public class mhsLap
    {
        public string deskripsi { get; set; }
        public int jam_plus { get; set; }
        public int jam_minus { get; set; }
    }
   
}
