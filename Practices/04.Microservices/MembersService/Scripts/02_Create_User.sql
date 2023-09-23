/*DROP USER 'membersserviceuser'@'localhost' ;*/
CREATE USER 'membersserviceuser'@'localhost' IDENTIFIED BY 'M3mb3rsS3rv1c3Us3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'membersserviceuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;