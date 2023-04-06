DELETE FROM `schooldb`.`Subjects` WHERE Id >= 0;
ALTER TABLE `schooldb`.`Subjects` AUTO_INCREMENT = 1;
INSERT INTO `schooldb`.`Subjects` (`Name`, `Room`, `Professor`) VALUES('Calculo', '101A', 'Hernando Guerrero');
INSERT INTO `schooldb`.`Subjects` (`Name`, `Room`, `Professor`) VALUES('Fisica', '102A', 'Carlos Garzon');
INSERT INTO `schooldb`.`Subjects` (`Name`, `Room`, `Professor`) VALUES('Etica', '201B', 'Angela Velez');
INSERT INTO `schooldb`.`Subjects` (`Name`, `Room`, `Professor`) VALUES('Logica', '103A', 'Eduardo Ruiz');
INSERT INTO `schooldb`.`Subjects` (`Name`, `Room`, `Professor`) VALUES('Ingles Tecnico', '301B', 'Amparo Sinisterra');
