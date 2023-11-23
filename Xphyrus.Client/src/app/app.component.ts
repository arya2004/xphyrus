import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Xphyrus.Client';

  constructor(private accountService: AccountService) {}
  
  ngOnInit(): void {
    //this.loadCurreentUser();
  }


  loadCurreentUser(){
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe();
    
  }

}
