using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class AktifitasModel
    {
        public int id_aktifitas { get; set; }
        public string deskripsi { get; set; }
        public string nim { get; set; }
        public string nama { get; set; }
        public int? jp { get; set; }
        public int? jm { get; set; }

    }


}
