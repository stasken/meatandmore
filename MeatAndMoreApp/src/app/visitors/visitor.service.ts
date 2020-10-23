import { Injectable } from "@angular/core";
import { retry, catchError } from "rxjs/operators";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { throwError, Observable } from "rxjs";
import { Visitor } from "./visitor.model";

@Injectable({
  providedIn: "root",
})
export class VisitorService {
  myApiUrl: string;

  constructor(private http: HttpClient) {
    this.myApiUrl = environment.apiUrl;
  }

  public getVisitorTypes() {
    return this.http
      .get<string[]>(this.myApiUrl + "loggedvisitors/types")
      .pipe(retry(1), catchError(this.errorHandler));
  }

  public getVisitors(): Observable<Visitor[]> {
    return this.http
      .get<Visitor[]>(this.myApiUrl + "loggedvisitors")
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
