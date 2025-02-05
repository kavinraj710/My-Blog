using Microsoft.JSInterop;
using Supabase;
using System;
using System.Threading.Tasks;
using static Supabase.Postgrest.Constants;
using BlogApplication.Models;
namespace BlogApplication.Services
{
    public class SupabaseService
    {
        private static Client _supabaseClient;
        private static bool _isInitialized = false;
        private readonly IJSRuntime _jsRuntime;

        public SupabaseService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized)
            {
                Console.WriteLine("Supabase Client is already initialized.");
                return;
            }

            try
            {
                var url = "https://ktxqkibjwpocicuonflp.supabase.co";
                var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imt0eHFraWJqd3BvY2ljdW9uZmxwIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzM4MzIwNjEsImV4cCI6MjA0OTQwODA2MX0.BfUhs87MB5DO_10WAmXt8_KA3DU0X-Y0El1nA4v-ptM";

                _supabaseClient = new Client(url, key);
                await _supabaseClient.InitializeAsync(); // Initializes database and authentication
                _isInitialized = true;
                Console.WriteLine("Supabase Client Initialized Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase Initialization Error: {ex.Message}");
                throw;
            }
        }

        public Client GetClient()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Supabase client is not initialized. Call InitializeAsync() first.");
            }

            return _supabaseClient;
        }

        public async Task<bool> RegisterBlog(string Title, string Summary, string Content,string Email)
        {
            try
            {
                // Create a new instance of FavoriteCity
                var favorite = new BlogApplication.Models.blog
                {
                    id = Guid.NewGuid(), // Generate a new unique ID
                    title = Title,
                    summary = Summary,
                    content = Content,
                    email= Email
                };

                // Attempt to insert the favorite city into the Supabase table
                var insertResponse = await _supabaseClient.From<BlogApplication.Models.blog>().Insert(favorite);

                // Check if the insertion was successful
                if (insertResponse != null && insertResponse.Models != null && insertResponse.Models.Any())
                {

                    Console.WriteLine("Added to the Favorite Weather");
                    return true;
                }
                else
                {

                    Console.WriteLine("Failed to insert favorite city. Response was null or empty.");
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error while saving favorite city: {ex.Message}");
                return false;
            }
        }
        public async Task<List<blog>> GetBlogAsync(string Email)
        {

            try
            {
                // Query the Supabase database for the user by email
                var response = await _supabaseClient.From<blog>()
                    .Filter("email", Operator.Equals, Email)
                    .Get();

                // Check if the query returned any results
                if (response.Models != null && response.Models.Any())
                {
                    // Return the first matching user
                    return response.Models ?? new List<blog>();
                }
                else
                {
                    Console.WriteLine($"No user found with email: {Email}");
                }
            }
            catch (Exception ex)
            {

            }

            // Return null if no user is found or an error occurs
            return null;
        }
        public async Task SetCookie(string accessToken, string refreshToken)
        {
            Console.WriteLine("Setting Cookies");
            await _jsRuntime.InvokeVoidAsync("cookieHelper.setCookie", "authToken", accessToken, 7);
            Console.WriteLine("Cookies Set");
        }

        public ValueTask DeleteCookieAsync(string name)
        {
            return _jsRuntime.InvokeVoidAsync("cookieHelper.deleteCookie", name);
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                var response = await _supabaseClient.From<User>()
                    .Filter("email", Operator.Equals, email)
                    .Get();

                if (response.Models != null && response.Models.Any())
                {
                    return response.Models.First();
                }
                else
                {
                    Console.WriteLine($"No user found with email: {email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by email: {ex.Message}");
            }

            return null;
        }
    }
}
