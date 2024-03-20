import {Inject, Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import { RegistrationReq } from "../contracts/user/registration-req";
import ApiRoutes from "../../common/api-routes";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly apiHost: string;
  private headers: HttpHeaders;
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiHost = `${baseUrl}`;
    this.headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
  }

  create(formData: RegistrationReq): Observable<any> {
    return this._http.post(this.apiHost + ApiRoutes.UserRegister, JSON.stringify(formData), {headers: this.headers} );
  }
}

