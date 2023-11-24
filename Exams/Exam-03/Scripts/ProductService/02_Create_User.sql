/*DROP USER 'productserviceuser'@'localhost' ;*/
CREATE USER 'productserviceuser' IDENTIFIED BY 'Pr0ductS3rv1c3Us3r';
GRANT ALL PRIVILEGES ON *.* TO 'productserviceuser' WITH GRANT OPTION;
FLUSH PRIVILEGES;