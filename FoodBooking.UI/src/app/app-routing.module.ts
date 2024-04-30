import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RestaurantComponent } from './pages/restaurant/restaurant.component';
const routes: Routes = [
  {path:'',component: RestaurantComponent},
  {path:'restaurant',component: RestaurantComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
