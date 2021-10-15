using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TaleLearnCode;

namespace FunctionApp
{
	public class MovieApi
	{

		private readonly JsonSerializerOptions _jsonSerializerOptions;

		public MovieApi(JsonSerializerOptions jsonSerializerOptions)
		{
			_jsonSerializerOptions = jsonSerializerOptions;
		}

		[Function("GetMovies")]
		public async Task<HttpResponseData> GetAllMoviesAsync(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = "movies")] HttpRequestData request,
			FunctionContext executionContext)
		{

			var logger = executionContext.GetLogger("GetMovies");
			logger.LogInformation("GetMovies");

			using MoviesContext moviesContext = new MoviesContext();
			List<Movie> movies = moviesContext.Movies.ToList();
			return await request.CreateResponseAsync(movies, _jsonSerializerOptions);

		}

		[Function("GetMovie")]
		public async Task<HttpResponseData> GetMovieAsync(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = "movies/{movieId}")] HttpRequestData request,
			FunctionContext executionContext,
			string movieId)
		{

			var logger = executionContext.GetLogger("GetMovie");
			logger.LogInformation($"GetMovie : {movieId}");

			using MoviesContext moviesContext = new MoviesContext();
			Movie movie = moviesContext.Movies
				.Where(x => x.MovieId == int.Parse(movieId))
				.FirstOrDefault();

			return await request.CreateResponseAsync(movie, _jsonSerializerOptions);

		}

		[Function("CreateMovie")]
		public async Task<HttpResponseData> CreateMovieAsync(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = "movies")] HttpRequestData request,
			FunctionContext executionContext)
		{

			var logger = executionContext.GetLogger("CreateMovie");
			logger.LogInformation($"CreateMovie");

			Movie movieRequest = await request.GetRequestParametersAsync<Movie>(_jsonSerializerOptions);

			using MoviesContext moviesContext = new MoviesContext();
			Movie movie = new()
			{
				Title = movieRequest.Title,
				ReleaseDate = movieRequest.ReleaseDate
			};
			moviesContext.Movies.Add(movie);
			moviesContext.SaveChanges();

			return request.CreateCreatedResponse($"http://localhost:7071/movies/{movie.MovieId}");

		}

		[Function("UpdateMovie")]
		public async Task<HttpResponseData> UpdateMovieAsync(
			[HttpTrigger(AuthorizationLevel.Function, "put", Route = "movies/{movieId}")] HttpRequestData request,
			FunctionContext executionContext,
			string movieId)
		{

			var logger = executionContext.GetLogger("UpdateMovie");
			logger.LogInformation($"UpdateMovie : {movieId}");

			Movie movieRequest = await request.GetRequestParametersAsync<Movie>(_jsonSerializerOptions);

			using MoviesContext moviesContext = new MoviesContext();
			Movie movie = moviesContext.Movies
				.Where(x => x.MovieId == int.Parse(movieId))
				.FirstOrDefault();

			movie.Title = movieRequest.Title;
			movie.ReleaseDate = movieRequest.ReleaseDate;
			moviesContext.SaveChanges();

			return request.CreateResponse(HttpStatusCode.NoContent);

		}

		[Function("DeleteMovie")]
		public HttpResponseData DeleteMovie(
			[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "movies/{movieId}")] HttpRequestData request,
			FunctionContext executionContext,
			string movieId)
		{

			var logger = executionContext.GetLogger("UpdateMovie");
			logger.LogInformation($"UpdateMovie : {movieId}");

			using MoviesContext moviesContext = new MoviesContext();
			Movie movie = moviesContext.Movies
				.Where(x => x.MovieId == int.Parse(movieId))
				.FirstOrDefault();

			moviesContext.Movies.Remove(movie);
			moviesContext.SaveChanges();

			return request.CreateResponse(HttpStatusCode.OK);

		}

	}

}