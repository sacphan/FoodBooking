import { Guid } from "guid-typescript";
import { IImage } from "./image-model";

export interface IProduct {
    id: Guid;
    name: string | null;
    description: string | null;
    price: string;
    image: IImage | null;
}