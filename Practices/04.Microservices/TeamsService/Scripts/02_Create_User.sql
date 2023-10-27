/*DROP USER 'teamsserviceuser'@'localhost' ;*/
CREATE USER 'teamsserviceuser' IDENTIFIED BY 'T34msS3rv1c3Us3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'teamsserviceuser' WITH GRANT OPTION;
FLUSH PRIVILEGES;