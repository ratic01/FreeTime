import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationHistoryDialogComponent } from './reservation-history-dialog.component';

describe('ReservationHistoryDialogComponent', () => {
  let component: ReservationHistoryDialogComponent;
  let fixture: ComponentFixture<ReservationHistoryDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReservationHistoryDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationHistoryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
