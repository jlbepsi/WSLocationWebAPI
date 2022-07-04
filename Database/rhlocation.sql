-- phpMyAdmin SQL Dump
-- version 4.9.5deb2
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : lun. 04 juil. 2022 à 10:16
-- Version du serveur :  8.0.29-0ubuntu0.20.04.3
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

--
-- Déchargement des données de la table `facture`
--

INSERT INTO `facture` (`id`, `date`, `adresse`) VALUES
(1, '2022-06-24 16:03:34', 'dsgf');

-- --------------------------------------------------------

--
-- Structure de la table `location`
--

CREATE TABLE `location` (
  `id` int NOT NULL,
  `idutilisateur` int NOT NULL,
  `idhabitation` int NOT NULL,
  `facture_id` int DEFAULT NULL,
  `datedebut` datetime NOT NULL,
  `datefin` datetime NOT NULL,
  `montanttotal` double NOT NULL DEFAULT '0',
  `montantverse` double NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Déchargement des données de la table `location`
--

INSERT INTO `location` (`id`, `idutilisateur`, `idhabitation`, `facture_id`, `datedebut`, `datefin`, `montanttotal`, `montantverse`) VALUES
(1, 5, 7, NULL, '2022-07-01 00:00:00', '2022-07-04 00:00:00', 0, 0),
(2, 6, 8, 1, '2022-07-05 00:00:00', '2022-07-07 00:00:00', 0, 0);

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
  ADD PRIMARY KEY (`id`),
  ADD KEY `facture_id` (`facture_id`);

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
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pour la table `reglement`
--
ALTER TABLE `reglement`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

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
-- Contraintes pour la table `location`
--
ALTER TABLE `location`
  ADD CONSTRAINT `location_ibfk_1` FOREIGN KEY (`facture_id`) REFERENCES `facture` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

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
