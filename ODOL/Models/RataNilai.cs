using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODOL.Models
{
    public class RataNilai
    {
        [Key]
        public int idPenilaian { get; set; }

        public string? NIM { get; set; }

        public string? Periode { get; set; }

        public double? Rata { get; set; }
    }
}
