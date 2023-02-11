using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ODOL.Models
{
    public class Penilaian
    {
        [Key]
        public int idPenilaian { get; set; }
        [ForeignKey("Pembimbing")]
        [Required]
        public int idPembimbing { get; set; }
        [ForeignKey("Mahasiswa")]
        [Required]
        
        public string? NIM { get; set; }
        [Required]

        public string? Periode { get; set; }
        [Required]
        
        public int? PengetahuanKerja { get; set; }
        [Required]

        public int? KualitasKertja { get; set; }
        [Required]
        
        public int? KecepatanKerja { get; set; }
        [Required]
        
        public int? SikapPerilaku { get; set; }
        [Required]
        public int? KreatifitasKerjaSama { get; set; }
        [Required]
        public int? Leadership { get; set; }
        [Required]
        public int? Beradaptasi { get; set; }
        [Required]
        public int? PenangananMasalah { get; set; }
        [Required]
        public string? Ulasan { get; set; }
        [ForeignKey("Pengguna")]
        public int? CreateadBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;


    }
}
