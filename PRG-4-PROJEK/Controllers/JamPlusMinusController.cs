using PRG_4_PROJEK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRG_4_PROJEK.Controllers
{
    public class JamPlusMinusController : Controller
    {
        private readonly Mahasiswa _mahasiswaRepository;

        public JamPlusMinusController(IConfiguration configuration)
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

    }
}
