import { TestBed } from '@angular/core/testing';

import { SideMenuNavbarService } from './side-menu-navbar.service';

describe('SideMenuNavbarService', () => {
  let service: SideMenuNavbarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SideMenuNavbarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
