
A trivial microservices project written in .NET Core 5, contains 2 services Auth and Mobile. Auth service is the main (entry point) of the project and has an ability to authenticate user with JWT token and pass authenticated requests to Mobile service.
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

Things todo (not prioritiesed):
* clean up dockerfile - reduce its size, check to security issues, public/internal ports
* apply Polly policies for in/out-going requests - retry, circuit break, timeout logic
* get rid of Newtonsoft Json to utilize more performant NetCore Json implementation
* think about protocols used inside (REST, gRPC, smth else)
* user-secrets and configuration for connection strings, certificates, licenses
* distributed tracing, telemetry, metrics
* gracefully shutdown middleware (simple RequestCounter or smth more complicated?)
* a proper health endpoints for L7 load-balancers and docker-compose

Architectural questions:
* it'd be better to use rev-proxy for security/auth with no need to pass JWT through all the services chain (Ocelot as a posibility)
* think hard about proper size of each service and it's boundaries (DDD bounded context is a posibility, <2 services only for 3-5 people team)
* prepare common source code for integration with Queue-based external software
* service discovery and distributed configs (Consul, ETCD)
* dev / stage / prod environment differences. How to mock the dependencies?
* review 12-factor app requirements once more!
