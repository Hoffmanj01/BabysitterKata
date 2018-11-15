using System;
using Xunit;
using BabysitterKata.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BabysitterKata.Tests
{
    public class InvoiceControllerShould
    {
        readonly InvoiceController _sut;

        public InvoiceControllerShould()
        {
            _sut = new InvoiceController();
        }

        [Fact]
        public void ReturnsA200StatusCode()
        {
            DateTime startime = new DateTime(2018, 11, 14, 5, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 14, 6, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 14, 7, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startime, bedTime, endTime) as OkObjectResult;

            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void ReturnsTheCorrectValuesWhenUsingFullHoursInSameDay()
        {
            DateTime startTime = new DateTime(2018, 11, 14, 5, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 14, 6, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 14, 7, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;
            var value = result.Value as double?;

            Assert.True(20 == value);
        }
    }
}
