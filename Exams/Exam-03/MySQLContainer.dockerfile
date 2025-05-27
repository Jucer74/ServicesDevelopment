FROM mysql:8.0

# Variables de entorno para configuración
ENV MYSQL_ROOT_PASSWORD=r00tP@ssw0rd
ENV MYSQL_DATABASE=moneybankdb
ENV MYSQL_USER=moneybankuser
ENV MYSQL_PASSWORD=M0n3yB4nkUs3r*01

# Copiar scripts de inicialización si los tienes
 #COPY ./init-scripts/ /docker-entrypoint-initdb.d/

EXPOSE 3306