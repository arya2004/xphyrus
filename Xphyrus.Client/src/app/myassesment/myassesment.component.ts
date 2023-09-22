import { Component, OnInit } from '@angular/core';
import { IAssesmentAdminHome } from '../shared/models/IAssesmentAdminHome';
import { AssesmentService } from '../assesment/assesment.service';

@Component({
  selector: 'app-myassesment',
  templateUrl: './myassesment.component.html',
  styleUrls: ['./myassesment.component.scss']
})
export class MyassesmentComponent implements OnInit {

  assToDisplay: IAssesmentAdminHome[] = [];

  constructor(private assService: AssesmentService) {}

  ngOnInit(): void {
    this.assService.getMyCreated().subscribe({
      next: item => {this.assToDisplay = item.result;
        console.log(this.assToDisplay);
        for(let a of this.assToDisplay){
          console.log(a.assesmentId);
          
        }
        
      }
     // error: err => this.errors = err.errors
    })
  }

}
