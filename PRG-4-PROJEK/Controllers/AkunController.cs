using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class AkunController : Controller
    {
        private readonly Karyawan _karyawanRepository;

        public AkunController(IConfiguration configuration)
        {
            _karyawanRepository = new Karyawan(configuration);
        }

        public IActionResult Index()
        {
            KaryawanModel karyawanModel = new KaryawanModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                karyawanModel = JsonConvert.DeserializeObject<KaryawanModel>(serializedModel);
            }

            return View(_karyawanRepository.getAllData());
        }

        [HttpGet]
        public IActionResult Create()
        {
            KaryawanModel karyawanModel = new KaryawanModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                karyawanModel = JsonConvert.DeserializeObject<KaryawanModel>(serializedModel);
            }
            /*if (karyawanModel.jk == "admin")
            {
                return RedirectToAction("Index", "Pasien");
            }*/
            return View();
        }

        [HttpPost]
        public IActionResult Create(KaryawanModel karyawanModel)
        {

            if (_karyawanRepository.IsNPKAlreadyExists(karyawanModel.npk))
            {
                ModelState.AddModelError("NPK", "NPK sudah terdaftar.");
                return View("Create", karyawanModel); // Ganti dengan nama view Anda
            }

            if (ModelState.IsValid)
            {
                _karyawanRepository.insertData(karyawanModel);
                TempData["SuccessMessage"] = "Data berhasil ditambahkan";
                return RedirectToAction("Index");
            }
            return View(karyawanModel);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            KaryawanModel karyawanModel = new KaryawanModel();
            KaryawanModel KaryawanModel = _karyawanRepository.getData(id);
            if (KaryawanModel == null)
            {
                return NotFound();
            }
            return View(KaryawanModel);
        }

        [HttpPost]
        public IActionResult Edit(KaryawanModel karyawanModel)
        {

            if (ModelState.IsValid)
            {
                KaryawanModel newAkunModel = _karyawanRepository.getData(karyawanModel.npk);
                if (newAkunModel == null)
                {
                    return NotFound();
                }

                newAkunModel.npk = karyawanModel.npk;
                newAkunModel.nama = karyawanModel.nama;
                newAkunModel.password = karyawanModel.password;
                newAkunModel.jk = karyawanModel.jk;
                newAkunModel.role = karyawanModel.role;
                newAkunModel.status = karyawanModel.status;

                _karyawanRepository.updateData(newAkunModel);
                TempData["SuccessMessage"] = "data berhasil diupdate.";
                return RedirectToAction("Index");
            }
            return View(karyawanModel);
        }


        [HttpPost]
        public IActionResult Delete(string id)
        {
            KaryawanModel karyawanModel = new KaryawanModel();

            var response = new { success = false, message = "Gagal menghapus data." };
            try
            {
                if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int karyawanId))
                {
                    _karyawanRepository.deleteData(karyawanId);
                    response = new { success = true, message = "Berhasil menghapus data." };
                }
                else
                {
                    response = new { success = false, message = "Karyawan tidak ditemukan" };
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

