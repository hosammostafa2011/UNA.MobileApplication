namespace Model.Mobile
{
    public class NOTIFICATION
    {
        public NOTIFICATION(string NOTIFICATION_LOG_ID, string NOTES)
        {
            this.NOTIFICATION_LOG_ID = NOTIFICATION_LOG_ID;
            this.NOTES = NOTES;
        }

        public string NOTIFICATION_LOG_ID { get; set; }
        public string NOTES { get; set; }
    }
}
