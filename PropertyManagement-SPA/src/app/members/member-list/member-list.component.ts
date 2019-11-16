import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../_services/property.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Property } from 'src/app/_models/Property';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  properties: Property[];

  constructor(private propertyService: PropertyService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {    
    this.route.data.subscribe(data => {
      this.properties = data['properties'];
    });
  }

  // loadProperties() {
  //   this.propertyService.getProperties().subscribe((properties: Property[]) => {
  //     this.properties = properties;
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }

}
