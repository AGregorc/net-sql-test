# NET-SQL-Test

## Creating database:

1. To run mysql locally with docker:

    docker run --name ec38 -p 3306:3306 -e MYSQL_ROOT_PASSWORD=ec38 -e MYSQL_USER=ec38 -e MYSQL_PASSWORD=ec38 -d mysql:latest

2. Run mysql terminal:

    docker exec -it ec38 /bin/bash
    mysql -u root -p

(password is ec38)

3. To create database, you can just copy-paste code from initialize.sql to terminal.

4. Then grant privileges for user ec38:

    GRANT ALL PRIVILEGES ON ec38.* TO 'ec38';

## Run project
At the end open project in Visual Studio and run it.
