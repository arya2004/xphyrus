import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  constructor( private router: Router,private toastr: ToastrService  ){}
    ngOnInit(): void {
     this.showSuccess();
    }
  
  showSuccess() {
    // this.toastr.success('Hello world!', 'Toastr fun!');
    // this.toastr.warning('Helld!', 'Toastr fun!');
    // this.toastr.warning('Helld!', 'Toastr fun!');
    // this.toastr.warning('Helld!', 'Toastr fun!');
    // this.toastr.warning('Helld!', 'Toastr fun!');
  }
}
