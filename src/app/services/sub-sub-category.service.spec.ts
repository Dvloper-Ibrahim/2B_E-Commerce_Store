import { TestBed } from '@angular/core/testing';

import { SubSubCategoryService } from './sub-sub-category.service';

describe('SubSubCategoryService', () => {
  let service: SubSubCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubSubCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
