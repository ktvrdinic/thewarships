-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 06, 2019 at 04:52 PM
-- Server version: 10.1.38-MariaDB
-- PHP Version: 7.3.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `thewarships`
--

-- --------------------------------------------------------

--
-- Table structure for table `bitka`
--

CREATE TABLE `bitka` (
  `ID` int(11) NOT NULL,
  `username1` varchar(40) COLLATE utf8_bin NOT NULL,
  `username2` varchar(40) COLLATE utf8_bin NOT NULL,
  `pobjednik` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Table structure for table `ships`
--

CREATE TABLE `ships` (
  `ID` int(11) NOT NULL,
  `name` varchar(40) COLLATE utf8_bin NOT NULL,
  `broj_topova` int(11) NOT NULL,
  `broj_jedra` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dumping data for table `ships`
--

INSERT INTO `ships` (`ID`, `name`, `broj_topova`, `broj_jedra`) VALUES
(1, 'Brigadier', 10, 5),
(2, 'Ship of the Line', 60, 10);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `username` varchar(40) COLLATE utf8_bin NOT NULL,
  `email` varchar(60) COLLATE utf8_bin NOT NULL,
  `lozinka` varchar(60) COLLATE utf8_bin NOT NULL,
  `broj_brodova` int(11) NOT NULL DEFAULT '1',
  `online` tinyint(1) NOT NULL DEFAULT '0',
  `zlato` int(11) NOT NULL DEFAULT '100',
  `rum` int(11) NOT NULL DEFAULT '100',
  `drvo` int(11) NOT NULL DEFAULT '100',
  `biseri` int(11) NOT NULL DEFAULT '50',
  `score` int(11) NOT NULL DEFAULT '0',
  `level` int(11) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`username`, `email`, `lozinka`, `broj_brodova`, `online`, `zlato`, `rum`, `drvo`, `biseri`, `score`, `level`) VALUES
('ktvrdinic', 'karlojerry@gmail.com', 'otocacoto', 1, 0, 100, 100, 100, 50, 0, 1),
('mtvrdinic', 'marin@gmail.com', 'otocacoto', 1, 0, 152, 98, 20, 50, 10, 1);

-- --------------------------------------------------------

--
-- Table structure for table `user_ship`
--

CREATE TABLE `user_ship` (
  `ID` int(11) NOT NULL,
  `username` varchar(40) COLLATE utf8_bin NOT NULL,
  `SID` int(11) NOT NULL,
  `snaga_topa` int(11) NOT NULL,
  `broj_jedra` int(11) NOT NULL,
  `brzina_broda` int(11) NOT NULL,
  `reloadTime` int(11) NOT NULL,
  `brojBombi` int(11) NOT NULL,
  `ostecenje` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bitka`
--
ALTER TABLE `bitka`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_bitka1` (`username1`),
  ADD KEY `FK_bitka2` (`username2`);

--
-- Indexes for table `ships`
--
ALTER TABLE `ships`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`username`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indexes for table `user_ship`
--
ALTER TABLE `user_ship`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_userShip` (`username`),
  ADD KEY `FK_shipUser` (`SID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bitka`
--
ALTER TABLE `bitka`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `ships`
--
ALTER TABLE `ships`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `user_ship`
--
ALTER TABLE `user_ship`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `bitka`
--
ALTER TABLE `bitka`
  ADD CONSTRAINT `FK_bitka1` FOREIGN KEY (`username1`) REFERENCES `users` (`username`),
  ADD CONSTRAINT `FK_bitka2` FOREIGN KEY (`username2`) REFERENCES `users` (`username`);

--
-- Constraints for table `user_ship`
--
ALTER TABLE `user_ship`
  ADD CONSTRAINT `FK_shipUser` FOREIGN KEY (`SID`) REFERENCES `ships` (`ID`),
  ADD CONSTRAINT `FK_userShip` FOREIGN KEY (`username`) REFERENCES `users` (`username`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
