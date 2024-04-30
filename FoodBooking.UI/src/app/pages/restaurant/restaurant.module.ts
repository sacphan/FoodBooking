import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantComponent } from './restaurant.component';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';

import {MatGridListModule} from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon'
@NgModule({
  declarations: [
    RestaurantComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatGridListModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class RestauranModule { }
