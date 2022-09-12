import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HttpClientModule } from "@angular/common/http";
import { TodoModule } from "./todo/todo.module";
import { AppRoutingModule } from "./app.routing.module";
import { CommonModule } from "./common/common.module";
import { AuthModule } from "./auth/auth.module";

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule,HttpClientModule,AppRoutingModule,TodoModule,CommonModule,AuthModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
