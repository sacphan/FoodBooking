import { Guid } from "guid-typescript";
import { IImage } from "./image-model";
import { IProduct } from "./product-model";

export interface IRestaurant
{
    id:Guid,
    name:string,
    description:string,
    title:string,
    image: IImage,
    createDate: Date,
    address:string,
    linkCrawl: string,
    sourceCrawlId: number,
    brand: string,
    products: IProduct[]

}

export interface IRestaurantsReponse
{
    restaurants:IRestaurant[],
    page:number,
    totalRow:number,
    totalPage:number
}


export enum ESourceCrawl {
    None,
    Shoppe,
    Grab
}