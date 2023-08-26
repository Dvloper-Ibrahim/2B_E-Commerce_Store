import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalCareOffersComponent } from './personal-care-offers.component';

describe('PersonalCareOffersComponent', () => {
  let component: PersonalCareOffersComponent;
  let fixture: ComponentFixture<PersonalCareOffersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PersonalCareOffersComponent]
    });
    fixture = TestBed.createComponent(PersonalCareOffersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
