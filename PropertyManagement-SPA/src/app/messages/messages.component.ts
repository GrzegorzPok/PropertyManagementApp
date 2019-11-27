import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../_services/property.service';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Message } from '../_models/message';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  messageContainer = 'Unread';
  temp: boolean = true;

  constructor(private propertyService: PropertyService, private authService: AuthService,
    private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    console.log('1');
    this.route.data.subscribe(data => {
      this.messages = data['messages'].result;
    });
  }

  loadReceivedMessages() {
    this.propertyService.getMessages(this.authService.decodedToken.nameid, false).subscribe((data: Message[]) => {
      this.messages = data;
    });
  }

  loadSentMessages() {
    this.propertyService.getMessages(this.authService.decodedToken.nameid, true).subscribe((data: Message[]) => {
      this.messages = data;
    });
  }

  loadUnreadMessages() {
    this.propertyService.getUnreadMessages(this.authService.decodedToken.nameid).subscribe((data: Message[]) => {
      this.messages = data;
      console.log(data);
    });
  }

  deleteMessage(id: number) {
    this.alertify.confirm('Are you sure you want to delete this message', () => {
      this.propertyService.deleteMessage(id).subscribe(() => {
        this.messages.splice(this.messages.findIndex(m => m.id == id), 1);
        this.alertify.success('Message has been deleted');
      }, error => {
        this.alertify.error(error);
      });
    });
  }
}
