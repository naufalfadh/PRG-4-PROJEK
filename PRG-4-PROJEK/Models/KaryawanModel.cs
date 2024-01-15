using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class KaryawanModel
    {
        public string npk { get; set; }
        public string nama { get; set; }
        public string password { get; set; }
        public string jk { get; set; }
        public string role { get; set; }
        public string status { get; set; }
    }

}
