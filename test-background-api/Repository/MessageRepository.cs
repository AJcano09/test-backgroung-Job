using test_background_api.dbContext.AppDbContext;
using test_background_api.dbContext.Entities;

namespace test_background_api.Repository;

public interface IMessageRepository
{
    Task<Message> CreateAsync(Message message);
    Task<Message?> GetByIdAsync(int id);
}

public class MessageRepository: IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Message> CreateAsync(Message message)
    {
        _context.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<Message?> GetByIdAsync(int id)
    {
        return await _context.Messages.FindAsync(id);
    }
}