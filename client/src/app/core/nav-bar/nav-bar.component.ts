import { Component, ElementRef, Renderer2 } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  constructor(public accountService: AccountService, private renderer: Renderer2, private el: ElementRef) {
    console.log(accountService.currentUser$);
  }


  

  toggleSidebar() {
    // Toggle the 'toggle-sidebar' class on the body element
    const body = this.el.nativeElement.ownerDocument.body;
    if (body.classList.contains('toggle-sidebar')) {
      this.renderer.removeClass(body, 'toggle-sidebar');
    } else {
      this.renderer.addClass(body, 'toggle-sidebar');
    }
  }
}
