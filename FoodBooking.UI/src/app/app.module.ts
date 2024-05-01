import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {MatSidenavModule} from '@angular/material/sidenav';
import { LayoutModule } from './pages/layout/layout.module';
import {MatListModule} from '@angular/material/list'; 
import { RestauranModule } from './pages/restaurant/restaurant.module';
import { ServiceModule } from './services/service.module';
import { CartModule } from './pages/component/component.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    MatSidenavModule,
    LayoutModule,
    MatListModule,
    RestauranModule,
    ServiceModule,
    CartModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
