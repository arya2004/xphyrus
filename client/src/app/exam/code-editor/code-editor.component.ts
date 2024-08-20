import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DiffEditorModel, NgxEditorModel } from 'ngx-monaco-editor-v2';
import { Subscription } from 'rxjs';
import { Examinee, IExaminee } from 'src/app/shared/models/IExaminee';
import { ITestRun, TestRun } from 'src/app/shared/models/ITestRun';
import { ExamService } from '../exam.service';

declare var monaco: any;


@Component({
  selector: 'app-code-editor',
  templateUrl: './code-editor.component.html',
  styleUrls: ['./code-editor.component.scss']
})
export class CodeEditorComponent implements OnInit, OnDestroy {
  id: string;
  title: string;
  private sub: Subscription;
  submissionForm: FormGroup;
  codeInput: string = 'Sample Code';
  editor: any;
  diffEditor: any;
  showMultiple: boolean = false;
  toggleLanguage: boolean = true;
  options = {
    theme: 'vs-dark'
  };
  code: string;
  cssCode: string = `.my-class { color: red; }`;
  jsCode: string = `function hello() {\n\tconsole.log("Hello")\n}\nhello()`;
  originalModel: DiffEditorModel = {
    code: 'heLLo world!',
    language: 'text/plain'
  };
  modifiedModel: DiffEditorModel = {
    code: 'hello orlando!',
    language: 'text/plain'
  };
  jsonCode: string = [
    '{',
    '    "p1": "v3",',
    '    "p2": false',
    '}'
  ].join('\n');
  model: NgxEditorModel = {
    value: this.jsonCode,
    language: 'json'
  };
  description: string = "<p>Write a function to find the longest common prefix string amongst an array of strings.</p><p>If there is no common prefix, return an empty string&#160;<code>&#34;&#34;</code>.</p><p>&#160;</p><p><span class=\"example\">Example 1:</span></p><pre><span>Input:</span> strs = [&#34;flower&#34;,&#34;flow&#34;,&#34;flight&#34;]&#10;<span>Output:</span> &#34;fl&#34;&#10;</pre><p><span class=\"example\">Example 2:</span></p><pre><span>Input:</span> strs = [&#34;dog&#34;,&#34;racecar&#34;,&#34;car&#34;]&#10;<span>Output:</span> &#34;&#34;&#10;<span>Explanation:</span> There is no common prefix among the input strings.&#10;</pre><p>&#160;</p><p><span>Constraints:</span></p><ul><li><code>1 &lt;= strs.length &lt;= 200</code></li><li><code>0 &lt;= strs[i].length &lt;= 200</code></li><li><code>strs[i]</code>&#160;consists of only lowercase English letters.</li></ul>";
  above: FormGroup;
  responseOutput: object = {};
  languageForm: FormGroup;
  languages = [
  
 
    { id: 50, name: "C", version: "GCC 9.2.0" },
    { id: 54, name: "C++", version: "GCC 9.2.0" },

    { id: 51, name: "C#", version: "Mono 6.6.0.161" },
   
    { id: 62, name: "Java", version: "OpenJDK 13.0.1" },
  

    { id: 71, name: "Python", version: "3.8.1" },
    
  ];

  constructor(
    private fb: FormBuilder,
    private studentService: ExamService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.submissionForm = this.fb.group({
      name: ['', Validators.required],
      linkedin: [''],
      twitter: [''],
      email: ['', [Validators.required, Validators.email]],
      language: ['']
    });

    this.above = this.fb.group({
      input: [''],
      output: ['']
    });

    this.languageForm = this.fb.group({
      selectedLanguage: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.updateOptions();
    this.sub = this.route.params.subscribe(params => {
      this.id = params['questionId'];
      this.loadAssessment(this.id);
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  /**
   * Load assessment details by ID
   * @param id Assessment ID
   */
  private loadAssessment(id: string): void {
    this.studentService.getOneAssessment(id).subscribe({
      next: data => {
        this.title = data.result.title;
        this.description = data.result.description;
      },
      error: err => {
        console.error('Failed to load assessment:', err);
      }
    });
  }

  /**
   * Update editor options
   */
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

  /**
   * Initialize the editor
   * @param editor Editor instance
   */
  onInit(editor: any): void {
    this.editor = editor;
    this.model = {
      value: this.jsonCode,
      language: 'json',
      uri: monaco.Uri.parse('a://b/foo.json')
    };
  }

  /**
   * Handle test run action
   */
  testRun(): void {
    if (this.languageForm.invalid) {
      console.error('Language form is invalid');
      return;
    }

    const coding: ITestRun = new TestRun();
    coding.source_code = this.code;
    const selectedLanguageId = this.languageForm.value.selectedLanguage;
    const selectedLanguage = this.languages.find(lang => lang.id === selectedLanguageId);

    if (!selectedLanguage) {
      console.error('Selected language not found');
      return;
    }

    coding.stdin = this.above.value.input;
    coding.exprected_output = this.above.value.output;

    this.studentService.testRun(coding).subscribe({
      next: data => {
        this.responseOutput = data.result;
        console.log(data.result);
      },
      error: err => {
        console.error('Test run failed:', err);
      }
    });
  }

  /**
   * Handle form submission
   */
  onSubmit(): void {
    const coding: ITestRun = new TestRun();
    coding.source_code = this.code;
    coding.stdin = this.above.value.input;
    coding.exprected_output = this.above.value.output;

    this.studentService.testRun(coding).subscribe({
      next: data => {
        this.responseOutput = data.result;
        console.log(data.result);
      },
      error: err => {
        console.error('Submission failed:', err);
      }
    });
  }

  /**
   * Handle code submission
   */
  onCodeSubmit(): void {
    const coding: IExaminee = new Examinee();
    coding.source_code = this.code;
    coding.email = this.submissionForm.value.email || '';
    coding.linkedIn = this.submissionForm.value.linkedin || '';
    coding.name = this.submissionForm.value.name || '';
    coding.twitter = this.submissionForm.value.twitter || '';
    coding.language = this.languageForm.value.selectedLanguage || '';
    coding.input = this.above.value.input || '';
    coding.assessmentId = this.id;

    this.studentService.submitRun(coding).subscribe({
      next: () => {
        this.router.navigateByUrl('');
      },
      error: err => {
        console.error('Code submission failed:', err);
        this.router.navigateByUrl('');
      }
    });
  }
}