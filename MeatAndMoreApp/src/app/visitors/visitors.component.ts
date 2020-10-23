import { Component, OnInit } from "@angular/core";
import { VisitorService } from "./visitor.service";
import { Visitor } from "./visitor.model";

@Component({
  selector: "app-visitors",
  templateUrl: "./visitors.component.html",
  styleUrls: ["./visitors.component.scss"],
})
export class VisitorsComponent implements OnInit {
  insideVisitors: Visitor[] = [];

  constructor(private visitorService: VisitorService) {}

  ngOnInit(): void {}
}
