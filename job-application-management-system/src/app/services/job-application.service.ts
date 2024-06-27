import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { JobApplication } from '../types/job-application';
import { Result } from '../types/result';

@Injectable({
  providedIn: 'root'
})
export class JobApplicationService {

  constructor(private http: HttpClient) { }

  baseUrl = "http://localhost:5171/api/job-application";

  jobApplication(jobApplication: FormData): Observable<Result<string>> {
    return this.http.post<Result<string>>(`${this.baseUrl}/application`, jobApplication);
  }

  getAllJobApplications(jobID: number): Observable<Result<JobApplication[]>> {
    return this.http.get<Result<JobApplication[]>>(`${this.baseUrl}/get-all-job-applications/${jobID}`);
  }

  getJobApplication(jobApplicationID: number): Observable<Result<JobApplication>>{
    return this.http.get<Result<JobApplication>>(`${this.baseUrl}/get-job-application/${jobApplicationID}`);
  }

}
