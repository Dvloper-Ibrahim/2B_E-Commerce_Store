import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CorporateSolutionsComponent } from './corporate-solutions.component';

describe('CorporateSolutionsComponent', () => {
  let component: CorporateSolutionsComponent;
  let fixture: ComponentFixture<CorporateSolutionsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CorporateSolutionsComponent]
    });
    fixture = TestBed.createComponent(CorporateSolutionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
