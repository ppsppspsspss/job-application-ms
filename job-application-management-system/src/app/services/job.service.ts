import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Job } from '../types/job';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  constructor(private http: HttpClient) { }

  baseUrl = "http://localhost:5171/api/job"

  jobApplication(jobApplication: any): Observable<string> {
    return this.http.post<string>(this.baseUrl + "/job-application", jobApplication, {
      responseType: 'text' as 'json'
    });
  }

  createOpening(openingForm: any): Observable<string> {
    return this.http.post<string>(this.baseUrl + "/create-opening", openingForm, {
      responseType: 'text' as 'json'
    });
  }
  
  getAllJobs(): Observable<Job[]>{
    return this.http.get<Job[]>(this.baseUrl + "/get-all-jobs", {
      headers: {
        'Content-Type': 'application/json'
    }
    });
  }

  getJob(jobID: number): Observable<Job>{
    return this.http.get<Job>(this.baseUrl + "/get-job/" + jobID , {
      headers: {
        'Content-Type': 'application/json'
    }
    });
  }

  updateStatus(jobID: number): Observable<string>{
    return this.http.patch<string>(this.baseUrl + "/update-status/" + jobID , {
      headers: {
        'Content-Type': 'application/json'
    }
    });
  }

  getJobRequirements(jobID: number): Observable<any[]>{
    return this.http.get<any[]>(this.baseUrl + "/get-job-requirements/" + jobID , {
      headers: {
        'Content-Type': 'application/json'
    }
    });
  }

  getJobResponsibilities(jobID: number): Observable<any[]>{
    return this.http.get<any[]>(this.baseUrl + "/get-job-responsibilities/" + jobID, {
      headers: {
        'Content-Type': 'application/json'
    }
    });
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