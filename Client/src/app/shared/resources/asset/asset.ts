import { Office } from "../office/office";
import { Product } from "../product/product";

export class Asset {
    id: number;
    purchaseDate: Date;

    productId: number;
    product: Product;
    
    officeId: number;
    office: Office;
}