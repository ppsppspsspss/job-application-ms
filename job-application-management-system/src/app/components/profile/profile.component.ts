import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JobApplicationService } from 'src/app/services/job-application.service';
import { JobService } from 'src/app/services/job.service';
import { JobApplication } from 'src/app/types/job-application';
import { Result } from 'src/app/types/result';

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
      (data: Result<JobApplication>) => {
        if (!data.isError && data.data) {
          this.jobApplication = data.data;
          if (this.jobApplication.jobID) {
            this.loadJob(this.jobApplication.jobID);
          }
        } else {
          console.log(data.messages);
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  loadJob(jobID: number): void {
    this.jobService.getJob(jobID).subscribe(
      (data: any) => { 
        this.job = data.data;
      },
      (error) => {
        console.log(error);
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

  showPdf(base64String: string) {
    const arrayBuffer = this.base64ToArrayBuffer(base64String);
    const blob = new Blob([arrayBuffer], { type: 'application/pdf' });
    const link = window.URL.createObjectURL(blob);
    window.open(link);
  }

  private base64ToArrayBuffer(base64: string): ArrayBuffer {
    const binaryString = window.atob(base64); 
    const len = binaryString.length;
    const bytes = new Uint8Array(len);
    for (let i = 0; i < len; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }
    return bytes.buffer;
  }


}
