To install & start Consul:
docker run -d -p 8500:8500 --name=dev-consul -e CONSUL_BIND_INTERFACE=eth0 consul

to install & start rabbitmq image:
docker run -d --hostname rabbitQueue --name ecomm-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management



The Application has been built using a clean Architecture (basically onion architecture). This will allow loose coupling and less dependency
The project consists of 3 Microservices following:
Services: get the list & details of all the available services like (electrician, beauty, etc)
Service Provider: This set of microservices handles the service providers tasks & roles
Service Receiver: This set of microservice delas with the task of user

1. Services: GetAllServices: Gets the list of all the available services
			 GetServiceAmount: fetches the amount of each service which will be used to calculate the total bill

2. Service Provider: Engage A service Provider: This service will be used by a service provider to get himself engage into a service for a user
					 Release Service provider: This service will set the provider free once he is finish with his task
					 Access Service provider: This service will used to access a service provider for a following service as when requested by the user.
											  It will first of all get the available service providers, as per the pincode (for each service provider),
											  and then sents the notification to all the providers. the interested one on first come basis, will get
											  himself engage.

3. Service Receiver: Create a new user: This service will create a new user
					 Get User Details: this service is used to get the user details, and is consumed by a service provider when he engage himself into
									   a service for a particular user, and then get the details for that user
					 Access Service: This service will allow the user to access the lists of services. Then this service will call asynchonously to
									 the Access service provider, and then synchronously call the Get service amount, in order to calculate the total bill.