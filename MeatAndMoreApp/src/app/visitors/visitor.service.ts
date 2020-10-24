import { Injectable, Inject } from "@angular/core";
import { retry, catchError } from "rxjs/operators";
import { environment } from "../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { throwError, Observable } from "rxjs";
import { Visitor } from "./visitor.model";
import { StorageService, LOCAL_STORAGE } from "ngx-webstorage-service";

@Injectable({
  providedIn: "root",
})
export class VisitorService {
  myApiUrl: string;

  constructor(
    private http: HttpClient,
    @Inject(LOCAL_STORAGE) private storage: StorageService
  ) {
    this.myApiUrl = environment.apiUrl;
  }

  public getVisitorTypes() {
    return this.http
      .get<string[]>(this.myApiUrl + "loggedvisitors/types")
      .pipe(retry(1), catchError(this.errorHandler));
  }

  public getVisitors(): Observable<Visitor[]> {
    console.log(this.storage.get(environment.storage.AUTH_TOKEN));
    return this.http
      .get<Visitor[]>(this.myApiUrl + "loggedvisitors", {
        headers: new HttpHeaders({
          Authorization:
            "Bearer " + this.storage.get(environment.storage.AUTH_TOKEN),
        }),
      })
      .pipe(retry(1), catchError(this.errorHandler));
  }

  public getVisitorsToLogOut(): Observable<Visitor[]> {
    return this.http
      .get<Visitor[]>(this.myApiUrl + "loggedvisitors/logout")
      .pipe(retry(1), catchError(this.errorHandler));
  }

  public addVisitor(
    firstname: string,
    lastname: string,
    type: string,
    companyname: string,
    licensePlate: string
  ): Observable<Visitor> {
    var date = new Date();
    date.setHours(date.getHours() + 2);
    const visitor = {
      Firstname: firstname,
      Lastname: lastname,
      TypeVisit: type,
      Companyname: companyname,
      LicensePlate: licensePlate,
      LoggedIn: date.toISOString(),
      InsideBuilding: true,
    };
    console.log(visitor);
    return this.http
      .post<Visitor>(this.myApiUrl + "loggedvisitors", visitor)
      .pipe(retry(1), catchError(this.errorHandler));
  }

  public removeInside(id: String): Observable<string> {
    return this.http
      .put<string>(this.myApiUrl + "loggedvisitors/" + id, "")
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
