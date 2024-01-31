using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using PRG_4_PROJEK.Models;

namespace PRG_4_PROJEK.Controllers
{
    public class PinMahasiswaController : Controller
    {
        private readonly ILogger<PinMahasiswaController> _logger;
        private readonly Mahasiswa _mahasiswaRepository;
        public PinMahasiswaController(IConfiguration configuration)
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
        public IActionResult PinMahasiswa(string rfid, string pin)
        {
            MahasiswaModel mahasiswaModel = _mahasiswaRepository.getDataByPassword(rfid, pin);

            if (mahasiswaModel == null)
            {
                TempData["ErrorMessage"] = "PIN salah.";

                // Menampilkan SweetAlert dengan pesan kesalahan
                string errorMessage = TempData["ErrorMessage"] as string;
                string script = "<script type='text/javascript'>" +
                                "Swal.fire({" +
                                "  icon: 'error'," +
                                "  title: 'Error!'," +
                                "  text: '" + errorMessage + "'," +
                                "}).then(function(){" +
                                "  window.location.href = '/PinMahasiswa';" + // Redirect ke halaman PinMahasiswa setelah menutup SweetAlert
                                "});" +
                                "</script>";

                TempData["Script"] = script;
                return RedirectToAction("Index", "PinMahasiswa");
            }

            // Sesuaikan dengan logika bisnis Anda untuk login Mahasiswa

            // Jika login berhasil, atur sesi dan arahkan ke halaman DashboardMahasiswa
            string serializedModel = JsonConvert.SerializeObject(mahasiswaModel);
            HttpContext.Session.SetString("Identity", serializedModel);
            HttpContext.Session.SetString("rfid", mahasiswaModel.rfid);
            HttpContext.Session.SetString("Id", mahasiswaModel.pin);
            HttpContext.Session.SetString("Nama", mahasiswaModel.nama);
            HttpContext.Session.SetString("JP", mahasiswaModel.jp.ToString()); // Convert to string
            HttpContext.Session.SetString("JM", mahasiswaModel.jm.ToString()); // Convert to string
            HttpContext.Session.SetString("Status", mahasiswaModel.status);

            // Menampilkan SweetAlert untuk login berhasil
            string successMessage = "Login berhasil!";
            string successScript = "<script type='text/javascript'>" +
                                   "Swal.fire({" +
                                   "  icon: 'success'," +
                                   "  title: 'Success!'," +
                                   "  text: '" + successMessage + "'," +
                                   "}).then(function(){" +
                                   "  window.location.href = '/DashboardMahasiswa';" + // Redirect ke halaman DashboardMahasiswa setelah menutup SweetAlert
                                   "});" +
                                   "</script>";

            TempData["SuccessMessage"] = successMessage;
            TempData["Script"] = successScript;
            return RedirectToAction("Index", "PinMahasiswa");
        }


        public IActionResult Logout()
        {
            // Hapus token otentikasi di sisi klien
            HttpContext.SignOutAsync();

            // Redirect pengguna ke halaman login
            return RedirectToAction("Index", "LoginMahasiswa");
        }
    }
}