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
    
    public async Task<bool> ScheduleSaveMessageJob(DateTime date,Guid taskId,string jsonData)
    {
        var scheduler = await _schedulerFactoryFactory.GetScheduler();
        var dateTask =   date - DateTime.Now;
        var jobKey = new JobKey(nameof(SaveMessageJob));
        var triggerKey = new TriggerKey($"{taskId.ToString()}");
        _logger.LogInformation("Log task with info triggerKey:[{S}]", triggerKey.ToString());
        // Create the trigger
        var trigger = TriggerBuilder
            .Create()
            .WithIdentity(triggerKey)
            .StartAt(DateTimeOffset.Now.Add(dateTask))
            .UsingJobData(Constant.TaskId,taskId)
            .UsingJobData(Constant.JsonData,jsonData)
            .ForJob(jobKey) 
            .Build();
        await scheduler.ScheduleJob(trigger);
        return true;
    }
}