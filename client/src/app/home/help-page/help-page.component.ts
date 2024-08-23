import { Component, ElementRef, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-help-page',
  templateUrl: './help-page.component.html',
  styleUrls: ['./help-page.component.scss']
})
export class HelpPageComponent {

  constructor( private renderer: Renderer2, private el: ElementRef) { }
  ngOnInit(): void {
    this.toggleSidebar()
     
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
