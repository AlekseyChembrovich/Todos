import { Component, OnInit } from "@angular/core";
import { AuthService } from "../auth.service";
import { map, Observable } from "rxjs";

@Component({
  selector: 'app-auth-menu',
  templateUrl: './auth.menu.component.html'
})
export class AuthMenuComponent implements OnInit {
  userData$!: Observable<{ name?: string }>

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.userData$ = this.authService.userData$.pipe(
      map(val => {
        return { name: val.name }
      })
    )
  }

  login(): void {
    this.authService.login()
  }

  logout(): void {
    this.authService.logout()
  }
}
