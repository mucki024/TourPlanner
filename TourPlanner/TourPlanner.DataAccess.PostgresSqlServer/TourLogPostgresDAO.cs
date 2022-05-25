using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccess.Common;
using TourPlanner.DataAccess.DAO;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.PostgresSqlServer
{
    public class TourLogPostgresDAO : ITourLogDAO
    {
        private const string SQL_GET_BY_ID = "SELECT * FROM public.\"Logs\"WHERE id=@Id";
        private const string SQL_GET_BY_TID = "SELECT * FROM public.\"Logs\"WHERE tid=@tid";
        private const string SQL_INSERT_NEW_LOG = "INSERT INTO public.\"Logs\"" +
            "(\"tid\",\"date\",\"difficulty\",\"comment\",\"time\",\"rating\")" +
            "VALUES (@tid,@date,@difficulty,@comment,@time,@rating) " +
            "RETURNING \"id\"";
        private const string SQL_UPDATE_LOG = "UPDATE public.\"Logs\"" +
            "SET\"date\" =@date,\"difficulty\"=@difficulty,\"comment\"=@comment,\"time\"=@time,\"rating\"=@rating)"+
            "WHERE \"tid\" = @tid,";
        private const string SQL_GET_ROW_COUNT = "SELECT  count(*) FROM \"Logs\"";
        private IDatabase database;

        public TourLogPostgresDAO() //Factory Based Constructor
        {
            this.database=DALFactory.GetDatabase();
        }
        public TourLog AddNewTourLog(int tourId, string comment, int diffuclty, DateTime timestamp, TimeSpan totalTime, int rating)
        {//defines insert command with parameters
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_LOG);
            database.DefineParameter(insertCommand, "@tid", DbType.Int32, tourId);
            database.DefineParameter(insertCommand, "@date", DbType.DateTime, timestamp);
            database.DefineParameter(insertCommand, "@difficulty", DbType.Int32, diffuclty);
            database.DefineParameter(insertCommand, "@comment", DbType.String, comment);
            database.DefineParameter(insertCommand, "@time", DbType.Time, totalTime);
            database.DefineParameter(insertCommand, "@rating", DbType.Int32, rating);
            return GetById(database.ExecuteNonQuery(insertCommand)); // does the DB request
        }
        public TourLog UpdateTourLog(TourLog model)
        {//defines update command & returns the updated tour logs ID
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE_LOG);
            database.DefineParameter(updateCommand, "@tid", DbType.Int32, model.TourID);
            database.DefineParameter(updateCommand, "@date", DbType.DateTime, model.Timestamp);
            database.DefineParameter(updateCommand, "@difficulty", DbType.Int32, (int)model.Difficulty);
            database.DefineParameter(updateCommand, "@comment", DbType.String, model.Comment);
            database.DefineParameter(updateCommand, "@time", DbType.Time, model.TotalTime);
            database.DefineParameter(updateCommand, "@rating", DbType.Int32, model.Rating);
            return GetById(database.ExecuteNonQuery(updateCommand)); // does the DB request
        }

        public bool DeleteTourLog(TourLog model)
        {
            return true;
        }


        public TourLog GetById(int tourLogId) //find in DB by logid
        {
            DbCommand findCommand = database.CreateCommand(SQL_GET_BY_ID);
            database.DefineParameter(findCommand, "@id", DbType.Int32, tourLogId);
            //nullable warning need to think about what to do
            return QueryFromDB(findCommand).FirstOrDefault(); //only 1 option anyways since ID is PK but needs to be done like that
        }

        public IEnumerable<TourLog> GetLogsForTour(int tourId) //find in DB by tourid
        {
            DbCommand getforTourCommand=database.CreateCommand(SQL_GET_BY_TID);
            database.DefineParameter(getforTourCommand, "@tid", DbType.Int32, tourId);
            return QueryFromDB(getforTourCommand); 
        }

        private IEnumerable<TourLog> QueryFromDB(DbCommand command) //helps avoid repetition
        {
            List<TourLog> LogList = new List<TourLog>();
            using (IDataReader reader = database.ExecuteReader(command)) //reads the return from the DB request
            {
                while (reader.Read()) //reads out each row we got
                {
                    LogList.Add(new TourLog( //generates object for each row & instantly appends it
                        (int)reader["tid"],
                        (string)reader["comment"],
                        (Int16)reader["difficulty"],
                        (DateTime)reader["date"],//maybe need to do as string in db if not work right
                        (TimeSpan)reader["time"],//maybe need to do as string in db if not work right
                        (Int16)reader["rating"]

                        ));
                }
            }
            return LogList; //returns all of them. 
        }
    }
}
