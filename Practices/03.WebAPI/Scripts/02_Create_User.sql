/*DROP USER 'teamsuser'@'localhost' ;*/
CREATE USER 'teamsuser'@'localhost' IDENTIFIED BY 'T34msUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'teamsuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;