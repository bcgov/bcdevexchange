[![img](https://img.shields.io/badge/Lifecycle-Retired-d45500)](https://github.com/bcgov/repomountie/blob/master/doc/lifecycle-badges.md)
The project is no longer being used and/or supported.

# **BC DevExchange**

The BCDevExchange is a web application that represents a supportive community enabling the government in British Columbia, Canada to deliver better digital services.

## Project Architecture

![&quot;TechnicalArchitechture&quot;](bcdevexchange/wwwroot/img/technical\_architecture/architecture.png)

## Technologies/Tools Used

- **BCDK** : The Jenkins pipeline for deployment is being set up by using BC Gov&#39;s tool named bcdk. More details can be found here:[https://github.com/david-kerins/bcdk](https://github.com/david-kerins/bcdk)
- **SonarQube** : SonarQube is an open-source platform developed by SonarSource to detect bugs, code smells, and security vulnerabilities. More details can be found here:[https://www.sonarqube.org/](https://www.sonarqube.org/)
- **Matomo** : Matomo is a free and open source web analytics application. It is being used to track online visits to the bcdevexchange website and display reports. More details can be found here:[https://matomo.org/](https://matomo.org/)


## Contributing

Features should be implemented in feature branches. Create a pull request against the develop branch to have your work reviewed for subsequent deployment. The develop branch contains all approved code. The master branch contains work that has passed the Quality Assurance process and is ready to be deployed to production. Hotfixes can be merged directly to master via a pull request, but should be merged back into the develop branch as well.


## Development Environment

**Dependencies**

- .Net Core 2.2
- Docker

**Run Locally**

- Install .Net Core 2.2 [https://dotnet.microsoft.com/download/dotnet-core/2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- Make an .env file locally and place the BEARER\_TOKEN needed from the site administrator. This BEARER\_TOKEN is required for pulling the data from the Eventbrite API.
- Run the project using command dotnet run from the terminal.


## Deployment

**Projects** : We have four projects in the Openshift

- ifttgq-tools: This project is for deploying Jenkins pipeline and any infrastructural setup like matomo and sonar.
- ifttgq-dev: This project is for deploying pull request
- ifttgq-test: This is an intermediate project which ensures the code is correct before deploying it to the prod project.
- ifttgq-prod: This project reflects the current status of master branch.

**Environment** : We have three environments:

| **OpenShift Project** | **Name** | **URL** |
| --- | --- | --- |
| ifttgq-dev | Development | https://bcdevexchange-dev-{PR}-ifttgq-dev.pathfinder.gov.bc.ca|
| ifttgq-test | Testing | https://bcdevexchange-test-ifttgq-test.pathfinder.gov.bc.ca|
| ifttgq-prod | Production | https://bcdevexchange-prod-ifttgq-prod.pathfinder.gov.bc.ca|

**Deployment Process**

The &quot;ifttgq-tools&quot; OpenShift project is used to trigger the deployment process for all environments. On creation of every pull request against the develop branch, a deployment containing the pull request number starts in the dev project. The deployment gets updated on every change to the pull request. The develop branch reflects the current pull request deployment in the dev project. To deploy to the Test and Prod environment, merge the develop branch into the master branch. &quot;ifttgq-prod&quot; has been configured to start the deployment process for production automatically when commits are made to the master branch.


## Team

The bcdevexchange is currently operated by the BC DevExchange Lab within the Government of British Columbia.
