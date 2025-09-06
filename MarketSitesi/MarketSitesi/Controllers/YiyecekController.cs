using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketSitesi.Repositories;
using MarketSitesi.Models;

namespace MarketSitesi.Controllers
{
    public class YiyecekController : Controller
    {
        private readonly IYiyecekRepository _yiyecekRepository;
        private readonly IKategoriRepository _kategoriRepository;

        public YiyecekController(IYiyecekRepository yiyecekRepository, IKategoriRepository kategoriRepository)
        {
            _yiyecekRepository = yiyecekRepository;
            _kategoriRepository = kategoriRepository;
        }

        // GET: Yiyecek
        public async Task<IActionResult> Index()
        {
            var yiyecekler = await _yiyecekRepository.GetYiyeceklerWithKategoriAsync();
            return View(yiyecekler);
        }

        // GET: Yiyecek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yiyecek = await _yiyecekRepository.GetYiyecekWithKategoriAsync(id.Value);
            if (yiyecek == null)
            {
                return NotFound();
            }

            return View(yiyecek);
        }

        // GET: Yiyecek/Create
        public async Task<IActionResult> Create()
        {
            ViewData["KategoriId"] = new SelectList(await _kategoriRepository.GetAllAsync(), "Id", "Ad");
            return View();
        }

        // POST: Yiyecek/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,Aciklama,Fiyat,StokAdedi,KategoriId")] Yiyecek yiyecek)
        {
            if (ModelState.IsValid)
            {
                await _yiyecekRepository.AddAsync(yiyecek);
                TempData["SuccessMessage"] = "Yiyecek başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(await _kategoriRepository.GetAllAsync(), "Id", "Ad", yiyecek.KategoriId);
            return View(yiyecek);
        }

        // GET: Yiyecek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yiyecek = await _yiyecekRepository.GetByIdAsync(id.Value);
            if (yiyecek == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(await _kategoriRepository.GetAllAsync(), "Id", "Ad", yiyecek.KategoriId);
            return View(yiyecek);
        }

        // POST: Yiyecek/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Aciklama,Fiyat,StokAdedi,KategoriId")] Yiyecek yiyecek)
        {
            if (id != yiyecek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _yiyecekRepository.UpdateAsync(yiyecek);
                    TempData["SuccessMessage"] = "Yiyecek başarıyla güncellendi.";
                }
                catch (Exception)
                {
                    if (!await _yiyecekRepository.ExistsAsync(yiyecek.Id))
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
            ViewData["KategoriId"] = new SelectList(await _kategoriRepository.GetAllAsync(), "Id", "Ad", yiyecek.KategoriId);
            return View(yiyecek);
        }

        // GET: Yiyecek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yiyecek = await _yiyecekRepository.GetYiyecekWithKategoriAsync(id.Value);
            if (yiyecek == null)
            {
                return NotFound();
            }

            return View(yiyecek);
        }

        // POST: Yiyecek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _yiyecekRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Yiyecek başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bu yiyeceği silemezsiniz çünkü içinde içecekler bulunmaktadır.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
