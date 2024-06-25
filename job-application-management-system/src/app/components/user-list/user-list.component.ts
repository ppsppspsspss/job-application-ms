import { Component, Input, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JobApplicationService } from 'src/app/services/job-application.service';
import { JobService } from 'src/app/services/job.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {

  @Input() jobID: number | null = null;
  applications: any[] = [];

  constructor(private jobApplicationService: JobApplicationService, private router: Router) { }

  ngOnInit(): void {
    if (this.jobID !== null) {
      this.loadApplications(this.jobID);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['jobID'] && this.jobID !== null) {
      this.loadApplications(this.jobID);
    }
  }

  loadApplications(jobID: number): void {
    this.jobApplicationService.getAllJobApplications(jobID).subscribe(
      (data: any) => { 
        this.applications = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  loadProfile(applicationID: number): void {
    if (this.jobID !== null) {
      this.router.navigate(['/applications', this.jobID, 'profile', applicationID]);
    }
  }

}
