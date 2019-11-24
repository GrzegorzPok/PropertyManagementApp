import { Photo } from './Photo';

export interface Property {
    id: number;
    country: string;
    city: string;
    street: string;
    propertyNumber: string;
    description: string;
    flatArea: number;
    roomNumbers: number;
    level: number;
    ownerId: any;
    photoUrl: string;
    photos?: Photo[];
}
