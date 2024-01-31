using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class AktifitasController : Controller
    {
        private readonly Aktifitas _aktifitasRepository;

        public AktifitasController(IConfiguration configuration)
        {
            _aktifitasRepository = new Aktifitas(configuration);
        }

        public IActionResult Index()
        {
            ViewBag.mahasiswaList = _aktifitasRepository.getAllDatamahasiswa();
            AktifitasModel aktifitasModel = new AktifitasModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                aktifitasModel = JsonConvert.DeserializeObject<AktifitasModel>(serializedModel);
            }

            return View(_aktifitasRepository.getAllData());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.mahasiswaList = _aktifitasRepository.getAllDatamahasiswa();
            AktifitasModel aktifitasModel = new AktifitasModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                aktifitasModel = JsonConvert.DeserializeObject<AktifitasModel>(serializedModel);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(AktifitasModel aktifitasModel)
        {
            Console.WriteLine(aktifitasModel.jp);

            if (ModelState.IsValid)
            {
                if(aktifitasModel.jp != null && aktifitasModel.jm != null)
                {
                    TempData["ErrorMessage"] = "Pilih Salah Satu";
                    return RedirectToAction("Create","Aktifitas");
                }
                _aktifitasRepository.insertData(aktifitasModel);
                TempData["SuccessMessage"] = "Data berhasil ditambahkan";
                return RedirectToAction("Index");
            }
            return View(aktifitasModel);
        }

       

        [HttpPost]
        public IActionResult Delete(int id)
        {
            AktifitasModel aktifitasModel = new AktifitasModel();

            var response = new { success = false, message = "Gagal menghapus data." };
            try
            {
                if (id != null)
                {
                    _aktifitasRepository.deleteData(id);
                    response = new { success = true, message = "Berhasil menghapus data." };
                }
                else
                {
                    response = new { success = false, message = "Aktifitas tidak ditemukan" };
                }
            }
            catch (Exception ex)
            {
                response = new { success = false, message = ex.Message };
            }

            return Json(response);
        }

    }
}
