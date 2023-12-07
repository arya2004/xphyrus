
 kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml

above loadballancer in different namespace
authservice-cip:80  put this in appsett.prod.json for url

# PS

Will continue this project on personal level. Fork/Continuation of EDI 3
Storage files on k8s are platform-sensitive. I have azure PVC/PV ones, but since my azure balance is over, will support the MikiKube ones.
Will switch to RabbitMQ from Azure Serivce Bus. 
Below is written by GPT, so ignore it, i guess :0

# Microservice-based Online Assessment Platform

This repository contains the source code and documentation for a Microservice-based Online Assessment (OA) platform built using ASP.NET Core for the backend and Angular for the frontend. This platform is designed to facilitate online assessments and examinations, offering a scalable and flexible solution for organizations to conduct assessments efficiently.

## Table of Contents

- [Introduction](#introduction)
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Getting Started](#getting-started)
- [Architecture](#architecture)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The Microservice-based OA platform is a modern web application designed to simplify the management and execution of online assessments. It offers a range of features to create, administer, and grade assessments, making it suitable for educational institutions, businesses, and any organization requiring online testing and evaluation.

## Technologies Used

The platform is built using the following technologies:

### Backend (ASP.NET Core)

- **ASP.NET Core**: The core framework for building robust, scalable, and high-performance web applications.

- **Microservices Architecture**: The platform is divided into microservices, each responsible for a specific functionality, allowing for flexibility and scalability.

- **Entity Framework Core**: For database operations, using code-first migrations and a relational database like SQL Server.

- **RESTful API**: For communication between frontend and backend microservices, following RESTful principles.

- **Authentication and Authorization**: Implementing authentication and authorization using JWT (JSON Web Tokens) for secure access control.

- **Docker**: Containerization of microservices for easy deployment and scaling.

### Frontend (Angular)

- **Angular**: A powerful and flexible JavaScript framework for building dynamic web applications.

- **TypeScript**: Strongly-typed language for building scalable and maintainable frontend code.

- **RxJS**: For handling asynchronous operations and managing data streams.

- **Bootstrap**: For responsive and modern user interface design.

- **Angular Material**: UI component library for building consistent and attractive user interfaces.

- **HttpClient**: To make HTTP requests to the backend API services.

## Features

The platform offers a wide range of features, including but not limited to:

- User registration and authentication.
- Admin dashboard for managing users, assessments, and results.
- Creating and editing assessment questions.
- Configurable assessment settings (time limits, randomization, etc.).
- Real-time monitoring of assessments.
- Automatic grading and result generation.
- Reporting and analytics for assessment results.
- User-friendly frontend with responsive design.

## Getting Started

To set up and run the Microservice-based OA platform locally, follow these steps:

1. Clone this repository to your local machine.

2. Navigate to the backend and frontend directories and follow the respective README files for setup instructions.

3. Configure the necessary environment variables.

4. Build and run the microservices and the Angular frontend.

5. Access the platform via your web browser.

## Architecture

The platform follows a microservices architecture to ensure scalability, maintainability, and modularity. Here's a high-level overview of the microservices:

- **User Service**: Manages user registration and authentication.

- **Assessment Service**: Handles assessment creation, management, and delivery.

- **Question Service**: Manages assessment questions and options.

- **Result Service**: Handles assessment result storage and reporting.

- **Gateway Service**: Serves as the entry point for the frontend and routes requests to the appropriate microservices.

- **Monitoring Service**: Provides real-time monitoring and analytics.

## Deployment

The platform can be deployed to various cloud providers like AWS, Azure, or Google Cloud, or on-premises servers. Docker containers are used for each microservice to simplify deployment.

## Contributing

We welcome contributions from the community. If you want to contribute to the platform, please follow our [contribution guidelines](CONTRIBUTING.md).

## License

This Microservice-based OA platform is open-source and licensed under the [MIT License](LICENSE).

---

Feel free to explore the code and documentation to get started with the Microservice-based Online Assessment Platform. If you have any questions or need assistance, please refer to the README files in each component's directory or reach out to the community for support. Happy coding!