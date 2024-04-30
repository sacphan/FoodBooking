import { Guid } from "guid-typescript";
import { IImage } from "./image-model";

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
    brand: string
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
    Grab,
    Shoppe
}