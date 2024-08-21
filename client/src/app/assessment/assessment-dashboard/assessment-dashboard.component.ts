import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import {  Difficulty } from 'src/app/shared/models/CodingQuestion';
import { INexusDashboard } from 'src/app/shared/models/INexus';
import { Test } from 'src/app/shared/models/Test';
import { Question } from 'src/app/shared/question';
import { AssessmentService, CodingQuestion } from '../assessment.service';


@Component({
  selector: 'app-assessment-dashboard',
  templateUrl: './assessment-dashboard.component.html',
  styleUrls: ['./assessment-dashboard.component.scss']
})
export class AssessmentDashboardComponent  implements OnInit {
  testForm: FormGroup;
  classroomId: string;
  testId: string;

  questions: CodingQuestion[] = [];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder, 
    private router: Router,
    private codingQuestionService: AssessmentService,
  ) {}


  ngOnInit(): void {
    this.testForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      duration: [0, Validators.required]
    });
 

    this.classroomId = this.route.snapshot.paramMap.get('classroomId') || '';
    this.testId = this.route.snapshot.paramMap.get('testId') || '';

   
    this.fetchCodingQuestions();
  }
  navigateTo(link: string): void {
    this.router.navigateByUrl(link);
  }


  onTestCreate(): void {
    if (this.testForm.valid) {
      const formData: Test = {
        title: this.testForm.value.title,
        description: this.testForm.value.description,
        startDate: new Date(this.testForm.value.startDate),
        endDate: new Date(this.testForm.value.endDate),
        duration: this.testForm.value.duration
      };
   
      console.log('Test created:', formData);

      // Reset the form and close the modal if needed
      this.testForm.reset();
    } else {
      console.log('Form is invalid');
    }
  }


   // Method to generate the link dynamically
   generateLink(questionId: any): string {
    return `/classroom/${this.classroomId}/test/${this.testId}/question/${questionId}`;
  }


 


  fetchCodingQuestions(): void {
    this.codingQuestionService.getCodingQuestionsByTest(this.testId).subscribe({
      next: data => {
        if (data.isSuccess) {
          this.questions = data.result;
          console.log('Coding questions fetched successfully:', this.questions);
        } else {
          console.error('Failed to fetch coding questions:', data.message);
        }
      },
      error: err => {
        console.error('Error fetching coding questions:', err);
      }
    });
  }




  deleteTest(id: string): void {
    // if (confirm('Are you sure you want to delete this company?')) {
    //   this.companyService.deleteNexus(id).subscribe({
    //     next: res => {
    //       console.log('Company deleted:', res);
    //       window.location.reload();  
    //     },
    //     error: err => {
    //       console.error('Error deleting company:', err);
    //       alert('There was an error deleting the company. Please try again later.');
    //     }
    //   });
    // }
  }

}
