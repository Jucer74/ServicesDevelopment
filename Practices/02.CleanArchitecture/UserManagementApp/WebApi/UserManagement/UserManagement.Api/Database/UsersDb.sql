-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: usersdb
-- ------------------------------------------------------
-- Server version	8.0.37

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(255) NOT NULL,
  `Fullname` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Username` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=102 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (2,'kplet0@joomla.org','Kari Plet','tZ0I%tm?**\"Y','kplet0'),(3,'sfookes1@unesco.org','Saxe Fookes','oI2#wlf\'','sfookes1'),(4,'pstuffins2@wiley.com','Pam Stuffins','rL0qW|q@~<Ya,','pstuffins2'),(5,'acristol3@dailymotion.com','Averil Cristol','kE60oQ~)','acristol3'),(6,'aphette4@dropbox.com','Adina Phette','cK7t!W%Z|khx*(W1','aphette4'),(7,'lhappert5@sfgate.com','Lexy Happert','xG2OJtFSw,W}12wl','lhappert5'),(8,'dsoppit6@about.me','Dulcea Soppit','gH6a&?@k(u\'{XNZ2','dsoppit6'),(9,'bdavydkov7@artisteer.com','Brander Davydkov','oP7q.3\"20e,9Yv','bdavydkov7'),(10,'cpallent8@un.org','Christina Pallent','hR9W<qY9l+,','cpallent8'),(11,'shuman9@edublogs.org','Shirl Human','xU2t%2d0*','shuman9'),(12,'cluqueta@icq.com','Charity Luquet','cD1a2P\"=4fmM','cluqueta'),(13,'hfortb@noaa.gov','Haskel Fort','zY0z{@j3`c>SIV','hfortb'),(14,'bcostanc@privacy.gov.au','Bette-ann Costan','eB6Vj>5De}JDh','bcostanc'),(15,'reisigd@webnode.com','Royal Eisig','vF5nEHFFU!isi>','reisigd'),(16,'scottue@123-reg.co.uk','Serge Cottu','jW4(Qa+%f7oTDA','scottue'),(17,'aantognozziif@kickstarter.com','Ayn Antognozzii','aZ8(#KEZ\"z?dlz4E','aantognozziif'),(18,'mtilberryg@slashdot.org','Marlow Tilberry','yC2*1X9f','mtilberryg'),(19,'cingremh@dyndns.org','Corilla Ingrem','eE2==3y47`j|','cingremh'),(20,'cbuncomi@histats.com','Cybil Buncom','gK97UCrs2,uz8','cbuncomi'),(21,'wvahlj@reverbnation.com','Wylma Vahl','zX06{q9iY9$6%','wvahlj'),(22,'despositok@unc.edu','Duncan Esposito','lE2)H4Fy{sTh\"7)','despositok'),(23,'wspreulll@printfriendly.com','Willabella Spreull','gS4r__5Ce(U}x~`','wspreulll'),(24,'qmillsapm@shareasale.com','Quintina Millsap','hL9B(ZlW+Yz','qmillsapm'),(25,'bhinkensn@addthis.com','Bobbee Hinkens','vI2I}E42&nwKj','bhinkensn'),(26,'spollastrinoo@360.cn','Stanly Pollastrino','jH1X!L@R@<pVPr','spollastrinoo'),(27,'carnoudp@last.fm','Carlos Arnoud','eS4|?`u1Gy`','carnoudp'),(28,'achildq@xinhuanet.com','Andrew Child','zJ0)ZbFJLnp7)6)','achildq'),(29,'ktamer@imageshack.us','Korey Tame','zN1I\"i_vAYhiH(','ktamer'),(30,'klockarts@nymag.com','Kailey Lockart','jI2o~HP@','klockarts'),(31,'mbangst@rambler.ru','Maybelle Bangs','dY6`1t|B(','mbangst'),(32,'lblakistonu@google.nl','L;urette Blakiston','hB5M&wIaMy','lblakistonu'),(33,'dfressonv@ted.com','Danielle Fresson','yO6Kw@BMhDXl','dfressonv'),(34,'bdunrigew@hp.com','Benji Dunrige','kV01>$jf`iEOrU','bdunrigew'),(35,'ramesburyx@goodreads.com','Richie Amesbury','cS2Z>f603sp6j','ramesburyx'),(36,'rmconiey@g.co','Raul McOnie','xF4jU#PRsI3','rmconiey'),(37,'gucz@ucsd.edu','Glenn Uc','sM0Ng&Qj)j','gucz'),(38,'blafflin10@google.co.jp','Bernardine Lafflin','rA2ZudK.D?lh,F','blafflin10'),(39,'tchessel11@accuweather.com','Timothee Chessel','wQ1cpN5ni_X','tchessel11'),(40,'lwoolvin12@mediafire.com','Lolly Woolvin','nV6UA+\"+bA1>dyo','lwoolvin12'),(41,'eskelton13@google.it','Emmett Skelton','pE6\'pWs!xz\"s','eskelton13'),(42,'dtregidgo14@list-manage.com','Damaris Tregidgo','bM6C$&&W%8L,','dtregidgo14'),(43,'mlehrian15@dot.gov','Merrile Lehrian','jT4Bpv!,/','mlehrian15'),(44,'gvan16@bloglines.com','Godard Van Leeuwen','yG0SZ7X{?=t7?s','gvan16'),(45,'cposten17@drupal.org','Catharina Posten','lJ5INyNV','cposten17'),(46,'osoots18@china.com.cn','Ofelia Soots','rT02~k9#?|/Ok.S','osoots18'),(47,'cfawcus19@simplemachines.org','Carmen Fawcus','zC0WT,!}EiL','cfawcus19'),(48,'ttomicki1a@sourceforge.net','Tabbi Tomicki','yQ9Ba!ld=1','ttomicki1a'),(49,'jwinspear1b@stanford.edu','Jenna Winspear','mB5u>{`zGFDR4','jwinspear1b'),(50,'cknox1c@altervista.org','Cherrita Knox','rC3lteQv<HBk','cknox1c'),(51,'hmowday1d@senate.gov','Hendrika Mowday','dN5ca+4J','hmowday1d'),(52,'swren1e@hp.com','Suki Wren','cA3@A608','swren1e'),(53,'gondracek1f@squidoo.com','Gawen Ondracek','lQ4N@IUPhlE','gondracek1f'),(54,'yharrema1g@mac.com','Yale Harrema','mH9zxdvMv+`5K?F\"','yharrema1g'),(55,'harch1h@prlog.org','Hilary Arch','yR2tyh6p,~{et+&!','harch1h'),(56,'ajaquest1i@slashdot.org','Anallese Jaquest','iV7yhRTBAV','ajaquest1i'),(57,'dclist1j@phpbb.com','Dillie Clist','dC3)WVYjA)$','dclist1j'),(58,'fgarci1k@ucoz.ru','Finley Garci','dA6X8(|%~)pq4beL','fgarci1k'),(59,'aashelford1l@howstuffworks.com','Adan Ashelford','hX8.j%.!&1','aashelford1l'),(60,'tstrelitzki1m@cbc.ca','Tessi Strelitzki','sM6505N63?p.w<','tstrelitzki1m'),(61,'swooding1n@whitehouse.gov','Shelagh Wooding','nU7FeNTMG2FWDS','swooding1n'),(62,'balesi1o@yellowpages.com','Barbra Alesi','gT8o0=A6}$>Q~3!b','balesi1o'),(63,'ddradey1p@cnbc.com','Deborah Dradey','wA9\"R#@1P','ddradey1p'),(64,'gtrappe1q@dagondesign.com','Gabriella Trappe','zF4UouB*D','gtrappe1q'),(65,'bthrelkeld1r@wikimedia.org','Benedicta Threlkeld','fM3UR_0kU_U','bthrelkeld1r'),(66,'lyelden1s@xinhuanet.com','Lucas Yelden','iS6j,#1EjP%W\'2','lyelden1s'),(67,'atirte1t@discovery.com','Alister Tirte','sR3~&pq/e&rK7AT','atirte1t'),(68,'wreside1u@webs.com','Walliw Reside','cQ5v),6jVO\'(YMz+','wreside1u'),(69,'skas1v@fda.gov','Sherrie Kas','fZ6hL)s=','skas1v'),(70,'nadnet1w@github.com','Nonna Adnet','jF7SX6bAj{tkH','nadnet1w'),(71,'alunam1x@networksolutions.com','Aleksandr Lunam','fU3p,X}YA','alunam1x'),(72,'rmattack1y@yandex.ru','Rhody Mattack','cW3)@/jjErNa','rmattack1y'),(73,'emckibbin1z@stanford.edu','Ellery McKibbin','yZ9*Vkh_\"L.jtyj\"','emckibbin1z'),(74,'jcornes20@about.me','Jerrold Cornes','cV3O60+4?|','jcornes20'),(75,'lmacvay21@deviantart.com','Lana MacVay','gZ1c&x/T','lmacvay21'),(76,'floache22@msu.edu','Free Loache','fL1acwLmt','floache22'),(77,'abougen23@netlog.com','Alric Bougen','nB4JLaoMJ#','abougen23'),(78,'swilne24@uol.com.br','Sabine Wilne','lD7wSBb2qm&bvI|5','swilne24'),(79,'bsamme25@ycombinator.com','Bryna Samme','vW0k}PxV$>a+N@','bsamme25'),(80,'jwakely26@irs.gov','Joceline Wakely','qB1ej=ZIqEUX\"','jwakely26'),(81,'pedgson27@google.cn','Pace Edgson','xU1uCKeH#','pedgson27'),(82,'cdashwood28@miitbeian.gov.cn','Clark Dashwood','rK4QswOkitPV~TZp','cdashwood28'),(83,'praselles29@scribd.com','Patton Raselles','wM6?yd!a&qNn>','praselles29'),(84,'rbrealey2a@alexa.com','Riordan Brealey','uZ3*h_%z','rbrealey2a'),(85,'mimort2b@elpais.com','Maria Imort','xQ69C\"DBQ_3lOOj','mimort2b'),(86,'tmclurg2c@arizona.edu','Temp McLurg','pK6\'X&<U','tmclurg2c'),(87,'pevens2d@google.it','Pavlov Evens','mG6uzOQWeL=','pevens2d'),(88,'bgulliford2e@cbc.ca','Bobbi Gulliford','oY0m6_f@<*yRP','bgulliford2e'),(89,'pgarahan2f@ihg.com','Pablo Garahan','yW60myJ?z`','pgarahan2f'),(90,'froskam2g@statcounter.com','Filippo Roskam','dY0Bx2gUX','froskam2g'),(91,'mklus2h@yale.edu','Micky Klus','eS8g6pknZ','mklus2h'),(92,'mbradnock2i@arstechnica.com','Milissent Bradnock','dK4Al_>w)IJJN','mbradnock2i'),(93,'slorenzetti2j@elegantthemes.com','Saloma Lorenzetti','bM3}hwM/a%8W','slorenzetti2j'),(94,'olechmere2k@elegantthemes.com','Oralla Lechmere','qV9&<%M`f','olechmere2k'),(95,'phealings2l@state.gov','Pollyanna Healings','dZ8WAp89l$$','phealings2l'),(96,'fharsum2m@cornell.edu','Fabien Harsum','fG9SJty1','fharsum2m'),(97,'hchopin2n@qq.com','Hal Chopin','fY6U`v*XR?1$IJ','hchopin2n'),(98,'ohillyatt2o@businesswire.com','Obie Hillyatt','sK8V%G?|','ohillyatt2o'),(99,'lbriat2p@foxnews.com','Louisette Briat','fT0ZheM#cEo','lbriat2p'),(100,'ecroutear2q@google.ru','Erhard Croutear','vE2U!&Jaqn$Fc','ecroutear2q'),(101,'tfritchly2r@imageshack.us','Trevor Fritchly','mA7TBpjP9','tfritchly2r');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-15 19:39:09
