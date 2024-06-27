import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { JobService } from 'src/app/services/job.service';

@Component({
  selector: 'app-opening-form',
  templateUrl: './opening-form.component.html',
  styleUrls: ['./opening-form.component.css'],
  providers: [MessageService, DatePipe]
})

export class OpeningFormComponent {

  constructor(private route: ActivatedRoute, private jobService: JobService, private router: Router, private messageService: MessageService, private datePipe: DatePipe) {}

  workHourStart: Date = new Date();
  workHourEnd: Date = new Date();
  type: string = 'Part Time';
  jobID: number | null = null;
  salary: number = 0.00;
  unlimited: boolean = false;
  maxApplicants: number | null = null;
  jobTitle: string = '';
  designation: string = '';
  phone: string = '01402246680';
  email: string = 'rianulamin.r@gmail.com';
  description: string = '';
  requirements: string[] = [];
  responsibilities: string[] = [];
  location: string = 'Software Development Department, Room no: DN0627, Level-6, Building- D, AIUB.';
  negotiable: boolean = false;
  status: boolean = true;
  deadline: Date | null = null;
  applicants: number | null = null;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.jobID = +params['jobID'] || null;
      if (this.jobID) {
        this.loadJobDetails(this.jobID);
      }
    });
  }

  loadJobDetails(jobID: number): void {
    this.jobService.getJob(jobID).subscribe(
      (result) => {
        if (!result.isError && result.data) {
          const job = result.data;
          this.jobTitle = job.jobTitle;
          this.designation = job.designation;
          this.type = job.jobType;
          this.workHourStart = this.parseTime(job.workHourStart);
          this.workHourEnd = this.parseTime(job.workHourEnd);
          this.salary = parseFloat(job.salary);
          this.negotiable = job.negotiable === 'true';
          this.description = job.description;
          this.phone = job.phone;
          this.email = job.email;
          this.location = job.location;
          this.maxApplicants = Number.parseInt(job.maxApplicants);
          this.deadline = this.parseDate(job.deadline);
          this.status = job.status === 'true';
          this.applicants = Number.parseInt(job.applicants);
  
          this.loadJobRequirements(job.jobID);
          this.loadJobResponsibilities(job.jobID);
          
        } else {
          console.log(result.messages);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load job details' });
        }
      },
      (error) => {
        console.log(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An error occurred while fetching job details' });
      }
    );
  }

  parseDate(dateString: string): Date {
    const [day, month, year] = dateString.split(' ');
    const monthIndex = new Date(`${month} 1`).getMonth(); 
    const date = new Date(Number.parseInt(year), monthIndex, parseInt(day));
    return date;
  }

  parseTime(timeString: string): Date {
    const [time, modifier] = timeString.split(' ');
    let [hours, minutes] = time.split(':').map(Number);

    if (modifier === 'PM' && hours < 12) {
      hours += 12;
    }
    if (modifier === 'AM' && hours === 12) {
      hours = 0;
    }

    const date = new Date();
    date.setHours(hours, minutes, 0, 0);
    return date;
  }

  onSubmit() {

    if (this.validateForm()) {

      const opening: any = {

        jobTitle: this.jobTitle,
        designation: this.designation,
        jobType: this.type,
        workHourStart: this.formatTime(this.workHourStart),
        workHourEnd: this.formatTime(this.workHourEnd),
        salary: this.salary.toString(),
        negotiable: this.negotiable.toString(),
        description: this.description,
        phone: this.phone,
        email: this.email,
        location: this.location,
        maxApplicants: this.maxApplicants?.toString(),
        deadline: this.formatDate(this.deadline),
        status: this.status.toString(),
        requirements: this.requirements,
        responsibilities: this.responsibilities

      };

      if (this.jobID) this.updateOpening(this.jobID, opening);

      else{
        opening.postedOn = this.currentDate();
        opening.applicants = '0';
        this.createOpening(opening);
      } 
  
    }
    else this.messageService.add({ severity: 'warn', summary: 'Error', detail: 'Could not create or update opening' });
  }

  createOpening(opening: any): void {
    this.jobService.createOpening(opening).subscribe(
      (response: any) => {
        console.log(response);
        if (response.isError && response.messages) {
          response.messages.forEach((message: string) => {
            this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: message });
          });
        } else {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'New opening created successfully' });
          setTimeout(() => {
            this.router.navigate(['/home']);
          }, 2000);
        }
      },
      (error: any) => {
        console.log(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create opening' });
      }
    );
  }
  

  loadJobRequirements(jobID: number): void {
    this.jobService.getJobRequirements(jobID).subscribe(
      (response: any) => {
        if (Array.isArray(response.data)) {
          this.requirements = response.data.map((item: any) => item.requirement);
        } 
      },
      (error) => {
        console.log(error);
      }
    );
  }

  loadJobResponsibilities(jobID: number): void {
    this.jobService.getJobResponsibilities(jobID).subscribe(
      (response: any) => {
        if (Array.isArray(response.data)) {
          this.responsibilities = response.data.map((item: any) => item.responsibility);
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  updateOpening(jobID: number, opening: any): void {
    this.jobService.updateJob(jobID, opening).subscribe(
      (response) => {
        if (!response.isError) {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Opening updated successfully' });
          setTimeout(() => {
            this.router.navigate(['/home']);
          }, 2000);
        } 
        else {
          console.log(response.messages); 
          response.messages.forEach((message: string) => {
            this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: message });
          });
        }
      },
      (error) => {
        console.log(error); 
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update opening' });
      }
    );
  }

  validateForm(): boolean {

    let flag = true
    
    if (!this.jobTitle || this.jobTitle.trim() === '') {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Opening title cannot be empty' });
      flag = false;
    }

    if (!this.designation || this.designation.trim() === '') {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Designation cannot be empty' });
      flag = false;
    }

    if (this.workHourStart >= this.workHourEnd) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Work hour start cannot be greater than work hour end' });
      flag = false;
    }

    if (!this.description || this.description.trim() === '') {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Description cannot be empty' });
      flag = false;
    }

    if (this.maxApplicants === null || this.maxApplicants <= 0) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Max applicants must be greater than 0' });
      flag = false;
    }

    if (this.maxApplicants !== null && this.applicants !== null && this.maxApplicants < this.applicants) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: `Max applicants cannot be less than the current number of applicants (${this.applicants})` });
      flag = false;
    }

    if (!this.deadline || new Date(this.deadline) < new Date()) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Opening deadline cannot be empty or a past date' });
      flag = false;
    }

    return flag;

  }

  formatTime(date: Date): string {
    return this.datePipe.transform(date, 'h:mm a')!;
  }

  formatDate(date: Date | null): string | null {
    if (date) {
      return this.datePipe.transform(date, 'd MMMM, y')!;
    } else {
      return null;
    }
  }

  currentDate(): string {
    const date = new Date();
    return this.datePipe.transform(date, 'd MMMM, y')!;
  }

  onCancel(){
    this.router.navigate(['/home']);
  }

  onBack(): void {
    this.router.navigate(['/home']);
  }
}
