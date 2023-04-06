/*DROP USER 'schoolappuser'@'localhost' ;*/
CREATE USER 'schoolappuser'@'localhost' IDENTIFIED BY 'Sch00lAppUs3r';
GRANT ALL PRIVILEGES ON *.* TO 'schoolappuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;