using faqRepository;
using FAQModels;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using UsedCar.Models.CertificationModel;
namespace FAQ.Controllers
{
    using Usedcarapi.FaqData;
    public class FAQController : Controller
    {

        private IFAQData _data;

        public FAQController(IFAQData faqdata)
        {
            _data = faqdata;
        }

        [HttpGet]
        public IActionResult GetAllFAQS()
        {
            return Ok(_data.getAllQuestions());

        }
        [HttpGet]
        public IActionResult GetFAQ(int id)
        {
            var faq_data = _data.getQuestionById(id);
            if (faq_data != null)
            {
                return Ok(_data.getQuestionById(id));
            }
            return NotFound("put valid id");



        }
    }
}