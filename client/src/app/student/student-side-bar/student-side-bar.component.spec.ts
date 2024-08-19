import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentSideBarComponent } from './student-side-bar.component';

describe('StudentSideBarComponent', () => {
  let component: StudentSideBarComponent;
  let fixture: ComponentFixture<StudentSideBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentSideBarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentSideBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
