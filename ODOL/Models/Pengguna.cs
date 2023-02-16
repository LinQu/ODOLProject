using System.ComponentModel.DataAnnotations;

namespace ODOL.Models
{
    public class Pengguna
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama harus diisi")]
        public string? Nama { get; set; }

        [Required(ErrorMessage = "Username harus diisi")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password harus diisi")]
        public string? Password { get; set; }

        [Required]
        public string? Role { get; set; }
        public string?  Status { get; set; }

        
        public int? CreateBy { get; set; }

        
        public DateTime? CreateDate { get; set; }

    
        public int? ModifBy { get; set; }

        
        public DateTime? ModifDate { get; set; }

        
    }
}
