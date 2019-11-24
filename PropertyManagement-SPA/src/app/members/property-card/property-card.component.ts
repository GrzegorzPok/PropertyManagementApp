import { Component, OnInit, Input } from '@angular/core';
import { Property } from 'src/app/_models/Property';
import { AuthService } from 'src/app/_services/auth.service';
import { PropertyService } from 'src/app/_services/property.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-property-card',
  templateUrl: './property-card.component.html',
  styleUrls: ['./property-card.component.css']
})
export class PropertyCardComponent implements OnInit {
  @Input() property: Property;

  constructor(private authSevice: AuthService, private propertyServie: PropertyService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  sendRent(id: number) {
    this.propertyServie.sendRent(id, this.authSevice.decodedToken.nameid).subscribe(data => {
      this.alertify.success('Rent succesfull');
    }, error => {
      this.alertify.error(error);
    });
  }

}
