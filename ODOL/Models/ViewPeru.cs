using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODOL.Models
{
    public class ViewPeru
    {
        [Key]
        public int Id { get; set; }
        
        public int? idPengguna { get; set; }

        [Required]
        public string? NamaPerusahaan { get; set; }
        [Required]
        public string? EmailPerusahaan { get; set; }
        [Required]
        public string? AlamatPerusahaan { get; set; }
        [Required]
        public string? Cabang { get; set; }
        [Required]
        public string? Group { get; set; }

        public List<ViewPem>? DaftarPembimbing { get; set; }

        public List<Mahasiswa>? DaftarMahasiswa { get; set; }

        public string? Status { get; set; }
        [ForeignKey("Pengguna")]
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }

        [ForeignKey("Pengguna")]
        public int? ModifBy { get; set; }
        public DateTime? ModifDate { get; set; }
    }
}
