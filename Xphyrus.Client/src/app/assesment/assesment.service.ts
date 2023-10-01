import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IResponse } from '../shared/models/IResponse';
import { IAssesmentAdminHome } from '../shared/models/IAssesmentAdminHome';

@Injectable({
  providedIn: 'root'
})
export class AssesmentService {

  baseUrl: string = environment.assesmentApiUrl



  constructor(private http: HttpClient) { }

  getMyCreated()
  {
    return this.http.get<IResponse<IAssesmentAdminHome[]>>(this.baseUrl + 'Assesment/created')
  }
}
