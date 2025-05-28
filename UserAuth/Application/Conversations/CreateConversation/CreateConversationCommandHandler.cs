using UserAuth.Application.Abstractions.Clock;
using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Domain.Abstractions;
using UserAuth.Domain.Conversations;
using UserAuth.Application.Conversations.AddMessage;

namespace UserAuth.Application.Conversations.CreateConversation;

internal sealed class CreateConversationCommandHandler : ICommandHandler<CreateConversationCommand, long>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateConversationCommandHandler(IConversationRepository conversationRepository, IDateTimeProvider dateTimeProvider)
    {
        _conversationRepository = conversationRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<long>> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var isGroup = request.roomId is not null && request.roomId > 0 ? true : false;

        var conversation = Conversation.Create(request.roomId, request.participants, isGroup, _dateTimeProvider.UtcNow);

        await _conversationRepository.AddAsync(conversation);

        return conversation.Id;
    }
}