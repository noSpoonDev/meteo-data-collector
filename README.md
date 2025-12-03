### About the app

This .NET console app is periodically performing the following pipeline:
- fetching XML meteo station report
- transforming it to JSON
- saving the JSON along with metadata in database

Built with best practices in mind, the app follows a layered architecture, adheres to SOLID design principles, and
incorporates structured logging.

### How to run

The easiest way to run it is with Docker, either as a full environment including the database (
see `docker-compose.yml`), or as a container with only the application.

Ensure Docker is installed on your machine, then in the folder containing `docker-compose.yml`, run:

`docker-compose up --build`