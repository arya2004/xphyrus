import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Assignment, IAssignment } from 'src/app/shared/models/IAssesmentCreate';

import { AngularEditorConfig } from '@kolkov/angular-editor';

import { ITestRun, TestRun } from 'src/app/shared/models/ITestRun';

import { DiffEditorModel, NgxEditorModel } from 'ngx-monaco-editor-v2';
import { AssignmentService } from '../assignment.service';




declare var monaco: any;

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnInit {
 
  id: string;
  private sub: any;
  constructor(private fb:FormBuilder, private studentService: AssignmentService,private route: ActivatedRoute) {}

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


  description = "<p>Write a function to find the longest common prefix string amongst an array of strings.</p><p>If there is no common prefix, return an empty string&#160;<code>&#34;&#34;</code>.</p><p>&#160;</p><p><span class=\"example\">Example 1:</span></p><pre><span>Input:</span> strs = [&#34;flower&#34;,&#34;flow&#34;,&#34;flight&#34;]&#10;<span>Output:</span> &#34;fl&#34;&#10;</pre><p><span class=\"example\">Example 2:</span></p><pre><span>Input:</span> strs = [&#34;dog&#34;,&#34;racecar&#34;,&#34;car&#34;]&#10;<span>Output:</span> &#34;&#34;&#10;<span>Explanation:</span> There is no common prefix among the input strings.&#10;</pre><p>&#160;</p><p><span>Constraints:</span></p><ul><li><code>1 &lt;= strs.length &lt;= 200</code></li><li><code>0 &lt;= strs[i].length &lt;= 200</code></li><li><code>strs[i]</code>&#160;consists of only lowercase English letters.</li></ul>"  ;
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
