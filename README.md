# 2B E-Commerce Store

This project was created with [Angular CLI](https://github.com/angular/angular-cli) version 16.1.0 for the front-end and [ASP.NET Core](https://github.com/dotnet/AspNetCore.Docs) ( .NET 7.0 ) for the back-end ( dashboard & Web API ).

I created the `ASP.NET Core Web App MVC` project for the dashboard, and the `ASP.NET Core Web API` project for the API.

## Development server

### Front-end

Enter `Frontend_for_main_website` folder and run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

### Back-end

The back-end is designed using **_[Onion Architecture](https://www.google.com/search?q=onion+architecture+c%23&oq=onion+arch&aqs=chrome.0.35i39i650j69i64j69i57j0i512l5.5228j0j9&sourceid=chrome&ie=UTF-8)_**, for the dashboard and the web API.

1. Enter `Backend_&_Dashboard_for_main_website` folder.
2. Open the solution with visual studio (recommended).
3. Go to the `2B_Store.Context` layer and remove the `Migrations` folder.
4. Go to the `Package Manager Console`, choose the default project to be `2B_Store.Context` and be sure to select `2B_Store.WepApi` or `2B_Store.MVC` as a startup project. Then run the command `add-migration any-name`, it will create a migrations folder and open the file for you to change anything before creating database if you want.
5. Then, in the `Package Manager Console` run the command `update-database`, it will create a database called **2B_Store**.
6. In the `Backend_&_Dashboard_for_main_website` folder, open `2B_Store.sql` file and run the whole script.
7. To run the webapi layer, set `2B_Store.WepApi` as a startup project, run it and the browser will open the server. I think it was `http://localhost:5775/swagger/index.html`. And to run the dashboard, set `2B_Store.MVC` as a startup project and do the same, it will open with a different port number.
8. The connection between the front-end and back-end is the localhost of the Web API that is stored in the front-end in this path `/Frontend_for_main_website/src/environments/environment.development.ts`. it looks like this :
   ```
   export const environment = {
   Production: false,
   Client_ID:
       'AcvNMt421l-NBJ7Mj836zlAUVBstJMALPNOr8MpJKDbH-qCd31vVrflOg-sOoB1uB8YNkLmtccg3t_gw',
   BaseApiUrl: 'http://localhost:5775',
   };
   ```
   **_So be sure :_** that the web API localhost is the same as the above `BaseApiUrl`, then you can place order, sign in, and create an account.

### _The project is permanently under development_

I will add some faetures, change the style, and improve some functionality. You're welcome for any suggested improvements 😃😍

## Hosted Projects

_Check out these 2 projects :_

[Dashboard](http://iti2bstoredashboard.somee.com/) and [2B Website](https://zesty-starlight-bfc4f9.netlify.app/)

## Code scaffolding

Enter `Frontend_for_main_website` folder and run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Enter `Frontend_for_main_website` folder and run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Enter `Frontend_for_main_website` folder and run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Enter `Frontend_for_main_website` folder and run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## FurEnter `Frontend_for_main_website` folder andther help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
