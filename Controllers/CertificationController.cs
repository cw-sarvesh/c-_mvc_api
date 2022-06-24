using faqRepository;
using FAQModels;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

using UsedCar.Interface.CertificationReport;
namespace Certification.Controllers
{

    public class CertificationController : Controller
    {

        private ICertificationReport _data;

        public CertificationController(ICertificationReport certificationReport)
        {

            _data = certificationReport;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            return Ok(await _data.GetAllInfo());

        }


    }
}