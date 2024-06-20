import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DiffEditorModel, NgxEditorModel } from 'ngx-monaco-editor-v2';
import { ExamineeService } from '../examinee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ITestRun, TestRun } from 'src/app/shared/models/ITestRun';
import { Examinee, IExaminee } from 'src/app/shared/models/IExaminee';
import { Subscription } from 'rxjs';

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
    { id: 45, name: "Assembly", version: "NASM 2.14.02" },
    { id: 46, name: "Bash", version: "5.0.0" },
    { id: 47, name: "Basic", version: "FBC 1.07.1" },
    { id: 75, name: "C", version: "Clang 7.0.1" },
    { id: 76, name: "C++", version: "Clang 7.0.1" },
    { id: 48, name: "C", version: "GCC 7.4.0" },
    { id: 52, name: "C++", version: "GCC 7.4.0" },
    { id: 49, name: "C", version: "GCC 8.3.0" },
    { id: 53, name: "C++", version: "GCC 8.3.0" },
    { id: 50, name: "C", version: "GCC 9.2.0" },
    { id: 54, name: "C++", version: "GCC 9.2.0" },
    { id: 86, name: "Clojure", version: "1.10.1" },
    { id: 51, name: "C#", version: "Mono 6.6.0.161" },
    { id: 77, name: "COBOL", version: "GnuCOBOL 2.2" },
    { id: 55, name: "Common Lisp", version: "SBCL 2.0.0" },
    { id: 56, name: "D", version: "DMD 2.089.1" },
    { id: 57, name: "Elixir", version: "1.9.4" },
    { id: 58, name: "Erlang", version: "OTP 22.2" },
    { id: 44, name: "Executable", version: "" },
    { id: 87, name: "F#", version: ".NET Core SDK 3.1.202" },
    { id: 59, name: "Fortran", version: "GFortran 9.2.0" },
    { id: 60, name: "Go", version: "1.13.5" },
    { id: 88, name: "Groovy", version: "3.0.3" },
    { id: 61, name: "Haskell", version: "GHC 8.8.1" },
    { id: 62, name: "Java", version: "OpenJDK 13.0.1" },
    { id: 63, name: "JavaScript", version: "Node.js 12.14.0" },
    { id: 78, name: "Kotlin", version: "1.3.70" },
    { id: 64, name: "Lua", version: "5.3.5" },
    { id: 89, name: "Multi-file program", version: "" },
    { id: 79, name: "Objective-C", version: "Clang 7.0.1" },
    { id: 65, name: "OCaml", version: "4.09.0" },
    { id: 66, name: "Octave", version: "5.1.0" },
    { id: 67, name: "Pascal", version: "FPC 3.0.4" },
    { id: 85, name: "Perl", version: "5.28.1" },
    { id: 68, name: "PHP", version: "7.4.1" },
    { id: 43, name: "Plain Text", version: "" },
    { id: 69, name: "Prolog", version: "GNU Prolog 1.4.5" },
    { id: 70, name: "Python", version: "2.7.17" },
    { id: 71, name: "Python", version: "3.8.1" },
    { id: 80, name: "R", version: "4.0.0" },
    { id: 72, name: "Ruby", version: "2.7.0" },
    { id: 73, name: "Rust", version: "1.40.0" },
    { id: 81, name: "Scala", version: "2.13.2" },
    { id: 82, name: "SQL", version: "SQLite 3.27.2" },
    { id: 83, name: "Swift", version: "5.2.3" },
    { id: 74, name: "TypeScript", version: "3.7.4" },
    { id: 84, name: "Visual Basic.Net", version: "vbnc 0.0.0.5943" }
  ];

  constructor(
    private fb: FormBuilder,
    private studentService: ExamineeService,
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
      this.id = params['id'];
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
