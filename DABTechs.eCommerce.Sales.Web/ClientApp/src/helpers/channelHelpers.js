import { IsNullOrWhiteSpace, TryParseInt, TryParseDecimal } from "./utilityHelpers";
import CultureInfo from "./cultureInfo";

// TODO BE REMOVED
import data from "../store/dummyData.json";

export const Get = () => {
    return data.mSettings;
} 

export const FormatAsCurrencyRange = (minimumAmount, maximumAmount, useAlternativeCurrencySymbol = false) =>
{
    let priceString = "";
    let minimum = 0;
    let maximum = 0;

    if (TryParseDecimal(minimumAmount,minimum) && TryParseDecimal(maximumAmount,maximum)) {
        priceString = (minimumAmount === maximumAmount)
            ? FormatAsCurrency(maximumAmount, useAlternativeCurrencySymbol)
            : FormatAsCurrencyLocalRange(minimum, maximum, useAlternativeCurrencySymbol);
    }

    return priceString;
}

export const FormatAsCurrency = (amount, round = true, useAlternativeCurrencySymbol = false) => {
    let format = (round && Math.ceil(amount) === amount) ? "C0" : "C";

    let culture = GetChannelCultureInfo(useAlternativeCurrencySymbol);

    //To handle currency with fractional-part when <numberdecimaldigits>0</numberdecimaldigits> 
    if (culture.NumberFormat.CurrencyDecimalDigits === 0) {
        if (amount - Math.Trunc(amount) !== 0) {
            culture.NumberFormat.CurrencyDecimalDigits = 2;
        }
    }

    var formattedAmount = amount.toString(format, culture);

    return formattedAmount;
}

export const GetChannelCultureInfo = (useAlternativeCurrencySymbol = false) => {
    const channelObject = Get();
    const cultureInfo = new CultureInfo(channelObject.Locale, false);

    if (!IsNullOrWhiteSpace(channelObject.CurrencyPosition)) {
        //format pattern for currency values.
        //More info on http://tiny.cc/hilkcy
        let positiveCurrencyPosition = 0;
        //More info on Negaive currency pattern go to http://tinyurl.com/jelfeau
        let negativeCurrencyPosition = 0;

        switch (channelObject.CurrencyPosition.toLowerCase()) {
            case "left":
                positiveCurrencyPosition = 0;
                negativeCurrencyPosition = 1;
                break;
            case "right":
                positiveCurrencyPosition = 1;
                negativeCurrencyPosition = 5;
                break;
            case "leftwithspace":
                positiveCurrencyPosition = 2;
                negativeCurrencyPosition = 9;
                break;
            case "rightwithspace":
                positiveCurrencyPosition = 3;
                negativeCurrencyPosition = 8;
                break;
            default:
                positiveCurrencyPosition = 0;
                negativeCurrencyPosition = 1;
        }


        cultureInfo.NumberFormat.CurrencyPositivePattern = positiveCurrencyPosition;
        cultureInfo.NumberFormat.CurrencyNegativePattern = negativeCurrencyPosition;
        cultureInfo.NumberFormat.CurrencySymbol = useAlternativeCurrencySymbol ? channelObject.AlternativeCurrencySymbol : channelObject.CurrencySymbol;
    }

    let numberGroupSeparator = channelObject.NumberGroupSeparator;
    if (!IsNullOrWhiteSpace(numberGroupSeparator)) {
        if (numberGroupSeparator === "space") {
            numberGroupSeparator = " ";
        }
        cultureInfo.NumberFormat.CurrencyGroupSeparator = numberGroupSeparator;
        cultureInfo.NumberFormat.NumberGroupSeparator = numberGroupSeparator;
    }

    var numberDecimalSeparator = channelObject.NumberDecimalSeparator;
    if (!IsNullOrWhiteSpace(numberDecimalSeparator)) {
        cultureInfo.NumberFormat.CurrencyDecimalSeparator = numberDecimalSeparator;
        cultureInfo.NumberFormat.NumberDecimalSeparator = numberDecimalSeparator;
    }

    var numberGroupSizes = channelObject.NumberGroupSizes;
    if (numberGroupSizes.lenght > 0) {
        cultureInfo.NumberFormat.CurrencyGroupSizes = numberGroupSizes.toArray();
        cultureInfo.NumberFormat.NumberGroupSizes = numberGroupSizes.toArray();
    }

    let numberDecimalDigits;
    if (!IsNullOrWhiteSpace(channelObject.NumberDecimalDigits) &&
        TryParseInt(channelObject.NumberDecimalDigits, numberDecimalDigits)) {
        cultureInfo.NumberFormat.CurrencyDecimalDigits = numberDecimalDigits;
        cultureInfo.NumberFormat.NumberDecimalDigits = numberDecimalDigits;
    }
    return cultureInfo;
}

const FormatAsCurrencyLocalRange = (minimumAmount, maximumAmount, useAlternativeCurrencySymbol = false) => {
    var roundMin = !(Math.ceil(minimumAmount) > minimumAmount);
    var roundMax = !(Math.ceil(maximumAmount) > maximumAmount);

    var minimumFormatted = FormatAsCurrency(minimumAmount, roundMin, useAlternativeCurrencySymbol);
    var maximumFormatted = FormatAsCurrency(maximumAmount, roundMax, useAlternativeCurrencySymbol);

    return `${minimumFormatted} - ${maximumFormatted}`;
}