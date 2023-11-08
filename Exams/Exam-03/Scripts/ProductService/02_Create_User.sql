/*DROP USER 'productserviceuser'@'localhost' ;*/
CREATE USER 'productserviceuser'@'localhost' IDENTIFIED BY 'Pr0ductS3rv1c3Us3r';
GRANT ALL PRIVILEGES ON *.* TO 'productserviceuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;