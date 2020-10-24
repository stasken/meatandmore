import { Injectable, Inject, Output, EventEmitter } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { LOCAL_STORAGE, StorageService } from "ngx-webstorage-service";
import { Observable, throwError } from "rxjs";
import { retry, catchError } from "rxjs/operators";
import { User } from "./user.model";
import { Router } from "@angular/router";

@Injectable({
  providedIn: "root",
})
export class LoginService {
  public myApiUrl: string;
  @Output() getLoggedIn: EventEmitter<any> = new EventEmitter();
  @Output() error: EventEmitter<any> = new EventEmitter();

  constructor(
    private http: HttpClient,
    @Inject(LOCAL_STORAGE) private storage: StorageService,
    private router: Router
  ) {
    this.myApiUrl = environment.apiUrl;
  }

  /**
   * Service calls
   */

  public isAuthenticated(): boolean {
    const currentTokenStorage =
      this.storage.get(environment.storage.AUTH_TOKEN) || [];
    if (currentTokenStorage.length === 0) {
      return false;
    }
    return true;
  }

  public logIn(username: string, password: string) {
    const user = {
      Username: username,
      Password: password,
    };
    return this.http
      .post<User>(this.myApiUrl + "users/login", user)
      .pipe(retry(1), catchError(this.errorHandler))
      .subscribe(
        (response) => {
          this.storage.set(environment.storage.AUTH_TOKEN, response.token);
          this.storage.set(environment.storage.AUTH_ID, response.id);
          this.router.navigate(["/visitors"]);
          this.getLoggedIn.emit(true);
        },
        (error) => this.error.emit(true)
      );
  }

  public logOut(logoutValues) {
    return this.http
      .put<string>(this.myApiUrl + "loggedvisitors/" + logoutValues.name, "")
      .pipe(retry(1), catchError(this.errorHandler));
  }

  /**
   * Helper methods
   */
  public errorHandler(error) {
    let errorMessage = "";
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }
}
