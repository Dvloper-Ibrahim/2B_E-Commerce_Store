import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeAppliancesOffersComponent } from './home-appliances-offers.component';

describe('HomeAppliancesOffersComponent', () => {
  let component: HomeAppliancesOffersComponent;
  let fixture: ComponentFixture<HomeAppliancesOffersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HomeAppliancesOffersComponent]
    });
    fixture = TestBed.createComponent(HomeAppliancesOffersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
