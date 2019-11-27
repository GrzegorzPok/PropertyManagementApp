import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { PropertyService } from 'src/app/_services/property.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-property-messages',
  templateUrl: './property-messages.component.html',
  styleUrls: ['./property-messages.component.css']
})
export class PropertyMessagesComponent implements OnInit {
  messages: Message[];
  myId: number;
  newMessage: any = {};

  constructor(private propertyService: PropertyService, private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadMessages();
    this.myId = this.authService.decodedToken.nameid;
  }

  loadMessages() {
    this.propertyService.getMessageThread(this.propertyService.selectEmitter.value)
      .subscribe(messages => {
        this.messages = messages;
      }, error => {
        this.alertify.error(error);
      });
  }

  sendMessage() {
    this.newMessage.senderId = this.authService.decodedToken.nameid;
    this.propertyService.sendMessage(this.propertyService.selectEmitter.value, this.newMessage).subscribe((message: Message) => {
      this.messages.unshift(message);
      this.newMessage = '';
    }, error => {
      this.alertify.error(error);
    });
  }  
}
