import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { NexusService } from '../nexus.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INexusDashboard } from 'src/app/shared/models/INexus';

/**
 * Component for managing the Nexus dashboard.
 */
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
  company: ICompany[] = [];
  nexus: INexusDashboard[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  newNexusForm: FormGroup;

  constructor(
    private companyService: NexusService, 
    private fb: FormBuilder, 
    private router: Router
  ) {
    // Initialize form
    this.newNexusForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', [Validators.required]],
    });
  }

  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
    this.getAllCompany();
  }

  /**
   * Lifecycle hook that is called when the component is destroyed.
   */
  ngOnDestroy(): void {
    // Complete the DataTable trigger to avoid memory leaks
    this.dtTrigger.unsubscribe();
  }

  /**
   * Fetch all companies from the service.
   */
  getAllCompany(): void {
    this.companyService.getNexus().subscribe({
      next: res => {
        this.nexus = res.result;
        this.dtTrigger.next(null);
      },
      error: err => {
        console.error('Error fetching companies:', err);
        alert('There was an error fetching the companies. Please try again later.');
      }
    });
  }

  /**
   * Delete a company by its ID.
   * @param id - The ID of the company to delete
   */
  deleteCompany(id: string): void {
    if (confirm('Are you sure you want to delete this company?')) {
      this.companyService.deleteNexus(id).subscribe({
        next: res => {
          console.log('Company deleted:', res);
          window.location.reload();  
        },
        error: err => {
          console.error('Error deleting company:', err);
          alert('There was an error deleting the company. Please try again later.');
        }
      });
    }
  }

  /**
   * Handle the creation of a new Nexus.
   */
  onNewNexusCreate(): void {
    if (this.newNexusForm.valid) {
      console.log('Form Value:', this.newNexusForm.value);

      this.companyService.postNexus(this.newNexusForm.value).subscribe({
        next: () =>  window.location.reload(),
        error: err => {
          console.error('Error creating Nexus:', err);
          alert('There was an error creating the Nexus. Please try again later.');
        }
      });
    } else {
      alert('Please fill out all required fields correctly.');
    }
  }
}
