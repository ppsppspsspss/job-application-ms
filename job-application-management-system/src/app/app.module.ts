import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { DialogModule } from 'primeng/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CheckboxModule } from 'primeng/checkbox';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { CalendarModule } from 'primeng/calendar';
import { ChipsModule } from 'primeng/chips';
import { FileUploadModule } from 'primeng/fileupload';
import { ToastModule } from 'primeng/toast';
import { TableModule } from 'primeng/table';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { HomeComponent } from './components/home/home.component';
import { CareerComponent } from './components/career/career.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { AuthService } from './services/auth.service';
import { ApplicationFormComponent } from './components/application-form/application-form.component';
import { HeaderComponent } from './components/header/header.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { ProfileComponent } from './components/profile/profile.component';
import { JobListComponent } from './components/job-list/job-list.component';
import { ApplicationsComponent } from './components/applications/applications.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { OpeningFormComponent } from './components/opening-form/opening-form.component';

@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
    HomeComponent,
    CareerComponent,
    SignInComponent,
    ApplicationFormComponent,
    HeaderComponent,
    UserListComponent,
    ProfileComponent,
    JobListComponent,
    ApplicationsComponent,
    NavbarComponent,
    OpeningFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ButtonModule,
    CardModule,
    DialogModule,
    BrowserAnimationsModule,
    InputTextModule,
    InputTextareaModule,
    CheckboxModule,
    ToggleButtonModule,
    FormsModule,
    CalendarModule,
    ChipsModule,
    FileUploadModule,
    ToastModule,
    TableModule,
    RadioButtonModule,
    InputNumberModule,
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }