import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Inject, Injectable} from "@angular/core";
import {map} from "rxjs/operators";
import { Country } from "../models/country";
import { Province } from "../models/province";
import ApiRoutes from "../../common/api-routes";

@Injectable({ providedIn: 'root' })
export class CountryService {
    private readonly apiHost: string;
    private headers: HttpHeaders;
    constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this.apiHost = `${baseUrl}`;
      this.headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
    }

    getCountries(): Observable<Country[]> {
        return this._http.post<any>(this.apiHost + ApiRoutes.CountryGetList, null)
          .pipe(map(val => val.countries as Country[]));
    }
}
