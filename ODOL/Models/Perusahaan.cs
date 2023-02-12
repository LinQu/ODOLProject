using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ODOL.Models
{
    public class Perusahaan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Pengguna")]
        public int? idPengguna { get; set; }
        [Required]
        public string? EmailPerusahaan { get; set; }
        [Required]
        public string? AlamatPerusahaan { get; set; }

        public string? Cabang { get; set; }

        public string? Group { get; set; }

        
        public string? Status { get; set; }
        [ForeignKey("Pengguna")]
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }

        [ForeignKey("Pengguna")]
        public int? ModifBy { get; set; }
        public DateTime? ModifDate { get; set; }





    }
}
