import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayPalComponent } from './pay-pal.component';

describe('PayPalComponent', () => {
  let component: PayPalComponent;
  let fixture: ComponentFixture<PayPalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayPalComponent]
    });
    fixture = TestBed.createComponent(PayPalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
