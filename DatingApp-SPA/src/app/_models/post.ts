export interface Post {
    id: number;
    postId: number;
    profileProfileName: string;
    profileId: number;
    title: string;
    content: string;
    section: string;
    created: Date;
    postComments: any;
    postLikers: Array<any>;
    profileAction: string;
    likesCounter: number;
    dislikeCounter: number;
}
