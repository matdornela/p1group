# AcmeFlights Assignment

![alt text](/acmeflights-image.jpg)

This repository is the starting point for our .NET assignment. You are going to create a flights booking engine. API clients should be able to search available flights and book them. **The purpose of the assignment is to see if there is a match between our problem solving and coding style**

We like to use some modern best practices in this assignment and try to point you in a certain direction. But don't take it too strictly. If you are struggling with something, just let it go and shine at the parts you are more familiar with. We are not expecting everyone to know everything ;) The same is true the other way around. When you think there's a more clever solution, just do it and argue why it's better.

## Excercise "requirements"

- Apply the following practices throughout the project
    - Domain Driven Design
    - CQRS
    - Mediator pattern (Using [MediatR](https://github.com/jbogard/MediatR))  
    - Persistence ignorance
    - SOLID
- An API that can
    - Search the flights for a destination (TEST)
        - Must show the flight details
        - Must show the lowest price for each found flight
    - Build up an order
        - Must use the Ordering domain (`Domain/Aggregates/OrderAggregate/`)
        - Must be able to fill the order with the (just the necessary) details, while still in draft state
    - Confirm an order
        - Must be able to confirm the order
- Business logic
    - An order that's still in draft must update the order line prices when the flight rate price changes
    - When the order price changes, the user must be notified (can be implemented as `Console.WriteLine`)
    - When an order is confirmed, the any ordered rates should lower their availability by the quantity ordered
- Other
    - The project must be runnable on MacOS and Windows
    - If there are additional steps for us to take to run it, please write them down

> **Do not worry** if you are not familiar, or are struggling with one or more of the requirements. Just do the best you can. Not being able to fulfill all requirements does not mean you "failed".
> 
> This assignment is intended to be a conversation starter. We would still love to see your solution, even if you were not able to fulfill all requirements! We will discuss the assignment afterwards, so there's always the opportunity to explain the decisions made

## Prerequisities

- Docker Desktop
- .NET Core 3.1 SDK

## Getting started

- Start the Postgres database with `docker-compose up -d` (the application is already configured properly, but if you want to connect to the db directly you can see the credentials in the `docker-compose.yml` file)
- You can now run the API project and everything should work. Upon start the application will run the migrations and seed data to the database.

## References 

- https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/
- https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/