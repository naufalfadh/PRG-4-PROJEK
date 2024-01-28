using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class DaftarKegiatanController : Controller
    {
        private readonly DaftarKegiatan _daftarkegiatanRepository;

        public DaftarKegiatanController(IConfiguration configuration)
        {
            _daftarkegiatanRepository = new DaftarKegiatan(configuration);
        }

        public IActionResult Index()
        {
            ViewBag.kegiatanList = _daftarkegiatanRepository.getAllDatakegiatan();
            DaftarKegiatanModel daftarkegiatanModel = new DaftarKegiatanModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "LoginMahasiswa");
            }
            else
            {
                daftarkegiatanModel = JsonConvert.DeserializeObject<DaftarKegiatanModel>(serializedModel);
            }

            return View(_daftarkegiatanRepository.getAllData());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.kegiatanList = _daftarkegiatanRepository.getAllDatakegiatan();
            DaftarKegiatanModel daftarkegiatanModel = new DaftarKegiatanModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "LoginMahasiswa");
            }
            else
            {
                daftarkegiatanModel = JsonConvert.DeserializeObject<DaftarKegiatanModel>(serializedModel);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(DaftarKegiatanModel daftarkegiatanModel)
        {
         
            if (ModelState.IsValid)
            {
                _daftarkegiatanRepository.insertData(daftarkegiatanModel);
                TempData["SuccessMessage"] = "Data berhasil ditambahkan";
                return RedirectToAction("Index");
            }
            return View(daftarkegiatanModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Membuat instance dari model KegiatanModel
            DaftarKegiatanModel daftarkegiatanModel = new DaftarKegiatanModel();



            // Menghapus data berdasarkan ID
            var response = new { success = false, message = "Gagal menghapus data." };
            try
            {
                if (id != null)
                {
                    _daftarkegiatanRepository.deleteData(id);
                    response = new { success = true, message = "Berhasil menghapus data." };
                }
                else
                {
                    response = new { success = false, message = "Kegiatan tidak ditemukan" };
                }
            }
            catch (Exception ex)
            {
                response = new { success = false, message = ex.Message };
            }

            // Langsung redirect ke dashboard tanpa memeriksa role
            return Json(response);
        }
    }
}
