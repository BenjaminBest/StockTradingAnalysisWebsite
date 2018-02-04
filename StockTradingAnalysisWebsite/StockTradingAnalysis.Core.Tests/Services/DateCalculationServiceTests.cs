using System;
using System.Globalization;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Core.Services;

namespace StockTradingAnalysis.Core.Tests.Services
{
    [TestClass]
    public class DateCalculationServiceTests
    {
        [TestMethod]
        [Description("GetStartAndEndDateOfWeek should return the correct start and end date of a week")]
        public void GetStartAndEndDateOfWeekShouldReturnTheCorrectStartAndEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfWeek(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 28, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 22, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfWeek should return the correct start and end date of a week for sunday")]
        public void GetStartAndEndDateOfWeekShouldReturnTheCorrectStartAndEndDateForSunday()
        {
            var date = new DateTime(2016, 08, 28, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfWeek(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 28, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 22, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfWeek should return the correct start and end date of a week for monday")]
        public void GetStartAndEndDateOfWeekShouldReturnTheCorrectStartAndEndDateForMonday()
        {
            var date = new DateTime(2016, 08, 22, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfWeek(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 28, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 22, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfMonth should return the correct start and end date of a month")]
        public void GetStartAndEndDateOfMonthShouldReturnTheCorrectStartAndEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfMonth(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfMonth should return the correct start and end date for the last day of a month")]
        public void GetStartAndEndDateOfMonthShouldReturnTheCorrectStartAndEndDateForTheLastDay()
        {
            var date = new DateTime(2016, 08, 1, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfMonth(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfMonth should return the correct start and end date for the first day of a month")]
        public void GetStartAndEndDateOfMonthShouldReturnTheCorrectStartAndEndDateForTheFirstDay()
        {
            var date = new DateTime(2016, 08, 31, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfMonth(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetEndDateOfMonth should return the correct end date of a month")]
        public void GetEndDateOfMonthShouldReturnTheCorrectEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var endDate = new DateCalculationService().GetEndDateOfMonth(date);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetEndDateOfMonth should return the correct end date for the last day of a month")]
        public void GetEndDateOfMonthShouldReturnTheCorrectEndDateForTheLastDay()
        {
            var date = new DateTime(2016, 08, 31, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var endDate = new DateCalculationService().GetEndDateOfMonth(date);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOf2Weeks should return the correct start end date")]
        public void GetStartAndEndDateOf2WeeksShouldReturnTheCorrectStartAndEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOf2Weeks(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 23, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 08, 09, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfYear should return the correct start and end date of a year")]
        public void GetStartAndEndDateOfYearShouldReturnTheCorrectStartAndEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfYear(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 12, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 01, 01, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfYear should return the correct start and end date for the last day of a year")]
        public void GetStartAndEndDateOfYearShouldReturnTheCorrectStartAndEndDateForTheLastDay()
        {
            var date = new DateTime(2016, 12, 31, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfYear(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 12, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 01, 01, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfYear should return the correct start and end date for the first day of a year")]
        public void GetStartAndEndDateOfYearShouldReturnTheCorrectStartAndEndDateForTheFirstDay()
        {
            var date = new DateTime(2016, 01, 01, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfYear(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 12, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 01, 01, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [Description("GetEndDateOfYear should return the correct end date of a year")]
        public void GetEndDateOfYearShouldReturnTheCorrectEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var endDate = new DateCalculationService().GetEndDateOfYear(date);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 12, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetEndDateOfYear should return the correct end date for the last day of a year")]
        public void GetEndDateOfYearShouldReturnTheCorrectEndDateForTheLastDay()
        {
            var date = new DateTime(2016, 12, 31, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var endDate = new DateCalculationService().GetEndDateOfYear(date);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 12, 31, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [Description("GetStartDateOfYear should return the correct start date of a year")]
        public void GetStartDateOfYearShouldReturnTheCorrectEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var endDate = new DateCalculationService().GetStartDateOfYear(date);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 01, 01, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartDateOfYear should return the correct end date for the first day of a year")]
        public void GetStartDateOfYearShouldReturnTheCorrectEndDateForTheFirstDay()
        {
            var date = new DateTime(2016, 01, 01, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var endDate = new DateCalculationService().GetStartDateOfYear(date);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 01, 01, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartDateOfYear should return the correct end date for the last day of a year")]
        public void GetStartDateOfYearShouldReturnTheCorrectEndDateForTheLastDay()
        {
            var date = new DateTime(2016, 12, 31, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var endDate = new DateCalculationService().GetStartDateOfYear(date);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 01, 01, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfQuarter should return the correct start and end date of a quarter")]
        public void GetStartAndEndDateOfQuarterShouldReturnTheCorrectStartAndEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfQuarter(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 09, 30, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 07, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfQuarter should return the correct start and end date for the last day of a quarter")]
        public void GetStartAndEndDateOfQuarterShouldReturnTheCorrectStartAndEndDateForTheLastDay()
        {
            var date = new DateTime(2016, 08, 1, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfQuarter(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 09, 30, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 07, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfQuarter should return the correct start and end date for the first day of a quarter")]
        public void GetStartAndEndDateOfQuarterShouldReturnTheCorrectStartAndEndDateForTheFirstDay()
        {
            var date = new DateTime(2016, 08, 31, 0, 0, 0);

            DateTime endDate;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfQuarter(date, out endDate);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 09, 30, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 07, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfQuarter should return the correct start and end date of a quarter")]
        public void GetStartAndEndDateOfQuarterShouldReturnTheCorrectQuarter()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            DateTime endDate;
            int quarter;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfQuarter(date, out endDate, out quarter);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 09, 30, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 07, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
            quarter.Should().Be(3);
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfQuarter should return the correct start and end date for the last day of a quarter")]
        public void GetStartAndEndDateOfQuarterShouldReturnTheCorrectQuarterForTheLastDay()
        {
            var date = new DateTime(2016, 08, 1, 0, 0, 0);

            DateTime endDate;
            int quarter;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfQuarter(date, out endDate, out quarter);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 09, 30, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 07, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
            quarter.Should().Be(3);
        }

        [TestMethod]
        [Description("GetStartAndEndDateOfQuarter should return the correct start and end date for the first day of a quarter")]
        public void GetStartAndEndDateOfQuarterShouldReturnTheCorrectQuarterForTheFirstDay()
        {
            var date = new DateTime(2016, 08, 31, 0, 0, 0);

            DateTime endDate;
            int quarter;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var startDate = new DateCalculationService().GetStartAndEndDateOfQuarter(date, out endDate, out quarter);

            endDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 09, 30, 23, 59, 59).ToString("dd.MM.yyyy HH:mm:ss"));
            startDate.ToString("dd.MM.yyyy HH:mm:ss").Should().Be(new DateTime(2016, 07, 1, 0, 0, 0).ToString("dd.MM.yyyy HH:mm:ss"));
            quarter.Should().Be(3);
        }

        [TestMethod]
        [Description("GetCalendarWeek should return the correct calender week")]
        public void GetCalendarWeekShouldReturnTheCorrectStartAndEndDate()
        {
            var date = new DateTime(2016, 08, 23, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var cw = new DateCalculationService().GetCalendarWeek(date);
            cw.Should().Be(34);
        }

        [TestMethod]
        [Description("GetCalendarWeek should return the correct start and end date for the last day of a month")]
        public void GetCalendarWeekShouldReturnTheCorrectCalenderWeekTheLastDay()
        {
            var date = new DateTime(2016, 08, 22, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var cw = new DateCalculationService().GetCalendarWeek(date);
            cw.Should().Be(34);
        }

        [TestMethod]
        [Description("GetCalendarWeek should return the correct start and end date for the first day of a month")]
        public void GetCalendarWeekShouldReturnTheCorrectCalenderWeekForTheFirstDay()
        {
            var date = new DateTime(2016, 08, 28, 0, 0, 0);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
            var cw = new DateCalculationService().GetCalendarWeek(date);
            cw.Should().Be(34);
        }
    }
}
