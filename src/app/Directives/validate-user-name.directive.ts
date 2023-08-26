import { Directive, Input, forwardRef } from '@angular/core';
import {
  Validator,
  AbstractControl,
  NG_ASYNC_VALIDATORS,
} from '@angular/forms';
import { CustomValidationService } from '../services/custom-validation.service';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';

@Directive({
  selector: '[appValidateUserName]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: forwardRef(() => ValidateUserNameDirective),
      multi: true,
    },
  ],
})
export class ValidateUserNameDirective implements Validator {
  constructor(
    private customValidator: CustomValidationService // private userService: UserService
  ) {}
  // @Input() userName = '';

  validate(control: AbstractControl) {
    return this.customValidator.userNameValidator(control);
    // return new Promise((resolve) => {
    //   this.userService.checkUserName(control.value).subscribe((res) => {
    //     if (res.userNameExists == false) {
    //       resolve(null);
    //     } else {
    //       resolve({ userNameExists: { valid: false } });
    //     }
    //   });
    // });
  }
}
