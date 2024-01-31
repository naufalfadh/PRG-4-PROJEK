using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using PRG_4_PROJEK.Models;

namespace PRG_4_PROJEK.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly Karyawan _karyawanRepository;
        public LoginController(IConfiguration configuration)
        {
            _karyawanRepository = new Karyawan(configuration);
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
        public IActionResult Login(string npk, string password)
        {
            if (string.IsNullOrEmpty(npk) || string.IsNullOrEmpty(password))
            {
                string errorMessage = "Username dan password harus diisi.";

                // Modifikasi menggunakan swal.fire
                string script = "<script type='text/javascript'>" +
                                "Swal.fire({" +
                                "  icon: 'error'," +
                                "  title: 'Error!'," +
                                "  text: '" + errorMessage + "'," +
                                "})" +
                                "</script>";

                TempData["ErrorMessage"] = errorMessage;
                TempData["Script"] = script;
                return RedirectToAction("Index", "Login");
            }
            KaryawanModel karyawanModel = _karyawanRepository.getDataByUsernamePassword(npk, password);
            if (karyawanModel == null || karyawanModel.status == "tidak aktif")
            {
                string errorMessage = "Username dan password salah.";

                // Modifikasi menggunakan swal.fire
                string script = "<script type='text/javascript'>" +
                                "Swal.fire({" +
                                "  icon: 'error'," +
                                "  title: 'Error!'," +
                                "  text: '" + errorMessage + "'," +
                                "})" +
                                "</script>";

                TempData["ErrorMessage"] = errorMessage;
                TempData["Script"] = script;
                return RedirectToAction("Index", "Login");
            }

            // Jika berhasil, atur sesi
            string serializedModel = JsonConvert.SerializeObject(karyawanModel);
            HttpContext.Session.SetString("Identity", serializedModel);
            HttpContext.Session.SetString("Id", karyawanModel.npk);
            HttpContext.Session.SetString("Nama", karyawanModel.nama);
            HttpContext.Session.SetString("Role", karyawanModel.role); // Simpan peran dalam sesi

            // Tambahkan script untuk menampilkan SweetAlert
            string successMessage = "Login berhasil!"; // Pesan sukses
            string successScript = "<script type='text/javascript'>" +
                                  "Swal.fire({" +
                                  "  icon: 'success'," +
                                  "  title: 'Success!'," +
                                  "  text: '" + successMessage + "'," +
                                  "}).then(function(){" +
                                  "  window.location.href = '/Dashboard';" + // Redirect ke halaman Dashboard setelah menutup SweetAlert
                                  "});" +
                                  "</script>";

            TempData["SuccessMessage"] = successMessage;
            TempData["Script"] = successScript;

            return RedirectToAction("Index", "Login");


        }


        public IActionResult Logout()
        {
            // Hapus token otentikasi di sisi klien
            HttpContext.SignOutAsync();

            // Redirect pengguna ke halaman login
            return RedirectToAction("Index", "Login");
        }
    }
}