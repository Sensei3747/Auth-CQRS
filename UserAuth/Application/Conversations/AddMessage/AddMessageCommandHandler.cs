using UserAuth.Application.Abstractions.Clock;
using UserAuth.Application.Abstractions.Messaging;

using UserAuth.Domain.Abstractions;
using UserAuth.Domain.Conversations;
using UserAuth.Domain.Entry;
using UserAuth.Infrastructure.Repositories;

namespace UserAuth.Application.Conversations.AddMessage;

internal sealed class AddMessageCommandHandler : ICommandHandler<AddMessageCommand, string>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEntryRepository _userRepository;

    public AddMessageCommandHandler(IMessageRepository messageRepository, IDateTimeProvider dateTimeProvider, IEntryRepository userRepository, IConversationRepository conversationRepository)
    {
        _messageRepository = messageRepository;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _conversationRepository = conversationRepository;
    }

    public async Task<Result<string>> Handle(AddMessageCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByRoomId(request.roomId);

        if (conversation is null)
        {
            return Result.Failure<string>(Error.NullValue);
        }

        var user = await _userRepository.GetById(request.senderId);

        var message = Message.Create(conversation.Id, request.senderId, user.Name, request.content, _dateTimeProvider.UtcNow);

        await _messageRepository.AddAsync(message);

        return message.Id;
    }
}