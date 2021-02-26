import { Filter } from "../filter";

export class CurrencyFilter extends Filter {
    ids: number[];
    name: string;
    codes: string[];
    exchangeRateRelativeToDollarMin: number;
    exchangeRateRelativeToDollarMax: number;
}