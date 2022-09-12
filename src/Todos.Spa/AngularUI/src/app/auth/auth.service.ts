import { Injectable } from "@angular/core";
import { LoginResponse, OidcSecurityService } from "angular-auth-oidc-client";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { UserData } from "./user.data";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticatedSubject$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private userDataSubject$: BehaviorSubject<UserData> = new BehaviorSubject<UserData>({ });

  constructor(private oidcSecurityService: OidcSecurityService) { }

  initAuth(): Observable<LoginResponse> {
    return this.oidcSecurityService.checkAuth()
      .pipe(

        tap({ // set up is authenticated
          next: value => {
            this.isAuthenticatedSubject$.next(value.isAuthenticated)
          }
        }),

        tap({ // set up user data
          next: value => {
            let userData: UserData = {
              name: value.userData?.name,
              sub: value.userData?.sub
            }
            this.userDataSubject$.next(userData)
          }
        })

      )
  }

  get isAuthenticated$(): Observable<boolean> {
    return this.isAuthenticatedSubject$.asObservable()
  }

  get userData$(): Observable<UserData> {
    return this.userDataSubject$.asObservable()
  }

  get token$(): Observable<string | null> {
    return this.oidcSecurityService.getAccessToken()
  }

  login(): void {
    this.oidcSecurityService.authorize()
  }

  logout(): void {
    this.oidcSecurityService.logoff()
  }
}
