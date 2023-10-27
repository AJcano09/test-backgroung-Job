using Quartz;

namespace test_background_api.Jobs;
public class JobHandler 
{
    private readonly ISchedulerFactory _schedulerFactoryFactory;
    private readonly ILogger<JobHandler> _logger;

    public JobHandler(ISchedulerFactory schedulerFactoryFactory,ILogger<JobHandler> logger)
    {
        _schedulerFactoryFactory = schedulerFactoryFactory;
        _logger = logger;
    }
    
    public async Task<bool> ScheduleSaveMessageJob(DateTime jobDate,Guid taskId,string jsonData)
    {
        try
        {
            var scheduler = await _schedulerFactoryFactory.GetScheduler();
            DateTime currentDate = DateTime.Now;
            var jobKey = new JobKey(nameof(SaveMessageJob));
            TimeSpan timeUntilJobStarts = jobDate - currentDate;
            if (timeUntilJobStarts.TotalMilliseconds > 0)
            {
                var triggerKey = new TriggerKey($"{taskId.ToString()}");
                _logger.LogInformation("Log task with info triggerKey:[{S}]", triggerKey.ToString());
                // Create the trigger
                var trigger = TriggerBuilder
                    .Create()
                    .WithIdentity(triggerKey)
                    .StartAt(new DateTimeOffset(jobDate))
                    .UsingJobData(Constant.TaskId, taskId)
                    .UsingJobData(Constant.JsonData, jsonData)
                    .ForJob(jobKey)
                    .Build();
                await scheduler.ScheduleJob(trigger);
                
            }
            else 
            {
                throw new InvalidOperationException("The job's start date has either passed or is invalid.");

            }
            _logger.LogInformation("job add to Store :jobId:[{S}] was succes", taskId.ToString());
          return true; 
        }
        catch (Exception e)
        {
            _logger.LogError("There are a problem with jobId[{taskid}] error: [{e}]", taskId.ToString(),e.ToString());
            return false;
        }
    }
}