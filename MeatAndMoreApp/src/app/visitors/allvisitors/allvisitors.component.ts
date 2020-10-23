import { Component, OnInit } from "@angular/core";
import { VisitorService } from "../visitor.service";
import { Visitor } from "../visitor.model";

@Component({
  selector: "app-allvisitors",
  templateUrl: "./allvisitors.component.html",
  styleUrls: ["./allvisitors.component.scss"],
})
export class AllvisitorsComponent implements OnInit {
  allVisitors: Visitor[] = [];
  displayedColumns: string[] = [
    "Firstname",
    "Lastname",
    "Type",
    "Company",
    "License plate",
    "Date",
    "Time in",
    "Time out",
  ];
  constructor(private visitorService: VisitorService) {}

  ngOnInit(): void {
    this.getVisitors();
  }
  getVisitors() {
    this.visitorService.getVisitors().subscribe((response) => {
      response.forEach((vis) => {
        this.allVisitors.push(vis);
      });
    });
  }
}
