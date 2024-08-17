import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { IResponse } from '../shared/models/IResponse';
import { environment } from 'src/environments/environment.development';

/**
 * Service to handle operations related to Nexus entities.
 */
@Injectable({
  providedIn: 'root'
})
export class NexusService {

  private apiUri = "https://localhost:5000/api/Nexus/";
  private assignmentUrl = environment.assessmentApiUrl;
  private assessmentUrl = "https://localhost:7000/api/Assesment";

  /**
   * Constructor to inject necessary services.
   * @param http HttpClient to perform HTTP requests.
   * @param router Router to navigate between routes.
   */
  constructor(private http: HttpClient, private router: Router) { }

  /**
   * Method to create a new Nexus.
   * @param nexus The Nexus object containing the Nexus details.
   * @returns Observable of the response.
   */
  postNexus(nexus: any) {
    return this.http.post<any>(this.apiUri, nexus);
  }

  /**
   * Method to get associated assessments for a specific Nexus.
   * @param nexusId The ID of the Nexus.
   * @returns Observable of the response.
   */
  getAssociatedAssessment(nexusId: string) {
    const url = `https://localhost:5000/api/CodingAssessment/GetAllForNexus?NexusId=${nexusId}`;
    return this.http.get<IResponse<any>>(url);
  }

  /**
   * Method to get all Nexus entities associated with the current user.
   * @returns Observable of the response.
   */
  getNexus() {
    console.log(this.apiUri);
    return this.http.get<any>(`${this.apiUri}GetMy`);
  }

  /**
   * Method to get a specific Nexus by its ID.
   * @param id The ID of the Nexus.
   * @returns Observable of the response.
   */
  getOneNexus(id: number) {
    const url = `${this.apiUri}GetOne?id=${id}`;
    return this.http.get<any>(url);
  }

  /**
   * Method to update an existing Nexus.
   * @param nexus The Nexus object containing the updated Nexus details.
   * @returns Observable of the response.
   */
  updateNexus(nexus: any) {
    return this.http.put<any>(this.apiUri, nexus);
  }

  /**
   * Method to delete a specific Nexus by its name.
   * @param name The name of the Nexus.
   * @returns Observable of the response.
   */
  deleteNexus(name: string) {
    const url = `${this.apiUri}?id=${name}`;
    return this.http.delete<any>(url);
  }
}
