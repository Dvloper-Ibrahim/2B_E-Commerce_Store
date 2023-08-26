import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubSubCategoryDetailsComponent } from './sub-sub-category-details.component';

describe('SubSubCategoryDetailsComponent', () => {
  let component: SubSubCategoryDetailsComponent;
  let fixture: ComponentFixture<SubSubCategoryDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SubSubCategoryDetailsComponent]
    });
    fixture = TestBed.createComponent(SubSubCategoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
