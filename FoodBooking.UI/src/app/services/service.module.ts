import { NgModule } from "@angular/core";
import { RestaurantService } from "./restaurant.service";
import { HttpClientModule } from "@angular/common/http";
import { CartService } from "./cart.service";

@NgModule({
    imports:[HttpClientModule],
    providers:[
        RestaurantService,
        CartService
    ],
    declarations:[]
})
export class ServiceModule{};
