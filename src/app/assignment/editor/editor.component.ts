import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Assignment, IAssignment } from 'src/app/shared/models/IAssesmentCreate';

import { AngularEditorConfig } from '@kolkov/angular-editor';

import { ITestRun, TestRun } from 'src/app/shared/models/ITestRun';
import { StudentService } from 'src/app/student/student.service';
import { DiffEditorModel, NgxEditorModel } from 'ngx-monaco-editor-v2';




declare var monaco: any;

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnInit {
 
  id: string;
  private sub: any;
  constructor(private fb:FormBuilder, private studentService: StudentService,private route: ActivatedRoute) {}

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
  jsCode = "function hello() {\n\t console.log(\"HEllo\")\n}\nhello()";

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
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id']; // (+) converts string 'id' to a number
        console.log(this.id);
        
      // In a real app: dispatch action to load the details here.
   });
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
    const lessonForm = this.fb.group({
      input: ['', Validators.required],
      output: ['', Validators.required]
    });
    this.cases.push(lessonForm);
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
    input: [''],
    output: ['']
  })
  form = this.fb.group({

    cases: this.fb.array([])
  });


  get cases() {
    return this.form.controls["cases"] as FormArray;
  }

  responseOutput: object =  {};

  favoriteSeason: string = 'C';
  seasons: string[] = ['C', 'C++', 'Java', 'Python'];

 


  testRun()
  { 
    const coding: ITestRun = new TestRun();
    coding.source_code = this.code;

    if (this.favoriteSeason == "C") {
      coding.language_id = 49;
    }
    if (this.favoriteSeason == "C++") {
      coding.language_id = 53;
    }
    if (this.favoriteSeason == "Java") {
      coding.language_id = 62;
    }

    if (this.favoriteSeason == "Python") {
      coding.language_id = 71;
    }
    coding.stdin = this.above.value.input
    coding.exprected_output = this.above.value.output
 
    console.log(JSON.stringify(coding));
    this.studentService.testRun(coding).subscribe({
      next: (data) => {this.responseOutput = data.result
        console.log(data.result)
      }
      
    })
  }

  onSubmit()
  { 
    const coding: ITestRun = new TestRun();
    coding.source_code = this.code;

    if (this.favoriteSeason == "C") {
      coding.language_id = 49;
    }
    if (this.favoriteSeason == "C++") {
      coding.language_id = 53;
    }
    if (this.favoriteSeason == "Java") {
      coding.language_id = 62;
    }

    if (this.favoriteSeason == "Python") {
      coding.language_id = 71;
    }
    coding.stdin = this.above.value.input
    coding.exprected_output = this.above.value.output
 
    console.log(JSON.stringify(coding));
    this.studentService.testRun(coding).subscribe({
      next: (data) => {this.responseOutput = data.result
        console.log(data.result)
      }
      
    })
  }

}
