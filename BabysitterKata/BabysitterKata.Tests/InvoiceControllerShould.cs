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
            DateTime startime = new DateTime(2018, 11, 14, 17, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 14, 18, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 14, 19, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startime, bedTime, endTime) as OkObjectResult;

            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void ReturnsTheCorrectValueWhenUsingFullHoursInSameDay()
        {
            DateTime startTime = new DateTime(2018, 11, 14, 17, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 14, 18, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 14, 19, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;
            var value = result.Value as double?;

            Assert.True(20 == value);
        }

        [Fact]
        public void ReturnsTheCorrectValueWhenUsingFullHoursAfterMidnight()
        {
            DateTime startTime = new DateTime(2018, 11, 14, 17, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 14, 18, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 15, 4, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;

            double? value = result.Value as double?;
            Assert.True(124 == value);
        }

        [Fact]
        public void ReturnTheCorrectValuesPartialHours()
        {
            DateTime startTime = new DateTime(2018, 11, 15, 17, 30, 00);
            DateTime bedTime = new DateTime(2018, 11, 15, 18, 0, 00);
            DateTime endTime = new DateTime(2018, 11, 15, 19, 15, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;

            double? value = result.Value as double?;
            Assert.True(124 == value);
        }

        //partial hours
        //partial hours after midnight

        //leaving before bedtime

        //leaving before bedtime but after midnight

        //daylight savings time

        //mimimulistic time
    }
}
