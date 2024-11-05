import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAllreservationsComponent } from './view-allreservations.component';

describe('ViewAllreservationsComponent', () => {
  let component: ViewAllreservationsComponent;
  let fixture: ComponentFixture<ViewAllreservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewAllreservationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewAllreservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
