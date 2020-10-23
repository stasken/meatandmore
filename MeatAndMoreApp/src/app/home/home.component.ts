import { Component, OnInit, Inject } from "@angular/core";
import { VisitorService } from "../visitors/visitor.service";
import { Visitor } from "../visitors/visitor.model";
import { LoginService } from "../login/login.service";
import { StorageService, LOCAL_STORAGE } from "ngx-webstorage-service";
import { environment } from "src/environments/environment";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  visitorValues: any;
  logoutValues: any;
  typeList: string[] = [];
  insideVisitors: Visitor[] = [];

  constructor(
    private loginService: LoginService,
    private visitorService: VisitorService,
    @Inject(LOCAL_STORAGE) private storage: StorageService
  ) {}

  ngOnInit(): void {
    this.visitorService.getVisitorTypes().subscribe((response) => {
      for (var type of response) {
        this.typeList.push(type);
      }
    });

    this.getVisitors();
  }

  addVisitor(formValues) {
    console.log(formValues.licensePlate);
    this.visitorService
      .addVisitor(
        formValues.firstname,
        formValues.lastname,
        formValues.type,
        formValues.companyname,
        formValues.licensePlate
      )
      .subscribe((response) => {
        console.log(response);
      });
  }

  getVisitors() {
    this.visitorService.getVisitors().subscribe((response) => {
      response.forEach((vis) => {
        if (vis.insideBuilding) {
          this.insideVisitors.push(vis);
        }
      });
    });
  }

  logVisitorOut(logoutValues) {
    this.loginService.logOut(logoutValues).subscribe((response) => {
      window.location.reload();
      console.log(response);
    });
  }
}
