using System.Timers;
using Obaki.Toolkit.Application.Features.D4Timer;
using Timer = System.Timers.Timer;

namespace Obaki.Toolkit.Client.Pages
{
    public partial class D4Timer : IDisposable
    {
        private static Timer? _timer;
        private static DateTime _baseTime;
        private string counter = string.Empty;
        private string bossCounter = string.Empty;
        private string message = string.Empty;
        private string bossMessage = string.Empty;
        private D4Boss? _upcomingBoss;
        private bool isOngoing = false;
        private TimeSpan _remainingTime;
        private TimeSpan _bossRemainingTime;
        protected override async Task OnInitializedAsync()
        {
            _upcomingBoss = await D4HttpClient.GetUpcomingBoss();
            if (_upcomingBoss is not null)
            {
                _bossRemainingTime = GetUpcomingBossRemainingTime(_upcomingBoss.Time);
                _baseTime = new DateTime(2023, 6, 13, 7, 0, 0, DateTimeKind.Utc);
                _remainingTime = GetRemainingTime();
                _timer = new(1000);
                _timer.Elapsed += TimerElapsed;
                _timer.Start();
            }
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
            _bossRemainingTime = _bossRemainingTime.Subtract(TimeSpan.FromSeconds(1));
            if (isOngoing)
            {
                counter = $"{_remainingTime.Minutes} minutes {_remainingTime.Seconds} seconds remaining";
            }
            else
            {
                if (_remainingTime.Hours > 0)
                {
                    counter = $"{_remainingTime.Hours} hour {_remainingTime.Minutes} minutes {_remainingTime.Seconds} seconds";
                }
                else
                {
                    counter = $"{_remainingTime.Minutes} minutes {_remainingTime.Seconds} seconds";
                }
            }

            if (_bossRemainingTime.Hours > 0)
            {
                bossCounter = $"{_bossRemainingTime.Hours} hour {_bossRemainingTime.Minutes} minutes {_bossRemainingTime.Seconds} seconds";
            }
            else
            {
                bossCounter = $"{_bossRemainingTime.Minutes} minutes {_bossRemainingTime.Seconds} seconds";
            }

            message = isOngoing ? "Helltide is now ongoing:" : "Next Helltide will start in:";
            bossMessage = $"{_upcomingBoss!.Name}  will show up in:";
            if (_remainingTime.TotalSeconds <= 0)
            {
                _remainingTime = GetRemainingTime();
            }

            StateHasChanged();
        }

        private static TimeSpan GetUpcomingBossRemainingTime(int time) => TimeSpan.FromMinutes(time);
        private TimeSpan GetRemainingTime()
        {
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeSinceStart = currentTime - _baseTime;

            int totalIntervalMinutes = 135;
            int elapsedIntervals = (int)Math.Floor(timeSinceStart.TotalMinutes / totalIntervalMinutes);
            TimeSpan timeElapsedWithinInterval = timeSinceStart - TimeSpan.FromMinutes(elapsedIntervals * totalIntervalMinutes);

            if (timeElapsedWithinInterval.TotalMinutes <= 60)
            {
                isOngoing = true;
                int intervalMinutesBy60 = 60 - timeElapsedWithinInterval.Minutes;
                return TimeSpan.FromMinutes(intervalMinutesBy60);
            }
            else
            {
                isOngoing = false;
                int intervalMinutesBy75 = 75 - timeElapsedWithinInterval.Minutes;
                Console.WriteLine(intervalMinutesBy75);
                return TimeSpan.FromMinutes(intervalMinutesBy75 > 60  ? intervalMinutesBy75 % 75 : intervalMinutesBy75);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer!.Elapsed -= TimerElapsed;
            }
        }
    }
}