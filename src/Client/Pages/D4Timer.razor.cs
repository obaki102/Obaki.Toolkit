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
        private int timerValue = 0;
        private int timerMinValue = -75;
        private int timerMaxValue = 0;
        private int timerBossValue = 0;
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
            
            if (isOngoing)
            {
                timerMinValue = -60;
                timerMaxValue = 0;
            }
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
            timerValue = (int)Math.Abs(_remainingTime.TotalMinutes) * -1;

            _bossRemainingTime = _bossRemainingTime.Subtract(TimeSpan.FromSeconds(1));
            timerBossValue = (int)Math.Abs(_bossRemainingTime.TotalMinutes) * -1;

            counter = FormatTime(_remainingTime);
            bossCounter = FormatTime(_bossRemainingTime);
            message = isOngoing ? "Helltide is now active:" : "Helltide will start in:";
            bossMessage = $"{_upcomingBoss!.Name} will show up in:";

            if (_remainingTime.TotalSeconds <= 0)
            {
                _remainingTime = GetRemainingTime();
            }

            StateHasChanged();
        }

        private static string FormatTime(TimeSpan time)
        {
            if (time.Hours > 0)
            {
                return time.Hours == 1 ? $"{time.Hours} hour {time.Minutes} minutes {time.Seconds} seconds"
                    : $"{time.Hours} hours {time.Minutes} minutes {time.Seconds} seconds";
            }
            else
            {
                return $"{time.Minutes} minutes {time.Seconds} seconds";
            }
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
                return TimeSpan.FromMinutes(intervalMinutesBy75 > 60 && intervalMinutesBy75 < 75 ? intervalMinutesBy75 % 75 : intervalMinutesBy75);
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