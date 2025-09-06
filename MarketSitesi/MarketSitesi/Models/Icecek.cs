using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketSitesi.Models
{
    public class Icecek
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İçecek adı gereklidir")]
        [StringLength(100, ErrorMessage = "İçecek adı en fazla 100 karakter olabilir")]
        [Display(Name = "İçecek Adı")]
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

        [Display(Name = "Hacim (ml)")]
        [Range(1, int.MaxValue, ErrorMessage = "Hacim 1'den büyük olmalıdır")]
        public int Hacim { get; set; }

        [Display(Name = "Yiyecek")]
        public int YiyecekId { get; set; }

        // Navigation Properties
        [ForeignKey("YiyecekId")]
        public virtual Yiyecek Yiyecek { get; set; } = null!;
    }
}
