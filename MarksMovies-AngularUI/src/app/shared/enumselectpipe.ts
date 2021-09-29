import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'EnumSelectPipe'})
export class EnumSelectPipe implements PipeTransform {
  transform(value, args:string[]) : any {
    let keys = [];
    for (var enumMember in value) {
      //if (!isNaN(parseInt(enumMember, 10))) {
        keys.push({key: enumMember, value: value[enumMember]});
      //} 
    }
    return keys;
  }
}