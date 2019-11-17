import { Injectable } from "@angular/core";
import { CanDeactivate } from '@angular/router';
import { PropertyEditComponent } from '../members/property-edit/property-edit.component';

@Injectable()
export class PreventUndavedChanged implements CanDeactivate<PropertyEditComponent> {
    canDeactivate(component: PropertyEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved changes will be lost')
        }
        return true;
    }
}