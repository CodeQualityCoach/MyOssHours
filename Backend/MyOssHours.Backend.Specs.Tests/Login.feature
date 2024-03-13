Feature: Login

As a user
I want to login with username and password
So that I have access to the system

@login
Scenario: Login with valid credentials
	Given the user <id> is logged in
	When the user information is requested
	Then the user has a claim with an email address containing <id>

Examples:
	| id    |
	| Alice |
	| Bob   |
