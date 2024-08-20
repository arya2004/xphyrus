import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-coding-question-create',
  templateUrl: './coding-question-create.component.html',
  styleUrls: ['./coding-question-create.component.scss']
})
export class CodingQuestionCreateComponent  implements OnInit, OnDestroy {
  id!: string;
  private sub: Subscription | null = null;
  description = "";
  selectedOption: string = "student";
  registerForm: FormGroup;
  codingQuestionForm: FormGroup;

  // Editor configuration
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

  constructor(
    private fb: FormBuilder, 
    private router: Router, 
    private route: ActivatedRoute
  ) {
    // Initialize form
    this.registerForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      associatedNexusId: ['']
    });
  }

  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
  ngOnInit(): void {
    // Subscribe to route parameters
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      console.log(this.id);

      // Update associatedNexusId with the id from route params
      this.registerForm.patchValue({ associatedNexusId: this.id });
    });
    this.codingQuestionForm = this.fb.group({
      title: [''],
      description: [''],
      difficulty: [1],  // Default to Easy
      test: ['']  // Optional
    });
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
      console.log(this.codingQuestionForm.value);
      // Handle the form submission logic here
    }
  }
}
