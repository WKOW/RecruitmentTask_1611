namespace RecruitmentTask_1611;

public static class ApiEndpoints
{
    private const string BasePath = "/api/v1";

    public static class Paths
    {
        public const string Activities = "Activities";
        public const string Users = "Users";
        public const string Orders = "Orders";
        public const string Authors = "Authors";
        public const string Books = "Books";

        public static string GetEndpoint(string resource)
        {
            return $"{BasePath}/{resource}";
        }

        public static string GetResourceById(string resource, int id)
        {
            return $"{BasePath}/{resource}/{id}";
        }
    }

    public static string ActivitiesEndpoint => GetFullPath(Paths.Activities);
    public static string UsersEndpoint => GetFullPath(Paths.Users);
    public static string OrdersEndpoint => GetFullPath(Paths.Orders);
    public static string AuthorsEndpoint => GetFullPath(Paths.Authors);
    public static string BooksEndpoint => GetFullPath(Paths.Books);

    private static string GetFullPath(string resource)
    {
        return $"{BasePath}/{resource}";
    }
}