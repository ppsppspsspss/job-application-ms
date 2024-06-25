import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JobService } from 'src/app/services/job.service';
import { Job } from 'src/app/types/job';

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.css']
})
export class JobListComponent {

  jobs: Job[] = [];
  loadJobInfo: Partial<Job> = {};
  jobRequirements: any[] = [];
  jobResponsibilities: any[] = [];
  checked: boolean = true;
  visible: boolean = false;

  constructor(private jobService: JobService, private router: Router) {}

  ngOnInit() {
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
        console.log(error)
      }
    );
  }

  loadJob(jobID: number){
    this.router.navigate(['/applications/', jobID]);
  }

  toggleStatus(event: Event, job: any) {
    event.stopPropagation();
    this.updateStatus(job.jobID);
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

  showDialog(event: Event, job: any): void {
    event.stopPropagation();
    this.jobService.getJob(job.jobID).subscribe(
      (data: Job) => { 
        this.loadJobInfo = data;
        this.loadJobRequirements(job.jobID);
        this.loadJobResponsibilities(job.jobID);
        this.visible = true;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  createNewOpening(){
    this.router.navigateByUrl('opening-form');
  }

  updateStatus(jobID: number): void {
    this.jobService.updateStatus(jobID).subscribe();
  }

  updateOpening(event: Event, jobID: number): void {
    event.stopPropagation();
    this.router.navigate(['/opening-form', jobID]);
  }

  deleteOpening(event: Event, jobID: number): void {
    event.stopPropagation();
    this.jobService.deleteJob(jobID).subscribe(
      () => {
        this.loadJobs(); 
      },
      (error) => {
        console.error(error);
      }
    );
  }

}
