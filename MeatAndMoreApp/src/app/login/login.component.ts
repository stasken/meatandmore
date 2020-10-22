import { Component, OnInit, Inject } from "@angular/core";
import { LoginService } from "./login.service";
import { LOCAL_STORAGE, StorageService } from "ngx-webstorage-service";
import { environment } from "src/environments/environment";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  constructor(private loginService: LoginService) {}

  ngOnInit(): void {}

  login(formValues) {
    this.loginService
      .logIn(formValues.username, formValues.password)
      .subscribe((response) => {
        console.log(response);
      });
  }
}
