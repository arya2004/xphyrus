import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { IResponse } from '../shared/models/IResponse';
import { TestRun } from '../shared/models/ITestRun';
import { Submit } from '../shared/models/ISubmit';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AssessmentService {

  constructor(private http: HttpClient, private router: Router) { }

  assignmnetUrl  = environment.assesmentApiUrl;
  subURl = "https://localhost:7003/api/TestRun/Run"
  doneURl = "https://localhost:7003/api/SubmissionAPI/Submit"
  studUrl = "https://localhost:7000/api/Participants/Joined"
  joimURl = "https://localhost:7000/api/Participants/Register"

  joinAssemsnet(AssesmentCode: string)
  {
    const headerDict = {
      'Content-Type': 'application/json',
      'Accept': 'text/plain'
    }
    
    const requestOptions = {                                                                                                                                                                                 
      headers: new HttpHeaders(headerDict), 
    };
    return this.http.post<IResponse<any>>(this.joimURl, JSON.stringify(AssesmentCode), requestOptions);
  }

  indexAssesmnet()
  {
    return this.http.get<IResponse<any>>(this.assignmnetUrl);
  }

  detailAssesment(id: number)
  {
    return this.http.get<IResponse<any>>(this.assignmnetUrl+ `?assesmentCode=${id}`);
  }

  testRun(code: TestRun)
  {
    return this.http.post<IResponse<any>>(this.subURl, code);
  }
  submitRun(code: Submit)
  {
    return this.http.post<IResponse<any>>(this.doneURl, code);
  }
  getJoined()
  {
    return this.http.get<IResponse<any>>(this.studUrl)
  }
  postNexus(nexus: any)
  {
    return this.http.post<any>("https://localhost:5000/api/CodingAssessment" , nexus);
  }

  getOneAssessment(nexus: any)
  {
    return this.http.get<IResponse<any>>(`https://localhost:5000/api/CodingAssessment/GetOne?id=${nexus}`);
    //https://localhost:5000/api/CodingAssessment/GetOne?id=42fe021b-8ae2-4c1c-aa0c-fe7c31446d84
  }
  getResults(assessmentId: any)
  {
    return this.http.get<IResponse<any>>(`https://localhost:5000/api/Results/GetAllForAssessment?AssessmentId=${assessmentId}`);
    //https://localhost:5000/api/CodingAssessment/GetOne?id=42fe021b-8ae2-4c1c-aa0c-fe7c31446d84
  }
    

}
