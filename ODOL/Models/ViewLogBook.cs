using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODOL.Models
{
    public class ViewLogBook
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Pembimbing")]
        public int? idPembimbing { get; set; }
        [ForeignKey("Mahasiswa")]
        public string? NIM { get; set; }

        public string? NamaPerusahaan { get; set; }
        public string? Cabang { get; set; }
        public string? Group { get; set; }
        public string? NamaMahasiswa { get; set; }
        public string? Prodi { get; set; }
        public string? Departemen { get; set; }
        public string? Kegiatan { get; set; }
        public string? RincianKegiatan { get; set; }
        public TimeSpan? JamStart { get; set; }
        public TimeSpan? JamEnd { get; set; }
        public DateTime? Tanggal { get; set; }
        public string? Ulasan { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        
    }
}
