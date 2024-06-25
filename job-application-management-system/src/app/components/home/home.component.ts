import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { JobApplicationService } from 'src/app/services/job-application.service';
import { JobService } from 'src/app/services/job.service';
import { Job } from 'src/app/types/job';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  token = localStorage.getItem("access-token");
  role = this.token != null ? 'Admin' : 'User';
  jobs: Job[] = [];
  jobRequirements: any[] = [];
  jobResponsibilities: any[] = [];
  visible: boolean = false;
  loadJob: any = {};

  constructor(private authService: AuthService, private jobService: JobService, private jobApplicationService: JobApplicationService, private router: Router) { }

  ngOnInit(): void {
    this.loadJobs();
  }

  getRemainingVacancies(job: any): number {
    return Number.parseInt(job.maxApplicants) - Number.parseInt(job.applicants);
  }

  loadJobs(): void {
    this.jobService.getAllJobs().subscribe(
      (data: Job[]) => { 
        this.jobs = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  loadJobRequirements(jobID: number): void {
    this.jobService.getJobRequirements(jobID).subscribe(
      (data: any) => { 
        this.jobRequirements = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  loadJobResponsibilities(jobID: number): void {
    this.jobService.getJobResponsibilities(jobID).subscribe(
      (data: any) => { 
        this.jobResponsibilities = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  showDialog(jobID: number): void {
    this.jobService.getJob(jobID).subscribe(
      (data: Job) => { 
        this.loadJob = data;
        this.loadJobRequirements(jobID);
        this.loadJobResponsibilities(jobID);
        this.visible = true;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  apply(jobID: number): void {
    this.router.navigate(['/application-form', jobID]);
  }

  truncateDescription(description: string): string {
    if (description.length > 164) {
      return description.substring(0, 164) + '...';
    }
    return description;
  }

  onBack(): void {
    this.router.navigate(['/']);  
  }

}