import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodingQuestionCreateComponent } from './coding-question-create.component';

describe('CodingQuestionCreateComponent', () => {
  let component: CodingQuestionCreateComponent;
  let fixture: ComponentFixture<CodingQuestionCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CodingQuestionCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CodingQuestionCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
