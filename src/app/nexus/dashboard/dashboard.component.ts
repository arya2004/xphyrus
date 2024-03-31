import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { NexusService } from '../nexus.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  company: ICompany[]  = [] ;
  constructor(public companyService: NexusService) { }
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
    this.getAllCompany();
    console.log(this.company)
   
  }

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


  deleteCompany(id: number)
  {
    console.log(id);
    this.companyService.DeleteNexus(id).subscribe({
      next: res => {
        console.log(res);
        window.location.reload();
      },
      error: err => console.log(err)
    });
  }
}
