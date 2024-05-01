import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartDialogComponent } from './cart/cart-dialog.component';
import { MatProgressBarModule} from '@angular/material/progress-bar';
import {MatButtonModule} from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
@NgModule({
  declarations: [
    CartDialogComponent
  ],
  imports: [
    CommonModule,
    MatProgressBarModule,
    MatButtonModule,
    MatDialogModule
    
  ]
})
export class CartModule { }
