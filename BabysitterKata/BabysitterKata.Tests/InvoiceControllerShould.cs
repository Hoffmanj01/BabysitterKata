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
        public void ReturnA200StatusCode()
        {
            DateTime startime = new DateTime(2018, 11, 14, 17, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 14, 18, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 14, 19, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startime, bedTime, endTime) as OkObjectResult;

            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void ReturnTheCorrectValueWhenUsingFullHoursInSameDay()
        {
            DateTime startTime = new DateTime(2018, 11, 14, 17, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 14, 18, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 14, 19, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;
            var value = result.Value as double?;

            Assert.True(20 == value);
        }

        [Fact]
        public void ReturnTheCorrectValueWhenUsingFullHoursAfterMidnight()
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
            Assert.True(20 == value);
        }
        
        [Fact]
        public void ReturnTheCorrectValuePartialHoursAfterMidnight()
        {
            DateTime startTime = new DateTime(2018, 11, 15, 17, 0, 00);
            DateTime bedTime = new DateTime(2018, 11, 15, 18, 45, 00);
            DateTime endTime = new DateTime(2018, 11, 16, 2, 15, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;

            double? value = result.Value as double?;
            Assert.True(112 == value);
        }
        
        [Fact]
        public void ReturnsheCorrectValueWhenLeavingBeforeBedTime()
        {
            DateTime startTime = new DateTime(2018, 11, 15, 17, 0, 00);
            DateTime bedTime = new DateTime(2018, 11, 15, 19, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 15, 19, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;

            double? value = result.Value as double?;
            Assert.True(24 == value);
        }

        [Fact]
        public void ReturnTheCorrectValueWhenLeavingBeforeBedTimePartialHours()
        {
            DateTime startTime = new DateTime(2018, 11, 15, 16, 25, 00);
            DateTime bedTime = new DateTime(2018, 11, 15, 19, 33, 00);
            DateTime endTime = new DateTime(2018, 11, 15, 19, 33, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;

            double? value = result.Value as double?;
            Assert.True(48 == value);
        }

        [Fact]
        public void ReturnTheCorrectValueWhenLeavingBeforeBedButAfterMidnight()
        {
            DateTime startTime = new DateTime(2018, 11, 15, 16, 00, 00);
            DateTime bedTime = new DateTime(2018, 11, 16, 2, 00, 00);
            DateTime endTime = new DateTime(2018, 11, 16, 2, 00, 00);

            OkObjectResult result = _sut.GetInvoicePrice(startTime, bedTime, endTime) as OkObjectResult;

            double? value = result.Value as double?;
            Assert.True(152 == value);
        }

        //leaving before bedtime but after midnight

        //daylight savings time

        //mimimulistic time

        //test the convertToFullHours
    }
}
