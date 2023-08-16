import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appCartDropdown]',
})
export class CartDropdownDirective {
  constructor(private element: ElementRef) {}

  @HostListener('click') controlDropdown() {}
}
