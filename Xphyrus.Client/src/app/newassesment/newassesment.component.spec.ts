import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewassesmentComponent } from './newassesment.component';

describe('NewassesmentComponent', () => {
  let component: NewassesmentComponent;
  let fixture: ComponentFixture<NewassesmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewassesmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewassesmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
