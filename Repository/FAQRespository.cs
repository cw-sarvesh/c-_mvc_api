using FAQModels;
using Usedcarapi.FaqData;
namespace faqRepository
{
    public class FAQRespository : IFAQData
    {
        public List<FAQModels.FAQModel> getAllQuestions()
        {
            return DataSource();
        }
        public FAQModel getQuestionById(int id)
        {
            Console.WriteLine(id);
            var x = DataSource().Where(x => x.ID == id).FirstOrDefault();
            if (x == null)
            {
                x = new FAQModel();
            }
            return x;
        }

        public List<FAQModel> DataSource()
        {
            return new List<FAQModel>(){

                new FAQModel() { ID = 1 , Question = "hello1", Answer = "Answer1Hwllo"},
                new FAQModel() { ID = 2 , Question = "hello2", Answer = "Answer2Hwllo"},
                new FAQModel() { ID = 3 , Question = "hello3", Answer = "Answer3Hwllo"},
                new FAQModel() { ID = 4 , Question = "hello4", Answer = "Answer4Hwllo"},
                new FAQModel() { ID = 5 , Question = "hello5", Answer = "Answer5Hwllo"},

    };
        }

    }
}

