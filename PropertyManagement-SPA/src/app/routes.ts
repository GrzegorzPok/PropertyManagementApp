import { Routes } from '@angular/router'; 
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { PropertyDetailsComponent } from './members/property-details/property-details.component';
import { PropertyDetailResolver } from './_resolvers/property-detail.resolver';
import { PropertyListResolver } from './_resolvers/property-list.resolver';
import { PropertyEditComponent } from './members/property-edit/property-edit.component';
import { PropertyEditResolver } from './_resolvers/property-edit.resolver';
import { PreventUndavedChanged } from './_guards/prevent-unsaved-changes.guard';
import { ListsResolver } from './_resolvers/lists.resolver';

export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'members', component: MemberListComponent, resolve: {properties: PropertyListResolver}},
            {path: 'members/:id', component: PropertyDetailsComponent, resolve: {property: PropertyDetailResolver}},
            {path: 'property/edit', component: PropertyEditComponent, resolve: {property: PropertyEditResolver}},
            {path: 'messages', component: MessagesComponent},
            {path: 'lists', component: ListsComponent, resolve: {properties: ListsResolver}},
        ]
    },
    {path: '**', redirectTo: 'home', pathMatch: 'full'}
];