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
export class ApplicationFormComponent {

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
  cvFile: File | null = null;
  coverLetterFile: File | null = null;

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

      const jobApplication = new FormData();

      if (this.jobID !== null && this.jobID !== undefined) jobApplication.append('jobID', this.jobID.toString());
      jobApplication.append('firstName', this.firstName);
      jobApplication.append('lastName', this.lastName);
      jobApplication.append('fathersName', this.fathersName);
      jobApplication.append('mothersName', this.mothersName);
      jobApplication.append('phone', this.phone);
      jobApplication.append('email', this.email);
      jobApplication.append('currentAddress', this.currentAddress);
      jobApplication.append('permanentAddress', this.permanentAddress);
      jobApplication.append('bscStatus', this.bsc ? "true" : "false");
      jobApplication.append('bscAdmissionDate', this.bsc ? this.formatDate(this.bscAdmissionDate) || '' : '');
      jobApplication.append('bscAIUB', this.bscAIUB ? "true" : "false");
      jobApplication.append('bscUniversity', this.universityBsc);
      jobApplication.append('bscCGPA', this.bscAIUB ? '' : this.cgpaBsc.toString());
      jobApplication.append('bscAIUBID', this.bscAIUB ? this.aiubIdBsc : '');
      jobApplication.append('bscGraduate', this.bscGraduate ? "true" : "false");
      jobApplication.append('bscGraduationDate', this.bscGraduate ? this.formatDate(this.bscGraduationDate) || '' : '');
      jobApplication.append('mscStatus', this.msc ? "true" : "false");
      jobApplication.append('mscAdmissionDate', this.msc ? this.formatDate(this.mscAdmissionDate) || '' : '');
      jobApplication.append('mscAIUB', this.mscAIUB ? "true" : "false");
      jobApplication.append('mscUniversity', this.universityMsc);
      jobApplication.append('mscCGPA', this.msc ? '' : this.cgpaMsc.toString());
      jobApplication.append('mscAIUBID', this.mscAIUB ? this.aiubIdMsc : '');
      jobApplication.append('mscGraduate', this.mscGraduate ? "true" : "false");
      jobApplication.append('mscGraduationDate', this.mscGraduate ? this.formatDate(this.mscGraduationDate) || '' : '');
      if (this.cvFile) jobApplication.append('cv', this.cvFile, this.cvFile.name);
      if (this.coverLetterFile) jobApplication.append('coverLetter', this.coverLetterFile, this.coverLetterFile.name);
      jobApplication.append('skills', JSON.stringify(this.skills));
  
      this.jobApplicationService.jobApplication(jobApplication).subscribe(
        response => {
          if (!response.isError) {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Application submitted successfully' });
            setTimeout(() => {
              this.router.navigate(['/home']);
            }, 2000);
          } else {
            response.messages.forEach((message: string) => {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: message });
            });
          }
        },
        error => {
          console.error(error);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to submit application' });
        }
      );
    } 
    else {
      this.messageService.add({ severity: 'warn', summary: 'Error', detail: 'Could not submit application' });
      console.log('Form is invalid');
    }
  }  

  onCVUpload(event: any) {
    this.cvFile = event.files[0];
  }

  onCoverLetterUpload(event: any): void {
    this.coverLetterFile = event.files[0];
  }

  validateForm(): boolean {

    let isValid = true;
    const containsNumberRegex = /\d/;
    const cgpaRegex = /^([0-3](\.\d{1,2})?|4(\.00?)?)$/;
  
    if (!this.firstName) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'First name field is required' });
      isValid = false;
    }
    else if (containsNumberRegex.test(this.firstName)) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'First name is invalid' });
      isValid = false;
    }

    if (!this.lastName) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Last name field is required' });
      isValid = false;
    }
    else if (containsNumberRegex.test(this.lastName)) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Last name is invalid' });
      isValid = false;
    }

    if (!this.fathersName) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Father\'s name field is required' });
      isValid = false;
    }
    else if (containsNumberRegex.test(this.fathersName)) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Father\'s name is invalid' });
      isValid = false;
    }

    if (!this.mothersName) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Mother\'s name field is required' });
      isValid = false;
    }
    else if (containsNumberRegex.test(this.mothersName)) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Mother\'s name is invalid' });
      isValid = false;
    }

    if (!this.phone) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Phone number field is required' });
      isValid = false;
    } 
    else if (!/^(01)\d{9}$/.test(this.phone)) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Invalid phone number' });
      isValid = false;
    }
    if (!this.email) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Email address field is required' });
      isValid = false;
    } 
    else if (!this.isValidEmailFormat(this.email)) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Invalid email' });
      isValid = false;
    } 
    if (!this.currentAddress) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Current address field is required' });
      isValid = false;
    }
    if (!this.permanentAddress) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Permanent field is required' });
      isValid = false;
    }
  
    if (this.bsc) {
      if (this.bscAIUB && (!this.aiubIdBsc || this.aiubIdBsc.trim() === '')) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'AIUB ID field is required' });
        isValid = false;
      }
      if (!this.universityBsc) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'University field is required' });
        isValid = false;
      }
      if (!this.cgpaBsc) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'CGPA field is required' });
        isValid = false;
      }
      else if (!cgpaRegex.test(this.cgpaBsc)) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Invalid CGPA' });
        isValid = false;
      }

      if (this.bscGraduate && !this.bscGraduationDate) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Graduation date field is required' });
        isValid = false;
      }
      if (!this.bscGraduate && !this.bscAdmissionDate) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Admission date field is required' });
        isValid = false;
      }
    } 
    else {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Can not apply without BSc' });
      isValid = false;
    }
  
    if (this.msc) {
      if (this.mscAIUB && (!this.aiubIdMsc || this.aiubIdMsc.trim() === '')) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'AIUB ID field is required' });
        isValid = false;
      }
      if (!this.universityMsc) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'University field is required' });
        isValid = false;
      }
      if (!this.cgpaMsc) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'CGPA field is required' });
        isValid = false;
      }
      else if (!cgpaRegex.test(this.cgpaMsc)) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Invalid CGPA.' });
        isValid = false;
      }

      if (this.mscGraduate && !this.mscGraduationDate) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Graduation date field is required' });
        isValid = false;
      }
      if (!this.mscGraduate && !this.mscAdmissionDate) {
        this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Admission date field is required' });
        isValid = false;
      }
    } 
  
    return isValid;
  }
  
  isValidEmailFormat(email: string): boolean {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  }
  
  onCancel(){
    this.router.navigate(['/home']);
  }

  onSameAsCurrentAddressChange() {
    if (this.sameAsCurrentAddress) this.permanentAddress = this.currentAddress;
    else this.permanentAddress = '';
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
    } 
    else return null;
  }

  onBack(): void {
    this.router.navigate(['/home']);  
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

}
