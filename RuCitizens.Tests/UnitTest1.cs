using System;
using Xunit;
using RuCitizens.Controllers;
using RuCitizens.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RuCitizen.Tests
{
    [TestCaseOrderer("RuCitizen.Tests.PriorityOrderer", "RuCitizen.Tests")]
    public class UnitTest1
    {
        private CitizensController CreateController()
        {
            DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder = optionsBuilder.UseSqlServer("Server=localhost;Database=TestCitizens;Trusted_Connection=false;User id=sa;Password=123QWEasd");
            var databaseContext = new DatabaseContext(optionsBuilder.Options);
            return new CitizensController(null, databaseContext);
        }

        [Fact, TestPriority(1)]
        public void TestGet()
        {
            CitizensController citizensController = CreateController();
            Assert.NotNull(citizensController);
            var ret = citizensController.Get(1);
            Assert.True(ret.Id == 1);
        }

        [Fact, TestPriority(2)]
        public void TestPost()
        {
            CitizensController citizensController = CreateController();
            Assert.NotNull(citizensController);

            Citizen newCitizen = GetPostData();
            OkResult ret = citizensController.Post(newCitizen) as OkResult;
            Assert.Equal(200, ret.StatusCode);
        }

        [Fact, TestPriority(3)]
        public void TestSearchAndPut()
        {
            CitizensController citizensController = CreateController();
            Assert.NotNull(citizensController);

            Citizen newCitizen = GetPostData();
            Citizen searched = citizensController.Search(newCitizen.FullName, null, null).FirstOrDefault();
            Assert.NotNull(searched);
            Assert.Equal(newCitizen.FullName, searched.FullName);

            OkResult ret = citizensController.Put(searched.Id, searched) as OkResult;
            Assert.Equal(200, ret.StatusCode);
        }

        [Fact, TestPriority(4)]
        public void TestDelete()
        {
            CitizensController citizensController = CreateController();
            Assert.NotNull(citizensController);

            Citizen newCitizen = GetPostData();
            Citizen searched = citizensController.Search(newCitizen.FullName, null, null).FirstOrDefault();
            Assert.NotNull(searched);

            OkResult ret = citizensController.Delete(searched.Id) as OkResult;
            Assert.Equal(200, ret.StatusCode);
        }

        private Citizen GetPostData()
        {
            return new Citizen()
            {
                FullName = "Testing FullName",
                Inn = "012345678910"
            };
        }

    }
}
