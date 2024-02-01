using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Karyawan _karyawanRepository;
        private readonly Mahasiswa _mahasiswaRepository;
        private readonly Kegiatan _kegiatanRepository;
        private readonly Pengajuan _pengajuanRepository;

        public DashboardController(IConfiguration configuration)
        {
            _karyawanRepository = new Karyawan(configuration);
            _mahasiswaRepository = new Mahasiswa(configuration);
            _kegiatanRepository = new Kegiatan(configuration);
            _pengajuanRepository = new Pengajuan(configuration);
        }

        public IActionResult Index()
        {
            ViewBag.TotalKaryawan = _karyawanRepository.GetTotalKaryawan();
            ViewBag.TotalMahasiswa = _mahasiswaRepository.GetTotalMahasiswa();
            ViewBag.TotalKegiatan = _kegiatanRepository.GetTotalKegiatan();
            ViewBag.TotalPengajuan = _pengajuanRepository.GetTotalPengajuan();
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

    }
}
