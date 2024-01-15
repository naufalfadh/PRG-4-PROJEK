using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class MahasiswaModel
    {
        public string nim { get; set; }
        public string nama { get; set; }
        public string rfid { get; set; }
        public string pin { get; set; }
        public string jk { get; set; }
        public int? jp { get; set; }
        public int? jm { get; set; }
        public string? softskil { get; set; }
        public string status { get; set; }
    }
}
