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
export class OpeningFormComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private jobService: JobService,
    private router: Router,
    private messageService: MessageService,
    private datePipe: DatePipe
  ) {}

  workHourStart: Date = new Date();
  workHourEnd: Date = new Date();
  type: string = 'Part Time';
  jobID: number | null = null;
  salary: number = 0.00;
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
      (data) => {
        this.jobTitle = data.jobTitle;
        this.designation = data.designation;
        this.type = data.jobType;
        this.workHourStart = this.parseTime(data.workHourStart);
        this.workHourEnd = this.parseTime(data.workHourEnd);
        this.salary = parseFloat(data.salary);
        this.negotiable = data.negotiable === 'true';
        this.description = data.description;
        this.phone = data.phone;
        this.email = data.email;
        this.location = data.location;
        this.maxApplicants = Number.parseInt(data.maxApplicants);
        this.loadJobRequirements(data.jobID),
        this.loadJobResponsibilities(data.jobID),
        this.deadline = this.parseDate(data.deadline);
        this.status = data.status === 'true';
      },
      (error) => {
        console.error(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load job details' });
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
      const opening = {
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
        maxApplicants: this.maxApplicants,
        postedOn: this.currentDate(),
        deadline: this.formatDate(this.deadline),
        applicants: '0',
        status: this.status.toString(),
        requirements: this.requirements,
        responsibilities: this.responsibilities
      };

      if (this.jobID) {
        this.updateOpening(this.jobID, opening);
      } else {
        this.createOpening(opening);
      }
    } else {
      this.messageService.add({ severity: 'warn', summary: 'Error', detail: 'Could not create or update opening' });
    }
  }

  createOpening(opening: any): void {
    this.jobService.createOpening(opening).subscribe(
      response => {
        console.log(response);
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'New opening created successfully' });
        setTimeout(() => {
          this.router.navigate(['/home']);
        }, 2000);
      },
      error => {
        console.error(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create opening' });
      }
    );
  }

  loadJobRequirements(jobID: number): void {
    this.jobService.getJobRequirements(jobID).subscribe(
      (data: any) => { 
        this.requirements = data.map((item: any) => item.requirement);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  loadJobResponsibilities(jobID: number): void {
    this.jobService.getJobResponsibilities(jobID).subscribe(
      (data: any) => { 
        this.responsibilities = data.map((item: any) => item.responsibility);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  updateOpening(jobID: number, opening: any): void {
    this.jobService.updateJob(jobID, opening).subscribe(
      response => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Opening updated successfully' });
        setTimeout(() => {
          this.router.navigate(['/home']);
        }, 2000);
      },
      error => {
        console.error(error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update opening' });
      }
    );
  }

  validateForm(): boolean {
    return true; 
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
