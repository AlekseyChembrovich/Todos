import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { AuthService } from "../auth.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let httpEvent!: Observable<HttpEvent<any>>;

    this.authService.token$.pipe(
      map(token => {
        request = request.clone({
          setHeaders: {
            authorization: `Bearer ${token}`
          }
        });

        return next.handle(request);
      })
    ).subscribe(data => httpEvent = data)

    return httpEvent;
  }
}
