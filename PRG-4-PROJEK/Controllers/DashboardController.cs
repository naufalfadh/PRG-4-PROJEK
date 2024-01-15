using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Karyawan _karyawanRepository;

        public DashboardController(IConfiguration configuration)
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

    }
}
