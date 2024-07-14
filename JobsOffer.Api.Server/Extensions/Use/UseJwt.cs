namespace JobsOffer.Api.Server.Extensions.Use;
public static class UseJwt
{
    public static void UseJWT(this IApplicationBuilder self)
    {
        self.UseAuthentication();
    }
}
