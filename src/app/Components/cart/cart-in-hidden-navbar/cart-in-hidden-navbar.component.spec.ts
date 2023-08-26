import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartInHiddenNavbarComponent } from './cart-in-hidden-navbar.component';

describe('CartInHiddenNavbarComponent', () => {
  let component: CartInHiddenNavbarComponent;
  let fixture: ComponentFixture<CartInHiddenNavbarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CartInHiddenNavbarComponent]
    });
    fixture = TestBed.createComponent(CartInHiddenNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
