import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { LOCAL_STORAGE, StorageService } from "ngx-webstorage-service";
import { Observable, throwError } from "rxjs";
import { retry, catchError } from "rxjs/operators";
import { User } from "./user.model";

@Injectable({
  providedIn: "root",
})
export class LoginService {
  public myApiUrl: string;

  constructor(
    private http: HttpClient,
    @Inject(LOCAL_STORAGE) private storage: StorageService
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

  public logIn(username: string, password: string): Observable<User> {
    const user = {
      Username: username,
      Password: password,
    };
    return this.http
      .post<User>(this.myApiUrl + "users/login", user)
      .pipe(retry(1), catchError(this.errorHandler));
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
