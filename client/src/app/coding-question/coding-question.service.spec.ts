import { TestBed } from '@angular/core/testing';

import { CodingQuestionService } from './coding-question.service';

describe('CodingQuestionService', () => {
  let service: CodingQuestionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CodingQuestionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
