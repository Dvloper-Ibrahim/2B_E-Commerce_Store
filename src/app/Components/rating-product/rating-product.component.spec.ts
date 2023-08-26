import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RatingProductComponent } from './rating-product.component';

describe('RatingProductComponent', () => {
  let component: RatingProductComponent;
  let fixture: ComponentFixture<RatingProductComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RatingProductComponent]
    });
    fixture = TestBed.createComponent(RatingProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
