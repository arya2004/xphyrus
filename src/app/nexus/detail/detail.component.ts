import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { NexusService } from '../nexus.service';


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
    this.getAllCompany();
    console.log(this.company)
   
  }



  syndicateId: number = 123; // Replace with your dynamic value
assignmentId: number = 456; // Replace with your dynamic value
syndicatePath: string = `/syndicate/${this.syndicateId}/assignment/${this.assignmentId}`;
company: ICompany[]  = [] ;

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  getAllCompany()
  {
    this.companyService.GetNexus().subscribe({
      next: res => {
      this.company = res.result;
      this.dtTrigger.next(null);
    },
  
    error: err => console.log(err)
  });
  }
}
