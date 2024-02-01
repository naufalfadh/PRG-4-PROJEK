using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRG_4_PROJEK.Models;

namespace PRG_4_PROJEK.Controllers
{
    public class PengajuanController : Controller
    {
        private readonly Pengajuan _pengajuanRepository;
        private readonly DaftarKegiatan _daftarkegiatanRepository;

        public PengajuanController(IConfiguration configuration)
        {
            _daftarkegiatanRepository = new DaftarKegiatan(configuration);
            _pengajuanRepository = new Pengajuan(configuration);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccJP(int id)
        {
            ViewBag.kegiatanList = _daftarkegiatanRepository.getAllDatakegiatan();
            PengajuanModel pengajuanModel = _pengajuanRepository.getData(id);
            if (pengajuanModel == null)
            {
                return NotFound();
            }
            return View(pengajuanModel);
        }

        [HttpPost]
        public IActionResult AccJP(PengajuanModel pengajuanModel)
        {
            pengajuanModel.status = "Diterima";
            pengajuanModel.tanggal_pengajuan = DateTime.Now;
            pengajuanModel.id_karyawan = HttpContext.Session.GetString("Id");
            Console.WriteLine(pengajuanModel.id_karyawan); 
            if (ModelState.IsValid)
            {
                _pengajuanRepository.AccJP(pengajuanModel);
                TempData["SuccessMessage"] = "Data berhasil ditambahkan";
                return RedirectToAction("IndexJP", "DaftarKegiatan");
            }

            return RedirectToAction("IndexJP", "DaftarKegiatan");
        }

        
    }
}
