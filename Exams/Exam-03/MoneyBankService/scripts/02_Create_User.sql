-- Primero asegúrate de que el usuario no existe
DROP USER IF EXISTS 'moneybankuser'@'%';
CREATE USER 'moneybankuser'@'%' IDENTIFIED BY 'M0n3yB4nkUs3r*01';
GRANT ALL PRIVILEGES ON moneybankdb.* TO 'moneybankuser'@'%';
FLUSH PRIVILEGES;