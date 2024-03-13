using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using LookupServiceAPI.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using backend.Models;

namespace Test
{
    [TestClass]
    public class ControllerTest
    {
        [TestMethod]
        [DynamicData(nameof(AdditionData))]
        public void GetAsyncCreditData(string ssn, bool shouldPass)
        {
            // Arrange
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Trace));
            ILogger<CreditDataController> logger = loggerFactory.CreateLogger<CreditDataController>();

            // Act
            var controller = new CreditDataController(logger);
            var task = controller.GetAsync(ssn);
            task.Wait();
            var result = task.Result;

            // assess

            if (shouldPass)
            {
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.AreEqual(200, okResult.StatusCode);
                var creditData = okResult.Value as CreditData;
                Assert.IsFalse(String.IsNullOrEmpty(creditData.Last_name));
                Assert.IsFalse(String.IsNullOrEmpty(creditData.First_name));
                Assert.IsFalse(String.IsNullOrEmpty(creditData.Address));
                Assert.IsTrue(creditData.Assessed_income > 0);
            }
            else
            {
                var notFoundResult = result as StatusCodeResult;
                Assert.IsNotNull(notFoundResult);
                Assert.AreEqual(404, notFoundResult.StatusCode);
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
