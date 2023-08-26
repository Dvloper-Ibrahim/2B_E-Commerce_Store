import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Section3IconComponent } from './section3-icon.component';

describe('Section3IconComponent', () => {
  let component: Section3IconComponent;
  let fixture: ComponentFixture<Section3IconComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [Section3IconComponent]
    });
    fixture = TestBed.createComponent(Section3IconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
