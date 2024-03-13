using backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class MergeServiceTest
    {
        [TestMethod]
        [DynamicData(nameof(AdditionData))]
        public void GetCreditData(string ssn, bool shouldPass)
        {

            // Act
            var mergeService = MergeService.GetInstance();
            var task = mergeService.MergeCreditData(ssn);
            task.Wait();
            var creditData = task.Result;

            // assess

            if (shouldPass)
            {
                Assert.IsNotNull(creditData);
                Assert.IsFalse(String.IsNullOrEmpty(creditData.Last_name));
                Assert.IsFalse(String.IsNullOrEmpty(creditData.First_name));
                Assert.IsFalse(String.IsNullOrEmpty(creditData.Address));
                Assert.IsTrue(creditData.Assessed_income > 0);
            }
            else
            {
                Assert.IsNull(creditData);
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
