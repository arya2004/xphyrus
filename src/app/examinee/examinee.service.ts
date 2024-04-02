import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';
import { IResponse } from '../shared/models/IResponse';
import { TestRun } from '../shared/models/ITestRun';
import { Submit } from '../shared/models/ISubmit';

@Injectable({
  providedIn: 'root'
})
export class ExamineeService {

  
  constructor(private http: HttpClient, private router: Router) { }

  assignmnetUrl  = environment.assesmentApiUrl;
  subURl = "https://localhost:7003/api/TestRun/Run"
  doneURl = "https://localhost:7003/api/SubmissionAPI/Submit"
  studUrl = "https://localhost:7000/api/Participants/Joined"
  joimURl = "https://localhost:7000/api/Participants/Register"



  testRun(code: TestRun)
  {
    return this.http.post<IResponse<any>>(this.subURl, code);
  }
  submitRun(code:any)
  {
    return this.http.post<IResponse<any>>(this.doneURl, code);
  }


  getOneAssessment(nexus: any)
  {
    return this.http.get<IResponse<any>>(`https://localhost:7137/api/CodingAssessment/GetOne?id=${nexus}`);
    //https://localhost:5000/api/CodingAssessment/GetOne?id=42fe021b-8ae2-4c1c-aa0c-fe7c31446d84
  }
  
}
