import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JobApplicationService } from 'src/app/services/job-application.service';
import { JobService } from 'src/app/services/job.service';
import { JobApplication } from 'src/app/types/job-application';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  jobApplication: any = {}
  job: any = {}

  constructor(private route: ActivatedRoute, private jobApplicationService: JobApplicationService, private jobService: JobService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.loadJobApplication(+params['applicationID']);
    });
  }

  loadJobApplication(jobApplicationID: number): void {
    this.jobApplicationService.getJobApplication(jobApplicationID).subscribe(
      (data: any) => { 
        this.jobApplication = data;
        if (this.jobApplication.jobID) {
          this.loadJob(this.jobApplication.jobID);
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }

  loadJob(jobID: number): void {
    this.jobService.getJob(jobID).subscribe(
      (data: any) => { 
        this.job = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  bscAIUB(jobApplication: JobApplication){

    return jobApplication.bscAIUB === 'true' ? true : false

  }

  mscAIUB(jobApplication: JobApplication){

    return jobApplication.mscAIUB === 'true' ? true : false

  }

  bscStatus(jobApplication: JobApplication){

    if(jobApplication.bscStatus === 'false') return "Did Not Appear"
    else if(jobApplication.bscStatus === 'true' && jobApplication.bscGraduate === 'true') return "Graduated"
    else if(jobApplication.bscStatus === 'true' && jobApplication.bscGraduate === 'false') return "On Study"
    else return ''

  }

  mscStatus(jobApplication: JobApplication){

    if(jobApplication.mscStatus === 'false') return "Did Not Appear"
    else if(jobApplication.mscStatus === 'true' && jobApplication.mscGraduate === 'true') return "Graduated"
    else if(jobApplication.mscStatus === 'true' && jobApplication.mscGraduate === 'false') return "On Study"
    else return ''

  }


}
