using FAQModels;

namespace Usedcarapi.FaqData
{
    public interface IFAQData
    {
        public List<FAQModels.FAQModel> getAllQuestions();
        public FAQModel getQuestionById(int id);
    }
}