import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Subject, Subscription } from 'rxjs';
import { NexusService } from 'src/app/nexus/nexus.service';
import { AssessmentService } from '../assessment.service';
import { IAssessmentDetail } from 'src/app/shared/models/IAssessment';
import { ICodingAssessmentResult } from 'src/app/shared/models/IResults';
import { ITestCase } from 'src/app/shared/models/ITestCase';

declare var monaco: any;

/**
 * Component for displaying and managing the dashboard.
 */
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
  nexusId!: string;
  assessmentId!: string;
  private sub: Subscription | null = null;

  // Example long text for display
  longText = `The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog
  from Japan. A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was
  originally bred for hunting.`;

  description = "<p>Write a function to find the longest common prefix string amongst an array of strings...</p>";
  
  assessment!: IAssessmentDetail;
  assResult: ICodingAssessmentResult[] = [];
  testCaseArray: ITestCase[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  // Form for new Nexus
  newNexusForm: FormGroup;

  constructor(
    private fb: FormBuilder, 
    private nexusService: NexusService, 
    private assessmentService: AssessmentService, 
    private route: ActivatedRoute, 
    private router: Router
  ) {
    // Initialize form
    this.newNexusForm = this.fb.group({
      inputCase: ['', Validators.required],
      outputCase: ['', [Validators.required]],
      associatedCodingAssessment: ['']
    });
  }

  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
  ngOnInit(): void {
    // DataTable options
    this.dtOptions = {
      pagingType: 'full_numbers'
    };

    // Subscribe to route parameters
    this.sub = this.route.params.subscribe(params => {
      this.nexusId = params['id'];
      this.assessmentId = params['codingAssessmentId'];
    });

    this.getAssessment();
    this.getResults();
    this.patchFormValues();
    this.getAllTestCases();
  }

  /**
   * Lifecycle hook that is called when the component is destroyed.
   */
  ngOnDestroy(): void {
    // Unsubscribe from route params to avoid memory leaks
    if (this.sub) {
      this.sub.unsubscribe();
    }

    // Complete the DataTable trigger to avoid memory leaks
    this.dtTrigger.unsubscribe();
  }

  /**
   * Patch form values with the provided values.
   */
  patchFormValues(): void {
    const patchedValues = {
      associatedCodingAssessment: this.assessmentId
    };

    this.newNexusForm.patchValue(patchedValues);
  }

  /**
   * Fetch assessment details.
   */
  getAssessment(): void {
    this.assessmentService.getOneAssessment(this.assessmentId).subscribe({
      next: res => {
        this.assessment = res.result;
      },
      error: err => {
        console.error('Error fetching assessment:', err);
        alert('There was an error fetching the assessment details. Please try again later.');
      }
    });
  }

  /**
   * Fetch assessment results.
   */
  getResults(): void {
    this.assessmentService.getResults(this.assessmentId).subscribe({
      next: res => {
        this.assResult = res.result;
        this.dtTrigger.next(null);
      },
      error: err => {
        console.error('Error fetching results:', err);
        alert('There was an error fetching the results. Please try again later.');
      }
    });
  }

  /**
   * Fetch all associated test cases.
   */
  getAllTestCases(): void {
    this.assessmentService.getAssociatedTestCase(this.assessmentId).subscribe({
      next: res => {
        this.testCaseArray = res.result;
        console.log(this.testCaseArray);
      },
      error: err => {
        console.error('Error fetching test cases:', err);
        alert('There was an error fetching the test cases. Please try again later.');
      }
    });
  }

  /**
   * Handle the creation of a new Nexus.
   */
  onNewNexusCreate(): void {
    if (this.newNexusForm.valid) {
      console.log(this.newNexusForm.value);

      this.assessmentService.postTestCase(this.newNexusForm.value).subscribe({
        next: () => window.location.reload(),
        error: err => {
          console.error('Error creating new Nexus:', err);
          alert('There was an error creating the new Nexus. Please try again later.');
        }
      });
    } else {
      alert('Please fill out all required fields correctly.');
    }
  }
}
