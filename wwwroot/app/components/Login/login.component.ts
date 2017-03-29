import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: './app/components/login/login.component.html',
    styleUrls: ['./app/components/login/login.component.css']
})
export class LoginComponent {
    title = "Login";
    loginForm = null;

    constructor(private fb: FormBuilder, private router: Router) {

        this.loginForm = fb.group({
            username: ["", Validators.required],
            password: ["", Validators.required]
        });

    }

    perforLogin(e) {
        e.preventDefault();
        alert(JSON.stringify(this.loginForm.value));
        
    }
}