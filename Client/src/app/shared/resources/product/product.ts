import { Brand } from "../brand/brand";
import { FormFactor } from "../form-factor/form-factor";

export class Product {
    id: number;
    name: string;
    price: number;
    brand: Brand;
    formFactor: FormFactor;
}