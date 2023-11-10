/*DROP USER 'categoryserviceuser'@'localhost' ;*/
CREATE USER 'categoryserviceuser'@'localhost' IDENTIFIED BY 'C4t3g0ryS3rv1c3Us3r';
GRANT ALL PRIVILEGES ON *.* TO 'categoryserviceuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;