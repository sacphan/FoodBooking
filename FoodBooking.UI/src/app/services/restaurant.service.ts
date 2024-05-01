import { Injectable } from '@angular/core';
import { IRestaurant, IRestaurantsReponse } from '../models/restaurant';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs';
import { Guid } from 'guid-typescript';

@Injectable()
export class RestaurantService {

  constructor(private http: HttpClient)
  {}

  private restaurantsUrl = 'api/Restaurant';

  getRestaurants(keyWord:string,page:number,record:number): Observable<IRestaurantsReponse> {
    const restaurants = this.http.get<IRestaurantsReponse>(`${this.restaurantsUrl}/Search?${keyWord!="" ? `KeyWord=${keyWord}&`:""}Page=${page}&Record=${record}`);
    return restaurants;
  }

  getRestaurantDetailById(id: Guid): Observable<IRestaurant> {
    const restaurants = this.http.get<IRestaurant>(`${this.restaurantsUrl}/${id}`);
    return restaurants;
  }

  createRestaurants( restaurant:IRestaurant): Observable<IRestaurant[]> {
    const result = this.http.post<IRestaurant[]>(this.restaurantsUrl,restaurant);
    return result;
  }
}
