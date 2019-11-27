export interface Message {
    id: number;
    senderId: number;
    propertyId: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;
    propertyAddress: string;
    senderLogin: string;
}
