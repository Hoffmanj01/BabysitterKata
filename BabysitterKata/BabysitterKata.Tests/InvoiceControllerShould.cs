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

        [Theory]
        [InlineData(5, 6, 7, 20)]
        public void ReturnsTheCorrectValuesWhenUsingFullHours(int StartHour, int BedHour, int EndHour, double RequiredPrice)
        {
            DateTime startTime = new DateTime(2018, 11, 14, StartHour, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 14, BedHour, 00, 00);
            DateTime endTime;
            if (StartHour > EndHour) endTime = new DateTime(2018, 11, 15, EndHour, 00, 00);
            else endTime = new DateTime(2018, 11, 14, EndHour, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;
            var value = result.Value as double?;

            Assert.True(RequiredPrice == value);
        }
    }
}
