import { Component, OnInit } from "@angular/core";
import { VisitorService } from "../visitor.service";
import { Visitor } from "../visitor.model";

@Component({
  selector: "app-all-inside-visitors",
  templateUrl: "./all-inside-visitors.component.html",
  styleUrls: ["./all-inside-visitors.component.scss"],
})
export class AllInsideVisitorsComponent implements OnInit {
  allVisitors: Visitor[] = [];
  displayedColumns: string[] = [
    "Firstname",
    "Lastname",
    "Type",
    "Company",
    "License plate",
    "Date",
    "Time in",
    "Remove from list",
  ];
  constructor(private visitorService: VisitorService) {}

  ngOnInit(): void {
    this.getVisitors();
  }

  getVisitors() {
    this.visitorService.getVisitors().subscribe((response) => {
      response.forEach((vis, index) => {
        if (vis.insideBuilding) {
          this.allVisitors.push(vis);
          console.log(vis);
        }
      });
    });
  }

  remove(index) {
    this.visitorService.removeInside(index.toString()).subscribe((response) => {
      console.log(response);
      document.getElementById(index).remove();
    });
  }
}
