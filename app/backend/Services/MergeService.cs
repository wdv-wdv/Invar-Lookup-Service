using backend.DataServices;
using backend.Interfaces;
using backend.Models;
using System;
using System.Threading.Tasks;

namespace backend.Services
{
    public sealed class MergeService : IMergeService
    {
        //Singleton initialisation 
        private MergeService()
        {
            this.creditDataDataService = new CreditDataDataService(new System.Net.Http.HttpClient());
        }
        private readonly CreditDataDataService creditDataDataService;

        private static MergeService instance;

        public static MergeService GetInstance()
        {
            if (instance == null)
            {
                instance = new MergeService();
            }
            return instance;
        }

        //Methods

        public async Task<CreditData> MergeCreditData(string ssn)
        {
            try
            {
                var personalDataTask = creditDataDataService.PersonalDetailsAsync(ssn);
                var assessed_incomeTask = creditDataDataService.AssessedIncomeAsync(ssn);
                var debtTask = creditDataDataService.DebtAsync(ssn);

                var personalData = await personalDataTask;
                var assessed_income = await assessed_incomeTask;
                var debt = await debtTask;

                if (personalData != null & assessed_income != null & debt != null)
                {
                    return new CreditData()
                    {
                        First_name = personalData.First_name,
                        Last_name = personalData.Last_name,
                        Address = personalData.Address,
                        Assessed_income = assessed_income.Assessed_income,
                        Balance_of_debt = debt.Balance_of_debt,
                        Complaints = (bool)debt.Complaints
                    };

                }
                else
                {
                    return null;
                }
            }
            catch (DataServices.ApiException ex)
            {
                switch (ex.StatusCode) {
                    case 404: return null;
                    default: throw;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
