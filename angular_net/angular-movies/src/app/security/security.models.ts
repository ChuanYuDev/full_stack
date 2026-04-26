export interface UserCredentialsDto {
    email: string;
    password: string;
}

export interface AuthenticationResponseDto {
    token: string;
    expiration?: Date;
}