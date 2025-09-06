using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketSitesi.Repositories;
using MarketSitesi.Models;

namespace MarketSitesi.Controllers
{
    public class IcecekController : Controller
    {
        private readonly IIcecekRepository _icecekRepository;
        private readonly IYiyecekRepository _yiyecekRepository;

        public IcecekController(IIcecekRepository icecekRepository, IYiyecekRepository yiyecekRepository)
        {
            _icecekRepository = icecekRepository;
            _yiyecekRepository = yiyecekRepository;
        }

        // GET: Icecek
        public async Task<IActionResult> Index()
        {
            var icecekler = await _icecekRepository.GetIceceklerWithYiyecekAsync();
            return View(icecekler);
        }

        // GET: Icecek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icecek = await _icecekRepository.GetIcecekWithYiyecekAsync(id.Value);
            if (icecek == null)
            {
                return NotFound();
            }

            return View(icecek);
        }

        // GET: Icecek/Create
        public async Task<IActionResult> Create()
        {
            ViewData["YiyecekId"] = new SelectList(await _yiyecekRepository.GetAllAsync(), "Id", "Ad");
            return View();
        }

        // POST: Icecek/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,Aciklama,Fiyat,StokAdedi,Hacim,YiyecekId")] Icecek icecek)
        {
            if (ModelState.IsValid)
            {
                await _icecekRepository.AddAsync(icecek);
                TempData["SuccessMessage"] = "İçecek başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["YiyecekId"] = new SelectList(await _yiyecekRepository.GetAllAsync(), "Id", "Ad", icecek.YiyecekId);
            return View(icecek);
        }

        // GET: Icecek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icecek = await _icecekRepository.GetByIdAsync(id.Value);
            if (icecek == null)
            {
                return NotFound();
            }
            ViewData["YiyecekId"] = new SelectList(await _yiyecekRepository.GetAllAsync(), "Id", "Ad", icecek.YiyecekId);
            return View(icecek);
        }

        // POST: Icecek/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Aciklama,Fiyat,StokAdedi,Hacim,YiyecekId")] Icecek icecek)
        {
            if (id != icecek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _icecekRepository.UpdateAsync(icecek);
                    TempData["SuccessMessage"] = "İçecek başarıyla güncellendi.";
                }
                catch (Exception)
                {
                    if (!await _icecekRepository.ExistsAsync(icecek.Id))
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
            ViewData["YiyecekId"] = new SelectList(await _yiyecekRepository.GetAllAsync(), "Id", "Ad", icecek.YiyecekId);
            return View(icecek);
        }

        // GET: Icecek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icecek = await _icecekRepository.GetIcecekWithYiyecekAsync(id.Value);
            if (icecek == null)
            {
                return NotFound();
            }

            return View(icecek);
        }

        // POST: Icecek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _icecekRepository.DeleteAsync(id);
            TempData["SuccessMessage"] = "İçecek başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
