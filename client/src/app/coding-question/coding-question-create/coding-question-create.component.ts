import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Subscription } from 'rxjs';
import { MarkdownModule } from 'ngx-markdown';
import { CodingQuestion, CodingQuestionService } from '../coding-question.service';
@Component({
  selector: 'app-coding-question-create',
  templateUrl: './coding-question-create.component.html',
  styleUrls: ['./coding-question-create.component.scss']
})
export class CodingQuestionCreateComponent  implements OnInit, OnDestroy {
  id!: string;
  private sub: Subscription | null = null;
  testId: string;
  codingQuestionForm: FormGroup;

  
  constructor(
    private fb: FormBuilder, 
    private router: Router, 
    private route: ActivatedRoute,
    private codingQuestionService: CodingQuestionService,
  ) {
  
 
  }

  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
  ngOnInit(): void {
    // Subscribe to route parameters
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      console.log(this.id);

  
    });
    this.codingQuestionForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      difficulty: [1],  // Default to Easy
    });
    this.testId = this.route.snapshot.paramMap.get('testId') || '';
  }

  /**
   * Lifecycle hook that is called when the component is destroyed.
   */
  ngOnDestroy(): void {
    // Unsubscribe from route params to avoid memory leaks
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }

  /**
   * Handles form submission.
   */
  onSubmit(): void {
    if (this.codingQuestionForm.valid) {
      let codingQuestion: any = {
        ...this.codingQuestionForm.value,
        testId: this.testId
      };
      codingQuestion.difficulty = parseInt(codingQuestion.difficulty, 10);
      console.log('Creating coding question:', codingQuestion);

      this.codingQuestionService.createCodingQuestion(codingQuestion).subscribe({
        next: data => {
          if (data.isSuccess) {
            console.log('Coding question created successfully:', data.result);
            window.location.reload()// Refresh the list of coding questions after creating a new one
          } else {
            console.error('Failed to create coding question:', data.message);
          }
        },
        error: err => {
          console.error('Creation failed:', err);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
  }



