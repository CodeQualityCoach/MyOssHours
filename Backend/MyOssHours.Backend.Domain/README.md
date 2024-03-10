# Domain Model

## Clean Architecture

	* Layer in "Clean Architecture"

## Domain Driven Design

	* Domain Model in Domain Project
	* Design of Aggregates, Entities, Value Objects
	* Create-Method to create new entities
	* Exceptions are thrown in the domain layer

## Namespace per Root Aggregate

    * Root aggregates are identified by an Interface
	* namespaces that only contain the root aggregate and all of its children
	* This includes the values objects for the uuid.

## Entity IDs / Value Objects

	* Use of Entity IDs and Value Objects
	* Entity IDs are immutable
	* Value Objects are compared by their properties
	* Each entity gets its own entity ID

## Exceptions and Base Classes

	* Use of exceptions base class for domain errors
	* Validation errors in the domain are thrown as exceptions

## Validation

    * Validation is either done in the aggregate root or injected
	* For validation injection, a lambda is used