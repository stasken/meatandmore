import { Component, OnInit, Inject } from "@angular/core";
import { VisitorService } from "../visitors/visitor.service";
import { Visitor } from "../visitors/visitor.model";
import { LoginService } from "../login/login.service";
import { StorageService, LOCAL_STORAGE } from "ngx-webstorage-service";
import { environment } from "src/environments/environment";
import { NgForm } from "@angular/forms";

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
  error: boolean = false;
  created: boolean = false;
  eng: boolean = true;
  nl: boolean = false;
  fr: boolean = false;
  currentVisitor: string;

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

    this.getVisitorsToLogOut();
  }

  addVisitor(formValues) {
    this.visitorService
      .addVisitor(
        formValues.firstname,
        formValues.lastname,
        formValues.type,
        formValues.companyname,
        formValues.licensePlate
      )
      .subscribe(
        (response) => {
          var resetForm = <HTMLFormElement>(
            document.getElementById("aandmeldform")
          );
          resetForm.reset();
          this.currentVisitor = `${formValues.firstname} ${formValues.lastname}`;
          this.created = true;
          var visitor: Visitor = {
            id: response.id,
            firstName: response.firstName,
            lastName: response.lastName,
            typeVisit: response.typeVisit,
            companyName: response.companyName,
            licensePlate: response.licensePlate,
            loggedIn: response.loggedIn,
            loggedOut: response.loggedOut,
            insideBuilding: response.insideBuilding,
          };
          this.insideVisitors.push(visitor);
        },
        (error) => (this.error = true)
      );
  }

  getVisitorsToLogOut() {
    this.visitorService.getVisitorsToLogOut().subscribe((response) => {
      console.log(response);
      response.forEach((vis) => {
        if (vis.insideBuilding) {
          this.insideVisitors.push(vis);
        }
      });
    });
  }

  logVisitorOut(logoutValues) {
    this.loginService.logOut(logoutValues).subscribe((response) => {
      for (var i = this.insideVisitors.length - 1; i >= 0; --i) {
        if (this.insideVisitors[i].id == logoutValues.name) {
          this.insideVisitors.splice(i, 1);
        }
      }
    });
  }

  engels() {
    this.eng = true;
    this.fr = false;
    this.nl = false;
  }

  nederlands() {
    this.eng = false;
    this.fr = false;
    this.nl = true;
  }

  frans() {
    this.eng = false;
    this.fr = true;
    this.nl = false;
  }
}
