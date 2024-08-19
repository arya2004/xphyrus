import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamSideBarComponent } from './exam-side-bar.component';

describe('ExamSideBarComponent', () => {
  let component: ExamSideBarComponent;
  let fixture: ComponentFixture<ExamSideBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExamSideBarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExamSideBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
