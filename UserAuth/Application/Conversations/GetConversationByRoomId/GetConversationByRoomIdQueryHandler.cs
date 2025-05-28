using UserAuth.Application.Abstractions.Messaging;
using UserAuth.Domain.Abstractions;
using UserAuth.Domain.Conversations;
using UserAuth.Domain.Entry;
using UserAuth.Infrastructure.Repositories;

namespace UserAuth.Application.Conversations.GetConversationByRoomId;

internal sealed class GetConversationByRoomIdQueryHandler : IQueryHandler<GetConversationByRoomIdQuery, ConversationResponse>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IEntryRepository _userRepository;
    public GetConversationByRoomIdQueryHandler(IConversationRepository conversationRepository, IMessageRepository messageRepository, IEntryRepository userRepository)
    {
        _conversationRepository = conversationRepository;
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<ConversationResponse>> Handle(GetConversationByRoomIdQuery request, CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByRoomId(request.roomId);

        if (conversation is null)
        {
            return Result.Failure<ConversationResponse>(Error.NullValue);
        }

        var skip = request.pageSize * (request.page - 1);

        var messages = await _messageRepository.GetByConversationIdAsync(conversation.Id, skip, request.pageSize);

        var conversationResponse = new ConversationResponse()
        {
            RoomId = conversation.RoomId,
            Participants = conversation.Participants,
            Messages = messages.Select(message => new MessageResponse()
            {
                SenderName = message.SenderName,
                Content = message.Content,
                CreatedOn = message.CreatedOn
            }).ToList()
        };

        return conversationResponse;
    }
}