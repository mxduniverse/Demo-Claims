# Claim management Software

Demo

## Prerequisites

*	SQL Server installed (Any version)
*	Visual Studio 2019
*	RabbitMQ installed and running on default settings ( url:  http://localhost:15672 credentials: guest/guest)


## How to run the solution

*	Clone or download the solution
*	Open the solution in Visual Studio
*	Run all the projects at the same time (Multi startup projects)

## More info
* AgentClient : Classical ASP.Net website for the agent to "CRUD" claims, users and view logs.
* ClaimService : The service responsible of managing claims
* IdentityService: The service responsible of managing users
* LogService: Responsible of logging actions for others services
* WebInsuranceClient: Angular 8 website

On launch, the application seeds 2 users Landry (landry/password) and Elisa (elisa/password)

You can use either of the users to login and perform actions.
