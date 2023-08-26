import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SideMenuNavbarService {

  constructor() { }
  private activeMenuSubject = new BehaviorSubject<string>('showMain');
  activeMenu$ = this.activeMenuSubject.asObservable();

  setActiveMenu(menu: string) {
    this.activeMenuSubject.next(menu);
}
}
