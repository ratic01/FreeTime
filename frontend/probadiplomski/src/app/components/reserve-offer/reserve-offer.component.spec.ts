import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReserveOfferComponent } from './reserve-offer.component';

describe('ReserveOfferComponent', () => {
  let component: ReserveOfferComponent;
  let fixture: ComponentFixture<ReserveOfferComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReserveOfferComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReserveOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
