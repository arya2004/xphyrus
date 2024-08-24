import { Component, ElementRef, Renderer2 } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  constructor(
    public accountService: AccountService,
    private renderer: Renderer2,
    private el: ElementRef
  ) {
    this.logCurrentUser();
  }

  /**
   * Logs the current user observable from the account service.
   */
  private logCurrentUser(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        console.log('Current user:', user);
      },
      error: (err) => {
        console.error('Failed to retrieve current user:', err);
      }
    });
  }

  /**
   * Toggles the 'toggle-sidebar' class on the body element.
   */
  toggleSidebar(): void {
    const body = this.el.nativeElement.ownerDocument.body;
    const sidebarClass = 'toggle-sidebar';

    if (body.classList.contains(sidebarClass)) {
      this.renderer.removeClass(body, sidebarClass);
      console.log('Sidebar toggled off');
    } else {
      this.renderer.addClass(body, sidebarClass);
      console.log('Sidebar toggled on');
    }
  }
}
