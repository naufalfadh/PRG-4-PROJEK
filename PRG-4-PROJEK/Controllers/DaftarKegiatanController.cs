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

        public IActionResult IndexP()
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

            return View(_daftarkegiatanRepository.getAllDataP());
        }

        public IActionResult IndexJP()
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

            return View(_daftarkegiatanRepository.getAllDataJP());
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

        public IActionResult CreateP()
        {
            ViewBag.kegiatanList = _daftarkegiatanRepository.getAllDatakegiatan();
            DaftarKegiatanModel daftarkegiatanModel = new DaftarKegiatanModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                daftarkegiatanModel = JsonConvert.DeserializeObject<DaftarKegiatanModel>(serializedModel);
            }

            return View(_daftarkegiatanRepository.getAllDataJP());
        }

        [HttpPost]
        public IActionResult Create(DaftarKegiatanModel daftarkegiatanModel)
        {
             ViewBag.kegiatanList = _daftarkegiatanRepository.getAllDatakegiatan();
            if (_daftarkegiatanRepository.CheckIfAlreadyRegistered(daftarkegiatanModel.id_kegiatan))
            {
                ModelState.AddModelError("", "Kegiatan tersebut sudah terdaftar.");
                TempData["ErrorMessage"] = "Kegiatan tersebut sudah terdaftar.";
                return View("Create", daftarkegiatanModel);

            }

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

        [HttpPost]
        public IActionResult AccPend(int id)
        {
            // Membuat instance dari model KegiatanModel
            DaftarKegiatanModel daftarkegiatanModel = new DaftarKegiatanModel();



            // Menghapus data berdasarkan ID
            var response = new { success = false, message = "Gagal menghapus data." };
            try
            {
                if (id != null)
                {
                    _daftarkegiatanRepository.AccPend(id);
                    response = new { success = true, message = "Berhasil menerima pendaftaran data." };
                }
                else
                {
                    response = new { success = false, message = "pendaftaran tidak ditemukan" };
                }
            }
            catch (Exception ex)
            {
                response = new { success = false, message = ex.Message };
            }

            // Langsung redirect ke dashboard tanpa memeriksa role
            return Json(response);
        }

        [HttpPost]
        public IActionResult PengajuanJP(int id)
        {
            // Membuat instance dari model KegiatanModel
            DaftarKegiatanModel daftarkegiatanModel = new DaftarKegiatanModel();



            // Menghapus data berdasarkan ID
            var response = new { success = false, message = "Gagal menghapus data." };
            try
            {
                if (id != null)
                {
                    _daftarkegiatanRepository.PengajuanJP(id);
                    response = new { success = true, message = "Berhasil mengirim form pengajuan data." };
                }
                else
                {
                    response = new { success = false, message = "pengajuan tidak ditemukan" };
                }
            }
            catch (Exception ex)
            {
                response = new { success = false, message = ex.Message };
            }

            // Langsung redirect ke dashboard tanpa memeriksa role
            return Json(response);
        }

        [HttpPost]
        public IActionResult BatalJP(int id, string deskripsi_penolakan)
        {
            DaftarKegiatanModel daftarkegiatanModel = new DaftarKegiatanModel();


            Console.WriteLine("id: "+id);
            Console.WriteLine("desc: " + deskripsi_penolakan);
            // Menghapus data berdasarkan ID
            var response = new { success = false, message = "Gagal melakukan penolakan." };
            try
            {
                if (id != null)
                {
                    _daftarkegiatanRepository.TolakJP(id,deskripsi_penolakan);
                    response = new { success = true, message = "Penolakan berhasil dilakukan." };
                }
                else
                {
                    response = new { success = false, message = "pendaftaran tidak ditemukan" };
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
