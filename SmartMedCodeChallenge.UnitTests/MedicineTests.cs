using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartMedCodeChallenge.Controllers.v1;
using SmartMedCodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartMedCodeChallenge.UnitTests
{
    [TestClass]
    public class MedicineTests
    {
        private MedicinesController _controller;
        private Medicine _medOne;
        private Medicine _medTwo;
        private Medicine _badMed;

        //private MedicineTests()
        //{
        //    var context = new MedicineContext(new DbContextOptions<MedicineContext>());

        //    _controller = new MedicinesController(context);
        //    _medOne = GetFirstMedicine();
        //    _medTwo = GetSecondMedicine();
        //    _badMed = GetInvalidMedicine();
        //}

        [TestInitialize]
        public void Initialise()
        {
            var options = new DbContextOptionsBuilder<MedicineContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
            var databaseContext = new MedicineContext(options);
            databaseContext.Database.EnsureCreated();

            //var context = databaseContext;

            _controller = new MedicinesController(databaseContext);
            _medOne = GetFirstMedicine();
            _medTwo = GetSecondMedicine();
            _badMed = GetInvalidMedicine();
        }

        private Medicine GetFirstMedicine()
        {
            return new Medicine
            {
                DateCreated = System.DateTime.Now,
                Name = "First Test Medicine",
                Quantity = 1
            };
        }
        private Medicine GetSecondMedicine()
        {
            return new Medicine
            {
                DateCreated = System.DateTime.Now,
                Name = "Second Test Medicine",
                Quantity = 2
            };
        }
        private Medicine GetInvalidMedicine()
        {
            return new Medicine
            {
                DateCreated = System.DateTime.Now,
                Name = "Bad Medicine",
                Quantity = 0
            };
        }

        [TestMethod]
        public async Task RunTests()
        {
            await AddMedicineOne();
            await GetMedicineOne();
            await AddMedicineTwo();
            await GetAllMedicines();
            await DeleteMedicineOne();
            await AddBadMedicine();
        }

        public async Task AddMedicineOne()
        {
            var result = await _controller.Post(_medOne);
            
            var viewResult = (CreatedAtActionResult)result.Result;
            var model = (Medicine)viewResult.Value;

            Assert.IsNotNull(model);
            Assert.AreEqual(_medOne, model);
        }

        public async Task GetMedicineOne()
        {
            var result = await _controller.Get(_medOne.Id);

            var viewResult = (CreatedAtActionResult)result.Result;
            var model = (Medicine)result.Value;

            Assert.IsNotNull(model);
            Assert.AreEqual(_medOne, result.Value);
        }

        public async Task AddMedicineTwo()
        {
            var result = await _controller.Post(_medTwo);

            var viewResult = (CreatedAtActionResult)result.Result;
            var model = (Medicine)viewResult.Value;

            Assert.IsNotNull(model);
            Assert.AreEqual(_medTwo, model);
        }

        public async Task GetAllMedicines()
        {
            var result = await _controller.GetAll();

            var success = result.Value.Contains(_medOne) && result.Value.Contains(_medTwo) ? true : false;

            Assert.AreEqual(true, success);
        }

        public async Task DeleteMedicineOne()
        {
            var result = (NoContentResult)await _controller.Delete(_medOne.Id);
            var statusCode = result.StatusCode;
            int expectedResult = 204;

            Assert.AreEqual(expectedResult, statusCode);
        }

        public async Task AddBadMedicine()
        {
            var result = await _controller.Post(_badMed);

            var viewResult = (BadRequestObjectResult)result.Result;
            var codeResult = (int)viewResult.StatusCode;

            Assert.AreEqual(400, codeResult);
        }
    }
}