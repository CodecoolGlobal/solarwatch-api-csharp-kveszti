# Solar Watch
This is a full-stack application using React with Vite for frontend, ASP.NET for backend and MSSQL for DB. It's purpose to let the user get sunrise and sunset data for a chosen city and date, using sunrise-sunset.org's API and geocoding API. The app uses authentication, you can create your own account. 

# Table of Contents
- [Used technologies](#used-technologies)  
- [Features](#features)  
- [Installation](#installation)   
- [Usage](#usage)

# Used technologies 
![Static Badge](https://img.shields.io/badge/ASP.NET-red?logo=.net) ![Static Badge](https://img.shields.io/badge/C%23-red?logo=c%23) ![Static Badge](https://img.shields.io/badge/Entity%20Framework-red?logo=dotnet%20entity) ![Static Badge](https://img.shields.io/badge/Identity-red?logo=identity)

![Static Badge](https://img.shields.io/badge/React-blue?logo=react) ![Static Badge](https://img.shields.io/badge/Javascript-blue?logo=javascript)
 ![Static Badge](https://img.shields.io/badge/Vite-blue?logo=vite) ![Static Badge](https://img.shields.io/badge/NPM-blue?logo=npm)

  ![Static Badge](https://img.shields.io/badge/MSSQL-black?logo=MSSQL) ![Static Badge](https://img.shields.io/badge/Docker-black?logo=docker)
# Features
 - Getting sunrise and sunset data of your chosen city and date
 - Authentication and account creation
 - There are a few other planned features
    - Display of search history
    - Choosing time zone options
  
# Installation
1. Prerequisites:
  - Backend software and package versions:
      - .NET 8.0 SDK
      - Aspire.Npgsql.EntityFrameworkCore.PostgreSQL	^8.1.0
      - DotNetEnv	^3.0.0	
      - Microsoft.AspNetCore.Authentication.JwtBearer	^8.0.7	
      - Microsoft.AspNetCore.Identity.EntityFrameworkCore	^8.0.7	
      - Microsoft.AspNetCore.OpenApi ^8.0.2	
      - Microsoft.EntityFrameworkCore	^8.0.7
      - Microsoft.EntityFrameworkCore.Design	^8.0.7 
      - Microsoft.EntityFrameworkCore.InMemory	^8.0.7
      - Microsoft.EntityFrameworkCore.SqlServer ^8.0.7
   - Frontend software and package versions:
      - Node.js ^22.2.0
      - Npm ^10.7.0
      - React	^18.2.0
      - React-dom	^18.2.0
      - React-router-dom	^6.25.1
      - Styled-components	^6.1.12
3. Clone the repo and download all the packages above.
4. You can start the program from docker compose:
  - Please find the ```docker-compose-pattern.yml``` file in the root folder, edit the placeholder info to your own information, and then rename the file to docker-compose.yml
  - Before giving the terminal command, you should make sure that you are in the root folder of the app
  - In the terminal: ```docker compose up```
5. You can also start the frontend and backend development servers locally:
  - Please keep in mind, that if you'd like to run the program this way, you need to setup an appsettings.json file locally in the SolarWatch directory. It needs to have the following structure (with your custom info inserted:)
    ```json
     "ConnectionStrings": {
       "Default": <Here comes your own connection string>
        },
      "Jwt": {
        "ValidIssuer": <Here comes your own string>,
        "ValidAudience": <Here comes your own string>,
        "IsserSigningKey": <Here comes your own string>
     }
    ```
  - It is also important that you set up an appsettings.Testing.json file, where you use the same connection string structure and use your in-memory db connection string.
  - From the root folder: ```cd SolarWatch```
  - In the terminal: ```dotnet run```
  - From the root folder: ```cd Frontend```
  - In the terminal: ```npm run dev```


# Usage 

