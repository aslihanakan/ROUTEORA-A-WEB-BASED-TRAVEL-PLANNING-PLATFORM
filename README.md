# Routeora – A Web-Based Travel Planning Platform

## Overview

Routeora is a web-based travel planning platform developed using ASP.NET Core MVC and SQLite. The application helps users explore travel destinations, discover visa-free countries, create personalized trips, and save favorite destinations for future travel plans.

The main goal of the project is to provide an easy-to-use travel assistant that combines destination information, route suggestions, and travel planning features within a single platform.

---

## Features

### User Management

* User registration and login system
* Session-based authentication
* Protected pages for authenticated users

### Destination Explorer

* Browse popular travel destinations
* View destination details such as country, description, and travel information

### Visa-Free Countries

* Explore countries that Turkish citizens can visit without a visa
* Access useful travel information for each destination

### Trip Planning

* Create personal travel plans
* Manage and organize trips
* Store travel information in the database

### Suggested Routes

* Discover predefined travel routes
* Explore multi-country travel itineraries
* Get inspiration for future trips

### Wishlist System

* Save favorite destinations
* Save visa-free countries
* Save suggested routes
* Access all saved items from a single page

---

## Technologies Used

### Backend

* ASP.NET Core MVC (.NET 8)
* C#

### Frontend

* HTML5
* CSS3
* Bootstrap 5
* Razor Views

### Database

* SQLite
* Entity Framework Core

### Development Tools

* Visual Studio 2022
* Git
* GitHub

---

## Project Structure

```text
Controllers/      Application controllers
Models/           Data models
Views/            Razor view pages
Data/             Database context
Migrations/       Entity Framework migrations
wwwroot/          Static files (CSS, JS, Images)
```

---

## Security Implementation

The application uses session-based authentication to protect user-specific operations.

Restricted pages such as trip management and wishlist functionality require a valid user session. If an unauthenticated user attempts to access protected resources directly through URL manipulation, the application automatically redirects the user to the login page.

This mechanism helps prevent unauthorized access to personal travel data and user-specific actions.

---

## Installation

1. Clone the repository:

```bash
git clone https://github.com/aslihanakan/ROUTEORA-A-WEB-BASED-TRAVEL-PLANNING-PLATFORM.git
```

2. Open the project in Visual Studio.

3. Restore dependencies:

```bash
dotnet restore
```

4. Run the application:

```bash
dotnet run
```

5. Open the browser and navigate to:

```text
https://localhost:xxxx
```


---

## Author

Aslıhan Akan

Computer Engineering Student

---

## License

This project was developed for educational purposes as part of a Web Programming course.
