using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using PRG_4_PROJEK.Models;

namespace PRG_4_PROJEK.Controllers
{
    public class LoginMahasiswaController : Controller
    {
        private readonly ILogger<LoginMahasiswaController> _logger;
        private readonly Mahasiswa _mahasiswaRepository;
        public LoginMahasiswaController(IConfiguration configuration)
        {
            _mahasiswaRepository = new Mahasiswa(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult LoginMahasiswa(string rfid)
        {

            MahasiswaModel mahasiswaModel = _mahasiswaRepository.getDataByUsername(rfid);

            if (mahasiswaModel == null)
            {
                TempData["ErrorMessage"] = "Mahasiswa belum terdaftar.";
                return RedirectToAction("Index", "LoginMahasiswa");
            }


            string serializedModel = JsonConvert.SerializeObject(mahasiswaModel);
            HttpContext.Session.SetString("Identity", serializedModel);
            HttpContext.Session.SetString("Id", mahasiswaModel.rfid);
            HttpContext.Session.SetString("Nama", mahasiswaModel.nama);
            HttpContext.Session.SetString("JP", mahasiswaModel.jp.ToString()); // Convert to string
            HttpContext.Session.SetString("JM", mahasiswaModel.jm.ToString()); // Convert to string
            HttpContext.Session.SetString("Status", mahasiswaModel.status);

            // Arahkan ke halaman Dashboard
            return RedirectToAction("Index", "PinMahasiswa");
        }

        public IActionResult Logout()
        {
            // Hapus token otentikasi di sisi klien
            HttpContext.SignOutAsync();

            // Redirect pengguna ke halaman login
            return RedirectToAction("Index", "DashboardUtama");
        }
    }
}