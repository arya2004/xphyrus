import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { NexusService } from '../nexus.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INexusDashboard } from 'src/app/shared/models/INexus';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  company: ICompany[]  = [] ;
  nexus: INexusDashboard[] = [];
  constructor(public companyService: NexusService, private fb:FormBuilder, private router: Router) { }
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
    this.companyService.getNexus().subscribe({
      next: res => {
      this.nexus = res.result;
      this.dtTrigger.next(null);
    },
  
    error: err => console.log(err)
  });
  }


  deleteCompany(id: string)
  {
    console.log(id);
    this.companyService.deleteNexus(id).subscribe({
      next: res => {
        console.log(res);
        window.location.reload();
      },
      error: err => console.log(err)
    });
  }

 
    newNexusForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', [Validators.required]],
     
    })

    onNewNexusCreate(){
      console.log(this.newNexusForm.value);
      
      this.companyService.postNexus(this.newNexusForm.value).subscribe({
        next: () => this.router.navigateByUrl('/Syndicate')
      })
    }
}
