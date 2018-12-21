function getStatistics() {

    var calculation = new Object();
    calculation.Multiplier = $("#Multiplier").val();
    calculation.StrikePrice = $("#StrikePrice").val();
    calculation.InitialSL = $("#InitialSl").val();
    calculation.InitialTP = $("#InitialTp").val();
    calculation.PricePerUnit = $("#PricePerUnit").val();
    calculation.OrderCosts = $("#OrderCosts").val();
    calculation.Units = $("#Units").val();
    calculation.WKN = $("#Wkn").val();
    calculation.IsLong = document.getElementById("IsLong").checked;

    $.ajax({
        url: '/Calculation/GetStatistics',
        data: calculation,
        dataType: "json",
        type: "POST",
        error: function () {
            alert("An error occurred.");
        },
        success: function (data) {
            document.getElementById('CalculationResults').innerHTML = data.Result;
        }
    });
}

function getPriceFromUnderlying(underlyingPrice, multiplier, strikePrice, isLong) {
    $.ajax({
        url: '/Calculation/CalculatePriceFromUnderlying',
        data: { 'underlyingPrice': underlyingPrice, 'multiplier': multiplier, 'strikePrice': strikePrice, 'isLong': isLong },
        dataType: "json",
        type: "POST",
        error: function () {
            alert("An error occurred.");
        },
        success: function (data) {
            document.getElementById('UnderlyingResult').innerHTML = data;
        }
    });
}

$(function () { $("#Multiplier").keyup(function () { getStatistics(); }); });
$(function () { $("#StrikePrice").keyup(function () { getStatistics(); }); });
$(function () { $("#InitialSl").keyup(function () { getStatistics(); }); });
$(function () { $("#InitialTp").keyup(function () { getStatistics(); }); });
$(function () { $("#PricePerUnit").keyup(function () { getStatistics(); }); });
$(function () { $("#OrderCosts").keyup(function () { getStatistics(); }); });
$(function () { $("#Units").keyup(function () { getStatistics(); }); });
$(function () { $("#IsLong").change(function () { getStatistics(); }); });

$(function () { $("#UnderlyingCalc").keyup(function () { getPriceFromUnderlying($("#UnderlyingCalc").val(), $("#Multiplier").val(), $("#StrikePrice").val(), document.getElementById("IsLong").checked); }); });