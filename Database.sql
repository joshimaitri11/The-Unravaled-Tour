IF DB_ID('UnraveledTour') IS NULL CREATE DATABASE UnraveledTour;
GO
USE UnraveledTour;
GO
IF OBJECT_ID('dbo.Tours') IS NULL
CREATE TABLE dbo.Tours (
    TourId   INT IDENTITY(1,1) PRIMARY KEY,
    TourDate DATE NOT NULL,
    City     NVARCHAR(80) NOT NULL,
    Country  NVARCHAR(60) NOT NULL,
    Venue    NVARCHAR(100) NOT NULL
);
GO
IF OBJECT_ID('dbo.Bookings') IS NULL
CREATE TABLE dbo.Bookings (
    BookingId   NVARCHAR(12) PRIMARY KEY,
    Email       NVARCHAR(200) NOT NULL,
    FullName    NVARCHAR(100) NOT NULL,
    City        NVARCHAR(80) NOT NULL,
    TourDate    DATE NOT NULL,
    Venue       NVARCHAR(100) NOT NULL,
    Pass        NVARCHAR(40) NOT NULL,
    Quantity    INT NOT NULL,
    TicketTotal INT NOT NULL,
    NightBudget INT NOT NULL,
    Outfit      NVARCHAR(40) NOT NULL,
    CreatedAt   DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Tours)
INSERT INTO dbo.Tours (TourDate, City, Country, Venue) VALUES
(N'2026-09-25',N'Hartford, CT',N'USA',N'PeoplesBank Arena'),
(N'2026-09-26',N'Hartford, CT',N'USA',N'PeoplesBank Arena'),
(N'2026-09-29',N'Pittsburgh, PA',N'USA',N'PPG Paints Arena'),
(N'2026-09-30',N'Pittsburgh, PA',N'USA',N'PPG Paints Arena'),
(N'2026-10-03',N'Washington, D.C.',N'USA',N'Capital One Arena'),
(N'2026-10-04',N'Washington, D.C.',N'USA',N'Capital One Arena'),
(N'2026-10-07',N'Charlotte, NC',N'USA',N'Spectrum Center'),
(N'2026-10-08',N'Charlotte, NC',N'USA',N'Spectrum Center'),
(N'2026-10-11',N'Chicago, IL',N'USA',N'United Center'),
(N'2026-10-12',N'Chicago, IL',N'USA',N'United Center'),
(N'2026-10-15',N'Boston, MA',N'USA',N'TD Garden'),
(N'2026-10-17',N'Boston, MA',N'USA',N'TD Garden'),
(N'2026-10-21',N'Montreal, QC',N'Canada',N'Bell Centre'),
(N'2026-10-22',N'Montreal, QC',N'Canada',N'Bell Centre'),
(N'2026-10-26',N'Toronto, ON',N'Canada',N'Scotiabank Arena'),
(N'2026-10-27',N'Toronto, ON',N'Canada',N'Scotiabank Arena'),
(N'2026-10-29',N'Columbus, OH',N'USA',N'Nationwide Arena'),
(N'2026-10-30',N'Columbus, OH',N'USA',N'Nationwide Arena'),
(N'2026-11-07',N'Philadelphia, PA',N'USA',N'Xfinity Mobile Arena'),
(N'2026-11-08',N'Philadelphia, PA',N'USA',N'Xfinity Mobile Arena'),
(N'2026-11-11',N'Atlanta, GA',N'USA',N'State Farm Arena'),
(N'2026-11-12',N'Atlanta, GA',N'USA',N'State Farm Arena'),
(N'2026-11-15',N'Orlando, FL',N'USA',N'Kia Center'),
(N'2026-11-16',N'Orlando, FL',N'USA',N'Kia Center'),
(N'2026-11-19',N'Sunrise, FL',N'USA',N'Amerant Bank Arena'),
(N'2026-11-20',N'Sunrise, FL',N'USA',N'Amerant Bank Arena'),
(N'2026-11-23',N'Nashville, TN',N'USA',N'Bridgestone Arena'),
(N'2026-11-24',N'Nashville, TN',N'USA',N'Bridgestone Arena'),
(N'2026-12-01',N'Vancouver, BC',N'Canada',N'Rogers Arena'),
(N'2026-12-02',N'Vancouver, BC',N'Canada',N'Rogers Arena'),
(N'2026-12-07',N'Seattle, WA',N'USA',N'Climate Pledge Arena'),
(N'2026-12-08',N'Seattle, WA',N'USA',N'Climate Pledge Arena'),
(N'2026-12-11',N'Oakland, CA',N'USA',N'Oakland Arena'),
(N'2026-12-12',N'Oakland, CA',N'USA',N'Oakland Arena'),
(N'2026-12-15',N'Sacramento, CA',N'USA',N'Golden 1 Center'),
(N'2026-12-16',N'Sacramento, CA',N'USA',N'Golden 1 Center'),
(N'2026-12-19',N'Las Vegas, NV',N'USA',N'T-Mobile Arena'),
(N'2026-12-20',N'Las Vegas, NV',N'USA',N'T-Mobile Arena'),
(N'2027-01-12',N'Los Angeles, CA',N'USA',N'Crypto.com Arena'),
(N'2027-01-13',N'Los Angeles, CA',N'USA',N'Crypto.com Arena'),
(N'2027-01-16',N'Los Angeles, CA',N'USA',N'Crypto.com Arena'),
(N'2027-01-17',N'Los Angeles, CA',N'USA',N'Crypto.com Arena'),
(N'2027-02-11',N'Brooklyn, NY',N'USA',N'Barclays Center'),
(N'2027-02-12',N'Brooklyn, NY',N'USA',N'Barclays Center'),
(N'2027-02-15',N'Brooklyn, NY',N'USA',N'Barclays Center'),
(N'2027-02-16',N'Brooklyn, NY',N'USA',N'Barclays Center'),
(N'2027-03-19',N'Stockholm',N'Sweden',N'Avicii Arena'),
(N'2027-03-20',N'Stockholm',N'Sweden',N'Avicii Arena'),
(N'2027-03-23',N'Amsterdam',N'Netherlands',N'Ziggo Dome'),
(N'2027-03-24',N'Amsterdam',N'Netherlands',N'Ziggo Dome'),
(N'2027-04-01',N'Munich',N'Germany',N'Olympiahalle'),
(N'2027-04-02',N'Munich',N'Germany',N'Olympiahalle'),
(N'2027-04-05',N'London',N'UK',N'The O2'),
(N'2027-04-06',N'London',N'UK',N'The O2'),
(N'2027-04-08',N'London',N'UK',N'The O2'),
(N'2027-04-09',N'London',N'UK',N'The O2'),
(N'2027-05-10',N'London',N'UK',N'The O2'),
(N'2027-04-23',N'Paris',N'France',N'Accor Arena'),
(N'2027-04-27',N'Milan',N'Italy',N'Unipol Forum'),
(N'2027-04-28',N'Milan',N'Italy',N'Unipol Forum'),
(N'2027-05-01',N'Barcelona',N'Spain',N'Palau Sant Jordi'),
(N'2027-05-02',N'Barcelona',N'Spain',N'Palau Sant Jordi');
GO
