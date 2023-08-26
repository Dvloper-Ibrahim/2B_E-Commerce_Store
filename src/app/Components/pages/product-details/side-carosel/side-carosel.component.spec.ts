import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideCaroselComponent } from './side-carosel.component';

describe('SideCaroselComponent', () => {
  let component: SideCaroselComponent;
  let fixture: ComponentFixture<SideCaroselComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SideCaroselComponent]
    });
    fixture = TestBed.createComponent(SideCaroselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
