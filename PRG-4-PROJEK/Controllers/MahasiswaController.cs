using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class MahasiswaController : Controller
    {
        private readonly Mahasiswa _mahasiswaRepository;

        public MahasiswaController(IConfiguration configuration)
        {
            _mahasiswaRepository = new Mahasiswa(configuration);
        }

        public IActionResult Index()
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                mahasiswaModel = JsonConvert.DeserializeObject<MahasiswaModel>(serializedModel);
            }

            return View(_mahasiswaRepository.getAllData());
        }

        [HttpGet]
        public IActionResult Create()
        {
            MahasiswaModel  mahasiswaModel = new MahasiswaModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                mahasiswaModel = JsonConvert.DeserializeObject<MahasiswaModel>(serializedModel);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(MahasiswaModel mahasiswaModel)
        {
            if (_mahasiswaRepository.IsNIMAlreadyExists(mahasiswaModel.nim))
            {
                ModelState.AddModelError("NIM", "NIM sudah terdaftar.");
            }

            if (_mahasiswaRepository.IsRFIDAlreadyExists(mahasiswaModel.rfid))
            {
                ModelState.AddModelError("RFID", "RFID sudah terdaftar.");
            }

            if (ModelState.IsValid)
            {
                _mahasiswaRepository.insertData(mahasiswaModel);
                TempData["SuccessMessage"] = "Data berhasil ditambahkan";
                return RedirectToAction("Index");
            }

            return View(mahasiswaModel);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();


            MahasiswaModel MahasiswaModel = _mahasiswaRepository.getData(id);
            if (MahasiswaModel == null)
            {
                return NotFound();
            }
            return View(MahasiswaModel);
        }

        [HttpPost]
        public IActionResult Edit(MahasiswaModel mahasiswaModel)
        {
            Console.WriteLine("test : "+mahasiswaModel.ToString());

            if (ModelState.IsValid)
            {
                MahasiswaModel newMahasiswaModel = _mahasiswaRepository.getData(mahasiswaModel.nim);
                if (newMahasiswaModel == null)
                {
                    return NotFound();
                }

                newMahasiswaModel.nim  = mahasiswaModel.nim;
                newMahasiswaModel.nama = mahasiswaModel.nama;
                newMahasiswaModel.rfid = mahasiswaModel.rfid;
                newMahasiswaModel.jk = mahasiswaModel.jk;
                newMahasiswaModel.pin = mahasiswaModel.pin;
                newMahasiswaModel.status = mahasiswaModel.status;

                _mahasiswaRepository.updateData(newMahasiswaModel);
                TempData["SuccessMessage"] = "Mahasiswa berhasil diupdate.";
                return RedirectToAction("Index");
            }
            return View(mahasiswaModel);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();

            var response = new { success = false, message = "Gagal menghapus data." };
            try
            {
                if (id != null)
                {
                    _mahasiswaRepository.deleteData(id);
                    response = new { success = true, message = "Berhasil menghapus data." };
                }
                else
                {
                    response = new { success = false, message = "Mahasiswa tidak ditemukan" };
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
