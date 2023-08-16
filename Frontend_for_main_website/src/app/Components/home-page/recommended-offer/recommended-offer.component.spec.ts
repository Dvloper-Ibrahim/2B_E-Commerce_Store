import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecommendedOfferComponent } from './recommended-offer.component';

describe('RecommendedOfferComponent', () => {
  let component: RecommendedOfferComponent;
  let fixture: ComponentFixture<RecommendedOfferComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecommendedOfferComponent]
    });
    fixture = TestBed.createComponent(RecommendedOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
