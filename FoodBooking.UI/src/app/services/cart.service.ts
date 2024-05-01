import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IProduct } from '../models/product-model';

@Injectable()
export class CartService {

  constructor(private http: HttpClient)
  {}
  private products: IProduct[] = [];

  public addOrUpdateProductToCart(product: IProduct) 
  {
    let indexProduct = this.products.findIndex(p=>p.id == product.id)

    if (indexProduct>0)
    {
        this.products[indexProduct] = product;
    }
    else
    {
        this.products.push(product);
    }
  }

  public removeProductFromCart(product: IProduct) 
  {
    this.products = this.products.filter(p => p.id !== product.id);
  }

  public getProducts()
  {
    return this.products;
  }

}
