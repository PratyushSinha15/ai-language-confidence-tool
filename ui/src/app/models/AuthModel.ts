export interface RegisterRequest {

  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName?: string;

}


export interface LoginRequest {

  username: string;
  password: string;

}


export interface AuthResponse {

  userId: string;
  username: string;
  email: string;

  firstName: string;
  lastName?: string;

  token: string;

}