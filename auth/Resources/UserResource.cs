namespace auth.Resources
{
    public sealed record UserResource(int Id, string UserName, string Email, DateTime? CreatedAt);
}
