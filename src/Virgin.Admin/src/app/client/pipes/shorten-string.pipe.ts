import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'turncateString'
})
export class ShortenStringPipe implements PipeTransform {

  transform(value: string | undefined, args?: any): string {
    let turncatedValue = value?.substring(0, 35)
    return turncatedValue + "...";
  }

  private truncate(str: string, n: number) {
    return (str.length > n) ? str.substr(0, n - 1) + '&hellip;' : str;
  };
}
