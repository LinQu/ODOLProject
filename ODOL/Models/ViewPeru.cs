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
        public int? idPembimbing { get; set; }
        public string? NamaPembimbing { get; set; }
        public string? JabatanPembimbing { get; set; }
        public string? EmailPembimbing { get; set; }
        public string? NIM { get; set; }
        public string? NamaMahasiswa { get; set; }
        public string? Prodi { get; set; }
        public string? Status { get; set; }
        [ForeignKey("Pengguna")]
        public int CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }

        [ForeignKey("Pengguna")]
        public int ModifBy { get; set; }
        public DateTime? ModifDate { get; set; }
    }
}
