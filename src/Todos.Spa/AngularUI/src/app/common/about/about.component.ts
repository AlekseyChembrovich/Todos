import { Component } from "@angular/core";

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html'
})
export class AboutComponent {
  tips: Array<string> = [
    "Plan your day wisely",
    "Figure out how you're currently spending your time",
    "Create a daily schedule—and stick with it",
    "Prioritize wisely",
    "Group similar tasks together",
    "Avoid the urge to multitask",
    "Assign time limits to tasks",
    "Build in buffers",
    "Learn to say no"
  ]
}
