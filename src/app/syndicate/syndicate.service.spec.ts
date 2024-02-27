import { TestBed } from '@angular/core/testing';

import { SyndicateService } from './syndicate.service';

describe('SyndicateService', () => {
  let service: SyndicateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SyndicateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
