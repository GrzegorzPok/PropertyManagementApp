import { Component, OnInit, Input, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { EventEmitter } from 'events';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('registration succesful');
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    console.log('cancelled')
  }

}
