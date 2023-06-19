namespace Obaki.Toolkit.Application.Features.D4Timer;

public interface ID4HttpClient
{
     public Task<D4Boss> GetUpcomingBoss();
    
}