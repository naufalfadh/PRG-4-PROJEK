using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class DashboardMahasiswaController : Controller
    {
        private readonly Mahasiswa _mahasiswaRepository;

        public DashboardMahasiswaController(IConfiguration configuration)
        {
            _mahasiswaRepository = new Mahasiswa(configuration);
        }

        public IActionResult Index()
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();

            string serializedModel = HttpContext.Session.GetString("Identity");

            if (serializedModel == null)
            {
                return RedirectToAction("Index", "LoginMahasiswa");
            }
            else
            {
                mahasiswaModel = JsonConvert.DeserializeObject<MahasiswaModel>(serializedModel);
            }

            return View(_mahasiswaRepository.getAllData());
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            List<mhsLap> MahasiswaModel = _mahasiswaRepository.getDetail(id);
            Console.WriteLine("jumlah : " + MahasiswaModel.Count);
            Console.WriteLine("id : " + id);
            if (MahasiswaModel == null)
            {
                return NotFound();
            }
            return View(MahasiswaModel);
        }

    }
}
