## Investec Solution

- I've implemented the solution following my own best practices and experience.

- The main principle is keeping code clean, simple to read and understand, testable, and following all SOLID principles.

- Anyone should be able to maintain this code and fix its bugs. If this is not the case, then I did a poor job.

- I did not want to add any kind of additional libraries to this solution, as adding libraries does not prove anything.
  There are different DI containers available, there are various loggers, ORM frameworks, different RESTful/Stateful/GraphQL(ful) frameworks for adding a Web later on existing code, and so on.
  I did add a WebApi layer to showcase the library's consumer side. It is functional, but requires a real database.
  
- My code does not use the old implementation of the service, as that implementation was: non-functional, insecure and had some design issues. It was subject to SQL injections and unscalable. That being said, since the code is using DI and interfaces to declare the business functionality, the old implementation can easily be consumed by an implementor of one of the business-model facades. (in the `Facades` folder)
The old implementation resides in /AnyCompany.Service/DAL/DataManagers/V1

- The projects in the solution were not targeting .NET Core, so I changed them to target Core2.2 instead of 4.5. 

### TODO:
#### Code:

If I were to continue implementing this solution, I would:

1. Configure resources, thread pools and caching on the API level
2. Add SwaggerUI which would be hosted with the solution, so that the API is easily accessible and testable
3. Add authentication mechanism (preferably using JWT or such, in order to avoid session-cookie storage).
4. Add authorisation mechanism - reuse some ready solution for it, if possible.
5. Caching, SSR and CDN for any assets that the FE uses
6. Data caching in a centralised, scalable system such as Redis

#### Tests:

1. Add integration tests that would cover complete flows as well as integration with 3rd parties
2. Add more extensive unit tests
3. Add a health endpoint + some pinging process that would make sure our environment in production is up and running, and serving customers. 

#### DevOps:
   
1. Add a Dockerfile in order to host the solution in a docker container, thus making the deployments easy and isolated. (+ docker-compose for dependencies)
2. Create a CI/CD pipeline, host it on a low-maintenance environment such as Heroku or Azure Devops (If the company policy allows that, instead of self-hosting or on-promise hosting)
3. Optionally, add DB migrations in order to support versions and history in the VCS. 
4. Set up ChatOps to manage deployments 
5. Set up a scheduler with a CRON jobs executer, for offline tasks execution (i.e upload data to BI environment, run analytics, etc) 
6. Add tools to monitor the performance of the system (FE+BE+communication) and alert us if the performance goes down.
7. Database analytics tools (embedded in Heroku out of the box, but we can integrate other tools) to make sure the database does not experience connections exhaustion, deadlocks, etc.


# Original instructions:

Hello Candidate,

Welcome to AnyCompany Entertainment.

Here is our mostly complete system for placing orders.

The developer who created this system did not follow any specific development methodology, 
but attempted to implement some patterns. Unfortunately he has now left the company.

The system should do the following:

 * Place an order, linked to a customer
 * Load all customers and their linked orders

Please do not change any of the existing static class declarations to be non-static.

Please refactor the solution according to your own best practices.

**Please fork this repository and submit your attempt via a pull request**

Details on how to do this can be found in the [Github help pages](https://help.github.com/en/articles/creating-a-pull-request-from-a-fork)

Best regards

John
CEO
