import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserSignIn } from 'src/Model/i-user-sign-in';
import { IUserSignUp } from 'src/Model/i-user-sign-up';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private postOptions = {};

  constructor(private httpClient: HttpClient) {
    this.postOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
  }

  checkUserName(userName: string): Observable<{ userNameExists: boolean }> {
    return this.httpClient.get<{ userNameExists: boolean }>(
      `${environment.BaseApiUrl}/api/user/checkUserName?username=${userName}`
    );
  }

  signUpFor(user: IUserSignUp): Observable<IUserSignUp> {
    return this.httpClient.post<IUserSignUp>(
      `${environment.BaseApiUrl}/api/user/register`,
      JSON.stringify(user),
      this.postOptions
    );
  }

  signInFor(
    user: IUserSignIn
  ): Observable<{ message: string; myToken: string }> {
    return this.httpClient.post<{ message: string; myToken: string }>(
      `${environment.BaseApiUrl}/api/user/login`,
      JSON.stringify(user),
      this.postOptions
    );
  }

  signOut(): Observable<{ message: string }> {
    return this.httpClient.get<{ message: string }>(
      `${environment.BaseApiUrl}/api/user/logout`
    );
  }

  deleteAccount(): Observable<{ message: string }> {
    return this.httpClient.delete<{ message: string }>(
      `${environment.BaseApiUrl}/api/user/DeleteAccount`
    );
  }
}
