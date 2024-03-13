using backend.DataServices;


namespace Test
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        [DynamicData(nameof(AdditionData))]
        public void PersonalDetails(string ssn, bool shouldPass)
        {
            try
            {
                //Act

                var dataService = new CreditDataDataService(new System.Net.Http.HttpClient());
                var task = dataService.PersonalDetailsAsync(ssn);
                task.Wait();
                var details = task.Result;
                //Assess

                Assert.AreEqual(true, shouldPass);
                Assert.IsNotNull(details);
                Assert.IsFalse(String.IsNullOrEmpty(details.Last_name));
                Assert.IsFalse(String.IsNullOrEmpty(details.First_name));
                Assert.IsFalse(String.IsNullOrEmpty(details.Address));

            }
            catch (AggregateException ex)
            {
                //Assess wrong ssn
                Assert.AreEqual(false, shouldPass);
                Assert.AreEqual(404, ((backend.DataServices.ApiException)ex.InnerException).StatusCode);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [TestMethod]
        [DynamicData(nameof(AdditionData))]
        public void AssessedIncome(string ssn, bool shouldPass)
        {
            try
            {
                //Act

                var dataService = new CreditDataDataService(new System.Net.Http.HttpClient());
                var task = dataService.AssessedIncomeAsync(ssn);
                task.Wait();
                var income = task.Result;

                //Assess right ssn

                Assert.AreEqual(true, shouldPass);
                Assert.IsNotNull(income);
                Assert.IsTrue(income.Assessed_income > 0);
            }
            catch (AggregateException ex)
            {
                //Assess wrong ssn
                Assert.AreEqual(false, shouldPass);
                Assert.AreEqual(404, ((backend.DataServices.ApiException)ex.InnerException).StatusCode);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [DynamicData(nameof(AdditionData))]
        public void Debt(string ssn, bool shouldPass)
        {
            try
            {
                //Act

                var dataService = new CreditDataDataService(new System.Net.Http.HttpClient());
                var task = dataService.DebtAsync(ssn);
                task.Wait();
                var debt = task.Result;

                //Assess right ssn

                Assert.AreEqual(true, shouldPass);
                Assert.IsNotNull(debt);
                Assert.IsNotNull(debt.Complaints);

            }
            catch (AggregateException ex)
            {
                //Assess wrong ssn
                Assert.AreEqual(false, shouldPass);
                Assert.AreEqual(404, ((backend.DataServices.ApiException)ex.InnerException).StatusCode);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        public static IEnumerable<object> AdditionData
        {
            get
            {
                return new[]
                {
                    new object[] {"424-11-9327", true }, //Emma
                    new object[] {"553-25-8346", true }, //Billy
                    new object[] {"287-54-7823", true }, //Gail
                    new object[] { "", false }
                };
            }
        }

    }
}