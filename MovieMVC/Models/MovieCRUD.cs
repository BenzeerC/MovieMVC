using System.Data.SqlClient;

namespace MovieMVC.Models
{
    public class MovieCRUD
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public MovieCRUD(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(configuration.GetConnectionString("defaultConnection"));
        }


        public IEnumerable<Movie> GetAllMovies()
        {
            List<Movie> list = new List<Movie>();
            string qry = "select * from movieMVC";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Movie m= new Movie();
                    m.Id = Convert.ToInt32(dr["id"]);
                    m.Name = dr["name"].ToString();
                    m.ReleaseDate = Convert.ToDateTime(dr["releasedate"]);
                    m.Type = dr["type"].ToString();
                    m.Stars = dr["stars"].ToString();
                    list.Add(m);


                }
            }
            con.Close();
            return list;
        }
        public Movie GetMovieById(int id)
        {
            Movie m = new Movie();
            string qry = "select * from movieMVC where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    m.Id = Convert.ToInt32(dr["id"]);
                    m.Name = dr["name"].ToString();
                    m.ReleaseDate = Convert.ToDateTime(dr["releasedate"]);
                    m.Type = dr["type"].ToString();
                    m.Stars = dr["stars"].ToString();
                }
            }
            con.Close();
            return m;
        }

        public int AddMovie(Movie movie)
            
        {
            
            int result = 0;
            string qry = "insert into movieMVC values(@name,@releasedate,@type,@stars)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name",movie.Name);
            cmd.Parameters.AddWithValue("@releasedate", movie.ReleaseDate);
            cmd.Parameters.AddWithValue("@type", movie.Type);
            cmd.Parameters.AddWithValue("@stars",movie.Stars);
            con.Open();

            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;


        }
        public int UpdateMovie(Movie movie)
        {
            
            int result = 0;
            string qry = "update movieMVC set name=@name,releasedate=@releasedate,type=@type,stars=@stars where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", movie.Name);
            cmd.Parameters.AddWithValue("@releasedate", movie.ReleaseDate);
            cmd.Parameters.AddWithValue("@type", movie.Type);
            cmd.Parameters.AddWithValue("@stars", movie.Stars);
            cmd.Parameters.AddWithValue("@id", movie.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }


        // soft delete --> record should be present in DB , but should not visible on the form
        public int DeleteMovie(int id)
        {
            int result = 0;
            string qry = "delete from movieMVC  where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }








    }

}



