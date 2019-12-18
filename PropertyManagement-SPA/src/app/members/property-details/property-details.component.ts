import { Component, OnInit, ViewChild } from '@angular/core';
import { Property } from 'src/app/_models/Property';
import { PropertyService } from 'src/app/_services/property.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { TabsetComponent } from 'ngx-bootstrap';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.css']
})
export class PropertyDetailsComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  property: Property;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  joined = false;

  constructor(private propertyService: PropertyService, private authService: AuthService, private aletrify: AlertifyService, private route: ActivatedRoute) { 
    this.propertyService.getRentedProperties(this.authService.decodedToken.nameid).subscribe(data => {
      for (const d of data) {
        if (d.id == this.propertyService.selectEmitter.value) {
          this.joined = true;
          break;
        }
      }
    });
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.property = data['property'];
    });

    this.route.queryParams.subscribe(params => {
      const selectedTab = params['tab'];
      this.memberTabs.tabs[selectedTab > 0 ? selectedTab : 0].active = true;
    });

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];
    this.galleryImages = this.getImages();
  }

  getImages() {
    const imageUrls = [];
    for (const photo of this.property.photos) {
      imageUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.description
      });
    }
    return imageUrls;
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  removeProporty() {
    this.aletrify.confirm('Are you sure you want to delete this property?', () => {
      this.propertyService.deleteProperty(this.propertyService.selectEmitter.value).subscribe(() => {
        this.aletrify.success('Property has been deleted');
      }, error => {
        this.aletrify.error('Failed to delete the property');
      });
    });
  }
}
