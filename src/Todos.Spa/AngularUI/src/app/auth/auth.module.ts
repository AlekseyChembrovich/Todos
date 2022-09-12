import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthMenuComponent } from "./auth.menu/auth.menu.component";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { JwtInterceptor } from "./interceptors/jwt.interceptor";
import { AuthInterceptor } from "./interceptors/auth.interceptor";
import { LogLevel, AuthModule as OidcModule } from "angular-auth-oidc-client";
import { environment } from "../../environments/environment";

@NgModule({
  declarations: [AuthMenuComponent],
  imports: [CommonModule,
    OidcModule.forRoot({
    config: {
      clientId: 'angular',
      authority: environment.authApi,
      responseType: 'code',
      redirectUrl: "http://localhost:4200",
      postLogoutRedirectUri: "http://localhost:4200",
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
