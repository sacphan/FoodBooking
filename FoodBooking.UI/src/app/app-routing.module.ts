import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RestaurantComponent } from './pages/restaurant/restaurant.component';
import { RestaurantDetailComponent } from './pages/restaurant/restaurant-detail/restaurant-detail.component';

const routes: Routes = [
  {path:'',component: RestaurantComponent},
  {path:'restaurant',component: RestaurantComponent},
  {path:'restaurant/:id',component: RestaurantDetailComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
