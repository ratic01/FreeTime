import { TestBed } from '@angular/core/testing';

import { TourPackageServiceService } from './tour-package-service.service';

describe('TourPackageServiceService', () => {
  let service: TourPackageServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TourPackageServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
