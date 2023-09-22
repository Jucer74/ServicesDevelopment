/*DROP USER 'memberserviceuser'@'localhost' ;*/
CREATE USER 'memberserviceuser'@'localhost' IDENTIFIED BY 'M3mb3rsS3rv1c3Us3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'memberserviceuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;