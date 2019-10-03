export interface Message {
    id: number;
    senderId: number;
    senderKnownAs: string;
    senderPhotoUrl;
    recipientId: number;
    recipientKnownAs: string;
    recipientPhotoUrl: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;
}
