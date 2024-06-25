import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JobService } from 'src/app/services/job.service';
import { MessageService } from 'primeng/api';
import { JobApplicationService } from 'src/app/services/job-application.service';

@Component({
  selector: 'app-application-form',
  templateUrl: './application-form.component.html',
  styleUrls: ['./application-form.component.css'],
  providers: [MessageService]
})
export class ApplicationFormComponent implements OnInit {

  constructor(private route: ActivatedRoute, private jobService: JobService, private jobApplicationService: JobApplicationService,private router: Router, private messageService: MessageService) { }

  jobID: number | null = null;
  bsc: boolean = false;
  msc: boolean = false;
  date: string = '';
  bscAIUB: boolean = false;
  bscGraduate: boolean = false;
  mscAIUB: boolean = false;
  mscGraduate: boolean = false;
  skills: string[] = [];
  firstName: string = '';
  lastName: string = '';
  fathersName: string = '';
  mothersName: string = '';
  phone: string = '';
  email: string = '';
  currentAddress: string = '';
  permanentAddress: string = '';
  sameAsCurrentAddress: boolean = false;
  bscAdmissionDate: string = '';
  universityBsc: string = '';
  cgpaBsc: string = '';
  aiubIdBsc: string = '';
  bscGraduationDate: string = '';
  mscAdmissionDate: string = '';
  universityMsc: string = '';
  cgpaMsc: string = '';
  aiubIdMsc: string = '';
  mscGraduationDate: string = '';
  expectedSalary: number = 0.00;

  job: any = {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.jobID = +params['jobID'];
      this.loadJob(this.jobID);
    });
    
    this.setUniversity()
  }

  onSubmit() {
    if (this.validateForm()) {

      const jobApplication = {
        jobID: this.jobID,
        firstName: this.firstName,
        lastName: this.lastName,
        fathersName: this.fathersName,
        mothersName: this.mothersName,
        phone: this.phone,
        email: this.email,
        currentAddress: this.currentAddress,
        permanentAddress: this.permanentAddress,
        bscStatus: this.bsc ? "true" : "false",
        bscAdmissionDate: this.bsc ? this.formatDate(this.bscAdmissionDate) : null,
        bscAIUB: this.bscAIUB ? "true" : "false",
        bscUniversity: this.universityBsc,
        bscCGPA: this.bscAIUB ? null : this.cgpaBsc,
        bscAIUBID: this.bscAIUB ? this.aiubIdBsc : null,
        bscGraduate: this.bscGraduate ? "true" : "false",
        bscGraduationDate: this.bscGraduate ? this.formatDate(this.bscGraduationDate) : null,
        mscStatus: this.msc ? "true" : "false",
        mscAdmissionDate: this.msc ? this.formatDate(this.mscAdmissionDate) : null,
        mscAIUB: this.mscAIUB ? "true" : "false",
        mscUniversity: this.universityMsc,
        mscCGPA: this.msc ? null : this.cgpaMsc,
        mscAIUBID: this.mscAIUB ? this.aiubIdMsc : null,
        mscGraduate: this.mscGraduate ? "true" : "false",
        mscGraduationDate: this.mscGraduate ? this.formatDate(this.mscGraduationDate) : null,
        cv: null,
        skills: this.skills
      };
  
      this.jobApplicationService.jobApplication(jobApplication).subscribe(
        response => {
          console.log(response);
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Application submitted successfully' });
          setTimeout(() => {
            this.router.navigate(['/home']);
          }, 2000); 
        },
        error => {
          console.error(error);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to submit application' });
        }
      );

    } else {
      this.messageService.add({ severity: 'warn', summary: 'Error', detail: 'Could not submit application' });
      console.log('Form is invalid');
    }
  }

  validateForm(): boolean {
    
    return true;
  }


  onCancel(){
    this.router.navigate(['/home']);
  }

  onSameAsCurrentAddressChange() {
    if (this.sameAsCurrentAddress) {
      this.permanentAddress = this.currentAddress;
    } else {
      this.permanentAddress = '';
    }
  }

  setUniversity() {
    if (this.bscAIUB) {
      this.universityBsc = 'American International University-Bangladesh';
    } else {
      this.universityBsc = '';
    }

    if (this.mscAIUB) {
      this.universityMsc = 'American International University-Bangladesh';
    } else {
      this.universityMsc = '';
    }
  }

  onBscAIUBChange() {
    if (this.bscAIUB) {
      this.universityBsc = 'American International University-Bangladesh';
    } else {
      this.universityBsc = '';
    }
  }

  onMscAIUBChange() {
    if (this.mscAIUB) {
      this.universityMsc = 'American International University-Bangladesh';
    } else {
      this.universityMsc = '';
    }
  }

  formatDate(dateString: string | null): string | null {
    if (dateString) {
        const date = new Date(dateString);
        const day = date.getDate();
        const month = date.getMonth() + 1; 
        const year = date.getFullYear();
        return `${day}/${month}/${year}`;
    } else {
        return null;
    }
}

  onBack(): void {
    this.router.navigate(['/home']);  
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

}
