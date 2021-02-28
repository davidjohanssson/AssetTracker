import { Filter } from "../filter";
import { OfficeFilter } from "../office/office.filter";
import { ProductFilter } from "../product/product.filter";

export class AssetFilter extends Filter {
    ids?: number[];
    purchaseDateMin?: Date;
    purchaseDateMax?: Date;
    productFilter?: ProductFilter;
    officeFilter?: OfficeFilter;

    constructor() {
        super();
        this.productFilter = new ProductFilter();
        this.officeFilter = new OfficeFilter();
    }
}