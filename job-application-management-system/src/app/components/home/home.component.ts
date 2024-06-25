import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';
import { JobApplicationService } from 'src/app/services/job-application.service';
import { JobService } from 'src/app/services/job.service';
import { Job } from 'src/app/types/job';
import { Result } from 'src/app/types/result';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [MessageService]
})
export class HomeComponent implements OnInit {

  token = localStorage.getItem("access-token");
  role = this.token != null ? 'Admin' : 'User';
  jobs: Job[] = [];
  jobRequirements: any[] = [];
  jobResponsibilities: any[] = [];
  visible: boolean = false;
  loadJob: any = {};

  constructor(private authService: AuthService, private jobService: JobService, private jobApplicationService: JobApplicationService, private router: Router, private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadJobs();
  }

  getRemainingVacancies(job: any): number {
    return Number.parseInt(job.maxApplicants) - Number.parseInt(job.applicants);
  }

  loadJobs(): void {
    this.jobService.getAllJobs().subscribe(
      (result: Result<Job[]>) => {
        if (!result.isError && result.data) {
          this.jobs = result.data;
        } 
        else {
          this.jobs = []
          console.error('Error loading jobs:', result.messages);
        }
      },
      (error) => {
        console.error('Error loading jobs:', error);
      }
    );
  }

  loadJobRequirements(jobID: number): void {
    this.jobService.getJobRequirements(jobID).subscribe(
      (result: Result<any[]>) => {
        if (!result.isError) {
          this.jobRequirements = result.data || [];
        } else {
          console.error('Error loading job requirements:', result.messages);
        }
      },
      (error) => {
        console.error('Error loading job requirements:', error);
      }
    );
  }

  loadJobResponsibilities(jobID: number): void {
    this.jobService.getJobResponsibilities(jobID).subscribe(
      (result: Result<any[]>) => {
        if (!result.isError) {
          this.jobResponsibilities = result.data || []; 
        } else {
          console.error('Error loading job responsibilities:', result.messages);
        }
      },
      (error) => {
        console.error('Error loading job responsibilities:', error);
      }
    );
  }

  showDialog(jobID: number): void {
    this.jobService.getJob(jobID).subscribe(
      (result) => {
        if (!result.isError && result.data) {
          const jobData = result.data;
          this.loadJob = {
            jobID: jobData.jobID,
            jobTitle: jobData.jobTitle,
            designation: jobData.designation,
            jobType: jobData.jobType,
            workHourStart: jobData.workHourStart,
            workHourEnd: jobData.workHourEnd,
            salary: jobData.salary,
            negotiable: jobData.negotiable,
            description: jobData.description,
            phone: jobData.phone,
            email: jobData.email,
            location: jobData.location,
            maxApplicants: jobData.maxApplicants,
            deadline: jobData.deadline,
            status: jobData.status
          };
  
          this.loadJobRequirements(jobID);
          this.loadJobResponsibilities(jobID);
          this.visible = true;
        } else {
          console.error('Failed to fetch job details:', result.messages);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to fetch job details' });
        }
      },
      (error) => {
        console.error('Error occurred while fetching job details:', error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An error occurred while fetching job details' });
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