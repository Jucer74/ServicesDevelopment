/*DROP USER 'teamsserviceuser'@'localhost' ;*/
CREATE USER 'teamsserviceuser'@'localhost' IDENTIFIED BY 'T34msS3rv1c3Us3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'teamsserviceuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;