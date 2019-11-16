import { Photo } from './Photo';

export interface Property {
    id: number;
    country: string;
    city: string;
    street: string;
    propertyNumber: string;
    photoUrl: string;
    photos?: Photo[];
}
