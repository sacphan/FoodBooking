import { NgModule } from "@angular/core";
import { RestaurantService } from "./restaurant.service";
import { HttpClientModule } from "@angular/common/http";
@NgModule({
    imports:[HttpClientModule],
    providers:[
        RestaurantService
    ],
    declarations:[]
})
export class ServiceModule{};
