using FAQModels;
using Usedcarapi.FaqData;
using UsedCar.Interface.CertificationReport;
using UsedCar.Models.CertificationModel;
using AEPLCore.DataAccess;
using AEPLCore.DataAccess.Contracts;
using Microsoft.Extensions.Options;
using Dapper;
namespace faqRepository

{
    public class CertificationRepository : ICertificationReport


    {
        private readonly ConnectionFactory<IOptions<ConnectionStringOption>> _connectionFactory;

        public CertificationRepository(ConnectionFactory<IOptions<ConnectionStringOption>> connectionFactory)
        {
            _connectionFactory = connectionFactory;

        }

        public async Task<List<CertificationModel>> GetAllInfo()
        {
            using var con = await _connectionFactory.GetReadOnlyConnection();
            int rowCount = await con.Connection.QueryFirstOrDefaultAsync<int>("select id from scoreitems where id = 1");

            return getCertificationData(rowCount);
        }


        public List<CertificationModel> getCertificationData(int count)
        {
            return new List<CertificationModel>(){


                    new CertificationModel(){
                        Component = "Engine",Condition="Excellent",Rating=count,PropertyCondition= new Dictionary<string, List<string>> {{"Silencer",new List<string>{"Exhaust is in perfect condition","Perfect Condition"}},}

                    },
                      new CertificationModel(){
                        Component = "Engine",Condition="Excellent",Rating=5,PropertyCondition= new Dictionary<string, List<string>> {{"Silencer",new List<string>{"Exhaust is in perfect condition","Perfect Condition"}},}

                    } , new CertificationModel(){
                        Component = "Engine",Condition="Excellent",Rating=5,PropertyCondition= new Dictionary<string, List<string>> {{"Silencer",new List<string>{"Exhaust is in perfect condition","Perfect Condition"}},}

                    } , new CertificationModel(){
                        Component = "Engine",Condition="Excellent",Rating=5,PropertyCondition= new Dictionary<string, List<string>> {{"Silencer",new List<string>{"Exhaust is in perfect condition","Perfect Condition"}},}

                    }  ,new CertificationModel(){
                        Component = "Engine",Condition="Excellent",Rating=5,PropertyCondition= new Dictionary<string, List<string>> {{"Silencer",new List<string>{"Exhaust is in perfect condition","Perfect Condition"}},}

                    }
    };
        }

    }
}

