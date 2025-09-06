using Microsoft.AspNetCore.Mvc;
using MarketSitesi.Repositories;
using MarketSitesi.Models;

namespace MarketSitesi.Controllers
{
    public class KategoriController : Controller
    {
        private readonly IKategoriRepository _kategoriRepository;

        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        // GET: Kategori
        public async Task<IActionResult> Index()
        {
            var kategoriler = await _kategoriRepository.GetKategorilerWithYiyeceklerAsync();
            return View(kategoriler);
        }

        // GET: Kategori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _kategoriRepository.GetKategoriWithYiyeceklerAsync(id.Value);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Kategori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategori/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,Aciklama")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                await _kategoriRepository.AddAsync(kategori);
                TempData["SuccessMessage"] = "Kategori başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _kategoriRepository.GetByIdAsync(id.Value);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Aciklama")] Kategori kategori)
        {
            if (id != kategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _kategoriRepository.UpdateAsync(kategori);
                    TempData["SuccessMessage"] = "Kategori başarıyla güncellendi.";
                }
                catch (Exception)
                {
                    if (!await _kategoriRepository.ExistsAsync(kategori.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _kategoriRepository.GetKategoriWithYiyeceklerAsync(id.Value);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _kategoriRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bu kategoriyi silemezsiniz çünkü içinde yiyecekler bulunmaktadır.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
