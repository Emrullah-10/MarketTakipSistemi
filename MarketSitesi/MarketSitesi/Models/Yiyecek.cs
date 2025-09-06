using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketSitesi.Models
{
    public class Yiyecek
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı gereklidir")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir")]
        [Display(Name = "Ürün Adı")]
        public string Ad { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        [Required(ErrorMessage = "Fiyat gereklidir")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Fiyat")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Fiyat { get; set; }

        [Required(ErrorMessage = "Stok adedi gereklidir")]
        [Display(Name = "Stok Adedi")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok adedi 0'dan küçük olamaz")]
        public int StokAdedi { get; set; }

        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }

        // Navigation Properties
        [ForeignKey("KategoriId")]
        public virtual Kategori Kategori { get; set; } = null!;
        public virtual ICollection<Icecek> Icecekler { get; set; } = new List<Icecek>();
    }
}
