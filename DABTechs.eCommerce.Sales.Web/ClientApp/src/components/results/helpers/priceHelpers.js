import { Get, GetChannelCultureInfo } from "../../../../../helpers/channelHelpers";
import { IsNullOrWhiteSpace, ToBoolean, TryParseDecimal, RemoveCurrencySymbolOnPriceRangeRight } from "../../../../../helpers/utilityHelpers";

export const SaleHighlightPrice = (priceItem) =>
{
    const channelHelper = Get();

    if (priceItem.IsDiscount !== null && ToBoolean(priceItem.IsDiscount) && !IsNullOrWhiteSpace(priceItem.MinWasPrice))
    {
        if (channelHelper.ShowOriginalPriceOverWasPrice)
        {
            return RemoveCurrencySymbolOnPriceRangeRight(
                FormatSalePrice1(channelHelper.SalePriceFormatSingleHighlight, 
                channelHelper.SalePriceFormatRangeHighlight,
                parseFloat(priceItem.MinPrice),
                parseFloat(priceItem.Price),
                priceItem.MinWasPrice,
                priceItem.MaxWasPrice,
                priceItem.OriginalMinPrice,
                priceItem.OriginalMaxPrice)
                );
        }
        else
        {
            return RemoveCurrencySymbolOnPriceRangeRight(
                FormatSalePrice3(channelHelper.SalePriceFormatSingleHighlight, 
                channelHelper.SalePriceFormatRangeHighlight,
                priceItem.MinPrice,
                priceItem.Price,
                priceItem.MinWasPrice,
                priceItem.MaxWasPrice));
        }
    }
    return "";
}

export const SalePlainPrice = (priceItem) =>
{
    const channelHelper = Get();

    if (priceItem.IsDiscount != null && ToBoolean(priceItem.IsDiscount) && !IsNullOrWhiteSpace(priceItem.MinWasPrice))
    {
        if (channelHelper.ShowOriginalPriceOverWasPrice)
        {

            return RemoveCurrencySymbolOnPriceRangeRight(FormatSalePrice1(channelHelper.SalePriceFormatSinglePlain, 
                channelHelper.SalePriceFormatRangePlain, 
                parseFloat(priceItem.MinPrice), 
                parseFloat(priceItem.Price), 
                priceItem.MinWasPrice, 
                priceItem.MaxWasPrice,
                priceItem.OriginalMinPrice,
                priceItem.OriginalMaxPrice));
        }
        else
        {

            return RemoveCurrencySymbolOnPriceRangeRight(
                FormatSalePrice3(channelHelper.SalePriceFormatSinglePlain,
                    channelHelper.SalePriceFormatRangePlain,
                    priceItem.MinPrice,
                    priceItem.Price,
                    priceItem.MinWasPrice,
                    priceItem.MaxWasPrice));
        }
    }
    return "";
}

const FormatPriceAsCurrency = (potentialNumber, useAlternativeCurrencySymbol = false) => {
    var priceString = "";
    let number;
    if (TryParseDecimal(potentialNumber, number)) {
        priceString = FormatAsCurrency(number, true, useAlternativeCurrencySymbol);
    }
    return priceString;
}

const FormatAsCurrency = (amount, round = true, useAlternativeCurrencySymbol = false) =>
{
    // TODO: link to tostring
    //const format = (round && Math.Ceil(amount) == amount) ? "C0" : "C";

    let culture = GetChannelCultureInfo(useAlternativeCurrencySymbol);

    //To handle currency with fractional-part when <numberdecimaldigits>0</numberdecimaldigits> 
    if (culture.NumberFormat.CurrencyDecimalDigits === 0)
    {
        if (amount - Math.Trunc(amount) !== 0)
        {
            culture.NumberFormat.CurrencyDecimalDigits = 2;
        }
    }

    // TODO: need to investigate next line
    //const formattedAmount = amount.toString(format, culture);
    const formattedAmount = amount.toString();

    return formattedAmount;
}

const FormatSalePrice1 = (singlePriceFormat, rangePriceFormat, salePriceMin, salePriceMax, fullPriceMin, fullPriceMax, originalPriceMin, originalPriceMax) => {
    let minPrice;
    let maxPrice;

    if (TryParseDecimal(originalPriceMin, minPrice) && TryParseDecimal(originalPriceMax, maxPrice)) {
        return FormatSalePrice3(singlePriceFormat, rangePriceFormat, salePriceMin, salePriceMax, parseFloat(fullPriceMin), parseFloat(fullPriceMax), minPrice, maxPrice);
    }

    return FormatSalePrice3(singlePriceFormat, rangePriceFormat, salePriceMin, salePriceMax, parseFloat(fullPriceMin), parseFloat(fullPriceMax));
}

const FormatSalePrice3 = (singlePriceFormat, rangePriceFormat, salePriceMin, salePriceMax, fullPriceMin, fullPriceMax, originalPriceMin = -1, originalPriceMax = -1) => {
    if (singlePriceFormat === null || rangePriceFormat === null || fullPriceMin === 0 || fullPriceMax === 0) {
        return "";
    }

    let minPrice;
    let maxPrice;

    if (originalPriceMin !== -1 && originalPriceMax !== -1) {
        minPrice = originalPriceMin;
        maxPrice = originalPriceMax;
    }
    else {
        minPrice = fullPriceMin;
        maxPrice = fullPriceMax;
    }

    const discountDecimalMin = (salePriceMin / minPrice) * 10;
    const discountDecimalMax = (salePriceMax / maxPrice) * 10;

    const discountPercentageMin = (1 - (salePriceMin / minPrice)) * 100;
    const discountPercentageMax = (1 - (salePriceMax / maxPrice)) * 100;

    let format;

    if ((rangePriceFormat.indexOf("{salePriceMin}") !== -1 && salePriceMin !== salePriceMax) ||
        (rangePriceFormat.indexOf("{fullPriceMin}") !== -1 && minPrice !== maxPrice)) {
        format = rangePriceFormat;
    }
    else {
        format = singlePriceFormat;
    }

    return format
        .replace("{salePriceMin}", FormatPriceAsCurrency(salePriceMin))
        .replace("{salePriceMax}", FormatPriceAsCurrency(salePriceMax))
        .replace("{fullPriceMin}", FormatPriceAsCurrency(minPrice))
        .replace("{fullPriceMax}", FormatPriceAsCurrency(maxPrice))
        .replace("{discountDecimalMin}", (Math.Ceil(discountDecimalMin * 10) / 10).toFixed(1))
        .replace("{discountDecimalMax}", (Math.Ceil(discountDecimalMax * 10) / 10).toFixed(1))
        .replace("{discountPercentageMin}", discountPercentageMin.toFixed(0) + "%")
        .replace("{discountPercentageMax}", discountPercentageMax.toFixed(0) + "%");

}