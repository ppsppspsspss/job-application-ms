import { Component, Input, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { JobApplicationService } from 'src/app/services/job-application.service';
import { JobApplication } from 'src/app/types/job-application';
import { Result } from 'src/app/types/result';
import * as FileSaver from 'file-saver';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {

  @Input() jobID: number | null = null;

  applications: JobApplication[] = [];
  selectedApplications: JobApplication[] = [];

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
      (result: Result<JobApplication[]>) => {
        if (!result.isError && result.data) {
          this.applications = result.data;
        } 
        else {
          console.log(result.messages);
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  loadProfile(applicationID: number): void {
    if (this.jobID !== null) {
      this.router.navigate(['/applications', this.jobID, 'profile', applicationID]);
    }
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

  download() {
    import('xlsx').then((xlsx) => {
      const worksheet = xlsx.utils.json_to_sheet(this.applications);
      const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
      const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
      this.saveAsExcelFile(excelBuffer, 'job_applications');
    });
  }

  saveAsExcelFile(buffer: any, fileName: string): void {
    const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
    const EXCEL_EXTENSION = '.xlsx';
    const data: Blob = new Blob([buffer], { type: EXCEL_TYPE });
    FileSaver.saveAs(data, `${fileName}_${new Date().getTime()}${EXCEL_EXTENSION}`);
  }

}
