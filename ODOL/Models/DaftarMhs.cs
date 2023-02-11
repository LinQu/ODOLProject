using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODOL.Models
{
    public class DaftarMhs
    {
        public string? nim { get; set; }
        public string? nama { get; set; }
        public string? mhs_angkatan { get; set; }
        public string? prodi { get; set; }
        public string? konsentrasi { get; set; }
        public string? email { get; set; }
        public string? dosen_wali { get; set; }
    }
}
