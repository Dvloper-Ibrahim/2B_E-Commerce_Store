import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarouselHome1Component } from './carousel-home1.component';

describe('CarouselHome1Component', () => {
  let component: CarouselHome1Component;
  let fixture: ComponentFixture<CarouselHome1Component>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CarouselHome1Component]
    });
    fixture = TestBed.createComponent(CarouselHome1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
