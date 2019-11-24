import { Component, OnInit, Input, ViewChild, HostListener } from '@angular/core';
import { Property } from 'src/app/_models/Property';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { PropertyService } from 'src/app/_services/property.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-property-edit',
  templateUrl: './property-edit.component.html',
  styleUrls: ['./property-edit.component.css']
})
export class PropertyEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  @Input() property: Property;
  saved: Boolean = false;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private propertyService: PropertyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.property = data['property'];
    });
  }

  updateProperty() {
    if (!this.saved) {
        this.propertyService.updateProperty(this.property.id, this.property).subscribe(next => {
        this.alertify.success('Profile updated successfylly');
        this.property = null;
        this.propertyService.selectEmitter.next(0);
      }, error => {
        console.log(error);
        this.alertify.error(error);
      });
    }
  }

  updateMainPhoto(photoUrl) {
    this.property.photoUrl = photoUrl;
  }
}
