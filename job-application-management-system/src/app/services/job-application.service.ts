import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { JobApplication } from '../types/job-application';
import { Result } from '../types/result';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class JobApplicationService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  baseUrl = "http://localhost:5171/api/job-application";

  jobApplication(jobApplication: FormData): Observable<Result<string>> {
    return this.http.post<Result<string>>(`${this.baseUrl}/application`, jobApplication);
  }

  getAllJobApplications(jobID: number): Observable<Result<JobApplication[]>> {
    const token = this.authService.getToken(); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<Result<JobApplication[]>>(`${this.baseUrl}/get-all-job-applications/${jobID}`, { headers });
  }

  getJobApplication(jobApplicationID: number): Observable<Result<JobApplication>>{
    const token = this.authService.getToken(); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<Result<JobApplication>>(`${this.baseUrl}/get-job-application/${jobApplicationID}`, { headers });
  }

}
