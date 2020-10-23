import { Component, OnInit, Inject } from "@angular/core";
import { LOCAL_STORAGE, StorageService } from "ngx-webstorage-service";
import { environment } from "src/environments/environment";
import { Router } from "@angular/router";

@Component({
  selector: "navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.scss"],
})
export class NavbarComponent implements OnInit {
  loggedIn: boolean;

  constructor(
    private router: Router,
    @Inject(LOCAL_STORAGE) private storage: StorageService
  ) {}

  ngOnInit(): void {
    var token = this.storage.get(environment.storage.AUTH_TOKEN);
    if (!token) {
      this.loggedIn = false;
    } else {
      this.loggedIn = true;
    }
  }

  logOut() {
    this.storage.remove(environment.storage.AUTH_TOKEN);
    this.router.navigate(["/"]);
    this.loggedIn = false;
  }
}
