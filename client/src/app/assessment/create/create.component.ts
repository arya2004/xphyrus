import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Subscription } from 'rxjs';
import { AssessmentService } from '../assessment.service';

/**
 * Component for creating a new assignment.
 */
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit, OnDestroy {
  id!: string;
  private sub: Subscription | null = null;
  description = "";
  selectedOption: string = "student";
  registerForm: FormGroup;

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
    private assessmentService: AssessmentService, 
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
    if (this.registerForm.valid) {
      const formValue = this.registerForm.value;

      // Convert date fields to ISO string format
      formValue.startDate = new Date(formValue.startDate).toISOString();
      formValue.endDate = new Date(formValue.endDate).toISOString();

      // Log form value for debugging
      console.log(formValue);

      // Post form data to the server
      this.assessmentService.postNexus(formValue).subscribe({
        next: () => this.router.navigateByUrl('/Syndicate'),
        error: (err) => {
          // Handle error
          console.error('Error posting nexus:', err);
          alert('There was an error creating the assignment. Please try again later.');
        }
      });
    } else {
      // Handle form invalid case
      alert('Please fill out all required fields correctly.');
    }
  }
}
