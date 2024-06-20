import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';
import { IResponse } from '../shared/models/IResponse';
import { TestRun } from '../shared/models/ITestRun';
import { Submit } from '../shared/models/ISubmit';

/**
 * Service to handle operations related to examinees.
 */
@Injectable({
  providedIn: 'root'
})
export class ExamineeService {

  private assignmentUrl = environment.assessmentApiUrl;
  private subUrl = "https://localhost:7003/api/TestRun/Run";
  private doneUrl = "https://localhost:7137/api/Submission/Submit";
  private studUrl = "https://localhost:7000/api/Participants/Joined";
  private joinUrl = "https://localhost:7000/api/Participants/Register";

  /**
   * Constructor to inject necessary services.
   * @param http HttpClient to perform HTTP requests.
   * @param router Router to navigate between routes.
   */
  constructor(private http: HttpClient, private router: Router) { }

  /**
   * Method to trigger a test run.
   * @param code The TestRun object containing the test details.
   * @returns Observable of the response.
   */
  testRun(code: TestRun) {
    return this.http.post<IResponse<any>>(this.subUrl, code);
  }

  /**
   * Method to submit a test run.
   * @param code The submission data.
   * @returns Observable of the response.
   */
  submitRun(code: any) {
    return this.http.post<IResponse<any>>(this.doneUrl, code);
  }

  /**
   * Method to get one assessment by its ID.
   * @param nexus The ID of the assessment.
   * @returns Observable of the response.
   */
  getOneAssessment(nexus: any) {
    const url = `https://localhost:7137/api/CodingAssessment/GetOne?id=${nexus}`;
    return this.http.get<IResponse<any>>(url);
  }
}
