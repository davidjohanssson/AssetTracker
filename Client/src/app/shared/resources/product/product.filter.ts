import { BrandFilter } from "../brand/brand.filter";
import { FormFactorFilter } from "../form-factor/form-factor.filter";

export class ProductFilter {
    ids?: number[];
    names?: string[];
    priceMin?: number;
    priceMax?: number;
    brandFilter?: BrandFilter;
    formFactorFilter?: FormFactorFilter;
}