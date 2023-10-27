using System.Text.Json;
using AutoMapper;
using Quartz;
using test_background_api.dbContext.Entities;
using test_background_api.Models;
using test_background_api.Repository;

namespace test_background_api.Jobs;

public class SaveMessageJob : IJob
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SaveMessageJob> _logger;

    public SaveMessageJob(IMessageRepository messageRepository,IMapper mapper,ILogger<SaveMessageJob> logger)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        Guid taskId = default;
        try
        {
            
            var dataMap = context.MergedJobDataMap;
            const string message = "Job started...";
            _logger.LogInformation($"[SAVE_MESSAGE_JOB][{context.FireInstanceId}] {message}");
            context.MergedJobDataMap.TryGetGuidValue(Constant.TaskId, out taskId);
            var jsonData = dataMap.GetString(Constant.JsonData);
            _logger.LogInformation("[SAVE_MESSAGE_JOB][{ContextFireInstanceId}],task id :[{taskIdWasFound}] data[{JsonData}]", context.FireInstanceId,taskId ,jsonData);

            if (jsonData != null)
            {
                var messageDto = JsonSerializer.Deserialize<MessageDto>(jsonData);
                var entity = _mapper.Map<Message>(messageDto);
                await _messageRepository.CreateAsync(entity);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[SAVE_MESSAGE_JOB][{context.FireInstanceId}][{taskId.ToString()}] fail with error:",
                ex);
        }
        _logger.LogInformation($"[SAVE_MESSAGE_JOB][{context.FireInstanceId}] Job whit ID[{taskId.ToString()}] was finishing succesful...");
    }
}