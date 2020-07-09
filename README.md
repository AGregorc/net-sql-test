# NET-SQL-Test

To run mysql locally with docker:
docker run --name ec38 -p 3306:3306 -e MYSQL_ROOT_PASSWORD=ec38 -e MYSQL_USER=ec38 -e MYSQL_PASSWORD=ec38 -d mysql:latest

Run mysql terminal:
docker exec -it ec38 /bin/bash
mysql -u root -p
(password is ec38)

To create database, you can just copy-paste code from initialize.sql to terminal.

Then grant privileges for user ec38:
GRANT ALL PRIVILEGES ON ec38.* TO 'ec38';

At the end open project in Visual Studio and run it