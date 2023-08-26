import { TestBed } from '@angular/core/testing';

import { RatingApiServiceService } from './rating-api-service.service';

describe('RatingApiServiceService', () => {
  let service: RatingApiServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RatingApiServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
