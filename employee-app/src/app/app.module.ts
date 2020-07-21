import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from './header/header.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ValidationMessageComponent } from './shared/validation-message/validation-message.component';
import { NgxPaginationModule }from 'ngx-pagination';
import { HttpClientModule } from '@angular/common/http';
import { DateDisplayPipe } from './Util/date-display.pipe';
import { ErrorComponent } from './error/error.component';
import { UploadFileComponent } from './upload-file/upload-file.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    EmployeeListComponent,
    EmployeeEditComponent,
    ValidationMessageComponent,
    DateDisplayPipe,
    ErrorComponent,
    UploadFileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
