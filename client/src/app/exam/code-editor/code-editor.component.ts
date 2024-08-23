import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DiffEditorModel, NgxEditorModel } from 'ngx-monaco-editor-v2';
import { map, Observable, Subscription } from 'rxjs';
import { Examinee, IExaminee } from 'src/app/shared/models/IExaminee';
import { ITestRun, TestRun } from 'src/app/shared/models/ITestRun';
import { CodingQuestion, ExamService, StartTestResponseDto, SubmitQuestionDto, Test } from '../exam.service';

declare var monaco: any;


@Component({
  selector: 'app-code-editor',
  templateUrl: './code-editor.component.html',
  styleUrls: ['./code-editor.component.scss']
})
export class CodeEditorComponent implements OnInit, OnDestroy {
  id: string;
  questionId: string;
  testId: string;
  
  private sub: Subscription;

  test$: Observable<StartTestResponseDto | null>;
  question: CodingQuestion | null = null;
  title: string = '';
  description: string = '';

 
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
   above: FormGroup;

  languageForm: FormGroup;
  languages = [
  
 
    { id: 50, name: "C", version: "GCC 9.2.0" },
    { id: 54, name: "C++", version: "GCC 9.2.0" },

    { id: 51, name: "C#", version: "Mono 6.6.0.161" },
   
    { id: 62, name: "Java", version: "OpenJDK 13.0.1" },
  

    { id: 71, name: "Python", version: "3.8.1" },
    
  ];

  examForm: FormGroup;
  currentTest: StartTestResponseDto | null = null;
  responseOutput: any;
  private testSubscription: Subscription;

  constructor(
    private fb: FormBuilder,
    private studentService: ExamService,
    private route: ActivatedRoute,
    private router: Router,
    private examService: ExamService
  ) {
   

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
      this.questionId = params['questionId'];
      this.testId = params['testId'];
      //this.loadAssessment(this.id);
      //this.fetchTests();
    });
    this.testSubscription = this.examService.getTest().subscribe(testData => {
      this.currentTest = testData;
      if (testData) {
        console.log('Test data updated:', testData);
      }
    });
    this.test$ = this.examService.getTest();
    this.test$.pipe(
      map((test: any) => {
        const questionId = this.route.snapshot.paramMap.get('questionId');
        if (test && questionId) {
          this.question = test.test.codingQuestions.find((q: { codingQuestionId: string; }) => q.codingQuestionId === questionId) || null;
          if (this.question) {
            this.title = this.question.title;
            this.description = this.question.description; // Assuming the description is within the questionText or elsewhere in the question object
          }
        }
      })
    ).subscribe();

  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
    if (this.testSubscription) {
      this.testSubscription.unsubscribe();
    }
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


  onSubmitQuestion(): void {
    
    ;
    if (this.currentTest) {
      const submitQuestionDto: SubmitQuestionDto = {
        submittedCode: this.code,
        marksAwarded: 0 // Initially zero, to be updated later
      };

      this.examService.submitQuestion(submitQuestionDto, this.questionId).subscribe({
        next: data => {
          if (data.isSuccess) {
            console.log('Question submitted successfully:', data.result);
          } else {
            console.error('Failed to submit question:', data.message);
          }
        },
        error: err => {
          console.error('Question submission failed:', err);
        }
      });
    } else {
      console.error('No test in progress');
    }
  }

  onSubmitTest(): void {
    if (this.currentTest) {
      const testId: string = this.currentTest.test.testId;

      this.examService.submitTest(testId).subscribe({
        next: data => {
          if (data.isSuccess) {
            console.log('Test submitted successfully');
          } else {
            console.error('Failed to submit test:', data.message);
          }
        },
        error: err => {
          console.error('Test submission failed:', err);
        }
      });
    } else {
      console.error('No test in progress');
    }
  }
}