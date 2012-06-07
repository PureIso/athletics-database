using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace AthleticsDatabase
{
    /// <summary>Contains methods call to database</summary>
    public class MsAccessDatabase
    {
        private readonly OleDbConnection _databaseConnection;
        private readonly string _connection;

        /// <summary>The Constructor</summary>
        /// <param name="databaseName">The name of the database</param>
        public MsAccessDatabase(string databaseName)
        {
            try
            {
                _connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + Path.DirectorySeparatorChar + databaseName + ".accdb; Jet OLEDB:Database Password=olawale1988//``;";
                _databaseConnection = new OleDbConnection(_connection);
                _databaseConnection.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #region Getter Methods / Accessors
        /*
        /// <summary>Validate a login user</summary>
        /// <param name="userName">The name of the user</param>
        /// <param name="userPassword">The password of the user</param>
        /// <returns>Returns True if the user is valid</returns>
        /// <exception cref="Exception"></exception>
        public bool ValidateUser(string userName, string userPassword)
        {
            var sqlQuery = ("SELECT * FROM admins WHERE adminName = '" + userName + "'");

            Open();//open connection
            try
            {
                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if we have a valid name
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return false;

                sqlQuery = ("SELECT * FROM admins WHERE adminName = '" + userName + "' AND adminPassword = '" + userPassword + "'");
                if (ConnectionState.Open.ToString() != "Open") return false;
                databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseDataReader = databaseCommand.ExecuteReader();

                //Check if we have a valid name and password - if the database has row then its valid
                return databaseDataReader == null || databaseDataReader.HasRows;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Close();//close connection
            }
        }
        */
        
        /// <summary>List all the events for the selected competition type</summary>
        /// <param name="competitionType">The comptition type, Indoor or Outdoor</param>
        /// <returns>An array of competition type events</returns>
        public IEnumerable<string> ListAllEvents(Enumerations.CompetitionType competitionType)
        {
            try
            {
                var sqlQuery = ("SELECT eventName FROM events WHERE typeID = " + (int)competitionType + "");

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var yearsList = new List<string>();
                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    //Get the value at column 0
                    yearsList.Add(databaseDataReader.GetValue(0).ToString());
                }
                return yearsList.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get statistics to popluate the leaderboard ranking
        /// {rank, performance, name, surname, date, venue and competition name} respectively.</summary>
        /// <param name="gender">The athlete gender</param>
        /// <param name="season">The Competition year</param>
        /// <param name="selectedEvent">The specific event</param>
        /// <param name="region">The Specified Region</param>
        /// <param name="competitionType">If competition is indoors or outdoors</param>
        /// <param name="ageGroup">The age group</param>
        /// <returns>Returns a 2 dimentional array to populate the leaderboard</returns>
        public IEnumerable<string[]> GetLeaderboard(Enumerations.Gender gender, string season, string selectedEvent, Enumerations.Region region, Enumerations.CompetitionType competitionType, string ageGroup)
        {
            try
            {
                var sqlQuery = GetLeaderboardSqlString(gender, season, selectedEvent, region, competitionType, ageGroup);

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var leaderboardArray = new List<string[]>();
                var rank = 1;
                
                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    var listItems = new string[7];
                    listItems[0] = rank.ToString(); //rank
                    listItems[1] = databaseDataReader.GetValue(0).ToString();//performance
                    listItems[2] = databaseDataReader.GetValue(1).ToString();//name
                    listItems[3] = databaseDataReader.GetValue(2).ToString();//surname
                    listItems[4] = databaseDataReader.GetValue(3).ToString();//competition date
                    listItems[5] = databaseDataReader.GetValue(4).ToString();//venue
                    listItems[6] = databaseDataReader.GetValue(5).ToString();//competition name
                    //Get the value at column 0
                    leaderboardArray.Add(listItems);
                    rank++;
                }
                return leaderboardArray.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>Get an athlete full details {id, name, surname, year of birth, region and club name}
        /// respectively from the database</summary>
        /// <param name="athleteId">The athlete ID</param>
        /// <param name="gender">The athletes gender</param>
        /// <returns>Returns a 2 dimentional array to populate the athlete full details form</returns>
        public string[] GetAthlete(int athleteId, Enumerations.Gender gender)
        {
            try
            {
                string sqlQuery;
                if (gender == Enumerations.Gender.Male)
                {
                    sqlQuery = "SELECT athletesmale.athleteMaleID, athletesmale.name, athletesmale.surname,athletesmale.yearOfBirth, regions.regionName, clubs.clubNames "
                        + "FROM (athletesmale INNER JOIN regions ON athletesmale.regionID = regions.regionID) INNER JOIN clubs ON athletesmale.clubID = clubs.clubsID "
                        + "WHERE athletesmale.athleteMaleID = " + athleteId + " ";
                }
                else
                {
                    sqlQuery = "SELECT athletesfemale.athleteFemaleID, athletesfemale.name, athletesfemale.surname,athletesfemale.yearOfBirth, regions.regionName, clubs.clubNames "
                        + "FROM (athletesfemale INNER JOIN regions ON athletesfemale.regionID = regions.regionID) INNER JOIN clubs ON athletesfemale.clubID = clubs.clubsID "
                        + "WHERE athletesfemale.athleteFemaleID = " + athleteId + " ";
                }

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                var listItems = new string[6];

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    listItems[0] = databaseDataReader.GetValue(0).ToString();//athlete ID
                    listItems[1] = databaseDataReader.GetValue(1).ToString();//athlete Name
                    listItems[2] = databaseDataReader.GetValue(2).ToString();//surname
                    listItems[3] = databaseDataReader.GetValue(3).ToString();//year of birth
                    listItems[4] = databaseDataReader.GetValue(4).ToString();//region
                    listItems[5] = databaseDataReader.GetValue(5).ToString();//club Name
                }
                return listItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get any athlete with specified name and surname {id, name, surname}respectively from the database</summary>
        /// <param name="name">The name to search or a suffix</param>
        /// <param name="surname">The surname to search or a suffix</param>
        /// <param name="gender">The gender</param>
        public IEnumerable<string[]> SearchAthletes(string name, string surname, Enumerations.Gender gender)
        {
            try
            {
                string sqlString;
                if (gender == Enumerations.Gender.Male)
                {
                    sqlString = "SELECT athleteMaleID, name, surname FROM athletesmale "
                                + "WHERE name Like '%" + name + "%' and  surname Like '%" + surname + "%'";
                }
                else
                {
                    sqlString = "SELECT athleteFemaleID, name, surname FROM athletesfemale "
                                + "WHERE name Like '%" + name + "%' and  surname Like '%" + surname + "%'";
                }

                var databaseCommand = new OleDbCommand(sqlString, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var athletesArray = new List<string[]>();

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    var listItems = new string[3];
                    listItems[0] = databaseDataReader.GetValue(0).ToString();//ID
                    listItems[1] = databaseDataReader.GetValue(1).ToString();//name
                    listItems[2] = databaseDataReader.GetValue(2).ToString();//surname
                    //Get the value at column 0
                    athletesArray.Add(listItems);
                }
                return athletesArray;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>List all clubs for a selected region</summary>
        /// <param name="region">The specified Region</param>
        public IEnumerable<string[]> ListAllClubs(int region)
        {
            try
            {
                string sqlQuery;
                if ((int)Enumerations.Region.AllIreland == region)
                {
                    sqlQuery = ("SELECT clubsID, clubNames FROM clubs ORDER by clubNames");
                }
                else
                {
                    sqlQuery = ("SELECT clubsID, clubNames FROM clubs WHERE regionID = " + region + " ORDER by clubNames");
                }

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var clubsList = new List<string[]>();
                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    var listItems = new string[2];
                    listItems[0] = databaseDataReader.GetValue(0).ToString();//ID
                    listItems[1] = databaseDataReader.GetValue(1).ToString();//name
                    //Get the value at column 0
                    clubsList.Add(listItems);
                }
                return clubsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get the list of events an athlete competed in for a specific year</summary>
        /// <param name="competitionType">The competition type Indoor or Outdoor</param>
        /// <param name="gender">The athletes gender</param>
        /// <param name="athleteId">The athlets ID</param>
        /// <param name="season">The specified year</param>
        public IEnumerable<string> ListEventPerAthletePerSeason(Enumerations.CompetitionType competitionType,Enumerations.Gender gender, int athleteId, string season)
        {
            try
            {
                var sqlQuery = ListEventPerAthletePerSeasonSqlString(competitionType, gender, athleteId, season);

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var yearsList = new List<string>();
                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    //Get the value at column 0
                    yearsList.Add(databaseDataReader.GetValue(0).ToString());
                }
                return yearsList.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get an athletes event history for a specific year
        /// {performance, positition, competition name, competition venue, competition date}respectively from the database</summary>
        /// <param name="competitionType">The competition type Indoor or Outdoor</param>
        /// <param name="gender">The athletes gender</param>
        /// <param name="athleteId">The athlets ID</param>
        /// <param name="season">The specified year</param>
        /// <param name="eventName">The event Name</param>
        public IEnumerable<string[]> ListAthleteEventHistory(Enumerations.CompetitionType competitionType, Enumerations.Gender gender, int athleteId, string season, string eventName)
        {
            try
            {
                var sqlQuery = ListAthleteEventHistorySqlString(competitionType, gender, athleteId, season, eventName);

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var leaderboardArray = new List<string[]>();

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    var listItems = new string[4];
                    listItems[0] = databaseDataReader.GetValue(0).ToString();//performance
                    listItems[1] = databaseDataReader.GetValue(1).ToString();//competition name
                    listItems[2] = databaseDataReader.GetValue(2).ToString();//competition venue
                    listItems[3] = databaseDataReader.GetValue(3).ToString();//competition date
                    //Get the value at column 0
                    leaderboardArray.Add(listItems);
                }
                return leaderboardArray;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>List all competition which took place on the specified season</summary>
        /// <param name="season">The season</param>
        /// <param name="country">The country</param>
        /// <param name="region">The region</param>
        public IEnumerable<string[]> ListAllCompetitions(string season, string country, string region)
        {
            try
            {
                var regionId = GetRegionId(region);
                string sqlQuery;
                if (regionId == 5)
                {
                    sqlQuery = "SELECT DISTINCT competitionID, competitionName, competitionTypeID, competitionVenue, competitionDate FROM competitions, competitiontype "
                               + "WHERE competitionSeason = " + season + " AND regionID <> " + (int)Enumerations.Region.NonIrish + " ";
                }
                else
                {
                    sqlQuery = "SELECT DISTINCT competitionID, competitionName, competitionTypeID, competitionVenue, competitionDate FROM competitions, competitiontype "
                               + "WHERE competitionSeason = " + season + " AND competitionCountry = '" + country + "' AND regionID = " + regionId + " ";
                }


                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var competitionsArray = new List<string[]>();

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    var listItems = new string[5];
                    listItems[0] = databaseDataReader.GetValue(0).ToString();//ID
                    listItems[1] = databaseDataReader.GetValue(1).ToString();//competition name
                    listItems[2]  = ((Enumerations.Region) Int32.Parse(databaseDataReader.GetValue(2).ToString())).ToString();//Type
                    listItems[3] = databaseDataReader.GetValue(3).ToString();//venue
                    listItems[4] = databaseDataReader.GetValue(4).ToString();//date
                    //Get the value at column 0
                    competitionsArray.Add(listItems);
                }
                return competitionsArray;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get the ID of a specified region</summary>
        /// <param name="region">The region name</param>
        public int GetRegionId(string region)
        {
            try
            {
                if (Enumerations.Region.AllIreland.ToString() == region || region == "All Ireland")
                {
                    return (int)Enumerations.Region.AllIreland;
                }
                if (Enumerations.Region.Connacht.ToString() == region)
                {
                    return (int)Enumerations.Region.Connacht;
                }
                if (Enumerations.Region.Leinster.ToString() == region)
                {
                    return (int)Enumerations.Region.Leinster;
                }
                if (Enumerations.Region.Munster.ToString() == region)
                {
                    return (int)Enumerations.Region.Munster;
                }
                return (int)Enumerations.Region.Ulster;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get all the competition years in the database</summary>
        /// <param name="competitionType">The comptition type, Indoor or Outdoor</param>
        /// <returns>An array of competition years</returns>
        public IEnumerable<string> GetCompetitionYear(Enumerations.CompetitionType competitionType)
        {
            try
            {
                string sqlQuery;
                if (competitionType == Enumerations.CompetitionType.All)
                {
                    sqlQuery = ("SELECT DISTINCT competitionSeason as competitionYear FROM competitions ");
                }
                else
                {
                    sqlQuery = ("SELECT DISTINCT competitionSeason as competitionYear FROM competitions ");
                    // cast the enum to int to get the enum value
                    sqlQuery += ("WHERE competitionTypeID = " + (int)competitionType + " ");
                }

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var yearsList = new List<string>();
                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    //Get the value at column 0
                    yearsList.Add(databaseDataReader.GetValue(0).ToString());
                }
                return yearsList.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get Contries where competitions took place in a specified season</summary>
        /// <param name="season">The specified season</param>
        public IEnumerable<string> GetCountries(string season)
        {
            try
            {
                var sqlQuery = "SELECT DISTINCT competitionCountry as selectedCountry FROM competitions "
                               + "WHERE competitionSeason = " + season + "";

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var countryArray = new List<string>();

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    countryArray.Add(databaseDataReader.GetValue(0).ToString());//Country
                }
                return countryArray;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get a list of region</summary>
        /// <param name="season">The season</param>
        /// <param name="country">the Country</param>
        /// <param name="competitionType">Indoor or outdoor</param>
        public IEnumerable<string> GetRegion(string season, string country, Enumerations.CompetitionType competitionType)
        {
            try
            {
                string sqlQuery;

                if (competitionType == Enumerations.CompetitionType.All)
                {
                    sqlQuery = "SELECT DISTINCT regionName as selectedRegion FROM competitions INNER JOIN regions ON competitions.regionID = regions.regionID "
                               + "WHERE (competitionDate Like '%/" + season + "') AND (competitionCountry = '" + country + "')";
                }
                else
                {
                    sqlQuery = "SELECT DISTINCT regionName as selectedRegion FROM competitions INNER JOIN regions ON competitions.regionID = regions.regionID "
                               + "WHERE (competitionDate Like '%/" + season + "') AND (competitionCountry = '" + country + "') AND (competitionTypeID = " + (int)competitionType + ")";
                }

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var countryArray = new List<string>();

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    countryArray.Add(databaseDataReader.GetValue(0).ToString());//Country
                }
                return countryArray;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get a Competition name</summary>
        /// <param name="season">The season</param>
        /// <param name="country">The country</param>
        /// <param name="region">The region</param>
        /// <param name="venue">The venue</param>
        /// <param name="competitionType">Indoor or Outdoor</param>
        public IEnumerable<string> GetCompetitionName(string season, string country,string region, string venue, Enumerations.CompetitionType competitionType)
        {
            try
            {
                var regionId = GetRegionId(region);
                var sqlQuery = "SELECT DISTINCT competitionName as selectedCompetitionName FROM competitions "
                               + "WHERE (competitionDate Like '%/" + season + "') AND (competitionCountry = '" + country + "') AND (competitionVenue = '" + venue + "') AND (regionID = " + regionId + ") AND (competitionTypeID = " + (int) competitionType + ")";

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var countryArray = new List<string>();

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    countryArray.Add(databaseDataReader.GetValue(0).ToString());//Country
                }
                return countryArray;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get competition Date</summary>
        /// <param name="season">The season</param>
        /// <param name="country">The country</param>
        /// <param name="region">The region</param>
        /// <param name="venue">The venue</param>
        /// <param name="competitionName">The competition Name</param>
        /// <param name="competitionType">Indoor or Outdoor</param>
        public IEnumerable<string> GetCompetitionDate(string season, string country, string region, string venue,string competitionName, Enumerations.CompetitionType competitionType)
        {
            try
            {
                var regionId = GetRegionId(region);
                var sqlQuery = "SELECT DISTINCT competitionDate as selectedCompetitionDate FROM competitions "
                               + "WHERE (competitionDate Like '%/" + season + "') AND (competitionCountry = '" + country + "') AND (regionID = " + regionId + ") AND (competitionVenue = '" + venue + "') AND (competitionName = '" + competitionName + "') AND (competitionTypeID = " + (int)competitionType + ")";

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var countryArray = new List<string>();

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    countryArray.Add(databaseDataReader.GetValue(0).ToString());//Country
                }
                return countryArray;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get a list of venues</summary>
        /// <param name="season">The season the venue was used</param>
        /// <param name="country">The country</param>
        /// <param name="region">The Region</param>
        /// <param name="competitionType">Indoor or outdoor competition</param>
        public IEnumerable<string> GetVenues(string season, string country,string region, Enumerations.CompetitionType competitionType)
        {
            try
            {
                var regionId = GetRegionId(region);
                var sqlQuery = "SELECT DISTINCT competitionVenue as selectedVenue FROM competitions "
                               + "WHERE (competitionDate Like '%/" + season + "') AND (competitionCountry = '" + country + "') AND (regionID = " + regionId + ") AND competitionTypeID = " + (int)competitionType + " ";
                
                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();

                //Check if the query results are not empty
                if (databaseDataReader != null && databaseDataReader.HasRows != true) return null;
                //setup a string list since we dont know the since an array would be useless
                var venueArray = new List<string>();

                //while query is not empty keep readying
                while (databaseDataReader != null && databaseDataReader.Read())
                {
                    venueArray.Add(databaseDataReader.GetValue(0).ToString());//Country
                }
                return venueArray;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Get competition ID</summary>
        /// <param name="date">The date of the competition</param>
        /// <param name="country">The contry</param>
        /// <param name="region">The region</param>
        /// <param name="venue">The venue</param>
        /// <param name="competitionName">The competition name</param>
        public int GetCompetitionId(string date, string country, string region, string venue, string competitionName)
        {
            try
            {
                var regionId = GetRegionId(region);
                var sqlQuery = "SELECT competitionID FROM competitions "
                    + "WHERE (competitionDate = '" + date + "') AND (competitionCountry = '" + country + "') AND (regionID = " + regionId + ") AND (competitionVenue = '" + venue + "') AND (competitionName = '" + competitionName + "') ";

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                return Convert.ToInt32(databaseCommand.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static string ListEventPerAthletePerSeasonSqlString(Enumerations.CompetitionType competitionType, Enumerations.Gender gender, int athleteId, string season)
        {
            switch (competitionType)
            {
                case Enumerations.CompetitionType.Indoor:
                    if (Enumerations.Gender.Male == gender)
                    {
                        return "SELECT DISTINCT events.eventName "
                                          + "FROM (((athletesmale INNER JOIN resultsmaleindoors ON  athletesmale.athleteMaleID = resultsmaleindoors.athleteMaleID) "
                                          + "INNER JOIN competitions ON  resultsmaleindoors.competitionID = competitions.competitionID) "
                                          + "INNER JOIN events ON events.eventsID = resultsmaleindoors.eventID) "
                                          + "WHERE athletesmale.athleteMaleID = " + athleteId + " And competitions.competitionSeason = " + season + " "
                                          + "ORDER BY events.eventName ";
                    }
                    return "SELECT DISTINCT events.eventName "
                                      + "FROM (((athletesfemale INNER JOIN resultsfemaleindoors ON  athletesfemale.athletefemaleID = resultsfemaleindoors.athletefemaleID) "
                                      + "INNER JOIN competitions ON  resultsfemaleindoors.competitionID = competitions.competitionID) "
                                      + "INNER JOIN events ON events.eventsID = resultsfemaleindoors.eventID) "
                                      + "WHERE athletesfemale.athletefemaleID = " + athleteId + " And competitions.competitionSeason = " + season + " "
                                      + "ORDER BY events.eventName ";
                case Enumerations.CompetitionType.Outdoor:
                    if (Enumerations.Gender.Male == gender)
                    {
                        return "SELECT DISTINCT events.eventName "
                                          + "FROM (((athletesmale INNER JOIN resultsmaleoutdoors ON  athletesmale.athleteMaleID = resultsmaleoutdoors.athleteMaleID) "
                                          + "INNER JOIN competitions ON  resultsmaleoutdoors.competitionID = competitions.competitionID) "
                                          + "INNER JOIN events ON events.eventsID = resultsmaleoutdoors.eventID) "
                                          + "WHERE athletesmale.athleteMaleID = " + athleteId + " And competitions.competitionSeason = " + season + " "
                                          + "ORDER BY events.eventName ";
                    }
                    return "SELECT DISTINCT events.eventName "
                                      + "FROM (((athletesfemale INNER JOIN resultsfemaleoutdoors ON  athletesfemale.athletefemaleID = resultsfemaleoutdoors.athletefemaleID) "
                                      + "INNER JOIN competitions ON  resultsfemaleoutdoors.competitionID = competitions.competitionID) "
                                      + "INNER JOIN events ON events.eventsID = resultsfemaleoutdoors.eventID) "
                                      + "WHERE athletesfemale.athletefemaleID = " + athleteId + " And competitions.competitionSeason = " + season + " "
                                      + "ORDER BY events.eventName ";
                default:
                    return null;
            }
        }
        private string ListAthleteEventHistorySqlString(Enumerations.CompetitionType competitionType, Enumerations.Gender gender, int athleteId, string season, string eventName)
        {
            var eventId = GetEventId(eventName, competitionType);
            switch (competitionType)
            {
                case Enumerations.CompetitionType.Indoor:
                    if (Enumerations.Gender.Male == gender)
                    {
                        return "SELECT DISTINCT resultsmaleindoors.performance, competitions.competitionName, competitions.competitionVenue, competitions.competitionDate "
                            + "FROM (((athletesmale INNER JOIN resultsmaleindoors ON  athletesmale.athleteMaleID = resultsmaleindoors.athleteMaleID) "
                            + "INNER JOIN competitions ON  resultsmaleindoors.competitionID = competitions.competitionID) "
                            + "INNER JOIN events ON events.eventsID = resultsmaleindoors.eventID) "
                            + "WHERE athletesmale.athleteMaleID = " + athleteId + " And competitions.competitionSeason = " + season + " And events.eventsID = " + eventId + " "
                            + "ORDER BY resultsmaleindoors.performance";
                    }
                    return "SELECT DISTINCT resultsfemaleindoors.performance, competitions.competitionName, competitions.competitionVenue, competitions.competitionDate "
                            + "FROM (((athletesfemale INNER JOIN resultsfemaleindoors ON  athletesfemale.athletefemaleID = resultsfemaleindoors.athletefemaleID) "
                            + "INNER JOIN competitions ON  resultsfemaleindoors.competitionID = competitions.competitionID) "
                            + "INNER JOIN events ON events.eventsID = resultsfemaleindoors.eventID) "
                            + "WHERE athletesfemale.athletefemaleID = " + athleteId + " And competitions.competitionSeason = " + season + " And events.eventsID = " + eventId + " "
                            + "ORDER BY resultsfemaleindoors.performance";
                case Enumerations.CompetitionType.Outdoor:
                    if (Enumerations.Gender.Male == gender)
                    {
                        return "SELECT DISTINCT resultsmaleoutdoors.performance, competitions.competitionName, competitions.competitionVenue, competitions.competitionDate "
                            + "FROM (((athletesmale INNER JOIN resultsmaleoutdoors ON  athletesmale.athleteMaleID = resultsmaleoutdoors.athleteMaleID) "
                            + "INNER JOIN competitions ON  resultsmaleoutdoors.competitionID = competitions.competitionID) "
                            + "INNER JOIN events ON events.eventsID = resultsmaleoutdoors.eventID) "
                            + "WHERE athletesmale.athleteMaleID = " + athleteId + " And competitions.competitionSeason = " + season + " And events.eventsID = " + eventId + " "
                            + "ORDER BY resultsmaleoutdoors.performance ";
                    }
                    return "SELECT DISTINCT resultsfemaleoutdoors.performance, competitions.competitionName, competitions.competitionVenue, competitions.competitionDate "
                            + "FROM (((athletesfemale INNER JOIN resultsfemaleoutdoors ON  athletesfemale.athletefemaleID = resultsfemaleoutdoors.athletefemaleID) "
                            + "INNER JOIN competitions ON  resultsfemaleoutdoors.competitionID = competitions.competitionID) "
                            + "INNER JOIN events ON events.eventsID = resultsfemaleoutdoors.eventID) "
                            + "WHERE athletesfemale.athletefemaleID = " + athleteId + " And competitions.competitionSeason = " + season + " And events.eventsID = " + eventId + " "
                            + "ORDER BY resultsfemaleoutdoors.performance ";
                default:
                    return null;
            }
        }
        
        private static string GetAgeSqlString(string ageGroup, Enumerations.Gender gender)
        {
            //Nested Switch statement
            switch (gender)
            {
                case Enumerations.Gender.Male:
                    switch (ageGroup)
                    {
                        case "Under 12":
                            return "athletesmale.yearOfBirth > " + (DateTime.Now.Year - 12) + " ";
                        case "Under 13":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 12) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 13) + "";
                        case "Under 15":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 13) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 15) + "";
                        case "Under 17":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 15) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 17) + "";
                        case "Under 19":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 17) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 19) + "";
                        case "Under 23":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 19) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 23) + "";
                        case "Seniors":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 23) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 35) + "";
                        case "Masters 1":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 23) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 35) + "";
                        case "Masters 2":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 40) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 45) + "";
                        case "Masters 3":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 45) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 50) + "";
                        case "Masters 4":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 50) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 55) + "";
                        case "Masters 5":
                            return "athletesmale.yearOfBirth <= " + (DateTime.Now.Year - 55) +
                                   " AND athletesmale.yearOfBirth > " + (DateTime.Now.Year - 110) + "";
                        default:
                            return "";
                    }
                case Enumerations.Gender.Female:
                    switch (ageGroup)
                    {
                        case "Under 12":
                            return "athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 12) + " ";
                        case "Under 13":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 12) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 13) + "";
                        case "Under 15":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 13) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 15) + "";
                        case "Under 17":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 15) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 17) + "";
                        case "Under 19":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 17) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 19) + "";
                        case "Under 23":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 19) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 23) + "";
                        case "Seniors":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 23) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 35) + "";
                        case "Masters 1":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 23) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 35) + "";
                        case "Masters 2":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 40) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 45) + "";
                        case "Masters 3":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 45) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 50) + "";
                        case "Masters 4":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 50) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 55) + "";
                        case "Masters 5":
                            return "athletesfemale.yearOfBirth <= " + (DateTime.Now.Year - 55) +
                                   " AND athletesfemale.yearOfBirth > " + (DateTime.Now.Year - 110) + "";
                        default:
                            return "";
                    }
                default:
                    return "";
            }
        }
        private string GetLeaderboardSqlString(Enumerations.Gender gender, string season, string selectedEvent, Enumerations.Region region, Enumerations.CompetitionType competitionType, string ageGroup)
        {
            var ageGroupString = GetAgeSqlString(ageGroup, gender);
            var eventId = GetEventId(selectedEvent, competitionType);
            //Nested Switch statement to determaine the leaderboard SQL String
            switch (ageGroupString)
            {
                case "":
                    switch (gender)
                    {
                        case Enumerations.Gender.Male:
                            switch (competitionType)
                            {
                                case Enumerations.CompetitionType.Indoor:
                                    switch (region)
                                    {
                                        case Enumerations.Region.AllIreland:
                                            if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 || eventId == 13 || eventId == 14)
                                                return "Select athletesmaleseasonsbestindoors.performance, "
                                                       + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestindoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestindoors.athleteMaleID) " 
                                                       + "INNER JOIN competitions ON athletesmaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                       + "Where athletesmaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestindoors.eventID = " + eventId + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesmaleseasonsbestindoors.performance, "
                                                   + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestindoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestindoors.athleteMaleID) "
                                                   + "INNER JOIN competitions ON athletesmaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                   + "Where athletesmaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestindoors.eventID = " + eventId + " "
                                                   + "order by performance+0"; //Ascending order
                                        default://Region Selected
                                            if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 || eventId == 13 || eventId == 14)
                                                return "Select athletesmaleseasonsbestindoors.performance, "
                                                       + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestindoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestindoors.athleteMaleID) "
                                                       + "INNER JOIN competitions ON athletesmaleseasonsbestindoors.competitionID = competitions.competitionID " 
                                                       + "Where athletesmaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestindoors.eventID = " + eventId + " AND athletesmale.regionID = " + (int)region + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesmaleseasonsbestindoors.performance, "
                                                   + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestindoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestindoors.athleteMaleID) " 
                                                   + "INNER JOIN competitions ON athletesmaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                   + "Where athletesmaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestindoors.eventID = " + eventId + " AND athletesmale.regionID = " + (int)region + " "
                                                   + "order by performance+0"; //Ascending order
                                    }
                                default: //If Outdoor Competitions
                                    switch (region)
                                    {
                                        case Enumerations.Region.AllIreland:
                                            if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 || eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 || eventId == 36 || eventId == 37)
                                                return "Select athletesmaleseasonsbestoutdoors.performance, "
                                                       + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestoutdoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestoutdoors.athleteMaleID) "
                                                       + "INNER JOIN competitions ON athletesmaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                       + "Where athletesmaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestoutdoors.eventID = " + eventId + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesmaleseasonsbestoutdoors.performance, "
                                                   + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestoutdoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestoutdoors.athleteMaleID) "
                                                   + "INNER JOIN competitions ON athletesmaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                   + "Where athletesmaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestoutdoors.eventID = " + eventId + " "
                                                   + "order by performance+0"; //Ascending order
                                        default://Region Selected
                                            if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 || eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 || eventId == 36 || eventId == 37)
                                                return "Select athletesmaleseasonsbestoutdoors.performance, "
                                                       + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestoutdoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestoutdoors.athleteMaleID) "
                                                       + "INNER JOIN competitions ON athletesmaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                       + "Where athletesmaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestoutdoors.eventID = " + eventId + " AND athletesmale.regionID = " + (int)region + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesmaleseasonsbestoutdoors.performance, "
                                                   + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestoutdoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestoutdoors.athleteMaleID) "
                                                   + "INNER JOIN competitions ON athletesmaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                   + "Where athletesmaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestoutdoors.eventID = " + eventId + " AND athletesmale.regionID = " + (int)region + " "
                                                   + "order by performance+0"; //Ascending order
                                    }
                            }
                        default: //Female Leaderboard
                            switch (competitionType)
                            {
                                case Enumerations.CompetitionType.Indoor:
                                    switch (region)
                                    {
                                        case Enumerations.Region.AllIreland:
                                            if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 ||eventId == 13 || eventId == 14)
                                                return "Select athletesfemaleseasonsbestindoors.performance, "
                                                       +"athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       +"FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestindoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestindoors.athletefemaleID) "
                                                       +"INNER JOIN competitions ON athletesfemaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                       +"Where athletesfemaleseasonsbestindoors.season = " +Convert.ToInt32(season) +" AND athletesfemaleseasonsbestindoors.eventID = " + eventId + " "
                                                       +"order by performance+0 DESC"; //decending order
                                            return "Select athletesfemaleseasonsbestindoors.performance, "
                                                   + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestindoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestindoors.athletefemaleID) "
                                                   + "INNER JOIN competitions ON athletesfemaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                   + "Where athletesfemaleseasonsbestindoors.season = " +Convert.ToInt32(season) +" AND athletesfemaleseasonsbestindoors.eventID = " + eventId + " "
                                                   + "order by performance+0"; //Ascending order
                                        default: //Region Selected
                                            if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 ||eventId == 13 || eventId == 14)
                                                return "Select athletesfemaleseasonsbestindoors.performance, "
                                                       + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestindoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestindoors.athletefemaleID) "
                                                       + "INNER JOIN competitions ON athletesfemaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                       + "Where athletesfemaleseasonsbestindoors.season = " +Convert.ToInt32(season) +" AND athletesfemaleseasonsbestindoors.eventID = " + eventId +" AND athletesfemale.regionID = " + (int) region + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesfemaleseasonsbestindoors.performance, "
                                                   + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestindoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestindoors.athletefemaleID) "
                                                   + "INNER JOIN competitions ON athletesfemaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                   + "Where athletesfemaleseasonsbestindoors.season = " +Convert.ToInt32(season) +" AND athletesfemaleseasonsbestindoors.eventID = " + eventId +" AND athletesfemale.regionID = " + (int) region + " "
                                                   + "order by performance+0"; //Ascending order
                                    }
                                default: //If Outdoor Competitions
                                    switch (region)
                                    {
                                        case Enumerations.Region.AllIreland:
                                            if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 ||eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 ||eventId == 36 || eventId == 37)
                                                return "Select athletesfemaleseasonsbestoutdoors.performance, "
                                                       + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestoutdoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestoutdoors.athletefemaleID) "
                                                       + "INNER JOIN competitions ON athletesfemaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                       + "Where athletesfemaleseasonsbestoutdoors.season = " +Convert.ToInt32(season) +" AND athletesfemaleseasonsbestoutdoors.eventID = " + eventId +" "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesfemaleseasonsbestoutdoors.performance, "
                                                   + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestoutdoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestoutdoors.athletefemaleID) "
                                                   + "INNER JOIN competitions ON athletesfemaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                   + "Where athletesfemaleseasonsbestoutdoors.season = " +Convert.ToInt32(season) +" AND athletesfemaleseasonsbestoutdoors.eventID = " + eventId + " "
                                                   + "order by performance+0"; //Ascending order
                                        default: //Region Selected
                                            if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 ||eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 ||eventId == 36 || eventId == 37)
                                                return "Select athletesfemaleseasonsbestoutdoors.performance, "
                                                       + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestoutdoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestoutdoors.athletefemaleID) "
                                                       + "INNER JOIN competitions ON athletesfemaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                       + "Where athletesfemaleseasonsbestoutdoors.season = " +Convert.ToInt32(season) +" AND athletesfemaleseasonsbestoutdoors.eventID = " + eventId +" AND athletesfemale.regionID = " + (int) region + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesfemaleseasonsbestoutdoors.performance, "
                                                   + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestoutdoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestoutdoors.athletefemaleID) "
                                                   + "INNER JOIN competitions ON athletesfemaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                   + "Where athletesfemaleseasonsbestoutdoors.season = " +Convert.ToInt32(season) +" AND athletesfemaleseasonsbestoutdoors.eventID = " + eventId +" AND athletesfemale.regionID = " + (int) region + " "
                                                   + "order by performance+0"; //Ascending order
                                    }
                            }
                    }
                default: //Age was selected
                    switch (gender)
                    {
                        case Enumerations.Gender.Male:
                            switch (competitionType)
                            {
                                case Enumerations.CompetitionType.Indoor:
                                    switch (region)
                                    {
                                        case Enumerations.Region.AllIreland:
                                            if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 || eventId == 13 || eventId == 14)
                                                return "Select athletesmaleseasonsbestindoors.performance, "
                                                       + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestindoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestindoors.athleteMaleID) "
                                                       + "INNER JOIN competitions ON athletesmaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                       + "Where athletesmaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestindoors.eventID = " + eventId + " AND " + ageGroupString + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesmaleseasonsbestindoors.performance, "
                                                   + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestindoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestindoors.athleteMaleID) "
                                                   + "INNER JOIN competitions ON athletesmaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                   + "Where athletesmaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestindoors.eventID = " + eventId + " AND " + ageGroupString + " "
                                                   + "order by performance+0"; //Ascending order
                                        default://Region Selected
                                            if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 || eventId == 13 || eventId == 14)
                                                return "Select athletesmaleseasonsbestindoors.performance, "
                                                       + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestindoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestindoors.athleteMaleID) "
                                                       + "INNER JOIN competitions ON athletesmaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                       + "Where athletesmaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestindoors.eventID = " + eventId + " AND athletesmale.regionID = " + (int)region + " AND " + ageGroupString + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesmaleseasonsbestindoors.performance, "
                                                   + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestindoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestindoors.athleteMaleID) "
                                                   + "INNER JOIN competitions ON athletesmaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                   + "Where athletesmaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestindoors.eventID = " + eventId + " AND athletesmale.regionID = " + (int)region + " AND " + ageGroupString + " "
                                                   + "order by performance+0"; //Ascending order
                                    }
                                default: //If Outdoor Competitions
                                    switch (region)
                                    {
                                        case Enumerations.Region.AllIreland:
                                            if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 || eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 || eventId == 36 || eventId == 37)
                                                return "Select athletesmaleseasonsbestoutdoors.performance, "
                                                       + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestoutdoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestoutdoors.athleteMaleID) "
                                                       + "INNER JOIN competitions ON athletesmaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                       + "Where athletesmaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestoutdoors.eventID = " + eventId + " AND " + ageGroupString + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesmaleseasonsbestoutdoors.performance, "
                                                   + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestoutdoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestoutdoors.athleteMaleID) "
                                                   + "INNER JOIN competitions ON athletesmaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                   + "Where athletesmaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestoutdoors.eventID = " + eventId + " AND " + ageGroupString + " "
                                                   + "order by performance+0"; //Ascending order
                                        default://Region Selected
                                            if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 || eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 || eventId == 36 || eventId == 37)
                                                return "Select athletesmaleseasonsbestoutdoors.performance, "
                                                       + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestoutdoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestoutdoors.athleteMaleID) "
                                                       + "INNER JOIN competitions ON athletesmaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                       + "Where athletesmaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestoutdoors.eventID = " + eventId + " AND athletesmale.regionID = " + (int)region + " AND " + ageGroupString + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesmaleseasonsbestoutdoors.performance, "
                                                   + "athletesmale.name, athletesmale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesmale INNER JOIN athletesmaleseasonsbestoutdoors ON athletesmale.athleteMaleID = athletesmaleseasonsbestoutdoors.athleteMaleID) "
                                                   + "INNER JOIN competitions ON athletesmaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                   + "Where athletesmaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesmaleseasonsbestoutdoors.eventID = " + eventId + " AND athletesmale.regionID = " + (int)region + " AND " + ageGroupString + " "
                                                   + "order by performance+0"; //Ascending order
                                    }
                            }
                        default: //Female Leaderboard
                            switch (competitionType)
                            {
                                case Enumerations.CompetitionType.Indoor:
                                    switch (region)
                                    {
                                        case Enumerations.Region.AllIreland:
                                            if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 || eventId == 13 || eventId == 14)
                                                return "Select athletesfemaleseasonsbestindoors.performance, "
                                                       + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestindoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestindoors.athletefemaleID) "
                                                       + "INNER JOIN competitions ON athletesfemaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                       + "Where athletesfemaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesfemaleseasonsbestindoors.eventID = " + eventId + " AND " + ageGroupString + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesfemaleseasonsbestindoors.performance, "
                                                   + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestindoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestindoors.athletefemaleID) "
                                                   + "INNER JOIN competitions ON athletesfemaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                   + "Where athletesfemaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesfemaleseasonsbestindoors.eventID = " + eventId + " AND " + ageGroupString + " "
                                                   + "order by performance+0"; //Ascending order
                                        default: //Region Selected
                                            if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 || eventId == 13 || eventId == 14)
                                                return "Select athletesfemaleseasonsbestindoors.performance, "
                                                       + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestindoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestindoors.athletefemaleID) "
                                                       + "INNER JOIN competitions ON athletesfemaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                       + "Where athletesfemaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesfemaleseasonsbestindoors.eventID = " + eventId + " AND athletesfemale.regionID = " + (int)region + " AND " + ageGroupString + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesfemaleseasonsbestindoors.performance, "
                                                   + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestindoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestindoors.athletefemaleID) "
                                                   + "INNER JOIN competitions ON athletesfemaleseasonsbestindoors.competitionID = competitions.competitionID "
                                                   + "Where athletesfemaleseasonsbestindoors.season = " + Convert.ToInt32(season) + " AND athletesfemaleseasonsbestindoors.eventID = " + eventId + " AND athletesfemale.regionID = " + (int)region + " AND " + ageGroupString + " "
                                                   + "order by performance+0"; //Ascending order
                                    }
                                default: //If Outdoor Competitions
                                    switch (region)
                                    {
                                        case Enumerations.Region.AllIreland:
                                            if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 || eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 || eventId == 36 || eventId == 37)
                                                return "Select athletesfemaleseasonsbestoutdoors.performance, "
                                                       + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestoutdoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestoutdoors.athletefemaleID) "
                                                       + "INNER JOIN competitions ON athletesfemaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                       + "Where athletesfemaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesfemaleseasonsbestoutdoors.eventID = " + eventId + " AND " + ageGroupString + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesfemaleseasonsbestoutdoors.performance, "
                                                   + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestoutdoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestoutdoors.athletefemaleID) "
                                                   + "INNER JOIN competitions ON athletesfemaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                   + "Where athletesfemaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesfemaleseasonsbestoutdoors.eventID = " + eventId + " AND " + ageGroupString + " "
                                                   + "order by performance+0"; //Ascending order
                                        default: //Region Selected
                                            if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 || eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 || eventId == 36 || eventId == 37)
                                                return "Select athletesfemaleseasonsbestoutdoors.performance, "
                                                       + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                       + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestoutdoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestoutdoors.athletefemaleID) "
                                                       + "INNER JOIN competitions ON athletesfemaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                       + "Where athletesfemaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesfemaleseasonsbestoutdoors.eventID = " + eventId + " AND athletesfemale.regionID = " + (int)region + " AND " + ageGroupString + " "
                                                       + "order by performance+0 DESC"; //decending order
                                            return "Select athletesfemaleseasonsbestoutdoors.performance, "
                                                   + "athletesfemale.name, athletesfemale.surname, competitions.competitionDate, competitions.competitionVenue, competitions.competitionName "
                                                   + "FROM (athletesfemale INNER JOIN athletesfemaleseasonsbestoutdoors ON athletesfemale.athletefemaleID = athletesfemaleseasonsbestoutdoors.athletefemaleID) "
                                                   + "INNER JOIN competitions ON athletesfemaleseasonsbestoutdoors.competitionID = competitions.competitionID "
                                                   + "Where athletesfemaleseasonsbestoutdoors.season = " + Convert.ToInt32(season) + " AND athletesfemaleseasonsbestoutdoors.eventID = " + eventId + " AND athletesfemale.regionID = " + (int)region + " AND " + ageGroupString + " "
                                                   + "order by performance+0"; //Ascending order
                                    }
                            }
                    }
            }

        }
        private int GetEventId(string eventName, Enumerations.CompetitionType competitionType)
        {
            try
            {
                var sqlQuery = ("SELECT eventsID FROM events WHERE (eventName = '" + eventName + "') AND (typeID = " + (int)competitionType + ")");

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                return Convert.ToInt32(databaseCommand.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private int GetClubId(int regionId,string clubName)
        {
            clubName = clubName.Replace("'", "''");
            try
            {
                var sqlQuery = "SELECT clubsID FROM clubs "
                    + "WHERE clubNames = '" + clubName + "' AND regionID = " + regionId + "";

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                return Convert.ToInt32(databaseCommand.ExecuteScalar());

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private int GetNextCompetitionId()
        {
            try
            {
                const string sqlQuery = ("SELECT Max(competitionID)+1 FROM competitions ");
                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var returnVal = Convert.ToInt32(databaseCommand.ExecuteScalar());
                return returnVal > 0 ? returnVal : 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private int GetNextClubId()
        {
            try
            {
                const string sqlQuery = ("SELECT Max(clubsID)+1 FROM clubs ");
                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var returnVal = Convert.ToInt32(databaseCommand.ExecuteScalar());
                return returnVal > 0 ? returnVal : 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private int GetNextResultId(Enumerations.Gender gender, Enumerations.CompetitionType competitionType)
        {
            try
            {
                string sqlQuery;
                if (gender == Enumerations.Gender.Male)
                {
                    sqlQuery = competitionType == Enumerations.CompetitionType.Indoor ? 
                        "SELECT Max(resultsMaleIndoorID)+1 FROM resultsmaleindoors " : "SELECT Max(resultsMaleOutdoorID)+1 FROM resultsmaleoutdoors ";
                }
                else
                {
                    sqlQuery = competitionType == Enumerations.CompetitionType.Indoor ?
                        "SELECT Max(resultsFemaleIndoorID)+1 FROM resultsfemaleindoors " : "SELECT Max(resultsFemaleOutdoorID)+1 FROM resultsfemaleoutdoors ";
                }
                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var returnVal = Convert.ToInt32(databaseCommand.ExecuteScalar());
                return returnVal > 0 ? returnVal : 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private int GetNextAthleteId(Enumerations.Gender gender)
        {
            try
            {
                var sqlQuery = gender == Enumerations.Gender.Male ? 
                    "SELECT Max(athleteMaleID)+1 FROM athletesmale " : "SELECT Max(athleteFemaleID)+1 FROM athletesfemale ";

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var returnVal = Convert.ToInt32(databaseCommand.ExecuteScalar());
                return returnVal > 0 ? returnVal : 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Setter Methods / Mutator
        /// <summary>Add a competition to the database</summary>
        /// <param name="competitionName">The competition name</param>
        /// <param name="competitionCountry">The country the competition is taking place</param>
        /// <param name="region">Thr region</param>
        /// <param name="venue">The venue</param>
        /// <param name="date">The date of the competition</param>
        /// <param name="competitionType">Indoor or Outdoor etc</param>
        public void AddCompetition(string competitionName, string competitionCountry, string region, string venue, string date, Enumerations.CompetitionType competitionType)
        {
            try
            {
                var regionId = GetRegionId(region);
                var sqlQuery = "SELECT * FROM competitions "
                    + "WHERE competitionName = '" + competitionName + "' AND competitionCountry = '" + competitionCountry + "' "
                    + "AND regionID = " + regionId + " AND competitionVenue = '" + venue + "' AND competitionTypeID = " + (int)competitionType + " "
                    + "AND competitionSeason = " + date.Substring(6, 4) + "";

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();
                if (databaseDataReader != null && databaseDataReader.HasRows)
                {
                    throw new Exception("Dublicate competitions not allowed!");
                }

                var id = GetNextCompetitionId();
                sqlQuery = "INSERT INTO competitions(competitionID, competitionName, regionID, competitionVenue, competitionDate, competitionCountry, competitiontypeID, competitionSeason)"
                    + "Values (" + id + ",'" + competitionName + "'," + regionId + ", '" + venue + "','" + date + "','" + competitionCountry + "'," + (int)competitionType + " ," + date.Substring(6, 4) + ")";
                databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Add a club to the database</summary>
        /// <param name="clubName">The club name</param>
        /// <param name="region">The club region</param>
        public void AddClub(string clubName, string region)
        {
            try
            {
                var regionId = GetRegionId(region);
                var sqlQuery = "SELECT * FROM clubs "
                    + "WHERE (regionID = " + regionId + ") AND (clubNames = '" + clubName + "' ";

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();
                if (databaseDataReader != null && databaseDataReader.HasRows)
                {
                    throw new Exception("Dublicate club not allowed!");
                }

                var id = GetNextClubId();
                sqlQuery = "INSERT INTO clubs(clubsID, regionID, clubNames)"
                    + "Values (" + id + "," + regionId + ", '" + clubName + "')";
                databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>Add an athlete into the database</summary>
        /// <param name="gender">The athlete's gender</param>
        /// <param name="name">The athletes name</param>
        /// <param name="surname">The athlete's surname</param>
        /// <param name="yob">The athletes year of birth</param>
        /// <param name="region">The athlete's club region</param>
        /// <param name="club">The athlete's club name</param>
        public void AddAthlete(Enumerations.Gender gender,string name, string surname,int yob,string region, string club)
        {
            try
            {
                var regionId = GetRegionId(region);
                var clubId = GetClubId(regionId, club);
                string sqlQuery;
                if(gender == Enumerations.Gender.Male)
                {
                    sqlQuery = "SELECT * FROM athletesmale "
                               + "WHERE (regionID = " + regionId + " AND name = '" + name + "' AND surname = '" +surname + "' "
                               + "AND clubID = " + clubId + " AND yearOfBirth = " + yob + ") ";
                }
                else
                {
                    sqlQuery = "SELECT * FROM athletesfemale "
                               + "WHERE (regionID = " + regionId + " AND name = '" + name + "' AND surname = '" + surname + "' "
                               + "AND clubID = " + clubId + " AND yearOfBirth = " + yob + ") ";
                }

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();
                if (databaseDataReader != null && databaseDataReader.HasRows)
                {
                    throw new Exception("Dublicate athletes not allowed!");
                }

                var id = GetNextAthleteId(gender);
                if (gender == Enumerations.Gender.Male)
                {
                    sqlQuery = "INSERT INTO athletesmale(athleteMaleID, name, surname,regionID, clubID, yearOfBirth) "
                               + "Values (" + id + ",'" + name + "','" + surname + "'," + regionId + ", " +
                               clubId + ", " + yob + ")";
                }
                else
                {
                    sqlQuery = "INSERT INTO athletesfemale(athleteFemaleID, name, surname,regionID, clubID, yearOfBirth) "
                               + "Values (" + id + ",'" + name + "','" + surname + "'," + regionId + ", " +
                               clubId + ", " + yob + ")";
                }
                databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Add a result to the database</summary>
        /// <param name="selectedEvent">The event in which the results are being entered for</param>
        /// <param name="performance">The athletes performance</param>
        /// <param name="athleteId">The athlete's id</param>
        /// <param name="competitionId">The competition Id</param>
        /// <param name="position">The event result position of the athlete</param>
        /// <param name="gender">The athletes gender</param>
        /// <param name="competitionType">If the competition is indoor or out door</param>
        /// <param name="season">The season the result is for</param>
        public void AddResults(string selectedEvent, string performance, int athleteId, int competitionId, string position, Enumerations.Gender gender, Enumerations.CompetitionType competitionType, int season)
        {
            try
            {
                var eventId = GetEventId(selectedEvent, competitionType);
                //Check if the current result is already in the database
                string sqlQuery;
                if (gender == Enumerations.Gender.Male)
                {
                    if (competitionType == Enumerations.CompetitionType.Indoor)
                    {
                        sqlQuery = "SELECT * FROM resultsmaleindoors "
                                   + "WHERE (eventID = " + eventId + ") AND (performance = '" + performance + "') AND (athleteMaleID = " + athleteId + ") "
                                   + "AND (competitionID = " + competitionId + ") AND (position = '" + position + "') ";    
                    }
                    else
                    {
                        sqlQuery = "SELECT * FROM resultsmaleoutdoors "
                                   + "WHERE (eventID = " + eventId + ") AND (performance = '" + performance + "') AND (athleteMaleID = " + athleteId + ") "
                                   + "AND (competitionID = " + competitionId + ") AND (position = '" + position + "') ";  
                    }    
                }
                else
                {
                    if (competitionType == Enumerations.CompetitionType.Indoor)
                    {
                        sqlQuery = "SELECT * FROM resultsfemaleindoors "
                                   + "WHERE (eventID = " + eventId + ") AND (performance = '" + performance + "') AND (athleteFemaleID = " + athleteId + ") "
                                   + "AND (competitionID = " + competitionId + ") AND (position = '" + position + "') ";
                    }
                    else
                    {
                        sqlQuery = "SELECT * FROM resultsfemaleoutdoors "
                                   + "WHERE (eventID = " + eventId + ") AND (performance = '" + performance + "') AND (athleteFemaleID = " + athleteId + ") "
                                   + "AND (competitionID = " + competitionId + ") AND (position = '" + position + "') ";
                    }  
                }

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                var databaseDataReader = databaseCommand.ExecuteReader();
                if (databaseDataReader != null && databaseDataReader.HasRows)
                {
                    throw new Exception("Dublicate results not allowed!");
                }

                //Insert new result
                var nextId = GetNextResultId(gender, competitionType);
                if (gender == Enumerations.Gender.Male)
                {
                    if (competitionType == Enumerations.CompetitionType.Indoor)
                    {
                        sqlQuery = "INSERT INTO resultsmaleindoors(resultsMaleIndoorID, performance, competitionID, athleteMaleID, eventID, position) "
                                   + "Values (" + nextId + ", '" + performance + "'," + competitionId + "," + athleteId + ", " + eventId + ", '" + position + "') ";
                    }
                    else
                    {
                        sqlQuery = "INSERT INTO resultsmaleoutdoors(resultsMaleOutdoorID, performance, competitionID, athleteMaleID, eventID, position) "
                                   + "Values (" + nextId + ", '" + performance + "'," + competitionId + "," + athleteId + ", " + eventId + ", '" + position + "') ";
                    }
                }
                else
                {
                    if (competitionType == Enumerations.CompetitionType.Indoor)
                    {
                        sqlQuery = "INSERT INTO resultsfemaleindoors(resultsFemaleIndoorID, performance, competitionID, athleteMaleID, eventID, position) "
                                   + "Values (" + nextId + ", '" + performance + "'," + competitionId + "," + athleteId + ", " + eventId + ", '" + position + "') ";
                    }
                    else
                    {
                        sqlQuery = "INSERT INTO resultsfemaleoutdoors(resultsFemaleOutdoorID, performance, competitionID, athleteMaleID, eventID, position) "
                                   + "Values (" + nextId + ", '" + performance + "'," + competitionId + "," + athleteId + ", " + eventId + ", '" + position + "') ";
                    }
                }

                databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseCommand.ExecuteNonQuery();

                //Check if current result is better than previous if any
                var update = false;
                var itEnteredLoop = false;

                if (gender == Enumerations.Gender.Male)
                {
                    if (competitionType == Enumerations.CompetitionType.Indoor)
                    {
                        sqlQuery = "SELECT performance , bestID FROM athletesmaleseasonsbestindoors ";
                        sqlQuery += "WHERE (eventID = " + eventId + ") AND (athleteMaleID = " + athleteId + ") AND (season = " + season + ") ";
                    }
                    else
                    {
                        sqlQuery = "SELECT performance , bestID FROM athletesmaleseasonsbestoutdoors ";
                        sqlQuery += "WHERE (eventID = " + eventId + ") AND (athleteMaleID = " + athleteId + ") AND (season = " + season + ") ";
                    }
                }
                else
                {
                    if (competitionType == Enumerations.CompetitionType.Indoor)
                    {
                        sqlQuery = "SELECT performance , bestID FROM athletesfemaleseasonsbestindoors ";
                        sqlQuery += "WHERE (eventID = " + eventId + ") AND (athleteFemaleID = " + athleteId + ") AND (season = " + season + ") ";
                    }
                    else
                    {
                        sqlQuery = "SELECT performance , bestID FROM athletesfemaleseasonsbestoutdoors ";
                        sqlQuery += "WHERE (eventID = " + eventId + ") AND (athleteFemaleID = " + athleteId + ") AND (season = " + season + ") ";
                    }
                }

                databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseDataReader = databaseCommand.ExecuteReader();
                while (databaseDataReader != null && databaseDataReader.HasRows)
                {
                    if (!(Convert.IsDBNull(databaseDataReader["performance"])))
                        {
                            var databaseTime = databaseDataReader["performance"].ToString();
                            var currentDatabaseId = int.Parse(databaseDataReader["bestID"].ToString());

                            if (gender == Enumerations.Gender.Male)
                            {
                                if (competitionType == Enumerations.CompetitionType.Indoor) //Indoors
                                {
                                    //Indoor Field Event
                                    //if new is greater than old and it is any field events
                                    if (String.CompareOrdinal(performance, databaseTime) > 0)
                                    {
                                        if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 || eventId == 13 || eventId == 14)
                                        {
                                            sqlQuery = "UPDATE athletesmaleseasonsbestindoors "
                                                + "SET bestID = " + currentDatabaseId + ", performance = '" + performance + "' ,position = '" + position + "', eventID = " + eventId + ", athleteMaleID = " + athleteId + ", competitionID = " + competitionId + " , season = " + season + " "
                                                + "WHERE bestID = " + currentDatabaseId + " ";
                                            update = true;
                                        }
                                        //Indoor Track Event
                                        //if new is less than old and its not field event
                                    }
                                    else if (String.CompareOrdinal(performance, databaseTime) < 0)
                                    {
                                        if (eventId == 1 || eventId == 2 || eventId == 3 || eventId == 4 || eventId == 5 || eventId == 6 || eventId == 7 || eventId == 8)
                                        {
                                            sqlQuery = "UPDATE athletesmaleseasonsbestindoors "
                                                + "SET bestID = " + currentDatabaseId + ", performance = '" + performance + "' ,position = '" + position + "', eventID = " + eventId + ", athleteMaleID = " + athleteId + ", competitionID = " + competitionId + " , season = " + season + " "
                                                + "WHERE bestID = " + currentDatabaseId + " ";
                                            update = true;
                                        }
                                    }
                                }
                                else //Outdoors Field Event
                                {
                                    //if new is greater than old and it is any field events
                                    if (String.CompareOrdinal(performance, databaseTime) > 0)
                                    {
                                        if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 || eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 || eventId == 36 || eventId == 37)
                                        {
                                            sqlQuery = "UPDATE athletesmaleseasonsbestoutdoors "
                                                + "SET bestID = " + currentDatabaseId + ", performance = '" + performance + "' ,position = '" + position + "', eventID = " + eventId + ", athleteMaleID = " + athleteId + ", competitionID = " + competitionId + " , season = " + season + " "
                                                + "WHERE bestID = " + currentDatabaseId + " ";
                                            update = true;
                                        }
                                    }
                                    else if (String.CompareOrdinal(performance, databaseTime) < 0)
                                    {
                                        if (eventId == 15 || eventId == 16 || eventId == 17 || eventId == 18 || eventId == 19 || eventId == 20 || eventId == 21 || eventId == 22 || eventId == 23 || eventId == 24 || eventId == 25 || eventId == 26 || eventId == 27)
                                        {
                                            sqlQuery = "UPDATE athletesmaleseasonsbestoutdoors "
                                                + "SET bestID = " + currentDatabaseId + ", performance = '" + performance + "' ,position = '" + position + "', eventID = " + eventId + ", athleteMaleID = " + athleteId + ", competitionID = " + competitionId + " , season = " + season + " "
                                                + "WHERE bestID = " + currentDatabaseId + " ";
                                            update = true;
                                        }
                                    }
                                }
                            }
                            else //Female
                            {
                                if (competitionType == Enumerations.CompetitionType.Indoor) //Indoors
                                {
                                    //indoors field events
                                    if (String.CompareOrdinal(performance, databaseTime) > 0)
                                    {
                                        if (eventId == 9 || eventId == 10 || eventId == 11 || eventId == 12 || eventId == 13 || eventId == 14)
                                        {
                                            sqlQuery = "UPDATE athletesfemaleseasonsbestindoors "
                                                + "SET bestID = " + currentDatabaseId + ", performance = '" + performance + "' ,position = '" + position + "', eventID = " + eventId + ", athleteFemaleID = " + athleteId + ", competitionID = " + competitionId + " , season = " + season + " "
                                                + "WHERE bestID = " + currentDatabaseId + "";
                                            update = true;
                                        }
                                    }
                                    else if (String.CompareOrdinal(performance, databaseTime) < 0)
                                    {
                                        if (eventId == 1 || eventId == 2 || eventId == 3 || eventId == 4 || eventId == 5 || eventId == 6 || eventId == 7 || eventId == 8)
                                        {
                                            sqlQuery = "UPDATE athletesfemaleseasonsbestindoors "
                                                + "SET bestID = " + currentDatabaseId + ", performance = '" + performance + "' ,position = '" + position + "', eventID = " + eventId + ", athleteFemaleID = " + athleteId + ", competitionID = " + competitionId + " , season = " + season + " "
                                                + "WHERE bestID = " + currentDatabaseId + "";
                                            update = true;
                                        }
                                    }
                                }
                                else //Outdoors
                                {
                                    //If the new Track Event Time is better / lower
                                    if (String.CompareOrdinal(performance, databaseTime) > 0)
                                    {
                                        if (eventId == 28 || eventId == 29 || eventId == 30 || eventId == 31 || eventId == 32 || eventId == 33 || eventId == 34 || eventId == 35 || eventId == 36 || eventId == 37)
                                        {
                                            sqlQuery = "UPDATE athletesfemaleseasonsbestoutdoors "
                                                + "SET bestID = " + currentDatabaseId + ", performance = '" + performance + "' ,position = '" + position + "', eventID = " + eventId + ", athleteFemaleID = " + athleteId + ", competitionID = " + competitionId + " , season = " + season + " "
                                                + "WHERE bestID = " + currentDatabaseId + "";
                                            update = true;
                                        }
                                    }
                                    else if (String.CompareOrdinal(performance, databaseTime) < 0)
                                    {
                                        if (eventId == 15 || eventId == 16 || eventId == 17 || eventId == 18 || eventId == 19 || eventId == 20 || eventId == 21 || eventId == 22 || eventId == 23 || eventId == 24 || eventId == 25 || eventId == 26 || eventId == 27)
                                        {
                                            sqlQuery = "UPDATE athletesfemaleseasonsbestoutdoors "
                                                + "SET bestID = " + currentDatabaseId + ", performance = '" + performance + "' ,position = '" + position + "', eventID = " + eventId + ", athleteFemaleID = " + athleteId + ", competitionID = " + competitionId + " , season = " + season + " "
                                                + "WHERE bestID = " + currentDatabaseId + "";
                                            update = true;
                                        }
                                    }
                                }
                            }
                        }
                    itEnteredLoop = true;
                }

                if (!itEnteredLoop) return;
                if (!update) return;

                databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseCommand.ExecuteNonQuery();

                //Insert New Results
                if (gender == Enumerations.Gender.Male)
                {
                    //Set the sql command here
                    if (competitionType == Enumerations.CompetitionType.Indoor)
                    {
                        sqlQuery =
                            "INSERT INTO athletesmaleseasonsbestindoors(bestID, performance, position, eventID, athleteMaleID, competitionID, season) ";
                        sqlQuery += "Values (" + nextId + ",'" + performance + "','" + position + "'," + eventId +
                                    "," + athleteId + "," + competitionId + ",  " + season + ")";
                    }
                    else
                    {
                        sqlQuery =
                            "INSERT INTO athletesmaleseasonsbestoutdoors(bestID, performance, position, eventID, athleteMaleID, competitionID, season) ";
                        sqlQuery += "Values (" + nextId + ",'" + performance + "','" + position + "'," + eventId +
                                    "," + athleteId + "," + competitionId + ",  " + season + ")";
                    }
                }
                else
                {
                    if (competitionType == Enumerations.CompetitionType.Indoor) //Indoors
                    {
                        sqlQuery =
                            "INSERT INTO athletesfemaleseasonsbestindoors(bestID, performance, position, eventID, athleteFemaleID, competitionID, season) ";
                        sqlQuery += "Values (" + nextId + ",'" + performance + "','" + position + "'," + eventId +
                                    "," + athleteId + "," + competitionId + ",  " + season + ")";
                    }
                    else
                    {
                        sqlQuery =
                            "INSERT INTO athletesfemaleseasonsbestoutdoors(bestID, performance, position, eventID, athleteFemaleID, competitionID, season) ";
                        sqlQuery += "Values (" + nextId + ",'" + performance + "','" + position + "'," + eventId +
                                    "," + athleteId + "," + competitionId + ",  " + season + ")";
                    }
                }

                databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>Update an athlete's information</summary>
        /// <param name="athleteId">Athlete's ID</param>
        /// <param name="gender">Athlete's gender</param>
        /// <param name="region">Club region</param>
        /// <param name="club">Club name</param>
        /// <param name="yearOfBirth">Athlete's year of birth</param>
        public void UpdateAthleteInfo(int athleteId, string gender, string region, string club, int yearOfBirth)
        {
            try
            {
                var regionId = GetRegionId(region);
                var clubId = GetClubId(regionId, club);
                string sqlQuery;
                if (gender == "Male")
                {
                    sqlQuery = "Update athletesmale "
                        + "SET athletesmale.clubID = " + clubId + ", athletesmale.regionID = " + regionId + " , athletesmale.yearOfBirth = " + yearOfBirth + " "
                        + "WHERE (athletesmale.athleteMaleID = " + athleteId + ")";
                }
                else
                {
                    sqlQuery = "Update athletesFemale "
                        + "SET athlathletesFemaleetesMale.clubID = " + clubId + ", athletesfemale.regionID = " + regionId + " , athletesfemale.yearOfBirth = " + yearOfBirth + " "
                        + "WHERE (athletesfemale.athleteFemaleID = " + athleteId + ")";

                }

                var databaseCommand = new OleDbCommand(sqlQuery, _databaseConnection);
                databaseCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        #endregion
    }
}
