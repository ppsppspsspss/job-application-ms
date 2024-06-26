import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Job } from '../types/job';
import { Result } from '../types/result';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  baseUrl = "http://localhost:5171/api/job"
  
  createOpening(openingForm: any): Observable<Result<string>> {
    const token = this.authService.getToken(); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.post<Result<string>>(`${this.baseUrl}/create-opening`, openingForm, { headers });
  }

  getAllJobs(showAll: boolean = false): Observable<Result<Job[]>> {
    return this.http.get<Result<Job[]>>(`${this.baseUrl}/get-all-jobs/${showAll}`);
  }

  getJob(jobID: number): Observable<Result<Job>> {
    return this.http.get<Result<Job>>(`${this.baseUrl}/get-job/${jobID}`);
  }

  updateStatus(jobID: number): Observable<Result<string>> {
    const token = this.authService.getToken(); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.patch<Result<string>>(`${this.baseUrl}/update-status/${jobID}`, {}, { headers });
  }

  deleteJob(jobID: number): Observable<Result<string>> {
    const token = this.authService.getToken(); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.delete<Result<string>>(`${this.baseUrl}/delete-job/${jobID}`, { headers });
  }

  updateJob(jobID: number, jobApplication: any): Observable<Result<string>> {
    const token = this.authService.getToken(); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.put<Result<string>>(`${this.baseUrl}/update-job/${jobID}`, jobApplication, { headers });
  }

}