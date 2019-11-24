import { Component, OnInit } from '@angular/core';
import { PaginationComponent } from 'ngx-bootstrap';
import { Property } from '../_models/Property';
import { AuthService } from '../_services/auth.service';
import { PropertyService } from '../_services/property.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  properties: Property[];
  pagination: PaginationComponent;


  constructor(private authService: AuthService, private propertyService: PropertyService,
              private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.properties = data['properties'].result;
      this.pagination = data['properties'].pagination;
    });
  }


  loadMyProperties() {
    this.propertyService.getMyProperties(this.authService.decodedToken.nameid).subscribe((properties: Property[]) => {
      this.properties = properties;
    }, error => {
      this.alertify.error(error);
    });
  }

  loadRentedProperties() {
    console.log(this.authService.decodedToken.nameid);
    this.propertyService.getRentedProperties(this.authService.decodedToken.nameid).subscribe((properties: Property[]) => {
      this.properties = properties;
    }, error => {
      this.alertify.error(error);
    });
  }
}
