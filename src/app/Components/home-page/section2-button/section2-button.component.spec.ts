import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Section2ButtonComponent } from './section2-button.component';

describe('Section2ButtonComponent', () => {
  let component: Section2ButtonComponent;
  let fixture: ComponentFixture<Section2ButtonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [Section2ButtonComponent]
    });
    fixture = TestBed.createComponent(Section2ButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
