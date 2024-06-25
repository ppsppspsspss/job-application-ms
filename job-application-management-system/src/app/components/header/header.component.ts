import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor(private authService: AuthService) { }

  signedIn: boolean = false

  ngOnInit(): void {
    this.signedIn = this.authService.isSignedIn();
  }

  logOut(){
    this.authService.logOut();
  }
  
}
