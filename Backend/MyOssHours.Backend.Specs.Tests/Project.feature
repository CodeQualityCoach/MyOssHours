Feature: Project

A short summary of the feature

#@project
#Scenario: Create Project
#	Given The user with id '<id>' is logged in
#	When The user creates a new project with the name '<name>'
#	Then The project with the name '<name>' is created
#
#Examples:
#	| name    | id    |
#	| Demo_01 | alice |
#	| Demo_02 | bob   |

@project
Scenario: Read Project
	Given the user Alice is logged in
	Given the following projects exist for user alice:
		| name    | description    |
		| Demo_01 | The is project demo 01 |
		| Demo_02 | This is project demo 02   |
	When the user alice reads the existing projects
	Then the result contains a project with the name 'Demo_01'
	Then the result contains a project with the name 'Demo_02'
