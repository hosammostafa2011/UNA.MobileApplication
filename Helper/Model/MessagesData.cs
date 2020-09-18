namespace MekhwahMunicipality
{
    public class ChatMessageData
    {
        public ChatUserData From { get; set; }
        public string Body { get; set; }
        public string When { get; set; }
        public bool IsRead { get; set; }
        public bool IsInbound { get; set; }
    }

    public class ChatUserData
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }

    public class MessageData
    {
        public ChatUserData From { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool HasAttachment { get; set; }
        public int ThreadCount { get; set; }
        public string When { get; set; }
        public bool IsStared { get; set; }
        public bool IsRead { get; set; }
        public string V_CATEGORY_ID { get;  set; }
        public string DELETE_Y_N { get;  set; }
        public string V_MESSAGE_RECIPIENT_ID { get; internal set; }
        public string V_BCC { get; internal set; }
        public bool V_BODY_Y_N { get; internal set; }
        public bool V_FORWARD_Y_N { get; internal set; }
        public string V_ATTACHMENT_COUNT { get; internal set; }
        public bool V_DELETE_Y_N { get; internal set; }
        public string V_BCC_IDS { get; internal set; }
        public string V_CC_IDS { get; internal set; }
        public string V_TO_IDS { get; internal set; }
        public string V_CC { get; internal set; }
        public bool V_REPLY_ALL_Y_N { get; internal set; }
        public bool V_REPLY_Y_N { get; internal set; }
        public string V_TO { get; internal set; }
    }

    public class NotificationData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public NotificationType Type { get; set; }
    }
}
