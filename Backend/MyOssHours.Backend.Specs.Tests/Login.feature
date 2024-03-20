Feature: Login

As a user
I want to login with username and password
So that I have access to the system

Alice and Bob have valid credentials
Charlie has invalid credentials

@login
Scenario Outline: Login with valid credentials
	When the user '<id>' is logged in
	Then a 200 is returned

Examples:
	| id    |
	| Alice |
	| Bob   |

@login
Scenario: Login with invalid credentials
	When the user '<id>' is logged in
	Then a 401 is returned

Examples:
	| id      |
	| Charlie |
	| Dave    |

@login
Scenario Outline: Get user information after login
	Given the user '<id>' is logged in
	When the user information is requested
	Then a 200 is returned
	And the user has a claim with an email address containing '<id>'

Examples:
	| id    |
	| Alice |
	| Bob   |

@login
Scenario: Cannot get user information without login
	When the user information is requested
	Then a 401 is returned