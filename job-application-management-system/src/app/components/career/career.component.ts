import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-career',
  templateUrl: './career.component.html',
  styleUrls: ['./career.component.css']
})
export class CareerComponent {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  handleJoinNow(){
    this.router.navigateByUrl('home');
  }

}
