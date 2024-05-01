import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantDetailComponent } from './restaurant-detail.component';

describe('RestaurantComponent', () => {
  let component: RestaurantDetailComponent;
  let fixture: ComponentFixture<RestaurantDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RestaurantDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RestaurantDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
