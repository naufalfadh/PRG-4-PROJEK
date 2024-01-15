using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class KegiatanController : Controller
    {
        private readonly Kegiatan _kegiatanRepository;

        public KegiatanController(IConfiguration configuration)
        {
            _kegiatanRepository = new Kegiatan(configuration);
        }

        public IActionResult Index()
        {
            KegiatanModel kegiatanModel = new KegiatanModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                kegiatanModel = JsonConvert.DeserializeObject<KegiatanModel>(serializedModel);
            }

            return View(_kegiatanRepository.getAllData());
        }

        [HttpGet]
        public IActionResult Create()
        {
            KegiatanModel kegiatanModel = new KegiatanModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                kegiatanModel = JsonConvert.DeserializeObject<KegiatanModel>(serializedModel);
            }
            /*if (kegiatanModel.jk == "admin")
            {
                return RedirectToAction("Index", "Pasien");
            }*/
            return View();
        }

        [HttpPost]
        public IActionResult Create(KegiatanModel kegiatanModel)
        {

            if (ModelState.IsValid)
            {
                _kegiatanRepository.insertData(kegiatanModel);
                TempData["SuccessMessage"] = "Data berhasil ditambahkan";
                return RedirectToAction("Index");
            }
            return View(kegiatanModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Mengambil data KegiatanModel dari repository berdasarkan ID
            KegiatanModel kegiatanModel = _kegiatanRepository.getData(id);

            // Memeriksa apakah data KegiatanModel ditemukan
            if (kegiatanModel == null)
            {
                // Jika tidak ditemukan, kembalikan response dengan status NotFound
                return NotFound();
            }

            // Langsung redirect ke dashboard tanpa memeriksa role
            return View(kegiatanModel);
        }



        [HttpPost]
        public IActionResult Edit(KegiatanModel kegiatanModel)
        {

            if (ModelState.IsValid)
            {
                KegiatanModel newKegiatanModel = _kegiatanRepository.getData(kegiatanModel.id_kegiatan);
                if (newKegiatanModel == null)
                {
                    return NotFound();
                }

                newKegiatanModel.id_kegiatan = kegiatanModel.id_kegiatan;
                newKegiatanModel.deskripsi = kegiatanModel.deskripsi;
                newKegiatanModel.kapasitas = kegiatanModel.kapasitas;

                _kegiatanRepository.updateData(newKegiatanModel);
                TempData["SuccessMessage"] = "data berhasil diupdate.";
                return RedirectToAction("Index");
            }
            return View(kegiatanModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Membuat instance dari model KegiatanModel
            KegiatanModel kegiatanModel = new KegiatanModel();

        

            // Menghapus data berdasarkan ID
            var response = new { success = false, message = "Gagal menghapus data." };
            try
            {
                if (id != null)
                {
                    _kegiatanRepository.deleteData(id);
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
