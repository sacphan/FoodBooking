import { Component } from '@angular/core';
import { RestaurantService } from '../../../services/restaurant.service';
import { IRestaurant, ESourceCrawl } from '../../../models/restaurant';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { MatDialog } from '@angular/material/dialog';
import { CartDialogComponent } from '../../component/cart/cart-dialog.component';

@Component({
  selector: 'app-restaurant-detail',
  templateUrl: './restaurant-detail.component.html',
  styleUrl: './restaurant-detail.component.scss'
})
export class RestaurantDetailComponent {
  restaurant:IRestaurant  = {
    name:""
  } as IRestaurant;
  restaurantId: Guid  = Guid.createEmpty();

  constructor( private _restaurantService:RestaurantService,private route: ActivatedRoute,private dialog: MatDialog) 
  {    
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.restaurantId = params['id'];
      this._restaurantService.getRestaurantDetailById(this.restaurantId).subscribe(result => {
        this.restaurant = result;
        this.restaurant.brand = ESourceCrawl[result.sourceCrawlId];
        
      });  
    });
  }

  openCartDialog() {
    const dialogRef = this.dialog.open(CartDialogComponent,{
      data: {title:"123"}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
    
}
