import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { NexusService } from '../nexus.service';
import { IAssessmentIndex } from 'src/app/shared/models/IAssessment';


@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent {
  id: string;
  private sub: any;
  constructor(private route: ActivatedRoute,public companyService: NexusService) { }
  




  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
    this.id = params['id']; // (+) converts string 'id' to a number
      console.log(this.id);
    });
    this.dtOptions = {
      pagingType: 'full_numbers'
    };


    this.getAllAss()
    console.log(this.assessment);
    
  }



company: ICompany[]  = [] ;
assessment: IAssessmentIndex[] = [];

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  getAllAss()
  {
    this.companyService.getAssociatedAssessment(this.id).subscribe({
      next: res => {
      this.assessment = res.result;
      this.dtTrigger.next(null);
    },
  
    error: err => console.log(err)
  });
  }


}
