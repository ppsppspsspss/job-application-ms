import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  providers: [MessageService]
})
export class SignInComponent {
  
  email: string = '';
  password: string = '';
  status: boolean = false;

  constructor(private authService: AuthService, private router: Router, private messageService: MessageService) {}

  handleSubmit(): void {
    if (this.email && this.password) {
      const user = {
        email: this.email,
        password: this.password
      };

      this.authService.signIn(user).subscribe(
        (response) => {
          if (response.isError) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: response.messages.join(', ') });
          } else {
            this.status = true;
            if (response.data) { 
              this.authService.setToken(response.data);
            }
            this.router.navigateByUrl('home');
          }
        },
        (error) => {
          console.log(error);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An error occurred during sign-in.' });
        }
      );
    } else {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Please enter both email and password.' });
    }
  }
}
