import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
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

  public logOut() {
    this.storage.remove(environment.storage.AUTH_TOKEN);
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
