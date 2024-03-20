import {Component, OnInit} from "@angular/core";
import {Province} from "../../../models/province";
import {RegistrationReq} from "../../../contracts/user/registration-req";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import { Country } from "../../../models/country";
import { ProvinceService } from "../../../services/province.service";
import { UserService } from "../../../services/user.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { RegistrationSteps } from "../../../../common/registration-steps";
import { MatSelectChange } from "@angular/material/select";
import AppRoutes from "../../../../common/app-routes";
import { CountryService } from "../../../services/country.service";

@Component({
  selector: 'registration',
  templateUrl: 'registration.component.html',
  styleUrls: ['registration.component.css']
})

export class RegistrationPageComponent implements OnInit{
  countries: Country[] = []
  provinces: Province[] = []


  stepperForm: FormGroup;

   get loginInfoForm(): FormGroup {
    return this.stepperForm.get(RegistrationSteps.LoginInfo) as FormGroup;
  }

  get addressInfoForm(): FormGroup {
    return this.stepperForm.get(RegistrationSteps.AddressInfo) as FormGroup;
  }

  private readonly _passwordValidationPattern: RegExp = /^(?=.*[a-zA-Z])(?=.*\d).+$/

  constructor(
        private _countryService: CountryService,
        private _provinceService: ProvinceService,
        private _accountService: UserService,
        private _snackBar: MatSnackBar,
        private _router: Router,
        private _formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this._countryService
      .getCountries()
      .subscribe(c => {
        this.countries = c
      });



    this.stepperForm = this._formBuilder.group({

      [RegistrationSteps.LoginInfo]: this._formBuilder.group({
        login: ['', Validators.email],
        password: ['', Validators.pattern(this._passwordValidationPattern)],
        confirmPassword: [''],
        isAgreeToWorkForFood: [false, Validators.required]
        //}),
      }, { validators: this.checkPasswords }),

      [RegistrationSteps.AddressInfo]: this._formBuilder.group({
        country: [null, Validators.required],
        province: [null, Validators.required]
      })
    });

  }

  onCountrySelected(evt: MatSelectChange) {
    this.provinces = [];

    if (evt.value)
      this._provinceService
        .getProvincesByCountryId(evt.value.id)
        .subscribe(p =>
          this.provinces = p
        );
  }

  checkPasswords(group: FormGroup) {
    const password = group.get('password');
    const confirmPassword = group.get('confirmPassword');

    if (password.value === confirmPassword.value)
      confirmPassword.setErrors(null);
    else
      confirmPassword.setErrors({ notSame: true });
  }


  onSubmit() {

    const loginInfo = this.stepperForm.get(RegistrationSteps.LoginInfo);
    const addressInfo = this.stepperForm.get(RegistrationSteps.AddressInfo);

    // Extract values from form groups
    const login = loginInfo.get('login').value;
    const password = loginInfo.get('password').value;
    const confirmPassword = loginInfo.get('confirmPassword').value;
    const isAgreeToWorkForFood = loginInfo.get('isAgreeToWorkForFood').value;
    const countryId = addressInfo.get('country').value.id;
    const provinceId = addressInfo.get('province').value.id;


    let registrationModel = {
      login,
      password,
      confirmPassword,
      isAgreeToWorkForFood,
      countryId,
      provinceId
    } as RegistrationReq;


    this._accountService.create(registrationModel)
      .subscribe(() => {
        this._router.navigate([AppRoutes.Success])
      }, errResp => {

        var msg = '';
        if (errResp.status == "404") {
          msg = errResp.message;
        }
        else {
          msg = errResp.error.Message;
        }

        this._snackBar.open(msg, 'Ok', {
          duration: 10000
      });
    })
  }
}
