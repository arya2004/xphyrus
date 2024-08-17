import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { IResponse } from '../shared/models/IResponse';
import { TestRun } from '../shared/models/ITestRun';
import { Submit } from '../shared/models/ISubmit';
import { environment } from 'src/environments/environment.development';

/**
 * Service to handle operations related to assessments.
 */
@Injectable({
  providedIn: 'root'
})
export class AssessmentService {

  private assignmentUrl = environment.assessmentApiUrl;
  private subUrl = "https://localhost:7003/api/TestRun/Run";
  private doneUrl = "https://localhost:7003/api/SubmissionAPI/Submit";
  private studUrl = "https://localhost:7000/api/Participants/Joined";
  private joinUrl = "https://localhost:7000/api/Participants/Register";

  /**
   * Constructor to inject necessary services.
   * @param http HttpClient to perform HTTP requests.
   * @param router Router to navigate between routes.
   */
  constructor(private http: HttpClient, private router: Router) { }

  /**
   * Method to post a new Nexus.
   * @param nexus The Nexus object containing the Nexus details.
   * @returns Observable of the response.
   */
  postNexus(nexus: any) {
    const url = "https://localhost:5000/api/CodingAssessment";
    return this.http.post<any>(url, nexus);
  }

  /**
   * Method to get one assessment by its ID.
   * @param nexus The ID of the assessment.
   * @returns Observable of the response.
   */
  getOneAssessment(nexus: any) {
    const url = `https://localhost:5000/api/CodingAssessment/GetOne?id=${nexus}`;
    return this.http.get<IResponse<any>>(url);
  }

  /**
   * Method to get results for a specific assessment.
   * @param assessmentId The ID of the assessment.
   * @returns Observable of the response.
   */
  getResults(assessmentId: any) {
    const url = `https://localhost:5000/api/Results/GetAllForAssessment?AssessmentId=${assessmentId}`;
    return this.http.get<IResponse<any>>(url);
  }

  /**
   * Method to post a new test case.
   * @param nexus The Nexus object containing the test case details.
   * @returns Observable of the response.
   */
  postTestCase(nexus: any) {
    const url = `https://localhost:5000/api/TestCase`;
    return this.http.post<any>(url, nexus);
  }

  /**
   * Method to get associated test cases for a specific Nexus.
   * @param nexusId The ID of the Nexus.
   * @returns Observable of the response.
   */
  getAssociatedTestCase(nexusId: string) {
    const url = `https://localhost:5000/api/TestCase/GetAllForAssessment?assessmentId=${nexusId}`;
    return this.http.get<IResponse<any>>(url);
  }
}
