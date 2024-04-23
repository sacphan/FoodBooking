import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LayoutComponent } from './layouts/layout.component';
import { RestaurantComponent } from './restaurant/restaurant.component';
@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    RestaurantComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
