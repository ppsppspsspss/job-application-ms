import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { JobService } from 'src/app/services/job.service';
import { Job } from 'src/app/types/job';
import { Result } from 'src/app/types/result';

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.css'],
  providers: [MessageService]
})
export class JobListComponent {

  jobs: Job[] = [];
  loadJobInfo: any = {};
  checked: boolean = true;
  visible: boolean = false;

  constructor(private jobService: JobService, private router: Router, private messageService: MessageService) {}

  ngOnInit() {
    this.loadJobs();
  }

  getRemainingVacancies(job: any): number {
    return (Number.parseInt(job.maxApplicants) - Number.parseInt(job.applicants));
  }

  loadJobs(): void {
    this.jobService.getAllJobs(true).subscribe(
      (result: Result<Job[]>) => {
        if (!result.isError) this.jobs = result.data || []; 
        else {
          console.log(result.messages);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load jobs' });
          this.jobs = []; 
        }
      },
      (error) => {
        console.log(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load jobs' });
        this.jobs = []; 
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

  showDialog(event: Event, job: any): void {
    event.stopPropagation();
    this.jobService.getJob(job.jobID).subscribe(
      (result) => {
        if (!result.isError && result.data) {
          const jobData = result.data;
          this.loadJobInfo = {
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
            applicants: jobData.applicants,
            postedOn: jobData.postedOn,
            deadline: jobData.deadline,
            status: jobData.status,
            jobRequirements: jobData.requirements,
            jobResponsibilities: jobData.responsibilities
          };
          this.visible = true;

        } 
        else {
          console.log(result.messages);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to fetch job details' });
        }
      },
      (error) => {
        console.log(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An error occurred while fetching job details' });
      }
    );
  }

  createNewOpening(){
    this.router.navigateByUrl('opening-form');
  }

  updateStatus(jobID: number): void {
    this.jobService.updateStatus(jobID).subscribe(
      (response) => {
        if (response.isError) console.error(response.messages.join(', '));
      },
      (error) => {
        console.log(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update status' });
      }
    );
  }

  updateOpening(event: Event, jobID: number): void {
    event.stopPropagation();
    this.router.navigate(['/opening-form', jobID]);
  }

  deleteOpening(event: Event, jobID: number): void {
    event.stopPropagation();
    this.jobService.deleteJob(jobID).subscribe(
      (response) => {
        if (response.isError) {
          const errorMessage = response.messages.join(', ');
          console.error(errorMessage);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: errorMessage });
        } 
        else this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Job deleted successfully' });
        this.loadJobs();
      },
      (error) => {
        console.log(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete opening' });
      }
    );
  }
  

}
