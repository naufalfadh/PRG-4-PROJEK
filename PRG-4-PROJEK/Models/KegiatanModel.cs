using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class KegiatanModel
    {
        public int id_kegiatan { get; set; }
        public string deskripsi { get; set; }
        public int kapasitas { get; set; }
    }

}
