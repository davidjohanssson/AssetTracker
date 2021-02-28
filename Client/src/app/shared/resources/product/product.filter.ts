import { BrandFilter } from "../brand/brand.filter";
import { Filter } from "../filter";
import { FormFactorFilter } from "../form-factor/form-factor.filter";

export class ProductFilter extends Filter {
    ids?: number[];
    names?: string[];
    priceMin?: number;
    priceMax?: number;
    brandFilter?: BrandFilter;
    formFactorFilter?: FormFactorFilter;

    constructor() {
        super();
        this.brandFilter = new BrandFilter();
        this.formFactorFilter = new FormFactorFilter();
    }
}