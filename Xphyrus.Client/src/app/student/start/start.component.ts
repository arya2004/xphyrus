import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Assignment, IAssignment } from 'src/app/shared/models/IAssesmentCreate';
import { StudentService } from '../student.service';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { DiffEditorModel, NgxEditorModel } from 'ngx-monaco-editor-v2';

declare var monaco: any;
@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {

  codeInput = 'Sample Code';
  editor: any;
  diffEditor: any;
  showMultiple = false;
  toggleLanguage = true;
  options = {
    theme: 'vs-dark'
  };
  code: string;
  cssCode = `.my-class {
  color: red;
}`;
  jsCode = `function hello() {
	 alert('Hello world!');
}`;

  originalModel: DiffEditorModel = {
    code: 'heLLo world!',
    language: 'text/plain'
  };

  modifiedModel: DiffEditorModel = {
    code: 'hello orlando!',
    language: 'text/plain'
  };

  jsonCode = [
    '{',
    '    "p1": "v3",',
    '    "p2": false',
    '}'
  ].join('\n');

  model: NgxEditorModel = {
    value: this.jsonCode,
    language: 'json'
  };

  ngOnInit() {
    this.updateOptions();
  }

  updateOptions() {
    this.toggleLanguage = !this.toggleLanguage;
    if (this.toggleLanguage) {
      this.code = this.cssCode;
      this.options = Object.assign({}, this.options, { language: 'c' });
    } else {
      this.code = this.jsCode;
      this.options = Object.assign({}, this.options, { language: 'javascript' });
    }

  }

 

  onInit(editor : any) {
    this.editor = editor;
    console.log(editor);
    this.model = {
      value: this.jsonCode,
      language: 'json',
      uri: monaco.Uri.parse('a://b/foo.json')
    };
    // let line = editor.getPosition();
    // let range = new monaco.Range(line.lineNumber, 1, line.lineNumber, 1);
    // let id = { major: 1, minor: 1 };
    // let text = 'FOO';
    // let op = { identifier: id, range: range, text: text, forceMoveMarkers: true };
    // editor.executeEdits("my-source", [op]);
    this.addLesson();
  }

  constructor(private fb:FormBuilder, private teacherService: StudentService) {}

  

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
    
      
  }
}
