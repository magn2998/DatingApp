export interface Profile {
    id: number;
    username: string;
    profileName: string;
    created: Date;
    lastActive: any;
    profileDescription?: string;
    postLikes: any[];
}
