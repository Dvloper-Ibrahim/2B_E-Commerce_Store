import { Component } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IUserSignUp } from 'src/Model/i-user-sign-up';
import { UserService } from 'src/app/services/user.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css'],
})
export class CreateAccountComponent {
  newUser = {} as IUserSignUp;

  constructor(private userService: UserService, private router: Router) {}

  showAndHide(button: HTMLElement, input: HTMLInputElement) {
    if (input.type == 'password') {
      button.classList.replace('bx-hide', 'bx-show');
      input.type = 'text';
    } else if (input.type == 'text') {
      button.classList.replace('bx-show', 'bx-hide');
      input.type = 'password';
    }
  }

  createUser() {
    this.userService.signUpFor(this.newUser).subscribe({
      next: (data) => {
        this.router.navigate(['sign-in']);
      },
      error: (err) => console.log(err.message),
    });
  }
}
