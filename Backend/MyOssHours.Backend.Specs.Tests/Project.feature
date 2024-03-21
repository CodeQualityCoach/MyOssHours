Feature: Project

As a user
I want to create, read, and delete a project
So this I can manage my projects

Background:
	Given the following projects exist:
		| name    | description                 | permissions             |
		| Demo_01 | The is project demo 01      | alice=owner, bob=none   |
		| Demo_02 | This is project demo 02     | alice=owner, bob=reader |
		| Demo_03 | The is project demo 03      | bob=owner, alice=none   |
		| Demo_04 | This is project demo 04     | bob=owner, alice=reader |
		| Demo_05 | The project will be deleted | bob=owner, alice=reader |

@project
Scenario Outline: Create Project
	Given the user '<id>' is logged in
	When the user creates a new project with the name '<name>'
	Then a 200 is returned
	And the project with the name '<name>' is created

Examples:
	| name    | id    |
	| Demo_11 | alice |
	| Demo_12 | bob   |

@project
Scenario Outline: Read My Projects
	Given the user 'alice' is logged in
	When the user reads the existing projects
	Then the result contains a project with the name 'Demo_01'
	And the result does not contain a project with the name 'Demo_03'
	And the result contains a project with the name 'Demo_04'

Scenario Outline: Cannot Read Others Projects
	Given the user 'alice' is logged in
	When the user reads the existing projects
	Then the result does not contain a project with the name 'Demo_03'

@project
Scenario Outline: Delete Project
	Given the user 'bob' is logged in
	When the user deletes the project 'Demo_02'
	Then a 200 is returned
	# @Peter: How to handle this??
	And the user reads the existing projects
	And the result does not contain a project with the name 'Demo_02'

@project
Scenario: Cannot Delete Project without owner permission
	Given the user Alice is logged in
	Given the following projects exist for user alice:
		| name    | description            |
		| Demo_01 | The is project demo 01 |
	Given the following projects exist for user bob:
		| name    | description             |
		| Demo_02 | This is project demo 02 |
	When the user alice deletes the project with the name 'Demo_02'
	Then a 403 is returned

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
