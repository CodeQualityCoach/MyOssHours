Feature: Project

As a user
I want to create, read, and delete a project
So this I can manage my projects

@project
Scenario: Create Project
	Given The user with id '<id>' is logged in
	When The user creates a new project with the name '<name>'
	Then The project with the name '<name>' is created

Examples:
	| name    | id    |
	| Demo_01 | alice |
	| Demo_02 | bob   |

@project
Scenario: Read Project
	Given the user Alice is logged in
	Given the following projects exist for user alice:
		| name    | description             |
		| Demo_01 | The is project demo 01  |
		| Demo_02 | This is project demo 02 |
	When the user alice reads the existing projects
	Then the result contains a project with the name 'Demo_01'
	Then the result contains a project with the name 'Demo_02'

@project
Scenario: Delete Project
	Given the user Alice is logged in
	Given the following projects exist for user alice:
		| name    | description             |
		| Demo_01 | The is project demo 01  |
		| Demo_02 | This is project demo 02 |
	When the user alice deletes the project with the name 'Demo_02'
	Then the result contains a project with the name 'Demo_01'
	Then the result does not contain a project with the name 'Demo_02'

@project
Scenario: Read projects I have permissions for
	Given the user Alice is logged in
	Given the following projects exist for user alice:
		| name    | description             |
		| Demo_01 | The is project demo 01  |
		| Demo_02 | This is project demo 02 |
	Given the user Bob is logged in
	Given the following projects exist for user bob:
		| name    | description             |
		| Demo_03 | The is project demo 03  |
		| Demo_04 | This is project demo 04 |
	When the user alice reads the existing projects
	Then the result contains a project with the name 'Demo_01'
	Then the result contains a project with the name 'Demo_02'
	Then the result does not contain a project with the name 'Demo_03'
	Then the result does not contain a project with the name 'Demo_04'
