import { Component, OnInit } from '@angular/core';
import { IAssesmentAdminHome } from '../shared/models/IAssesmentAdminHome';
import { AssesmentService } from './assesment.service';

@Component({
  selector: 'app-assesment',
  templateUrl: './assesment.component.html',
  styleUrls: ['./assesment.component.scss']
})
export class AssesmentComponent implements OnInit {
  assToDisplay: IAssesmentAdminHome[] = [];

  constructor(private assService: AssesmentService) {}

  ngOnInit(): void {
    this.assService.getMyCreated().subscribe({
      next: item => this.assToDisplay = item.result
     // error: err => this.errors = err.errors
    })
  }

 



}
