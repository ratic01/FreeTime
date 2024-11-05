import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PackageQuantityDialogComponent } from './package-quantity-dialog.component';

describe('PackageQuantityDialogComponent', () => {
  let component: PackageQuantityDialogComponent;
  let fixture: ComponentFixture<PackageQuantityDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PackageQuantityDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PackageQuantityDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
