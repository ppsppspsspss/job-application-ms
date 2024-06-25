import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
  email: string = '';
  password: string = '';
  status: boolean = false;

  constructor(private authService: AuthService,  private router: Router) {}

  handleSubmit(form: NgForm): void {
    if (form.valid) {
      const user = {
        email: this.email,
        password: this.password
      }
      this.authService.signIn(user).subscribe(
        (response) => {
          this.authService.setToken(response);
          this.status = false;
          this.router.navigateByUrl('home');
        },
        (error) => {
          console.error(error);
          this.status = true;
        }
      );
    }
  }
}
