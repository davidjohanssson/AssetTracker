import { OfficeFilter } from "../office/office.filter";
import { ProductFilter } from "../product/product.filter";

export class AssetFilter {
    ids?: number[];
    purchaseDateMin?: Date;
    purchaseDateMax?: Date;
    productFilter?: ProductFilter;
    officeFilter?: OfficeFilter;
}