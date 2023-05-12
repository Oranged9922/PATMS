# PATMS
something something something something system (TBD)
(Project Assisting Task Manager System ?)

## Purpose
The purpose of this REST API is to manage tasks (tickets) for project management. 
The API should provide endpoints for clients to create, update, and track tasks for various projects.

## Functional Requirements
- Clients should be able to register and login using a `POST` request to the `/auth/login` and `/auth/register` endpoint respectively.
- Clients should be able to create new tasks using a `POST` request to the `/tasks` endpoint.
- Clients should be able to view and modify existing tasks using `GET`, `PUT`, and `DELETE` requests to the `/tasks/{taskId}` endpoint.
- Clients should be able to add dependency to specific task using a `PUT` request to the `/tasks/{taskId}` endpoint.
- Clients should be able to add comments to tasks using a `POST` request to the `/tasks/{taskId}/comments` endpoint.
- Clients should be able to assign tasks to specific team members using a `PUT` request to the `/tasks/{taskId}/assignee` endpoint.
- Clients should be able to add subtasks to specific task using a `PUT` request to the `/tasks/{taskId}/subtasks` endpoint.
- Clients should be able to set due dates for tasks using a `PUT` request to the `/tasks/{taskId}/dueDate` endpoint.
- Clients should be able to prioritize tasks based on importance using a `PUT` request to the `/tasks/{taskId}/priority` endpoint.
- Clients should be able to filter and search tasks based on various criteria using query parameters.
- Clients should be able to create and manage projects using similar endpoints.

## Technical Requirements

- The API should be built using C# programming language.
- The API should use Entity Framework to interact with the SQLite database.
- The API should be RESTful and follow RESTful principles.
- The API should use JSON as the data format for requests and responses.
- The API should be secure and protect user data using authentication and authorization.

## Additional Features (Nice to have)

- Integration with email to send notifications about task updates
- Logging and monitoring features to track API usage and performance

## Assumptions and constraints
- The API will be developed using Microsoft Visual Studio IDE.
- The API will be developed with consideration for scalability and performance.
- The API will be developed with the idea of being deployed on a server using a web server such as IIS or Apache.
- The API will use HTTPS to ensure secure communication between clients and the server.
- The database schema and Entity Framework models will be designed to handle task and project data efficiently and effectively.