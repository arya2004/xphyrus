import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodingQuestionDashboardComponent } from './coding-question-dashboard.component';

describe('CodingQuestionDashboardComponent', () => {
  let component: CodingQuestionDashboardComponent;
  let fixture: ComponentFixture<CodingQuestionDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CodingQuestionDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CodingQuestionDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
