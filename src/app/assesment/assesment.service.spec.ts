import { TestBed } from '@angular/core/testing';

import { AssesmentService } from './assesment.service';

describe('AssesmentService', () => {
  let service: AssesmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AssesmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
