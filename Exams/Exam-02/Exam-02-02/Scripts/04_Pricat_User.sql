/*DROP USER 'pricatuser'@'localhost' ;*/
CREATE USER 'pricatuser'@'localhost' IDENTIFIED BY 'Pr1c4tUs3r';
GRANT ALL PRIVILEGES ON *.* TO 'pricatuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;