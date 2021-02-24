import { Filter } from "../filter";

export class CurrencyFilter extends Filter {
    ids: number[];
    names: string[];
    exchangeRateRelativeToDollarMin: number;
    exchangeRateRelativeToDollarMax: number;
}