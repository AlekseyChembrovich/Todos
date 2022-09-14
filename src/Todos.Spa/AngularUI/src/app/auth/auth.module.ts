import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthMenuComponent } from "./auth.menu/auth.menu.component";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { JwtInterceptor } from "./interceptors/jwt.interceptor";
import { AuthInterceptor } from "./interceptors/auth.interceptor";
import { LogLevel, AuthModule as OidcModule } from "angular-auth-oidc-client";
import { environment } from "../../environments/environment";

const getApplicationUrl = () => {
  const protocol = window.location.protocol
  const host = window.location.host
  return `${protocol}//${host}`
}

@NgModule({
  declarations: [AuthMenuComponent],
  imports: [CommonModule,
    OidcModule.forRoot({
    config: {
      clientId: 'angular',
      authority: environment.authApi,
      responseType: 'code',
      redirectUrl: getApplicationUrl(), // "http://localhost:4200"
      postLogoutRedirectUri: getApplicationUrl(), // "http://localhost:4200"
      scope: 'first_scope openid profile',
      logLevel: LogLevel.Debug
    },
  })],
  exports: [AuthMenuComponent,OidcModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
})
export class AuthModule { }
