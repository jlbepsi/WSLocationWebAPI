-- phpMyAdmin SQL Dump
-- version 4.9.5deb2
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : lun. 04 juil. 2022 à 15:43
-- Version du serveur :  8.0.27
-- Version de PHP : 7.4.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `rhlocation`
--

-- --------------------------------------------------------

--
-- Structure de la table `facture`
--

CREATE TABLE `facture` (
  `id` int NOT NULL COMMENT 'Doit être identique à location_id',
  `date` datetime NOT NULL,
  `adresse` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Structure de la table `location`
--

CREATE TABLE `location` (
  `id` int NOT NULL,
  `idutilisateur` int NOT NULL,
  `idhabitation` int NOT NULL,
  `datedebut` datetime NOT NULL,
  `datefin` datetime NOT NULL,
  `montanttotal` double NOT NULL DEFAULT '0',
  `montantverse` double NOT NULL DEFAULT '0'
) ;

--
-- Déchargement des données de la table `location`
--

INSERT INTO `location` (`id`, `idutilisateur`, `idhabitation`, `datedebut`, `datefin`, `montanttotal`, `montantverse`) VALUES
(1, 5, 7, '2022-07-01 00:00:00', '2022-07-04 00:00:00', 600, 0),
(2, 6, 8, '2022-07-05 00:00:00', '2022-07-07 00:00:00', 0, 0);

-- --------------------------------------------------------

--
-- Structure de la table `location_optionpayantero`
--

CREATE TABLE `location_optionpayantero` (
  `location_id` int NOT NULL,
  `optionpayantero_id` int NOT NULL,
  `prix` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Déclencheurs `location_optionpayantero`
--
DELIMITER $$
CREATE TRIGGER `majd_location_optionpayante` AFTER DELETE ON `location_optionpayantero` FOR EACH ROW BEGIN
    UPDATE location
    SET montanttotal = montanttotal - OLD.prix
    WHERE id = OLD.location_id;
end
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `maji_location_optionpayante` AFTER INSERT ON `location_optionpayantero` FOR EACH ROW BEGIN
    UPDATE location
    SET montanttotal = montanttotal + NEW.prix
    WHERE id = NEW.location_id;
end
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `optionpayantero`
--

CREATE TABLE `optionpayantero` (
  `id` int NOT NULL,
  `libelle` varchar(100) NOT NULL,
  `description` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Déchargement des données de la table `optionpayantero`
--

INSERT INTO `optionpayantero` (`id`, `libelle`, `description`) VALUES
(1, 'Ménage', 'A la fin du séjour'),
(2, 'Drap de lit', 'Pour l\'ensemble des lits'),
(3, 'Linge de maison', 'Linge de toilette pour la salle de bain');

-- --------------------------------------------------------

--
-- Structure de la table `reglement`
--

CREATE TABLE `reglement` (
  `id` int NOT NULL,
  `location_id` int NOT NULL,
  `montant` decimal(10,0) NOT NULL,
  `dateversement` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `typereglement_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Déchargement des données de la table `reglement`
--

INSERT INTO `reglement` (`id`, `location_id`, `montant`, `dateversement`, `typereglement_id`) VALUES
(4, 1, '200', '2022-07-04 11:01:31', 1),
(5, 1, '200', '2022-07-04 11:01:52', 1),
(6, 1, '200', '2022-07-04 11:01:57', 1);

--
-- Déclencheurs `reglement`
--
DELIMITER $$
CREATE TRIGGER `maj_montant_verse` AFTER INSERT ON `reglement` FOR EACH ROW BEGIN
    UPDATE location
    SET montantverse = montantverse + NEW.montant
    WHERE location.id = NEW.location_id;
end
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `relance`
--

CREATE TABLE `relance` (
  `id` int NOT NULL,
  `location_id` int NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `motif` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Déchargement des données de la table `relance`
--

INSERT INTO `relance` (`id`, `location_id`, `date`, `motif`) VALUES
(1, 1, '2022-06-24 16:04:05', 'ee');

-- --------------------------------------------------------

--
-- Structure de la table `typereglement`
--

CREATE TABLE `typereglement` (
  `id` int NOT NULL,
  `libelle` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Déchargement des données de la table `typereglement`
--

INSERT INTO `typereglement` (`id`, `libelle`) VALUES
(1, 'Carte Bleue'),
(2, 'Chèque bancaire'),
(3, 'Chèque vacance');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `facture`
--
ALTER TABLE `facture`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `location`
--
ALTER TABLE `location`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `location_optionpayantero`
--
ALTER TABLE `location_optionpayantero`
  ADD PRIMARY KEY (`location_id`,`optionpayantero_id`),
  ADD KEY `location_optionpayantero_id_fk` (`optionpayantero_id`);

--
-- Index pour la table `optionpayantero`
--
ALTER TABLE `optionpayantero`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `reglement`
--
ALTER TABLE `reglement`
  ADD PRIMARY KEY (`id`),
  ADD KEY `typereglement_id` (`typereglement_id`),
  ADD KEY `location_id` (`location_id`);

--
-- Index pour la table `relance`
--
ALTER TABLE `relance`
  ADD PRIMARY KEY (`id`),
  ADD KEY `location_id` (`location_id`);

--
-- Index pour la table `typereglement`
--
ALTER TABLE `typereglement`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `location`
--
ALTER TABLE `location`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `reglement`
--
ALTER TABLE `reglement`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT pour la table `relance`
--
ALTER TABLE `relance`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `typereglement`
--
ALTER TABLE `typereglement`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `facture`
--
ALTER TABLE `facture`
  ADD CONSTRAINT `facture_ibfk_1` FOREIGN KEY (`id`) REFERENCES `location` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Contraintes pour la table `location_optionpayantero`
--
ALTER TABLE `location_optionpayantero`
  ADD CONSTRAINT `location_optionpayantero_ibfk_1` FOREIGN KEY (`location_id`) REFERENCES `location` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `location_optionpayantero_id_fk` FOREIGN KEY (`optionpayantero_id`) REFERENCES `optionpayantero` (`id`);

--
-- Contraintes pour la table `reglement`
--
ALTER TABLE `reglement`
  ADD CONSTRAINT `reglement_ibfk_1` FOREIGN KEY (`typereglement_id`) REFERENCES `typereglement` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `reglement_ibfk_2` FOREIGN KEY (`location_id`) REFERENCES `location` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Contraintes pour la table `relance`
--
ALTER TABLE `relance`
  ADD CONSTRAINT `relance_ibfk_1` FOREIGN KEY (`location_id`) REFERENCES `location` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
