import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantComponent } from './restaurant.component';
import { RestaurantDetailComponent } from './restaurant-detail/restaurant-detail.component';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';

import {MatGridListModule} from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon'
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    RestaurantComponent,
    RestaurantDetailComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatGridListModule,
    MatButtonModule,
    MatIconModule,
    RouterModule
  ]
})
export class RestauranModule { }
