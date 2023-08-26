import { TestBed } from '@angular/core/testing';

import { ProductsPagesService } from './products-pages.service';

describe('ProductsPagesService', () => {
  let service: ProductsPagesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductsPagesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
