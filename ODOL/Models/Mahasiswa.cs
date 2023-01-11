using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODOL.Models
{
    public class Mahasiswa
    {
        [Key]
        public string NIM { get; set; }
        [ForeignKey("Perusahaan")]
        public int IdPerusahaan { get; set; }

        [Required]
        public string? NamaMahasiswa { get; set; }
        [Required]
        public string? Prodi { get; set; }

        public string? Status { get; set; }

        [ForeignKey("Pengguna")]
        public string? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [ForeignKey("Pengguna")]
        public string? ModifBy { get; set; }

        public DateTime? ModifDate { get; set; }
    }
}
