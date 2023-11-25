import { Component, OnInit, VERSION, ViewEncapsulation } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { ActivatedRoute } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Assignment, IAssignment } from 'src/app/shared/models/IAssesmentCreate';
import { TeacherService } from '../teacher.service';

@Component({
  selector: 'app-assesment',
  templateUrl: './assesment.component.html',
  styleUrls: ['./assesment.component.scss']
})
export class AssesmentComponent {
 
  constructor(private fb:FormBuilder, private teacherService: TeacherService) {}

  ngOnInit(): void {
    this.addLesson();
  }

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });


  description = "<font face=\"Arial\">sdf<u>sdf</u></font><p><font face=\"Arial\"><u>sdfsfd</u></font></p><p><font face=\"Arial\"><u><sup>jhjvhv</sup></u></font></p><h4><u><font face=\"Comic Sans MS\" size=\"3\"><sup>kjnkjnkj</sup></font></u></h4><h1><u><font face=\"Comic Sans MS\"><sup><font size=\"3\">kjbhvuvvvvvvvvvvv</font><font size=\"6\">jvvjv</font></sup></font></u></h1>";
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
   
  };


  above = this. fb.group({
    title: ['']
  })
  form = this.fb.group({

    cases: this.fb.array([])
  });


  get cases() {
    return this.form.controls["cases"] as FormArray;
  }

  addLesson() {
    const lessonForm = this.fb.group({
      input: ['', Validators.required],
      output: ['', Validators.required]
    });
    this.cases.push(lessonForm);
  }



  onSubmit()
  { 
    const coding: IAssignment = new Assignment();
    coding.title = this.above.value.title
    coding.description = this.description
    coding.startDate = this.range.value.start
    coding.endDate = this.range.value.end
    coding.evaluationCases = this.cases.value
 
    console.log(JSON.stringify(coding));
    
      this.teacherService.postAssignment
  }
}
