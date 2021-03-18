-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: cityinformation
-- ------------------------------------------------------
-- Server version	8.0.23

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
-- Table structure for table `__migrationhistory`
--

DROP TABLE IF EXISTS `__migrationhistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__migrationhistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ContextKey` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Model` longblob NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`,`ContextKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__migrationhistory`
--

LOCK TABLES `__migrationhistory` WRITE;
/*!40000 ALTER TABLE `__migrationhistory` DISABLE KEYS */;
INSERT INTO `__migrationhistory` VALUES ('202005111704009_InitialCreate','CityInformation.Migrations.Configuration',_binary '‹\0\0\0\0\0\0\Ý\\[o\ã¸~/\Ðÿ \è±\ÈZ¹t\ÓÀ\ÞE\ÖIÚ “Æ™E\ß´D;\ÂH¤V¢²	Šýe}\èO\ê_()Q\Þ$\ÊVlg1ÀÀ\"¿sxxH\æÿù\ïô§—8ržaš…\ÍÜ“É±\ë@\ä\ã Dë™›“\ÕŸÜŸ~üóŸ¦WAü\âüRÑ1:\Úe3÷‰\ä\Üó2ÿ	\Æ ›Ä¡Ÿ\â¯\È\ÄÇ±\ìÿ\Í;9ñ …p)–\ãL¿äˆ„1,>\è\ç#&$\Ñ-`”ñrZ³(P;\Ã,>œ¹ó¼Þ Nc@¨,“²…\ë\\D! \Ò,`´r€&Eýù\×.HŠ\Ñz‘\Ð=¾&Ò­@”AÞ‡ó†Ü¶;Ç§¬;^Ó°‚òóŒ\àx \à\É×\'7\ßH\Ën­?ªÁ+ªiò\Êz]hq\æ\Þ°(ú‚#ª\0™\áù<JñÌ½­Y\\d\É$“ªá¤„¼N)\Üo8ý>i#9\Ö\íŽj{:³G\Î<Hž\Â‚9IAt\ä<\ä\Ë(ôÿ	_ñwˆfg\'\Ë\ÕÙ§Apöñ¯ð\ìC»§´¯”N( E)N`Jeƒ«ºÿ®\ã‰\í<¹aÝ¬Õ¦\Ô\nµ%:5\\\ç¼|†hMž\è¤9ý\ä:\×\áªn\\_QHgmDÒœ~\Þ\åQ–¬\ë½Nž\ìÿ®§>Ž\Âõ<‡\ëb\è%þt\â¤t^}QQ›=…I9½„ñþ\ÆÉ®S³oÑ¾\Ê\Úoœ§>\ë6’<‚t\r‰(\Ý\ÔkŒ\×Ê¤\Ôøf]¡¾i3IUóÖ’²m2*»ž\r•¼o\Ë\×\Ú\â.’„^aZL#]§ß°&5‘®1¡[B´k\äñ*a4Â’hÁ…z$«0a\ÝËŸ15@€\Ëü\0²Œ®Á?@ö\Ô!:ý9‚\è\è\ç)5£qò\æ\Üž0‚wy¼dö¿;^£\r\Í\ãoø\Zø§Wˆµ\Ú\Z\ï3ö¿\ãœ\\¡\àø•ø û|c{€QÄ¹ð}˜e\×Ô˜a0\Ç\Ô\á®\0o9;\Ç©}»$ó„±\Þ\'‘–\Óoi\ã—\è)\ß\Ä@¦óOºDýŒ\×!²µ\"5‹ZRôŠ\ÊÉ†Š\ÊÀ\ì$\å”fA‚^9Kª\Ñ<¾b„\Æwù\n\Ø\Ã÷ù¶Û¼MkAKºBÂ¿CSºŒ€˜¢flÖ}8\Åð1¦o¾7œ~Q>6«fC±Œ?\n\ØÃŸ\r…˜´ø9˜Wbqªˆ)¼½þŒ\Õ?\ç$\Év=„n\îšùn\Ö\0\Ót¹\È2\ì‡\Å,Ð„Àx\0C”ŸúpN4£\ì¡£†²-–Ð¾¹²QÝ£KA¿\ÎA\æƒ@U#\íP0@°jG\Õ\ÖDFD\áþ¢ð¤–S\Ö°CPFgjˆˆ:-B\ä‡	ˆzµ$µ´\Ü\ÂX\ßkr\Í%L b{5a\Ã\\a\Ô|¤A\é\Ó\Ð\ÔkY\\·!\Z¼VÓ˜÷¹°Í¸+ñ‰\Ød\ïl°Kî¿½‰avkl\ÆÙ­ŒA½}(?«\Ø\Z€|p94•NL\å.\ÕNT\Ô\ØTTÉ»3\Ðòˆj;þ\Òyõ\Ð\ÌS<(\ï~[\ïT\×lS\ÐÇ™f\é{\Ò6„¶€©jž—KV	_ˆ\æpF\å\äç³Œ»º²‰0ð$bÈ¦ñwµ~¨\×\r\"Q`ch= ü:PR&\Ô\0\áªX^§tÜ‹\0[\Å\Ý:aù\Ú/Á¶l@\Ån_‹¶Í—§²qZ>\êž\ÕÖ ¹\Õa¡…£1yñ;n¡S\\VUŒ/<\ÄnuŒF‡‚z<Wƒ’ªÎŒ®¥\Ê4ûµ¤sÈ†¸d[iIrŸZª:3º–¸ö+I\ãp¶R‘¸…4ÙªHG½\Û\ÔuS¯Ì˜\âSÏZ5½I¢u+ÕŠ—8žgõ\ÃbxòQ\\bx~¦\ÉAª¥­9œ‚5”j)k*\éu˜f\ä°,\Î3b…L»·\Z–ÿŠe{ûT±\Ú*jö›\Çxõ—ø\Â~«:$\çšö2f^MJ\×Ø€¾¹\Ã\Ò\ß@RMô~Ž£<Ff\'\ËÜº¼\Ãk·/KT„©\'É¯8QŠ\ÆWWT¿\Õ\à¨cÄªý˜\Í\ËaRyå…¶•nòL\Í(U ªb\n^\ímðL\Í\à“ý\Å\á\ãÕ‹ð6ó‹\'©´x\Ñ@ŒVžƒÖª³GSQÚ˜b=¢”oÒ†”ªH\Ù\Î*„lWl„gÐ¨žÂžƒšG\ÒFWk\í‘5%mhMõ\Ø\Z™\å:{TM\ÒIXSm\Ýd \È\é\ï`\ÆS\ÌV[Xy\Ø\Ýn3`¼Íª8\ÎØº\ÓoµŠbñ[{Œ—¤EO|[YT\ç\ØÎ¢\æH¸ \Îk|3¦p\Í-,ò]\×üf¼avû¦Ö¡úd’š{}ø“yS~\à²xd#ŸÀJ×©\Ô8so_¿FV?)~Î£²õ¼¢¸(\\ÁŒ”¹\î\'öbHx£s8\ïe¼,\"Í\ÕôhF³di¡gúO Us&¶xSÒ€*\á\èÀ—™û\ï¢\Õy\Ù`¿Š\â#\ç&ûŠ\Â_sZñ˜\æ\Ðù]\Í\'\Ç\Þ\âUG-\è\ï\ïâ¹„½\Êoþõ­lz\äÜ§t6;Ç’¢7~ñ\Å iÊ¦[H³ñÓŠ÷;Û„÷\nZTi¶lþ<a‰q4\ÊÛ„SÉŠKªabiŸl\n¦yY0\Ô8J3½\Z\Ø\Ìød  Ÿ¤x2ðB?,_\Ý\í¼þAÁF’\Z_„ˆlý–À~%ªZ\îq+Òœ›v±*z\î\Í\Å\Þ*1s\ßÛ“’²½\é¤W3²\íFM¸\Þ\Î7yg‰Ì£í™š<\åÑ°÷i\íožœ|(ùÈ·¾\ß4\ä]fw\Ü*ý¡Ž EN“ò³ÿ´\â]Ûš)ü{à¹™Ã’‡\Ì\Øø6¿ÿ\á]›)2|\à\Æ6(øÀlm_û\çž-\Íz\Ý{Z¯š¡d¸\ÆÑ…û\Òv\ËXû\Ì\r–˜\ZA\éQ–¯-õyb]9®=3Ss‚š\ÌX™8\n_…¢›\í°¾ò\r¿³³œ¦›­!­³‹7_ÿ;ysšnÞ†d\É}$k\ÓuI\à=\ëXW\Õ{J0zÒ“\Ï\Þ\ç³v\ÞÉ¿§|\âQ”\"\ÌÃµòûIE%cN\é\Â\ê\r1\Ý;[¬‘\î\ßY¸n ØŸnD\ÐvÍš†¥T›·$QE\"Ehn!\ÝR/R®€Oh5;\ÏÅ‹x»ÿX\Â\à\Ý\ç$\É	\í2Œ—‘ðbN@ÿ\"\'Z”yzŸ°¯lŒ.P1C½¿G?\ça\Ôr_kbB\æ]ð /KÂ‚½\ë\×\Z\é#K ®¾\Ú)z„qQ°\ì-À3\ÜD6j~Ÿ\á\Zø¯M\Ð\Ò?¢Ú§—!X§ \Î8FÓž~R\â—ÿð¸Ù³³T\0\0','6.2.0-61023'),('202006041608196_ChangedSlika','CityInformation.Migrations.Configuration',_binary '‹\0\0\0\0\0\0\Ý\\[o\ã¸~/\Ðÿ \è±\ÈZ¹t\ÓÀ\ÞE\ÖIÚ “Æ™E\ß´D;\ÂH¤V¢²	Šýe}\èO\ê_()Q\Þ$\ÊVlg1ÀÀ\"¿sxxH\æÿù\ïô§—8ržaš…\ÍÜ“É±\ë@\ä\ã Dë™›“\ÕŸÜŸ~üóŸ¦WAü\âüRÑ1:\Úe3÷‰\ä\Üó2ÿ	\Æ ›Ä¡Ÿ\â¯\È\ÄÇ±\ìÿ\Í;9ñ …p)–\ãL¿äˆ„1,>\è\ç#&$\Ñ-`”ñrZ³(P;\Ã,>œ¹ó¼Þ Nc@¨,“²…\ë\\D! \Ò,`´r€&Eýù\×.HŠ\Ñz‘\Ð=¾&Ò­@”AÞ‡ó†Ü¶;Ç§¬;^Ó°‚òóŒ\àx \à\É×\'7\ßH\Ën­?ªÁ+ªiò\Êz]hq\æ\Þ°(ú‚#ª\0™\áù<JñÌ½­Y\\d\É$“ªá¤„¼N)\Üo8ý>i#9\Ö\íŽj{:³G\Î<Hž\Â‚9IAt\ä<\ä\Ë(ôÿ	_ñwˆfg\'\Ë\ÕÙ§Apöñ¯ð\ìC»§´¯”N( E)N`Jeƒ«ºÿ®\ã‰\í<¹aÝ¬Õ¦\Ô\nµ%:5\\\ç¼|†hMž\è¤9ý\ä:\×\áªn\\_QHgmDÒœ~\Þ\åQ–¬\ë½Nž\ìÿ®§>Ž\Âõ<‡\ëb\è%þt\â¤t^}QQ›=…I9½„ñþ\ÆÉ®S³oÑ¾\Ê\Úoœ§>\ë6’<‚t\r‰(\Ý\ÔkŒ\×Ê¤\Ôøf]¡¾i3IUóÖ’²m2*»ž\r•¼o\Ë\×\Ú\â.’„^aZL#]§ß°&5‘®1¡[B´k\äñ*a4Â’hÁ…z$«0a\ÝËŸ15@€\Ëü\0²Œ®Á?@ö\Ô!:ý9‚\è\è\ç)5£qò\æ\Üž0‚wy¼dö¿;^£\r\Í\ãoø\Zø§Wˆµ\Ú\Z\ï3ö¿\ãœ\\¡\àø•ø û|c{€QÄ¹ð}˜e\×Ô˜a0\Ç\Ô\á®\0o9;\Ç©}»$ó„±\Þ\'‘–\Óoi\ã—\è)\ß\Ä@¦óOºDýŒ\×!²µ\"5‹ZRôŠ\ÊÉ†Š\ÊÀ\ì$\å”fA‚^9Kª\Ñ<¾b„\Æwù\n\Ø\Ã÷ù¶Û¼MkAKºBÂ¿CSºŒ€˜¢flÖ}8\Åð1¦o¾7œ~Q>6«fC±Œ?\n\ØÃŸ\r…˜´ø9˜Wbqªˆ)¼½þŒ\Õ?\ç$\Év=„n\îšùn\Ö\0\Ót¹\È2\ì‡\Å,Ð„Àx\0C”ŸúpN4£\ì¡£†²-–Ð¾¹²QÝ£KA¿\ÎA\æƒ@U#\íP0@°jG\Õ\ÖDFD\áþ¢ð¤–S\Ö°CPFgjˆˆ:-B\ä‡	ˆzµ$µ´\Ü\ÂX\ßkr\Í%L b{5a\Ã\\a\Ô|¤A\é\Ó\Ð\ÔkY\\·!\Z¼VÓ˜÷¹°Í¸+ñ‰\Ød\ïl°Kî¿½‰avkl\ÆÙ­ŒA½}(?«\Ø\Z€|p94•NL\å.\ÕNT\Ô\ØTTÉ»3\Ðòˆj;þ\Òyõ\Ð\ÌS<(\ï~[\ïT\×lS\ÐÇ™f\é{\Ò6„¶€©jž—KV	_ˆ\æpF\å\äç³Œ»º²‰0ð$bÈ¦ñwµ~¨\×\r\"Q`ch= ü:PR&\Ô\0\áªX^§tÜ‹\0[\Å\Ý:aù\Ú/Á¶l@\Ån_‹¶Í—§²qZ>\êž\ÕÖ ¹\Õa¡…£1yñ;n¡S\\VUŒ/<\ÄnuŒF‡‚z<Wƒ’ªÎŒ®¥\Ê4ûµ¤sÈ†¸d[iIrŸZª:3º–¸ö+I\ãp¶R‘¸…4ÙªHG½\Û\ÔuS¯Ì˜\âSÏZ5½I¢u+ÕŠ—8žgõ\ÃbxòQ\\bx~¦\ÉAª¥­9œ‚5”j)k*\éu˜f\ä°,\Î3b…L»·\Z–ÿŠe{ûT±\Ú*jö›\Çxõ—ø\Â~«:$\çšö2f^MJ\×Ø€¾¹\Ã\Ò\ß@RMô~Ž£<Ff\'\ËÜº¼\Ãk·/KT„©\'É¯8QŠ\ÆWWT¿\Õ\à¨cÄªý˜\Í\ËaRyå…¶•nòL\Í(U ªb\n^\ímðL\Í\à“ý\Å\á\ãÕ‹ð6ó‹\'©´x\Ñ@ŒVžƒÖª³GSQÚ˜b=¢”oÒ†”ªH\Ù\Î*„lWl„gÐ¨žÂžƒšG\ÒFWk\í‘5%mhMõ\Ø\Z™\å:{TM\ÒIXSm\Ýd \È\é\ï`\ÆS\ÌV[Xy\Ø\Ýn3`¼Íª8\ÎØº\ÓoµŠbñ[{Œ—¤EO|[YT\ç\ØÎ¢\æH¸ \Îk|3¦p\Í-,ò]\×üf¼avû¦Ö¡úd’š{}ø“yS~\à²xd#ŸÀJ×©\Ô8so_¿FV?)~Î£²õ¼¢¸(\\ÁŒ”¹\î\'öbHx£s8\ïe¼,\"Í\ÕôhF³di¡gúO Us&¶xSÒ€*\á\èÀ—™û\ï¢\Õy\Ù`¿Š\â#\ç&ûŠ\Â_sZñ˜\æ\Ðù]\Í\'\Ç\Þ\âUG-\è\ï\ïâ¹„½\Êoþõ­lz\äÜ§t6;Ç’¢7~ñ\Å iÊ¦[H³ñÓŠ÷;Û„÷\nZTi¶lþ<a‰q4\ÊÛ„SÉŠKªabiŸl\n¦yY0\Ô8J3½\Z\Ø\Ìød  Ÿ¤x2ðB?,_\Ý\í¼þAÁF’\Z_„ˆlý–À~%ªZ\îq+Òœ›v±*z\î\Í\Å\Þ*1s\ßÛ“’²½\é¤W3²\íFM¸\Þ\Î7yg‰Ì£í™š<\åÑ°÷i\íožœ|(ùÈ·¾\ß4\ä]fw\Ü*ý¡Ž EN“ò³ÿ´\â]Ûš)ü{à¹™Ã’‡\Ì\Øø6¿ÿ\á]›)2|\à\Æ6(øÀlm_û\çž-\Íz\Ý{Z¯š¡d¸\ÆÑ…û\Òv\ËXû\Ì\r–˜\ZA\éQ–¯-õyb]9®=3Ss‚š\ÌX™8\n_…¢›\í°¾ò\r¿³³œ¦›­!­³‹7_ÿ;ysšnÞ†d\É}$k\ÓuI\à=\ëXW\Õ{J0zÒ“\Ï\Þ\ç³v\ÞÉ¿§|\âQ”\"\ÌÃµòûIE%cN\é\Â\ê\r1\Ý;[¬‘\î\ßY¸n ØŸnD\ÐvÍš†¥T›·$QE\"Ehn!\ÝR/R®€Oh5;\ÏÅ‹x»ÿX\Â\à\Ý\ç$\É	\í2Œ—‘ðbN@ÿ\"\'Z”yzŸ°¯lŒ.P1C½¿G?\ça\Ôr_kbB\æ]ð /KÂ‚½\ë\×\Z\é#K ®¾\Ú)z„qQ°\ì-À3\ÜD6j~Ÿ\á\Zø¯M\Ð\Ò?¢Ú§—!X§ \Î8FÓž~R\â—ÿð¸Ù³³T\0\0','6.2.0-61023');
/*!40000 ALTER TABLE `__migrationhistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `administrator`
--

DROP TABLE IF EXISTS `administrator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `administrator` (
  `idAdministratora` varchar(128) NOT NULL,
  `imeAdministratora` varchar(128) NOT NULL,
  PRIMARY KEY (`idAdministratora`),
  UNIQUE KEY `imeAdministratora_UNIQUE` (`imeAdministratora`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
INSERT INTO `administrator` VALUES ('f3f63f10-5751-4cde-9ee1-df52cc05aa42','Admin1'),('993b1465-f2a9-492c-a175-775c7494650d','Admin2');
/*!40000 ALTER TABLE `administrator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
INSERT INTO `aspnetroles` VALUES ('administrator','Administrator'),('korisnik','Korisnik'),('poduzeÄ‡e','PoduzeÄ‡e');
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserclaims`
--

LOCK TABLES `aspnetuserclaims` WRITE;
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ProviderKey` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `UserId` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `IX_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserlogins`
--

LOCK TABLES `aspnetuserlogins` WRITE;
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `RoleId` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_UserId` (`UserId`),
  KEY `IX_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserroles`
--

LOCK TABLES `aspnetuserroles` WRITE;
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
INSERT INTO `aspnetuserroles` VALUES ('13464ef6-ed0b-42aa-b8e1-ee5752c25bfc','korisnik'),('43e1f5b0-87b3-4abf-8533-e8f39277fdcc','korisnik'),('4f2b39b4-27f3-4a98-b6a1-c8d232fefd7f','poduzeÄ‡e'),('5805ffa0-cd7c-4464-bd8f-a707cc8051f9','poduzeÄ‡e'),('6b935eea-27e7-458a-838e-31cfee92fd00','poduzeÄ‡e'),('846a2a99-2a11-47f8-8334-a7cfe57d5464','poduzeÄ‡e'),('940d7f93-0bfb-4885-a7d6-f9e772c6db87','korisnik'),('993b1465-f2a9-492c-a175-775c7494650d','administrator'),('a5c2a14d-ba9e-4019-b6db-069cb1435e60','poduzeÄ‡e'),('d407a8ba-96bf-4569-afaa-e376161fa906','poduzeÄ‡e'),('d55d4d54-a884-4a87-adb2-d5cfc4589130','poduzeÄ‡e'),('d78552fd-70a7-465c-82ae-65acd3ac2b1f','korisnik'),('e671796c-81d7-4f26-b672-413f7796fef1','korisnik'),('f3f63f10-5751-4cde-9ee1-df52cc05aa42','administrator');
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusers` (
  `Id` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Email` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusers`
--

LOCK TABLES `aspnetusers` WRITE;
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` VALUES ('13464ef6-ed0b-42aa-b8e1-ee5752c25bfc','ivan.ivic@mail.com',0,'AKhQNWNKmpM2q8d4OFnPB12roQnNV3qV/WIAgojtkpHcqk35r/h3nw7GJoVTfjd+iA==','cfd93868-9945-452a-81af-f2305bef732e',NULL,0,0,NULL,1,0,'ivan1'),('43e1f5b0-87b3-4abf-8533-e8f39277fdcc','mario.maric@mail.com',0,'APBK/UvaTZnA9KEaDovbCGJUqQtXgbK+uhCJQx9fUM/6gqvbnbvzpNgv55hPxOOtSg==','33773200-b86b-41f4-a566-3d3c1daed0f1',NULL,0,0,NULL,1,0,'mario1'),('4f2b39b4-27f3-4a98-b6a1-c8d232fefd7f','kiosk1@cityinfo.com',0,'ACXH6x1QYZOY4PD4Uubj6XWqIMRAEnmrAdcL0uFtYhYtHy9PH60KAv3w8yULGEhPzg==','f5c428af-60a4-4b5c-831a-4710a37d2b94',NULL,0,0,NULL,1,0,'kiosk1'),('5805ffa0-cd7c-4464-bd8f-a707cc8051f9','gym1@cityinfo.com',0,'AF3iRh+vPD5vx/1nXNN249bIgEjtL1XIPe0ByLa7eLjTROVdTZrHV7MKiBLAiB0bBA==','1d11f193-2326-473e-91b9-6b4aa03e1fc9',NULL,0,0,NULL,1,0,'gym1'),('6b935eea-27e7-458a-838e-31cfee92fd00','benzinska1@cityinfo.com',0,'AIoMpuPNWGMgeEnYEHqR32CIVy+Luhqf6rbo3TV0EYOfXL5HhvKcZv63pf9zafti0Q==','4acef6d3-e876-4834-80b3-3f0d2844432f',NULL,0,0,NULL,1,0,'benzinska1'),('846a2a99-2a11-47f8-8334-a7cfe57d5464','Smile1@cityinfo.com',0,'AM9+0t6jLNkaE/iPnO3V6BS6i7jclLhLF+D3jog9P9TcV8/jNzmdyMqjLBkU5hrDjQ==','695147a0-668d-485a-99ed-9c779193dceb',NULL,0,0,NULL,1,0,'Smile1'),('940d7f93-0bfb-4885-a7d6-f9e772c6db87','janko.jankovic@mail.com',0,'AAEsIFO2W//78eW57bljPYZ4x1NpQpRujki7Z3I/0rFBl/0XzjOzTslKRyG9Th6boA==','4a443424-1435-47ad-9ce2-28c401353ac7',NULL,0,0,NULL,1,0,'janko1'),('993b1465-f2a9-492c-a175-775c7494650d','Admin2@cityinfo.com',0,'ALE3UX5mk56M7jZ9ocPZ0AbYYKpiTqFs0mFjF8rrIFo2eEK2ukmKid0y/ZXBJ+DMHQ==','e7490e64-9480-4f90-b54b-e15ef271301a',NULL,0,0,NULL,1,0,'Admin2'),('a5c2a14d-ba9e-4019-b6db-069cb1435e60','kafic1@cityinfo.com',0,'AFJgE+GtiUGYsTlOSstowPXPvyIdKz/qTrpPu/iZ2DHQ4aqhfXbvNyF2IH9dDDSJ4g==','783403fa-a8f6-4958-9c80-d2f7ee1ebebd',NULL,0,0,NULL,1,0,'kafic1'),('d407a8ba-96bf-4569-afaa-e376161fa906','ffhrenovka@cityinfo.com',0,'AMSG+h49fl+nVXcTwGpRYUr/JxjHpIOo1MYmSHFJs0V8hKdm28E1+mydvTM8x/RXHw==','8368d7ed-e564-4e74-9279-449567464665',NULL,0,0,NULL,1,0,'ffhrenovka'),('d55d4d54-a884-4a87-adb2-d5cfc4589130','mjenjacnica1@cityinfo.com',0,'ACVlpUuGF0JZKCq32xFW89qUgQikgr1R6D4HKcSwodB5hl5pttV/T0Y/OtnlheKxvg==','c1e133d0-3c87-48ea-a425-558d963f88b0',NULL,0,0,NULL,1,0,'mjenjacnica1'),('d78552fd-70a7-465c-82ae-65acd3ac2b1f','hrvoje.horvat@mail.com',0,'ALHy+LvfimtdE2afyqoO2eV0QfjDo66Uci7yNz/C1QqdxtfBJTc+oIpMRQLm8891iQ==','b8ec9a19-8910-4211-9fc7-f9f1e2c173ba',NULL,0,0,NULL,1,0,'hrvoje1'),('e671796c-81d7-4f26-b672-413f7796fef1','zeljko.zeljkic@gmail.com',0,'AOP5F1vYahJ6XeX4WbpCr3xBC64SPUXB0xEafkHT/bHjberB7OdhWWALkebhbELDJg==','64cde801-023e-45c5-8b60-c5cc2dec5f24',NULL,0,0,NULL,1,0,'zeljko1'),('f3f63f10-5751-4cde-9ee1-df52cc05aa42','Admin1@cityinfo.com',0,'AC5vII8oizBWsnApqMaXjWiUxk/7I+ouQLQNEQ5bNcDcR4M1Vh6MLbVbWtVYVjqF0w==','9670047d-2e90-491b-b9fd-6f9e224e4533',NULL,0,0,NULL,1,0,'Admin1');
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `djelatnost`
--

DROP TABLE IF EXISTS `djelatnost`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `djelatnost` (
  `idDjelatnost` int NOT NULL,
  `imeDjelatnost` varchar(100) NOT NULL,
  PRIMARY KEY (`idDjelatnost`),
  UNIQUE KEY `imeDjelatnost_UNIQUE` (`imeDjelatnost`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `djelatnost`
--

LOCK TABLES `djelatnost` WRITE;
/*!40000 ALTER TABLE `djelatnost` DISABLE KEYS */;
INSERT INTO `djelatnost` VALUES (6,'Benzinska postaja'),(2,'Fast food'),(1,'KafiÄ‡'),(4,'Kiosk'),(3,'MjenjaÄnica'),(5,'Teretana');
/*!40000 ALTER TABLE `djelatnost` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `grad`
--

DROP TABLE IF EXISTS `grad`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `grad` (
  `imeGrad` varchar(128) NOT NULL,
  `idGrad` int NOT NULL,
  `idÅ½upanija` int NOT NULL,
  PRIMARY KEY (`idGrad`),
  KEY `idÅ½upanija_FK_idx` (`idÅ½upanija`),
  CONSTRAINT `idÅ½upanija_FK` FOREIGN KEY (`idÅ½upanija`) REFERENCES `Å¾upanija` (`idÅ½upanija`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `grad`
--

LOCK TABLES `grad` WRITE;
/*!40000 ALTER TABLE `grad` DISABLE KEYS */;
INSERT INTO `grad` VALUES ('Bjelovar',1,1),('ÄŒakovec',2,3),('Osijek',3,4),('Split',4,6),('Zadar',5,5),('VaraÅ¾din',6,2),('Zagreb',7,7),('Daruvar',8,1),('Virovitica',9,10),('Rijeka',10,9),('Sisak',11,8),('Dubrovnik',12,11),('Novska',13,8),('Prelog',14,3),('Novi Marof',15,2);
/*!40000 ALTER TABLE `grad` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `korisnik`
--

DROP TABLE IF EXISTS `korisnik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `korisnik` (
  `idKorisnik` varchar(128) NOT NULL,
  `registracijskiEmailKorisnik` varchar(128) NOT NULL,
  `putanjaDoProfilneSlike` varchar(200) DEFAULT NULL,
  `korisniÄkoImeKorisnik` varchar(128) NOT NULL,
  PRIMARY KEY (`idKorisnik`),
  UNIQUE KEY `registracijskiEmailKorisnik_UNIQUE` (`registracijskiEmailKorisnik`),
  UNIQUE KEY `korisniÄkoImeKorisnik_UNIQUE` (`korisniÄkoImeKorisnik`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `korisnik`
--

LOCK TABLES `korisnik` WRITE;
/*!40000 ALTER TABLE `korisnik` DISABLE KEYS */;
INSERT INTO `korisnik` VALUES ('13464ef6-ed0b-42aa-b8e1-ee5752c25bfc','ivan.ivic@mail.com','','ivan1'),('43e1f5b0-87b3-4abf-8533-e8f39277fdcc','mario.maric@mail.com',NULL,'mario1'),('940d7f93-0bfb-4885-a7d6-f9e772c6db87','janko.jankovic@mail.com','~/Images/dependent-on-technology-wallpaper-for-1920x1080-hdtv-1080p-1390-15205206414.jpg','janko1'),('d78552fd-70a7-465c-82ae-65acd3ac2b1f','hrvoje.horvat@mail.com',NULL,'hrvoje1'),('e671796c-81d7-4f26-b672-413f7796fef1','zeljko.zeljkic@gmail.com',NULL,'zeljko1');
/*!40000 ALTER TABLE `korisnik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `poduzeÄ‡e`
--

DROP TABLE IF EXISTS `poduzeÄ‡e`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `poduzeÄ‡e` (
  `idPoduzeÄ‡e` varchar(128) NOT NULL,
  `imePoduzeÄ‡e` varchar(128) NOT NULL,
  `opisPoduzeÄ‡e` varchar(5000) DEFAULT NULL,
  `kontaktTelefon` varchar(200) DEFAULT NULL,
  `kontaktEmail` varchar(200) DEFAULT NULL,
  `tipPoslovnogObjekta` int DEFAULT NULL,
  `gradPoduzeÄ‡e` int DEFAULT NULL,
  `Å¾upanijaPoduzeÄ‡e` int DEFAULT NULL,
  `ulicaPoduzeÄ‡e` int DEFAULT NULL,
  `korisniÄkoImePoduzeÄ‡e` varchar(128) NOT NULL,
  `javanPoduzeÄ‡e` tinyint(1) NOT NULL,
  PRIMARY KEY (`idPoduzeÄ‡e`),
  UNIQUE KEY `korisniÄkoImePoduzeÄ‡e_UNIQUE` (`korisniÄkoImePoduzeÄ‡e`),
  KEY `tipPoslovnogObjekta_FK_idx` (`tipPoslovnogObjekta`),
  KEY `gradPoduzeÄ‡e_idx` (`gradPoduzeÄ‡e`),
  KEY `Å¾upanijaPoduzeÄ‡e_FK_idx` (`Å¾upanijaPoduzeÄ‡e`),
  KEY `ulicaPoduzeÄ‡e_FK_idx` (`ulicaPoduzeÄ‡e`),
  CONSTRAINT `gradPoduzeÄ‡e_FK` FOREIGN KEY (`gradPoduzeÄ‡e`) REFERENCES `grad` (`idGrad`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `tipPoslovnogObjekta_FK` FOREIGN KEY (`tipPoslovnogObjekta`) REFERENCES `djelatnost` (`idDjelatnost`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ulicaPoduzeÄ‡e_FK` FOREIGN KEY (`ulicaPoduzeÄ‡e`) REFERENCES `ulica` (`idUlica`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Å¾upanijaPoduzeÄ‡e_FK` FOREIGN KEY (`Å¾upanijaPoduzeÄ‡e`) REFERENCES `Å¾upanija` (`idÅ½upanija`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `poduzeÄ‡e`
--

LOCK TABLES `poduzeÄ‡e` WRITE;
/*!40000 ALTER TABLE `poduzeÄ‡e` DISABLE KEYS */;
INSERT INTO `poduzeÄ‡e` VALUES ('4f2b39b4-27f3-4a98-b6a1-c8d232fefd7f','Stare Novine','Kiosk s najboljim cijenama','0542374823','kisok1@mail.com',4,6,2,3,'kiosk1',1),('5805ffa0-cd7c-4464-bd8f-a707cc8051f9','Teretana Budi Jak','Ako Å¾eliÅ¡ doÄ‡i do svoje najbolje forme, ovo je mjesto gdje Ä‡eÅ¡ to ostvariti.','04723847829','gym1@mail.com',5,10,9,12,'gym1',1),('6b935eea-27e7-458a-838e-31cfee92fd00','Gas Place','Benzinska postaja s najniÅ¾im cijenama','0423742374','benzinska1@mail.com',6,9,10,13,'benzinska1',1),('846a2a99-2a11-47f8-8334-a7cfe57d5464','KafiÄ‡ Smile','KafiÄ‡ Äija Ä‡e vam se atmosfera svidjeti i uÄiniti vas sretnim.','098765432',NULL,1,5,5,5,'Smile1',1),('a5c2a14d-ba9e-4019-b6db-069cb1435e60','KafiÄ‡','KafiÄ‡ s lijepim pogledom na Å¡etnicu.','01323472','kafic1@mail.com',1,1,1,1,'kafic1',1),('d407a8ba-96bf-4569-afaa-e376161fa906','Fast Food Hrenovka','Najbolji hot dogovi u gradu.','05678932','ffhrenovka@mail.com',2,4,6,4,'ffhrenovka',1),('d55d4d54-a884-4a87-adb2-d5cfc4589130','MjenjaÄnica Jug','MjenjaÄnica sa najmanjom provizijom.','027378235','mjenjacnica1@mail.com',3,7,7,6,'mjenjacnica1',1);
/*!40000 ALTER TABLE `poduzeÄ‡e` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recenzija`
--

DROP TABLE IF EXISTS `recenzija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recenzija` (
  `idRecenzija` int NOT NULL,
  `tekstRecenzija` varchar(500) NOT NULL,
  `vlasnikRecenzijaID` varchar(128) NOT NULL,
  `odobrenoRecenzija` tinyint(1) NOT NULL,
  `datumRecenzija` varchar(100) NOT NULL,
  `poduzeÄ‡eRecenzijaID` varchar(128) NOT NULL,
  PRIMARY KEY (`idRecenzija`),
  KEY `vlasnikRecenzija_FK_idx` (`vlasnikRecenzijaID`),
  KEY `poduzeÄ‡eRecenzijaID_FK_idx` (`poduzeÄ‡eRecenzijaID`),
  CONSTRAINT `poduzeÄ‡eRecenzijaID_FK` FOREIGN KEY (`poduzeÄ‡eRecenzijaID`) REFERENCES `poduzeÄ‡e` (`idPoduzeÄ‡e`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `vlasnikRecenzijaID_FK` FOREIGN KEY (`vlasnikRecenzijaID`) REFERENCES `korisnik` (`idKorisnik`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recenzija`
--

LOCK TABLES `recenzija` WRITE;
/*!40000 ALTER TABLE `recenzija` DISABLE KEYS */;
INSERT INTO `recenzija` VALUES (1,'Jako dobar kafiÄ‡, zadovoljni sam :)','13464ef6-ed0b-42aa-b8e1-ee5752c25bfc',1,'21-Jun-20','a5c2a14d-ba9e-4019-b6db-069cb1435e60'),(2,'Zaista dobar Fast food sa prihatljivim cijenama I dobrom klopom.','940d7f93-0bfb-4885-a7d6-f9e772c6db87',1,'21-Jun-20','d407a8ba-96bf-4569-afaa-e376161fa906'),(3,'Super osoblje, kvalitetno gorivo i najniÅ¾e cijene u gradu.','940d7f93-0bfb-4885-a7d6-f9e772c6db87',1,'21-Jun-20','6b935eea-27e7-458a-838e-31cfee92fd00');
/*!40000 ALTER TABLE `recenzija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `slika`
--

DROP TABLE IF EXISTS `slika`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `slika` (
  `idSlika` varchar(200) NOT NULL,
  `putanjaSlike` varchar(200) NOT NULL,
  `idPoduzeÄ‡a` varchar(128) NOT NULL,
  PRIMARY KEY (`idSlika`),
  KEY `idPoduzeÄ‡a_FK_idx` (`idPoduzeÄ‡a`),
  CONSTRAINT `idPoduzeÄ‡e_FK` FOREIGN KEY (`idPoduzeÄ‡a`) REFERENCES `poduzeÄ‡e` (`idPoduzeÄ‡e`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `slika`
--

LOCK TABLES `slika` WRITE;
/*!40000 ALTER TABLE `slika` DISABLE KEYS */;
INSERT INTO `slika` VALUES ('21304807051664952','~/Images/image1195704082213048069.jpg','4f2b39b4-27f3-4a98-b6a1-c8d232fefd7f');
/*!40000 ALTER TABLE `slika` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ulica`
--

DROP TABLE IF EXISTS `ulica`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ulica` (
  `idUlica` int NOT NULL,
  `imeUlica` varchar(256) NOT NULL,
  `idGrad` int NOT NULL,
  PRIMARY KEY (`idUlica`),
  KEY `idGrad_FK_idx` (`idGrad`),
  CONSTRAINT `idGrad_FK` FOREIGN KEY (`idGrad`) REFERENCES `grad` (`idGrad`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ulica`
--

LOCK TABLES `ulica` WRITE;
/*!40000 ALTER TABLE `ulica` DISABLE KEYS */;
INSERT INTO `ulica` VALUES (1,'Bjelovarska',1),(2,'JelaÄiÄ‡eva',2),(3,'Teslina',6),(4,'Gajeva',4),(5,'RadiÄ‡eva',5),(6,'Ilica',7),(7,'Slavonska ulica',3),(8,'Zelena ulica',8),(9,'MoslavaÄka ulica',11),(10,'Starogradska ulica',12),(11,'Borova ulica',13),(12,'RijeÄka',10),(13,'VirovitiÄka',9);
/*!40000 ALTER TABLE `ulica` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Å¾upanija`
--

DROP TABLE IF EXISTS `Å¾upanija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Å¾upanija` (
  `idÅ½upanija` int NOT NULL,
  `imeÅ½upanija` varchar(128) NOT NULL,
  PRIMARY KEY (`idÅ½upanija`),
  UNIQUE KEY `idÅ½upanija_UNIQUE` (`idÅ½upanija`),
  UNIQUE KEY `imeÅ½upanija_UNIQUE` (`imeÅ½upanija`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Å¾upanija`
--

LOCK TABLES `Å¾upanija` WRITE;
/*!40000 ALTER TABLE `Å¾upanija` DISABLE KEYS */;
INSERT INTO `Å¾upanija` VALUES (1,'Bjelovarsko-bilogorska'),(11,'DubrovaÄko-neretvanska'),(7,'Grad Zagreb'),(3,'MeÄ‘imurska'),(4,'OsjeÄko-baranjska'),(9,'Primorsko-goranska'),(8,'SisaÄko-MoslavaÄka'),(6,'Splitsko-dalmatinska'),(2,'VaraÅ¾dinska'),(10,'VirovitiÄko-podravska'),(5,'Zadarska');
/*!40000 ALTER TABLE `Å¾upanija` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-03-18 22:56:44
