-- MySQL dump 10.13  Distrib 5.7.12, for Win32 (AMD64)
--
-- Host: localhost    Database: customproducts
-- ------------------------------------------------------
-- Server version	5.7.15-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `adress`
--

DROP TABLE IF EXISTS `adress`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `adress` (
  `idAdress` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `FKCity` bigint(20) unsigned NOT NULL,
  `street` varchar(25) NOT NULL,
  `house` varchar(25) NOT NULL,
  `flat` varchar(25) DEFAULT NULL,
  `Second_Name` varchar(25) NOT NULL,
  `First_Name` varchar(25) NOT NULL,
  `Midle_Name` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`idAdress`),
  UNIQUE KEY `idAdress` (`idAdress`),
  UNIQUE KEY `FKCity` (`FKCity`,`street`,`house`,`flat`),
  KEY `FKCity_2` (`FKCity`,`street`,`house`,`flat`),
  CONSTRAINT `adress_ibfk_1` FOREIGN KEY (`FKCity`) REFERENCES `city` (`id_City`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adress`
--

LOCK TABLES `adress` WRITE;
/*!40000 ALTER TABLE `adress` DISABLE KEYS */;
INSERT INTO `adress` VALUES (19,18,'Насорова','4','4','Кузнєц','Семен','Петрович'),(20,13,'Оводова','3','','Малишев','Артем','Олексієв'),(21,23,'Київська','5','2','Кіяниця','Віталій','Максимович'),(22,7,'Мізкевича','4','3','Миколюк','Влад','Сергійович'),(23,11,'Несторова','54','3','Яценко','Володимир','Ярославович'),(24,3,'Вільна','45','3','Чукань','Костя','Валентинович'),(25,12,'Михайлова','3','65','Гончар','Максим','Петрович'),(26,6,'Вічна','4','32','Борзов','Микола','Семенович'),(27,5,'Соборна','43','22','Михайлов','Олесандр','Вікторович'),(28,25,'Просідна','32','1','Голосов','Петро','Олексійович'),(29,8,'Монастирська','63','2','Зеленько','Сергій','Іванович');
/*!40000 ALTER TABLE `adress` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cargo`
--

DROP TABLE IF EXISTS `cargo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cargo` (
  `id_Cargo` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name_Cargo` varchar(25) NOT NULL,
  `masa` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_Cargo`),
  UNIQUE KEY `id_Cargo` (`id_Cargo`),
  UNIQUE KEY `name_Cargo` (`name_Cargo`),
  KEY `name_2` (`name_Cargo`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cargo`
--

LOCK TABLES `cargo` WRITE;
/*!40000 ALTER TABLE `cargo` DISABLE KEYS */;
INSERT INTO `cargo` VALUES (1,'Щебень',1001),(2,'Сахар',50),(3,'Кружка',1),(4,'Ведро',1),(5,'Телевизор',15),(6,'Холодильник',60),(7,'Стол',37),(8,'Кабель',2),(9,'Ваза',3),(10,'Велосипед',24);
/*!40000 ALTER TABLE `cargo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `city`
--

DROP TABLE IF EXISTS `city`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `city` (
  `id_City` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name_City` varchar(25) NOT NULL,
  `Map_City` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`id_City`),
  UNIQUE KEY `id_City` (`id_City`),
  UNIQUE KEY `name_City` (`name_City`),
  KEY `name_City_2` (`name_City`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `city`
--

LOCK TABLES `city` WRITE;
/*!40000 ALTER TABLE `city` DISABLE KEYS */;
INSERT INTO `city` VALUES (1,'Дніпро','https://www.google.com.ua/maps/place/%D0%94%D0%BD%D0%B5%D0%BF%D1%80%D0%BE%D0%BF%D0%B5%D1%82%D1%80%D0%BE%D0%B2%D1%81%D0%BA,+%D0%94%D0%BD%D0%B5%D0%BF%D1%80%D0%BE%D0%BF%D0%B5%D1%82%D1%80%D0%BE%D0%B2%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.8111507,26.9302029,6z/data=!4m5!3m4!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!8m2!3d48.464717!4d35.046183'),(2,'Вінниця','https://www.google.com.ua/maps/place/%D0%92%D1%96%D0%BD%D0%BD%D0%B8%D1%86%D1%8F,+%D0%92%D1%96%D0%BD%D0%BD%D0%B8%D1%86%D1%8C%D0%BA%D0%B0+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C,+%D0%A3%D0%BA%D1%80%D0%B0%D1%97%D0%BD%D0%B0/@48.5288155,26.7046096,6z/data=!4m13!1m7!3m6!1s0x472d5b65195a6489:0xcbd4e159eedd74fe!2z0JLRltC90L3QuNGG0Y8sINCS0ZbQvdC90LjRhtGM0LrQsCDQvtCx0LvQsNGB0YLRjCwg0KPQutGA0LDRl9C90LA!3b1!8m2!3d49.233083!4d28.468217!3m4!1s0x472d5b65195a6489:0xcbd4e159eedd74fe!8m2!3d49.233083!4d28.468217'),(3,'Луганськ','https://www.google.com.ua/maps/place/%D0%9B%D1%83%D0%B3%D0%B0%D0%BD%D1%81%D0%BA,+%D0%9B%D1%83%D0%B3%D0%B0%D0%BD%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@49.1145224,26.6732427,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x411fc564844c6285:0xf8e02e7e879e7164!8m2!3d48.5747899!4d39.309082'),(5,'Хмельницький','https://www.google.com.ua/maps/place/%D0%A5%D0%BC%D0%B5%D0%BB%D1%8C%D0%BD%D0%B8%D1%86%D0%BA%D0%B8%D0%B9,+%D0%A5%D0%BC%D0%B5%D0%BB%D1%8C%D0%BD%D0%B8%D1%86%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@49.1650981,27.0639209,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x4732064344bb178b:0xd9f30b3b24d9c964!8m2!3d49.4216941!4d26.987915'),(6,'Харків','https://www.google.com.ua/maps/place/%D0%A5%D0%B0%D1%80%D1%8C%D0%BA%D0%BE%D0%B2,+%D0%A5%D0%B0%D1%80%D1%8C%D0%BA%D0%BE%D0%B2%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.8583995,26.5848884,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x4127a09f63ab0f8b:0x2d4c18681aa4be0a!8m2!3d49.9936155!4d36.2329102'),(7,'Маріуполь','https://www.google.com.ua/maps/place/%D0%9C%D0%B0%D1%80%D0%B8%D1%83%D0%BF%D0%BE%D0%BB%D1%8C,+%D0%94%D0%BE%D0%BD%D0%B5%D1%86%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.6197798,27.0488113,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x40e6e6a7bee7582b:0xa5d118300a75b5ce!8m2!3d47.0963053!4d37.5457764'),(8,'Чернігів','https://www.google.com.ua/maps/place/%D0%A7%D0%B5%D1%80%D0%BD%D0%B8%D0%B3%D0%BE%D0%B2,+%D0%A7%D0%B5%D1%80%D0%BD%D0%B8%D0%B3%D0%BE%D0%B2%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.6197798,27.0488113,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x46d5488971ee3597:0x2a2348d3e76038b5!8m2!3d51.4984845!4d31.2890625'),(10,'Житомир','https://www.google.com.ua/maps/place/%D0%96%D0%B8%D1%82%D0%BE%D0%BC%D0%B8%D1%80,+%D0%96%D0%B8%D1%82%D0%BE%D0%BC%D0%B8%D1%80%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.9592853,26.9347859,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x472c64a32bfa355d:0xf14ad2a3d9b9e229!8m2!3d50.2542299!4d28.6578369'),(11,'Київ','https://www.google.com.ua/maps/place/%D0%9A%D0%B8%D0%B5%D0%B2/@48.8667112,27.0060478,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x40d4cf4ee15a4505:0x764931d2170146fe!8m2!3d50.4505091!4d30.5255127'),(12,'Миколаїв','https://www.google.com.ua/maps/place/%D0%9D%D0%B8%D0%BA%D0%BE%D0%BB%D0%B0%D0%B5%D0%B2,+%D0%9D%D0%B8%D0%BA%D0%BE%D0%BB%D0%B0%D0%B5%D0%B2%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.3147721,27.1128928,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x40c5cb89fb7213d5:0x970e39fab9e05680!8m2!3d46.9765045!4d31.9921875'),(13,'Одеса','https://www.google.com.ua/maps/place/%D0%9E%D0%B4%D0%B5%D1%81%D1%81%D0%B0,+%D0%9E%D0%B4%D0%B5%D1%81%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.238198,27.3417229,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x40c6318a0b864c43:0x129f8fe28cf2176c!8m2!3d46.4832647!4d30.7232666'),(14,'Херсон','https://www.google.com.ua/maps/place/%D0%A5%D0%B5%D1%80%D1%81%D0%BE%D0%BD,+%D0%A5%D0%B5%D1%80%D1%81%D0%BE%D0%BD%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.3355956,27.142151,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x40c41aa25d9bf967:0xf3a5478c8e3121fe!8m2!3d46.6343507!4d32.6184082'),(15,'Донецьк','https://www.google.com.ua/maps/place/%D0%94%D0%BE%D0%BD%D0%B5%D1%86%D0%BA,+%D0%94%D0%BE%D0%BD%D0%B5%D1%86%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@49.0172657,26.9337995,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x40e0909500919a2d:0x36335efdc5856f84!8m2!3d48.0156498!4d37.8039551'),(18,'Симфірополь','https://www.google.com.ua/maps/place/%D0%A1%D0%B8%D0%BC%D1%84%D0%B5%D1%80%D0%BE%D0%BF%D0%BE%D0%BB%D1%8C/@48.4935899,26.5633205,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x40eadddedf991cc5:0x9c29422fbc780b40!8m2!3d44.9511931!4d34.1015625'),(23,'Севастополь','https://www.google.com.ua/maps/place/%D0%A1%D0%B5%D0%B2%D0%B0%D1%81%D1%82%D0%BE%D0%BF%D0%BE%D0%BB%D1%8C/@48.6319444,26.5545512,6z/data=!4m13!1m7!3m6!1s0x40dbe303fd08468f:0xa1cf3d5f2c11aba!2z0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQuiwg0JTQvdC10L_RgNC-0L_QtdGC0YDQvtCy0YHQutCw0Y8g0L7QsdC70LDRgdGC0Yw!3b1!8m2!3d48.464717!4d35.046183!3m4!1s0x409525ef659144f5:0xbd2da7afff4d34cc!8m2!3d44.6178442!4d33.5247803'),(25,'Черкаси','https://www.google.com.ua/maps/place/%D0%A7%D0%B5%D1%80%D0%BA%D0%B0%D1%81%D1%81%D1%8B,+%D0%A7%D0%B5%D1%80%D0%BA%D0%B0%D1%81%D1%81%D0%BA%D0%B0%D1%8F+%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C/@48.424426,26.7056474,6z/data=!4m5!3m4!1s0x40d14b866064977f:0xf8dce723a9cbb5d8!8m2!3d49.444433!4d32.059767');
/*!40000 ALTER TABLE `city` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `custom_products`
--

DROP TABLE IF EXISTS `custom_products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `custom_products` (
  `id_Custom_Products` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `FKWhere` bigint(20) unsigned NOT NULL,
  `InTime` datetime NOT NULL,
  `FKAdress` bigint(20) unsigned NOT NULL,
  `FKCargo` bigint(20) unsigned NOT NULL,
  `FKTrack` bigint(20) unsigned NOT NULL,
  `FKTransport` bigint(20) unsigned NOT NULL,
  `FKRepresentation` bigint(20) unsigned NOT NULL,
  `IsCompleted` varchar(3) NOT NULL,
  PRIMARY KEY (`id_Custom_Products`),
  UNIQUE KEY `id_Custom_Products` (`id_Custom_Products`),
  UNIQUE KEY `FKCargo` (`FKCargo`,`FKTrack`,`FKTransport`,`FKRepresentation`),
  KEY `FKTrack` (`FKTrack`),
  KEY `FKTransport` (`FKTransport`),
  KEY `FKRepresentation` (`FKRepresentation`),
  KEY `FKAdress` (`FKAdress`),
  KEY `FKWhere` (`FKWhere`),
  CONSTRAINT `custom_products_ibfk_1` FOREIGN KEY (`FKCargo`) REFERENCES `cargo` (`id_Cargo`),
  CONSTRAINT `custom_products_ibfk_3` FOREIGN KEY (`FKTrack`) REFERENCES `track` (`id_Track`),
  CONSTRAINT `custom_products_ibfk_4` FOREIGN KEY (`FKTransport`) REFERENCES `transports` (`id_Transport`),
  CONSTRAINT `custom_products_ibfk_5` FOREIGN KEY (`FKRepresentation`) REFERENCES `representation` (`id_Representation`),
  CONSTRAINT `custom_products_ibfk_6` FOREIGN KEY (`FKAdress`) REFERENCES `adress` (`idAdress`),
  CONSTRAINT `custom_products_ibfk_7` FOREIGN KEY (`FKWhere`) REFERENCES `city` (`id_City`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `custom_products`
--

LOCK TABLES `custom_products` WRITE;
/*!40000 ALTER TABLE `custom_products` DISABLE KEYS */;
INSERT INTO `custom_products` VALUES (35,3,'2017-05-10 20:53:02',23,3,2,7,8,'0'),(36,12,'2017-05-11 10:57:02',24,4,4,4,5,'0');
/*!40000 ALTER TABLE `custom_products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `representation`
--

DROP TABLE IF EXISTS `representation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `representation` (
  `id_Representation` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name_Representation` varchar(25) NOT NULL,
  PRIMARY KEY (`id_Representation`),
  UNIQUE KEY `id_Representation` (`id_Representation`),
  UNIQUE KEY `name_Representation` (`name_Representation`),
  KEY `name_Representation_2` (`name_Representation`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `representation`
--

LOCK TABLES `representation` WRITE;
/*!40000 ALTER TABLE `representation` DISABLE KEYS */;
INSERT INTO `representation` VALUES (11,'MistExpress'),(3,'Автолюкс'),(5,'АВТОПОЧТА'),(7,'Атарес-Плюс'),(6,'Витал Спец Сервис'),(8,'Деливери'),(9,'Ин-Тайм'),(10,'КМ Экспресс'),(2,'Нова пошта'),(1,'Укр пошта');
/*!40000 ALTER TABLE `representation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `store`
--

DROP TABLE IF EXISTS `store`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `store` (
  `id_Store` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name_Serial` varchar(25) NOT NULL,
  `FKCity` bigint(20) unsigned NOT NULL,
  PRIMARY KEY (`id_Store`),
  UNIQUE KEY `id_Store` (`id_Store`),
  UNIQUE KEY `name_Serial` (`name_Serial`),
  KEY `FKCity` (`FKCity`),
  KEY `name_Serial_2` (`name_Serial`),
  CONSTRAINT `store_ibfk_1` FOREIGN KEY (`FKCity`) REFERENCES `city` (`id_City`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `store`
--

LOCK TABLES `store` WRITE;
/*!40000 ALTER TABLE `store` DISABLE KEYS */;
INSERT INTO `store` VALUES (22,'Склад міста Вінниця',2);
/*!40000 ALTER TABLE `store` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `track`
--

DROP TABLE IF EXISTS `track`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `track` (
  `id_Track` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `length` bigint(20) unsigned NOT NULL,
  `name_Track` varchar(25) NOT NULL,
  PRIMARY KEY (`id_Track`),
  UNIQUE KEY `id_Track` (`id_Track`),
  UNIQUE KEY `name_Track` (`name_Track`),
  KEY `name_Track_2` (`name_Track`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `track`
--

LOCK TABLES `track` WRITE;
/*!40000 ALTER TABLE `track` DISABLE KEYS */;
INSERT INTO `track` VALUES (1,120,'Винница-Хмельницкий'),(2,230,'Житомир-Киев'),(3,250,'Мариуполь-Донецк'),(4,470,'Донецк-Винница'),(5,280,'Харьков-Киев'),(6,320,'Черновцы-Винница'),(7,290,'Винница-Донецк'),(8,240,'Житомир-Хмельницкий'),(9,220,'Киев-Хмельницкий'),(10,278,'Винница-Харьков');
/*!40000 ALTER TABLE `track` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `track_city`
--

DROP TABLE IF EXISTS `track_city`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `track_city` (
  `id_Track_City` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `FKTrack` bigint(20) unsigned NOT NULL,
  `FKCity` bigint(20) unsigned NOT NULL,
  PRIMARY KEY (`id_Track_City`),
  UNIQUE KEY `id_Track_City` (`id_Track_City`),
  UNIQUE KEY `FKTrack` (`FKTrack`,`FKCity`),
  KEY `FKCity` (`FKCity`),
  CONSTRAINT `track_city_ibfk_1` FOREIGN KEY (`FKTrack`) REFERENCES `track` (`id_Track`),
  CONSTRAINT `track_city_ibfk_2` FOREIGN KEY (`FKCity`) REFERENCES `city` (`id_City`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `track_city`
--

LOCK TABLES `track_city` WRITE;
/*!40000 ALTER TABLE `track_city` DISABLE KEYS */;
INSERT INTO `track_city` VALUES (1,1,2),(30,1,5),(3,2,1),(2,2,10),(17,2,23),(5,4,1),(4,4,5),(8,6,1),(7,6,2),(6,6,8),(18,6,13);
/*!40000 ALTER TABLE `track_city` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transports`
--

DROP TABLE IF EXISTS `transports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `transports` (
  `id_Transport` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name_Transport` varchar(25) NOT NULL,
  `roominess` bigint(20) DEFAULT NULL,
  `spead` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_Transport`),
  UNIQUE KEY `id_Transport` (`id_Transport`),
  UNIQUE KEY `name_Transport` (`name_Transport`,`roominess`),
  KEY `name_Transport_2` (`name_Transport`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transports`
--

LOCK TABLES `transports` WRITE;
/*!40000 ALTER TABLE `transports` DISABLE KEYS */;
INSERT INTO `transports` VALUES (1,'Легковой автомобиль',100,150),(2,'Грузовик',1000,100),(3,'Минивен',300,130),(4,'Камаз',4000,80),(5,'Мотоцикл',40,150),(6,'Кур\'єр',20,7),(7,'Катер',170,110),(8,'Вертолет',300,180),(9,'Велосипед',10,24),(10,'Самовывоз',NULL,NULL);
/*!40000 ALTER TABLE `transports` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-06-29 18:43:53
