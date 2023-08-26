import { Injectable } from '@angular/core';
import { ValidatorFn, AbstractControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { UserService } from './user.service';
import { environment } from 'src/environments/environment.development';
import { async } from '@angular/core/testing';

@Injectable({
  providedIn: 'root',
})
export class CustomValidationService {

  constructor(private userService: UserService) {}

  MatchPassword(password: string, confirmPassword: string) {
    return (formGroup: FormGroup) => {
      const passwordControl = formGroup.controls[password];
      const confirmPasswordControl = formGroup.controls[confirmPassword];

      if (!passwordControl || !confirmPasswordControl) {
        return null;
      }

      if (
        confirmPasswordControl.errors &&
        !confirmPasswordControl.errors['passwordMismatch']
      ) {
        return null;
      }

      if (passwordControl.value !== confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({ passwordMismatch: true });
      } else {
        confirmPasswordControl.setErrors(null);
      }
      return;
    };
  }

  userNameValidator(userControl: AbstractControl) {
    return new Promise(async (resolve) => {
      setTimeout(() => {
        this.userService.checkUserName(userControl.value).subscribe((data) => {
          if (data.userNameExists) {
            resolve({ userNameExists: true });
          } else {
            resolve(null);
          }
        });
        // if (this.validateUserName(userControl.value)) {
        // if (this.validateUserName(userControl.value)) {
        //   resolve(() => {
        //     userControl.setErrors({ userNameExists: true });
        //   });
        // } else {
        //   resolve(null);
        // }
      }, 1000);
    });
  }

  isExisted: boolean = false;
  validateUserName(userName: string) {
    // let isExisted: boolean;
    // let res = await fetch(
    //   `${environment.BaseApiUrl}/api/user/checkUserName?username=${userName}`
    // ).then((data) => data.json());

    // isExisted = res.userNameExists;
    // return isExisted;

    // let xhr = new XMLHttpRequest();
    // xhr.open(
    //   'GET',
    //   `${environment.BaseApiUrl}/api/user/checkUserName?username=${userName}`
    // );
    // xhr.send();
    // xhr.onreadystatechange = function () {
    //   if (xhr.readyState === 4 && xhr.status === 200) {
    //     var result = JSON.parse(xhr.response)['userNameExists'] as boolean;
    //     isExisted = result;
    //   }
    // };

    this.userService.checkUserName(userName).subscribe({
      next: (data) => {
        console.log(data);
        this.isExisted = data.userNameExists;
      },
      error: (error) => console.log('Error : ', error),
    });
    return this.isExisted;
    // const UserList = ['ankit', 'admin', 'user', 'superuser'];
    // return UserList.indexOf(userName) > -1;
  }
}
