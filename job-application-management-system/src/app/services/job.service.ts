import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Job } from '../types/job';
import { Result } from '../types/result';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  constructor(private http: HttpClient) { }

  baseUrl = "http://localhost:5171/api/job"

  
  createOpening(openingForm: any): Observable<Result<string>> {
    return this.http.post<Result<string>>(`${this.baseUrl}/create-opening`, openingForm);
  }

  getAllJobs(): Observable<Result<Job[]>> {
    return this.http.get<Result<Job[]>>(`${this.baseUrl}/get-all-jobs`);
  }

  getJob(jobID: number): Observable<Result<Job>> {
    return this.http.get<Result<Job>>(`${this.baseUrl}/get-job/${jobID}`);
  }

  updateStatus(jobID: number): Observable<string>{
    return this.http.patch<string>(this.baseUrl + "/update-status/" + jobID , {
      headers: {
        'Content-Type': 'application/json'
    }
    });
  }

  getJobRequirements(jobID: number): Observable<Result<any[]>> {
    return this.http.get<Result<any[]>>(`${this.baseUrl}/get-job-requirements/${jobID}`);
  }

  getJobResponsibilities(jobID: number): Observable<Result<any[]>> {
    return this.http.get<Result<any[]>>(`${this.baseUrl}/get-job-responsibilities/${jobID}`);
  }

  deleteJob(jobID: number): Observable<string> {
    return this.http.delete<string>(this.baseUrl + "/delete-job/" + jobID, {
      responseType: 'text' as 'json'
    });
  }

  updateJob(jobID: number, jobApplication: any): Observable<string> {
    return this.http.put<string>(this.baseUrl + "/update-job/" + jobID, jobApplication, {
      responseType: 'text' as 'json'
    });
  }

}