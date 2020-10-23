import { Component, OnInit } from "@angular/core";
import { VisitorService } from "../visitor.service";

@Component({
  selector: "app-grouped-visitors",
  templateUrl: "./grouped-visitors.component.html",
  styleUrls: ["./grouped-visitors.component.scss"],
})
export class GroupedVisitorsComponent implements OnInit {
  public hoursArray: number[] = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
  displayedColumns: string[] = ["Hours", "Count"];
  constructor(private visitorService: VisitorService) {}

  ngOnInit(): void {
    this.getVisitors();
  }
  getVisitors() {
    this.visitorService.getVisitors().subscribe((response) => {
      response.forEach((vis) => {
        if (vis.loggedOut) {
          var inn = new Date(vis.loggedIn);
          var out = new Date(vis.loggedOut);
          var time = ((out.getTime() - inn.getTime()) / (1000 * 3600)).toFixed(
            0
          );
          if (Number(time) > 12) {
            this.hoursArray[13] += 1;
          } else {
            this.hoursArray[time] += 1;
          }
        }
      });
    });
  }
  Z;
}
