using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODOL.Models
{
    public class ViewPem
    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("Pengguna")]
        public int? idPengguna { get; set; }

        
        public string? NamaPembimbing { get; set; }

        [Required]
        [ForeignKey("Perusahaan")]
        public int? idPerusahaan { get; set; }


        [Required]
        public string? Jabatan { get; set; }

        [Required]
        public string? EmailPembimbing { get; set; }

        public string? Status { get; set; }

        [ForeignKey("Pengguna")]
        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [ForeignKey("Pengguna")]
        public int? ModifBy { get; set; }

        public DateTime? ModifDate { get; set; }

    }
}
