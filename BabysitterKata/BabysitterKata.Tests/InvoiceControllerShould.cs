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

        //should check status code

        [Theory]
        [InlineData(5, 6, 7, 0)]
        public void ReturnTheCorrectValuesWhenUsingFullHours(int StartHour, int BedHour, int EndHour, double RequiredPrice)
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
