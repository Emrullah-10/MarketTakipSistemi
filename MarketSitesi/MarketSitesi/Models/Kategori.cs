using System.ComponentModel.DataAnnotations;

namespace MarketSitesi.Models
{
    public class Kategori
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı gereklidir")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir")]
        [Display(Name = "Kategori Adı")]
        public string Ad { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        // Navigation Properties
        public virtual ICollection<Yiyecek> Yiyecekler { get; set; } = new List<Yiyecek>();
    }
}
