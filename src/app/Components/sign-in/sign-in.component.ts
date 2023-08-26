import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { IUserSignIn } from 'src/Model/i-user-sign-in';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent {
  successLogin: boolean = false;
  user = {} as IUserSignIn;
  loginMessage: string = '';
  // authenticated: boolean = false;

  constructor(
    private userService: UserService,
    private router: Router // private authService: AuthService
  ) {}

  ngOnInit(): void {}

  showAndHide(button: HTMLElement, input: HTMLInputElement) {
    if (input.type == 'password') {
      button.classList.replace('bx-hide', 'bx-show');
      input.type = 'text';
    } else if (input.type == 'text') {
      button.classList.replace('bx-show', 'bx-hide');
      input.type = 'password';
    }
  }

  // @Output() authorizeEvent = new EventEmitter<boolean>();
  // this.authorizeEvent.emit(this.authenticated);

  signIn() {
    this.userService.signInFor(this.user).subscribe({
      next: (res) => {
        // console.log(res);
        this.successLogin = true;
        localStorage.setItem('_2B_User', res.myToken);
        this.loginMessage = res.message;
        setTimeout(() => {
          // location.reload();
          // this.authenticated = true;
          // this.router.navigate(['']);
          location.assign('');
        }, 2000);
      },
      error: (err) => {
        // console.log(err);
        // this.successLogin = false;
        this.loginMessage = err.error;
      },
    });
  }
}
