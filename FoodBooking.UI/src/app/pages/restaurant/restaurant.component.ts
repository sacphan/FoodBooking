import { Component } from '@angular/core';
import { RestaurantService } from '../../services/restaurant.service';
import { IRestaurant, ESourceCrawl } from '../../models/restaurant';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrl: './restaurant.component.scss'
})
export class RestaurantComponent {
  keyWord:string="";
  pageActive:number = 1;
  restaurants:IRestaurant[]=[];
  totalPage:number= 1;
  constructor( private _restaurantService:RestaurantService) 
  {    
  }

  ngOnInit() {
    this._restaurantService.getRestaurants(this.keyWord,this.pageActive,100).subscribe(result => {
      this.restaurants = result.restaurants;
      this.restaurants.forEach(restaurant => {
        restaurant.brand = ESourceCrawl[restaurant.sourceCrawlId];
      });
      this.pageActive = result.page;
      this.totalPage = result.totalPage;
    });  }
}
