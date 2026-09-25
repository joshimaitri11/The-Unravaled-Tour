========================================================================
The Unraveled Tour - Fan-Made Concert Planner (ASP.NET Web Forms, C#)
========================================================================

This project runs on both WINDOWS (via Visual Studio & LocalDB)
and LINUX (via Docker & Mono XSP4).

------------------------------------------------------------------------
1. HOW TO RUN ON WINDOWS (Visual Studio 2019 or 2022)
------------------------------------------------------------------------
Requirements:
- Visual Studio 2019 or 2022 with the "ASP.NET and web development" workload.
- SQL Server LocalDB (installed automatically with Visual Studio).

Step-by-Step Instructions:

Step 1: Set up the Database
1. Open Visual Studio.
2. In the top menu, go to: View > SQL Server Object Explorer.
3. In the explorer tree on the left, expand:
   (localdb)\MSSQLLocalDB
4. Right-click on (localdb)\MSSQLLocalDB and choose "New Query...".
5. Open Database.sql in this project folder, copy all text, and paste it into the query window.
6. Click the green Execute button (or press Ctrl + Shift + E).
   This creates the UnraveledTour database, the Tours and Bookings tables, and seeds all 62 tour dates.

Step 2: Open this Project
1. In Visual Studio, go to top menu: File > Open > Web Site... (or Shift + Alt + O).
2. Select this folder (unraveled-tour-aspnet) and click Open.

Step 3: Run the Website
1. In the Solution Explorer pane on the right side, find Default.aspx.
2. Right-click Default.aspx and choose "Set As Start Page".
3. Press Ctrl + F5 (or click the green Play button labeled "IIS Express").
4. Your browser will open automatically at:
   http://localhost:XXXXX/Default.aspx


------------------------------------------------------------------------
2. HOW TO RUN ON LINUX (Using Docker & Mono XSP4)
------------------------------------------------------------------------
Requirements:
- Docker and Docker Compose installed.

Step-by-Step Instructions:

Step 1: Open Terminal in this folder
   cd /home/khushu/Downloads/unraveled-tour-aspnet

Step 2: Ensure Docker Permissions (one-time setup)
   sudo chmod 666 /var/run/docker.sock

Step 3: Start the Containers
   docker compose up -d
   (If building for the first time: docker compose up --build -d)

Step 4: Open in Your Browser
   Navigate to:
   http://localhost:5000/Default.aspx

Step 5: Stop the Application (when finished)
   docker compose down


------------------------------------------------------------------------
3. LOGIN ACCOUNTS
------------------------------------------------------------------------
- Fan Login: Any email address (e.g. fan@unraveled.fan). No password needed.
             Plans and tickets are saved under your email.
- Admin Login: admin@unraveled.fan
               When logged in with this email, an "ADMIN" link appears
               in the navbar, allowing you to add, edit, and delete tour dates.


------------------------------------------------------------------------
4. FILE GUIDE
------------------------------------------------------------------------
Default.aspx      : Login page (Spotify style album player card)
Home.aspx         : Hero banner, album intro, and tour navigation cards
Tours.aspx        : Tour dates list loaded from SQL Server with search filter
Plan.aspx         : Concert planner (pass choice, tickets qty, night budget, outfits)
Admin.aspx        : Admin dashboard for adding, editing, and deleting tour dates
Site.master       : Master header navigation and footer layout
Database.sql      : SQL script for creating database, tables, and seeding tour dates
Web.config        : Application configuration and database connection string
App_Code/Db.cs    : C# helper class for executing SQL queries and commands
style.css         : Complete styling and responsive CSS rules
site.js           : Floating heart animations and active nav links
Dockerfile        : Linux Docker build setup for Mono XSP4
docker-compose.yml: Service setup running SQL Server 2022 and Mono XSP4 together

------------------------------------------------------------------------
Fan project, not affiliated with Olivia Rodrigo or Live Nation.
========================================================================
