import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { IAssignment } from '../shared/models/IAssesmentCreate';
import { IResponse } from '../shared/models/IResponse';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class NexusService {

  
  constructor(private http: HttpClient, private router: Router) { }

  ApiUri  = "https://localhost:5000/api/Nexus/";


  assignmnetUrl  = environment.assesmentApiUrl;
  assUrl = "https://localhost:7000/api/Assesment"

  postAssignment(assignment: IAssignment)
  {
    return this.http.post<IResponse<any>>(this.assignmnetUrl , assignment);
  }

  indexAssesmnet()
  {
    return this.http.get<IResponse<any>>(this.assignmnetUrl);
  }

  detailAssesment(id: number)
  {
    return this.http.get<IResponse<any>>(this.assignmnetUrl+ `?assesmentCode=${id}`);
  }

  getCreated()
  {
    return this.http.get<IResponse<any>>(this.assignmnetUrl + "created")
  }


  loadDetailed(id: string)
  {
    return this.http.get<IResponse<any>>(this.assUrl+ "?assesmentCode=" + id )
  }

  postNexus(nexus: any)
  {
    return this.http.post<any>(this.ApiUri , nexus);
  }
  
  getAssociatedAssessment(nexusId:string)
  {
    return this.http.get<IResponse<any>>("https://localhost:5000/api/CodingAssessment/GetAllForNexus"+ `?NexusId=${nexusId}`)
  }

  GetNexus()
  {
    console.log(this.ApiUri);
    
    
    return this.http.get<any>(this.ApiUri+ "GetMy");
   
  }
  
  GetOneNexus(id: number)
  {
    return this.http.get<any>(this.ApiUri + "GetOne" + `?id=${id}`);
  }

  UpdateNexus(nexus: any)
  {
    return this.http.put<any>("https://localhost:5000/api/Nexus/", nexus);
  }

  DeleteNexus(name: string)
  {
    return this.http.delete<any>(this.ApiUri + `?id=${name}`);
  }
}
