import { TestBed } from '@angular/core/testing';

import { NexusService } from './nexus.service';

describe('NexusService', () => {
  let service: NexusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NexusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
