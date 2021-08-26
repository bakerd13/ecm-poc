import { Get } from "./channelHelpers";

export const IsNullOrWhiteSpace = (input) => {
    if (typeof input === 'undefined' || input === null) return true;
    return !/\S/.test(input);
}

export const TryParseInt = (value, outValue) => {
    if(value !== null && value.length > 0) {
            if (!isNaN(value)) {
                outValue.value = parseInt(value, 10);
                return true;
            }
    }
    return false;
}

export const TryParseDecimal = (value, outValue) => {
    if(value !== null && value.length > 0) {
            if (!isNaN(value)) {
                outValue.value = parseFloat(value);
                return true;
            }
    }
    return false;
}

export const RemoveCurrencySymbolOnPriceRangeRight = (price) => {
    const channelObject = Get();
    if (channelObject.CurrencyPosition === "right" || channelObject.CurrencyPosition === "rightwithspace") {
        const occurrencesOfCurrencySymbol = price.match(channelObject.CurrencySymbol).length;

        if (occurrencesOfCurrencySymbol > 1) {
            const startIndex = price.indexOf(channelObject.CurrencySymbol);
            const currencySymbolLength = channelObject.CurrencySymbol.Length;

            return price.substr(startIndex, currencySymbolLength);
        }
    }

    return price;
}

export const ToBoolean = (string) => {
    switch(string.toLowerCase().trim()){
        case "true": case "yes": case "1": return true;
        case "false": case "no": case "0": case null: return false;
        default: return Boolean(string);
    }
}

export const SplitLayoutItem = (item) => {
    const items = item.split("_");
    return {type: items[0], itemNumber: items[1]};
}