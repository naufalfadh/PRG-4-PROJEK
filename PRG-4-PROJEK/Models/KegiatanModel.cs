using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRG_4_PROJEK.Models
{
    public class KegiatanModel : IValidatableObject
    {
        public int id_kegiatan { get; set; }

        [Required(ErrorMessage = "Deskripsi harus diisi.")]
        public string deskripsi { get; set; }

        [Required(ErrorMessage = "Kapasitas harus diisi.")]
        public int? kapasitas { get; set; }

        [Required(ErrorMessage = "Tanggal Mulai harus diisi.")]
        [Display(Name = "Tanggal Mulai")]
        [DataType(DataType.Date)]
        public DateTime? tglmulai { get; set; }

        [Required(ErrorMessage = "Tanggal Selesai harus diisi.")]
        [Display(Name = "Tanggal Selesai")]
        [DataType(DataType.Date)]
        public DateTime? tglselesai { get; set; }

        public string status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (tglmulai != null && tglmulai < DateTime.Today)
            {
                yield return new ValidationResult("Tanggal Mulai tidak boleh kurang dari hari ini.", new[] { nameof(tglmulai) });
            }

            if (tglselesai != null && tglselesai < tglmulai)
            {
                yield return new ValidationResult("Tanggal Selesai harus setidaknya sama atau setelah Tanggal Mulai.", new[] { nameof(tglselesai) });
            }
        }
    }
}
