import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  @Output() back: EventEmitter<void> = new EventEmitter<void>();

  onBack(): void {
    this.back.emit();
  }
  
}
