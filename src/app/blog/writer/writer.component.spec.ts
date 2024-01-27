import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WriterComponent } from './writer.component';

describe('WriterComponent', () => {
  let component: WriterComponent;
  let fixture: ComponentFixture<WriterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WriterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WriterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
