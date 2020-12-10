# Claim management Software

Demo

## Prerequisites

*	SQL Server Installed (Any version)
*	Visual studio 2019
*	RabbitMQ installed and running on default settings ( url:  http://localhost:15672 credentials: guest/guest)


## How to run the solution

*	Clone or Download the Solution
*	Open the Solution in Visual studio
*	run all the projects at the same time (Multi startup projects)

## More info
* AgentClient : classical asp.net website for the agent to "crud" claims, users and view logs
* ClaimService : the service responsible of managing claims
* IdentityService: the service responsible of managing the users
* LogService: Responsable of logging actions for others services
* WebInsuranceClient: Angular 8 website

On launch, the application seeds 2 users Landry (landry/password) and Elisa (elisa/password)

You can use both users to login and do the actions.
