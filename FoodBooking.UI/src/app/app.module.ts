import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RestaurantComponent } from './restaurant/restaurant.component';
import { LayoutModule } from './layout/layout.module';
import {MatSidenavModule} from '@angular/material/sidenav';
import { AppRoutingModule } from './app-route.module';
@NgModule({
  declarations: [
    AppComponent,
    RestaurantComponent,
  ],
  imports: [
    BrowserModule,
    LayoutModule,
    MatSidenavModule,
    AppRoutingModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
