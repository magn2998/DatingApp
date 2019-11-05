export interface Post {
    id: number;
    profileProfileName: string;
    title: string;
    content: string;
    section: string;
    created: Date;
    postComments: any;
    postLikers: Array<any>;
    profileAction: string;
}
