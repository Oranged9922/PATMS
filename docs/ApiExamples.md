# Authentication

## Commands
``Route[/auth/register]``
### Request:
```
Method - POST,
RequestUri - "auth/register?FirstName=L&LastName=S&Email=ls%40email.com&Password=lsemail123",
```
### Response:
#### Correct registration
```
{
	"id": 1,
	"firstName": "L",
	"lastName": "S",
	"email": "ls@email.com",
	"token": "ey...9.eyJ...32GwG7vgA"
}
```
#### Duplicate Email
```
{
	"type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
	"title": "Email is already in use.",
	"status": 409,
	"errorCodes": [
		"User.DuplicateEmail"
	]
}
```

## Queries
``Route[/auth/login]``
### Request:
```
Method - POST,
RequestUri - "auth/login?email=ls%40email.com&password=lsemail123",
```

### Response:
### Correct login
```
{
	"id": 1,
	"firstName": "L",
	"lastName": "S",
	"email": "ls@email.com",
	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZ2l2ZW5fbmFtZSI6IkwiLCJmYW1pbHlfbmFtZSI6IlMiLCJqdGkiOiJlN2E2ZDk4OC1hZGM1LTQ4OTctOTkyOS0zZTk2YTA4NWNkYmIiLCJleHAiOjE2ODM5MTUxMDMsImlzcyI6InBhdG1zIiwiYXVkIjoicGF0bXMifQ.7AiAr5Ih4Xs-y_jPPtCKYJRt2J4EleyDU3ncb0IdnZ8"
}
```
### Incorrect credentials
```
{
	"type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
	"title": "One or more validation errors occurred.",
	"status": 400,
	"errors": {
		"Auth.InvalidCredentials": [
			"Invalid credentials."
		]
	}
}
```