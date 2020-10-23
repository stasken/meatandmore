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

  constructor(
    private router: Router,
    @Inject(LOCAL_STORAGE) private storage: StorageService,
    private loginService: LoginService
  ) {}

  ngOnInit(): void {}

  login(formValues) {
    this.loginService
      .logIn(formValues.username, formValues.password)
      .subscribe((response) => {
        this.storage.set(environment.storage.AUTH_TOKEN, response.token);
        this.storage.set(environment.storage.AUTH_ID, response.id);
        this.router.navigate(["/"]);
        window.location.reload();
      });
  }
}
