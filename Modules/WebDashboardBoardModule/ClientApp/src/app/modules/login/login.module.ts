import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [LoginComponent],
    imports: [CommonModule, RouterModule],
})
export class LoginModule {}
