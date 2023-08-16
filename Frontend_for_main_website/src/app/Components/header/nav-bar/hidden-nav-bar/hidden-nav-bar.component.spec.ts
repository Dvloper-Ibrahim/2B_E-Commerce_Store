import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HiddenNavBarComponent } from './hidden-nav-bar.component';

describe('HiddenNavBarComponent', () => {
  let component: HiddenNavBarComponent;
  let fixture: ComponentFixture<HiddenNavBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HiddenNavBarComponent]
    });
    fixture = TestBed.createComponent(HiddenNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
