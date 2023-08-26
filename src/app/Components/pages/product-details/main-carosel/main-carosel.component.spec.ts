import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainCaroselComponent } from './main-carosel.component';

describe('MainCaroselComponent', () => {
  let component: MainCaroselComponent;
  let fixture: ComponentFixture<MainCaroselComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MainCaroselComponent]
    });
    fixture = TestBed.createComponent(MainCaroselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
