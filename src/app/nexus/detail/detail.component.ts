import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { NexusService } from '../nexus.service';
import { IAssessmentIndex } from 'src/app/shared/models/IAssessment';

/**
 * Component for displaying the details of a specific company and its associated assessments.
 */
@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit, OnDestroy {
  id!: string;
  company: ICompany[] = [];
  assessment: IAssessmentIndex[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  private sub: Subscription | null = null;

  constructor(
    private route: ActivatedRoute,
    public companyService: NexusService
  ) {}

  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
  ngOnInit(): void {
    // Subscribe to route parameters
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      console.log('Company ID:', this.id);
    });

    // Initialize DataTable options
    this.dtOptions = {
      pagingType: 'full_numbers'
    };

    // Fetch associated assessments
    this.getAllAssessments();
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
   * Fetch all assessments associated with the company.
   */
  getAllAssessments(): void {
    this.companyService.getAssociatedAssessment(this.id).subscribe({
      next: res => {
        this.assessment = res.result;
        this.dtTrigger.next(null);
      },
      error: err => {
        console.error('Error fetching assessments:', err);
        alert('There was an error fetching the assessments. Please try again later.');
      }
    });
  }
}
