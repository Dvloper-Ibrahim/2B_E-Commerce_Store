import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatDiscount'
})
export class FormatDiscountPipe implements PipeTransform {

  transform(value: number | undefined): string {
    if (value === undefined) {
      return ''; // Return an empty string if the value is undefined
    }

    const formattedValue = (value ).toFixed(0); // Convert to percentage and round to 0 decimal places
    return `${formattedValue}%`;
  }

}
