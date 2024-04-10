using Topshelf;

namespace GostCryptographyAPI.TopShelf
{
    public static class ConfigureService
    {
        public static void Configure()
        {
            HostFactory.Run(x =>
            {
                x.Service<RestService>(s =>
                {
                    s.ConstructUsing(() => new RestService());
                    s.WhenStarted(rs => rs.Start());
                    s.WhenStopped(rs => rs.Stop());
                    s.WhenShutdown(rs => rs.Stop());
                });
                x.RunAs(username: @"username", password: "password");
                x.StartAutomatically();

                x.SetServiceName(".NET GostCryptography API OWIN service");
                x.SetDisplayName(".NET GostCryptography API OWIN service");
                x.SetDescription("Localhost API for GOST-cryptography operations");
            });
        }
    }
}
