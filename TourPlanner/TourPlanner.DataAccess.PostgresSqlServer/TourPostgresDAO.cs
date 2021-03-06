using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TourPlanner.DataAccess.Common;
using TourPlanner.DataAccess.DAO;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.PostgresSqlServer
{
    public class TourPostgresDAO : ITourDAO
    {
        private const string SQL_GET_BY_ID = "SELECT * FROM public.\"Tours\"WHERE id=@Id";
        private const string SQL_GET_TOURS = "SELECT * FROM public.\"Tours\"";
        private const string SQL_INSERT_NEW_LOG = "INSERT INTO public.\"Tours\"" +
            "(\"name\",\"description\",\"from\",\"to\",\"transport_type\",\"estimated_length\",\"distance\")" +
            "VALUES (@name,@description,@from,@to,@transport_type,@estimated_length,@distance) " +
            "RETURNING \"id\"";
        private const string SQL_UPDATE_TOUR = "UPDATE public.\"Tours\"" +
            "SET name = @name, description = @description "+
            "WHERE id=@id";
        private const string SQL_FULL_TEXT_SEARCH = "SELECT * FROM public.fulltextsearch(@param)";
        private IDatabase database;
        private ITourLogDAO logDAO;

        public TourPostgresDAO() //<----
        {
            this.database = DALFactory.GetDatabase();
            this.logDAO = DALFactory.CreateTourLogDAO(); 
        }
        public Tour AddNewTour(Tour tour)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_LOG);
            database.DefineParameter(insertCommand, "@name", DbType.String, tour.Tourname);
            database.DefineParameter(insertCommand, "@from", DbType.String, tour.Start);
            database.DefineParameter(insertCommand, "@to", DbType.String, tour.Destination);
            database.DefineParameter(insertCommand, "@transport_type", DbType.String, tour.TransportType);
            database.DefineParameter(insertCommand, "@description", DbType.String, tour.RouteInformation);
            database.DefineParameter(insertCommand, "@estimated_length", DbType.Time, tour.EstimatedTime); //initially set as 0 cause not sure how we will do it yet
            database.DefineParameter(insertCommand, "@distance", DbType.Double, tour.TourDistance); //initially set as 0 cause not sure how we will do it yet
            return FindById(database.ExecuteNonQuery(insertCommand)); // does the DB request
        }
        public Tour UpdateTour(Tour tour)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE_TOUR);
            database.DefineParameter(updateCommand, "@name", DbType.String, tour.Tourname);          
            database.DefineParameter(updateCommand, "@description", DbType.String, tour.RouteInformation);
            database.DefineParameter(updateCommand, "@id", DbType.Int32, tour.TourID);
            database.ExecuteNonQuery(updateCommand);
            return FindById(tour.TourID); // does the DB request
        }

        public IEnumerable<Tour> SearchForTours(string param)
        {
            DbCommand searchTourCommand = database.CreateCommand(SQL_FULL_TEXT_SEARCH);
            database.DefineParameter(searchTourCommand, "@param", DbType.String, param);
            return QueryFromDB(searchTourCommand);
        }

        public Tour FindById(int tourId)
        {
            DbCommand findCommand = database.CreateCommand(SQL_GET_BY_ID);
            database.DefineParameter(findCommand, "@id", DbType.Int32, tourId);
            //nullable warning need to think about what to do
            return QueryFromDB(findCommand).FirstOrDefault(); //only 1 option anyways since ID is PK but needs to be done like that
        }

        public IEnumerable<Tour> GetTours()
        {
            DbCommand getforTourCommand = database.CreateCommand(SQL_GET_TOURS);
            return QueryFromDB(getforTourCommand);
        }

        private IEnumerable<Tour> QueryFromDB(DbCommand command) //helps avoid repetition
        {
            List<Tour> TourList = new List<Tour>();
            using (IDataReader reader = database.ExecuteReader(command)) //reads the return from the DB request
            {
                while (reader.Read()) //reads out each row we got
                {
                    Tour toAdd = new Tour( //generates object for each row & instantly appends it
                        (int)reader["id"],
                        (string)reader["name"],
                        (string)reader["description"],
                        (string)reader["from"],
                        (string)reader["to"],
                        (string)reader["transport_type"],
                        (double)reader["distance"],
                        (TimeSpan)reader["estimated_length"]//maybe need to do as string in db if not work right
                        );
                    toAdd.AddLogs(logDAO.GetLogsForTour((int)reader["id"])); //it should really be an int in code but there are references on it
                    TourList.Add(toAdd);

                }
            }
            return TourList; //returns all of them. 
        }
    }
}
