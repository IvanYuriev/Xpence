
A trivial microservices project written in .NET Core 5, contains 2 services Auth and Mobile. Auth service is the main (enity point) of the project and has an ability to authenticate user with JWT token and pass authenticated requests to Mobile service.
Docker compose files are presented to run both services at the same time with:

```
docker-compose -f docker-compose.debug.yml up
```

1. Authentication has NO real credentials validation, so it is ok to use *any* username and passwords:

```
curl -X POST http://localhost:5000/auth/login -d '{"Username": "a", "Password":"a"}' -H "Content-Type: application/json" | jq ".AccessToken"
```
2. This request should provide you an access token to be used for the next request:

```
curl http://localhost:5000/payments -H "Authorization: Bearer <access token>" | jq
```
*don't forget to replace \<access token\> with valid token received on step 1*

Additional features:
* health endpoint is available with /health API endpoint for each service
* versioning is enabled but is not used
* tasks file for VS code is setup for building, watching, runing projects
* Unit tests projects have been appended BUT there is no feature now available for Unit testing (only integration testing makes sense at the moment)
* Serilog is used and configured for structured logging

:exclamation: NOTE: code is NOT ready for production use and should be treated as an example.