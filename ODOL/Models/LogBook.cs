using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ODOL.Models
{
    public class LogBook
    {
        [Key]
        public int idLogBook { get; set; }
        [Required]
        [ForeignKey("Pembimbing")]
        public int? idPembimbing { get; set; }
        [ForeignKey("Mahasiswa")]
        public string? NIM { get; set; }

        [Required]
        public string? Departemen { get; set; }
        [Required]
        public string? Kegiatan { get; set; }
        [Required]
        public string? RincianKegiatan { get; set; }
        [Required]
        public TimeSpan? JamStart { get; set; }
        [Required]
        public TimeSpan? JamEnd { get; set; }
        public DateTime? Tanggal { get; set; } = DateTime.Now;
        public string? Ulasan { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;



    }
}
