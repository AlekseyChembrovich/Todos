import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ReactiveFormsModule } from "@angular/forms";
import { AboutComponent } from "./about/about.component";
import { BrowserModule } from "@angular/platform-browser";

const routes: Routes = [
  { path: '', component: AboutComponent }
]

@NgModule({
  declarations: [AboutComponent],
  imports: [RouterModule.forChild(routes), ReactiveFormsModule, BrowserModule]
})
export class CommonModule { }
