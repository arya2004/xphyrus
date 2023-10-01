import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyassesmentComponent } from './myassesment.component';

describe('MyassesmentComponent', () => {
  let component: MyassesmentComponent;
  let fixture: ComponentFixture<MyassesmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyassesmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyassesmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
