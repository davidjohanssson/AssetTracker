import { CurrencyFilter } from "../currency/currency.filter";
import { Filter } from "../filter";

export class OfficeFilter extends Filter {
    ids?: number[];
    cities?: string[];
    currencyFilter?: CurrencyFilter;

    constructor() {
        super();
        this.currencyFilter = new CurrencyFilter();
    }
}