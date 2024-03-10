

# The `Domain` layer 

## Constraints

The following constraints are defined for the domain layer

* A `User` reflects a person which uses the system. A user is defined as follows
  * A user has a unique id 'uuid'
  * A user has an 'email' and a 'name' as mandatory fields
  * The 'email' and the 'name' are unique

* A `Project` defines a root for the hours
  * A project has a unique id 'uuid'
  * A project has a 'name' and a 'description'
  * The name is unique
  * A project has a list of user which have permissions for the project `ProjectMembers`
  * A project has a list of work items `WorkItems`

* A `ProjectPermission` is an assignment for a user to a project
  * A permission level can be 'none' (default), 'read', 'contribute', or 'owner'.
  * A permission level always include the previous level e.g. contribute includes read (and none)
  * A project has always an 'owner'
  * A user can only have one permission level
  * A user needs read permissions to read the project data including child data
  * A user needs contribute permissions to add project hours to the project
  * A user needs owner permissions to add work items to the project
  * A user needs owner permissions to assign permissions 
 
* A `Workitem` is used to assign the project hours to smaller pieces
  * A work item has a unique id 'uuid'
  * A work item has a 'name' and a 'description'
  * The name is unique withing a `Project`
  * A work item has a list of `ProjectHours`
  * A work item can only be deleted by project owner

* A `ProjectHour` is an assignment of work done to a project (within a work item)
  * A project hour has a unique id 'uuid'
  * A project hour has a 'user', a 'date' and a 'timespan'
  * a project hour can be deleted by the user or an project owner
