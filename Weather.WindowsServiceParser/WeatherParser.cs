
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

using NLog;

namespace Weather.WindowsServiceParser
{
    public partial class WeatherParser : ServiceBase
    {
        private readonly Logger logger;
        private readonly Processor processor;

        private Timer timer;
        
        public WeatherParser()
        {
            this.InitializeComponent();

            this.logger = LogManager.GetCurrentClassLogger();
            this.processor = new Processor(this.logger);
        }

        protected override void OnStart(string[] args)
        {
            this.timer = new Timer();
            this.timer.AutoReset = true;
            this.timer.Enabled = true;
            this.timer.Interval = this.MillisecondsToMinute(ConfigurationManager.AppSettings["interval"]);
            this.timer.Elapsed += this.OnTimedEvent;
            this.timer.Start();
            
            this.OnTimedEvent(null, null);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            this.processor.Process();
        }

        private double MillisecondsToMinute(string input)
        {
            var result = double.Parse(input);

            return result * 1000 * 60;
        }

        protected override void OnStop()
        {
            this.logger.Error("OnStop");
            this.timer.Stop();
            this.timer.Dispose();
        }

#if DEBUG
        public void Start()
        {
            this.OnTimedEvent(null, null);
        }
#endif
    }
}
