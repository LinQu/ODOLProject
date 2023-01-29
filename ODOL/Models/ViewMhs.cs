using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ODOL.Models
{
    public class ViewMhs
    {
        [Key]
        public string? NIM { get; set; }
        [ForeignKey("Perusahaan")]
        public int? IdPerusahaan { get; set; }
        
        public string? NamaPerusahaan { get; set; }
        public string? Cabang { get; set; }
        public string? Group { get; set; }

        public string? NamaMahasiswa { get; set; }
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
