import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../material/material.module';

@NgModule({
    declarations: [LoginComponent],
    imports: [CommonModule, RouterModule, MaterialModule],
})
export class LoginModule {}
