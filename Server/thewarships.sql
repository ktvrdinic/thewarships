-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 04, 2019 at 04:00 AM
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
  `winner` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Table structure for table `ships`
--

CREATE TABLE `ships` (
  `ID` int(11) NOT NULL,
  `name` varchar(40) COLLATE utf8_bin NOT NULL,
  `no_cannons` int(11) NOT NULL DEFAULT '1',
  `speed` int(11) NOT NULL DEFAULT '1',
  `turn_speed` int(11) NOT NULL DEFAULT '1',
  `strength` int(11) NOT NULL DEFAULT '1',
  `tier` int(11) NOT NULL DEFAULT '1',
  `singleCannonDmg` int(11) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dumping data for table `ships`
--

INSERT INTO `ships` (`ID`, `name`, `no_cannons`, `speed`, `turn_speed`, `strength`, `tier`, `singleCannonDmg`) VALUES
(1, 'Brig', 8, 5, 10, 350, 1, 5),
(2, 'Carrack', 10, 6, 11, 420, 2, 10),
(3, 'Cutter', 10, 8, 10, 500, 3, 10),
(4, 'Fluyt', 10, 10, 12, 700, 4, 10),
(5, 'Frigate', 10, 10, 14, 1000, 5, 10),
(6, 'Galleon', 10, 15, 17, 1500, 6, 10),
(7, 'Ship of the line', 10, 20, 20, 2000, 7, 10);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `username` varchar(40) COLLATE utf8_bin NOT NULL,
  `email` varchar(60) COLLATE utf8_bin NOT NULL,
  `password` varchar(60) COLLATE utf8_bin NOT NULL,
  `no_ships` int(11) NOT NULL DEFAULT '1',
  `online` tinyint(1) NOT NULL DEFAULT '0',
  `gold` int(11) NOT NULL DEFAULT '100',
  `rum` int(11) NOT NULL DEFAULT '100',
  `wood` int(11) NOT NULL DEFAULT '100',
  `pearl` int(11) NOT NULL DEFAULT '50',
  `experience` int(11) NOT NULL DEFAULT '1',
  `level` int(11) NOT NULL DEFAULT '1',
  `no_victory` int(11) NOT NULL DEFAULT '0',
  `no_lose` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`username`, `email`, `password`, `no_ships`, `online`, `gold`, `rum`, `wood`, `pearl`, `experience`, `level`, `no_victory`, `no_lose`) VALUES
('karlotvrdinic', 'ktvrdi@gmail.com', '$2y$10$SkxVrS8GdLhPVIIYkjXsOOgNt6M4eqKh6LW9jYCetQJkE3E7d6FiK', 1, 0, 100, 100, 100, 50, 1, 1, 0, 0),
('karlozap', 'karlozap13@gmail.com', '$2y$10$5eC9ZiTjY6/CrOS7gx4xbu9jmOptKHXv0xB/cMDONnxCG4SUGh8P2', 1, 0, 18320, 1970, 7988, 356, 1, 1, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `user_ship`
--

CREATE TABLE `user_ship` (
  `ID` int(11) NOT NULL,
  `username` varchar(40) COLLATE utf8_bin NOT NULL,
  `no_cannons` int(11) NOT NULL DEFAULT '0',
  `speed` int(11) NOT NULL DEFAULT '0',
  `strength` int(11) NOT NULL DEFAULT '0',
  `turn_speed` int(11) NOT NULL DEFAULT '0',
  `singleCannonDmg` int(11) NOT NULL DEFAULT '0',
  `shipName` varchar(40) COLLATE utf8_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dumping data for table `user_ship`
--

INSERT INTO `user_ship` (`ID`, `username`, `no_cannons`, `speed`, `strength`, `turn_speed`, `singleCannonDmg`, `shipName`) VALUES
(2, 'karlozap', 0, 1, 4, 5, 5, 'Brig'),
(7, 'karlozap', 0, 0, 0, 0, 0, 'Ship of the line'),
(10, 'karlozap', 0, 0, 0, 0, 0, 'Carrack'),
(11, 'karlotvrdinic', 0, 0, 0, 0, 0, 'Brig'),
(12, 'karlozap', 0, 0, 0, 0, 0, 'Frigate');

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
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `name` (`name`);

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
  ADD KEY `FK_shipUser` (`shipName`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bitka`
--
ALTER TABLE `bitka`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT for table `ships`
--
ALTER TABLE `ships`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `user_ship`
--
ALTER TABLE `user_ship`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

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
  ADD CONSTRAINT `FK_shipUser` FOREIGN KEY (`shipName`) REFERENCES `ships` (`name`),
  ADD CONSTRAINT `FK_userShip` FOREIGN KEY (`username`) REFERENCES `users` (`username`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
