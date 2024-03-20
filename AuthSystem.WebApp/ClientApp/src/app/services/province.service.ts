import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Inject, Injectable} from "@angular/core";
import {map} from "rxjs/operators";
import { Country } from "../models/country";
import { Province } from "../models/province";
import ApiRoutes from "../../common/api-routes";
import { GetByCountryIdReq } from "../contracts/province/get-by-countryid-req";

@Injectable({ providedIn: 'root' })
export class ProvinceService {
    private readonly apiHost: string;
    private headers: HttpHeaders;
    constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this.apiHost = `${baseUrl}`;
      this.headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
    }

  getProvincesByCountryId(countryId: number): Observable<Province[]> {
    var req = new GetByCountryIdReq();
    req.countryId = countryId;

    return this._http.post<any>(this.apiHost + ApiRoutes.ProvinceGetListByCountryId, JSON.stringify(req), { headers: this.headers })
          .pipe(map(val => val.provinces as Province[]));
  }
}
