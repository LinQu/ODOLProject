using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODOL.Models
{
    public class Mahasiswa
    {
        [Key]
        [Required(ErrorMessage = "NIM masih kosong")]
        public string? NIM { get; set; }
        [Required(ErrorMessage = "Perusahaan Harus Di isi")]
        [ForeignKey("Perusahaan")]
        public int? IdPerusahaan { get; set; }

        [Required(ErrorMessage = "Nama Mahasiswa masih kosong")]
        public string? NamaMahasiswa { get; set; }
        [Required(ErrorMessage = "Prodi masih kosong")]
        public string? Prodi { get; set; }

        public string? Status { get; set; }

        [ForeignKey("Pengguna")]
        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [ForeignKey("Pengguna")]
        public int? ModifBy { get; set; }

        public DateTime? ModifDate { get; set; }
    }
}
