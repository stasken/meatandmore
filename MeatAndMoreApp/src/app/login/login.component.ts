import { Component, OnInit, Inject, Input } from "@angular/core";
import { LoginService } from "./login.service";
import { LOCAL_STORAGE, StorageService } from "ngx-webstorage-service";
import { environment } from "src/environments/environment";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  loginValues: any;
  error: boolean = false;

  constructor(
    private router: Router,
    @Inject(LOCAL_STORAGE) private storage: StorageService,
    private loginService: LoginService
  ) {
    loginService.error.subscribe((error) => (this.error = error));
  }

  ngOnInit(): void {}

  login(formValues) {
    this.loginService.logIn(formValues.username, formValues.password);
  }
}
