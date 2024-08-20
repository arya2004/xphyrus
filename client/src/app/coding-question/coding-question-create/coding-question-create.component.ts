import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Subscription } from 'rxjs';
import { MarkdownModule } from 'ngx-markdown';
@Component({
  selector: 'app-coding-question-create',
  templateUrl: './coding-question-create.component.html',
  styleUrls: ['./coding-question-create.component.scss']
})
export class CodingQuestionCreateComponent  implements OnInit, OnDestroy {
  id!: string;
  private sub: Subscription | null = null;

  codingQuestionForm: FormGroup;

  
  constructor(
    private fb: FormBuilder, 
    private router: Router, 
    private route: ActivatedRoute
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
