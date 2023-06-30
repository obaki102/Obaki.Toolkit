namespace Obaki.Toolkit.Application.Features.D4Timer;

public static class D4TimerConstants
{
    public const string BaseAddress = "https://api.worldstone.io";
    public const string BaseAddressWithProxy = "https://corsproxy.io";

    public static class GetUpcomingBossWithProxy
    {
        public const string Endpoint = "?https://api.worldstone.io/world-bosses";
    }

    public static class GetUpcomingBoss
    {
        public const string Endpoint = "/world-bosses";
    }

}