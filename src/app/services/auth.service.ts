import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';

export type DecodedJWT = {
  unique_name: string;
  email: string;
  nameid: string;
  role: string;
  nbf: number;
  exp: number;
  iat: number;
};

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = '_2B_User';

  isAuthenticated(): boolean {
    const token = this.getToken();
    // Check if the token is valid (not expired, etc.)
    return !!token && !this.isTokenExpired(token);
  }

  isTokenExpired(token: string | null): boolean {
    if (!token) return true;
    else {
      const decodedToken: DecodedJWT = jwtDecode(token);
      const expirationTime = decodedToken.exp * 1000; // Convert to milliseconds
      const currentTime = new Date().getTime();

      return expirationTime < currentTime;
    }
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}
