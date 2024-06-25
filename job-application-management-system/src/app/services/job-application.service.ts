import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { JobApplication } from '../types/job-application';

@Injectable({
  providedIn: 'root'
})
export class JobApplicationService {

  constructor(private http: HttpClient) { }

  baseUrl = "http://localhost:5171/api/job-application";

  jobApplication(jobApplication: any): Observable<string> {
    return this.http.post<string>(this.baseUrl + "/job-application", jobApplication, {
      responseType: 'text' as 'json'
    });
  }

  getAllJobApplications(jobID: number): Observable<JobApplication>{
    return this.http.get<JobApplication>(this.baseUrl + "/get-all-job-applications/" + jobID , {
      headers: {
        'Content-Type': 'application/json'
    }
    });
  }

  getJobApplication(jobApplicationID: number): Observable<JobApplication>{
    return this.http.get<JobApplication>(this.baseUrl + "/get-job-application/" + jobApplicationID , {
      headers: {
        'Content-Type': 'application/json'
    }
    });
  }

  hasApplied(jobID: number): Observable<any>{
    return this.http.get<any>(this.baseUrl + "/has-applied/" + jobID , {
      headers: {
        'Content-Type': 'application/json'
    }
    });
  }

}
