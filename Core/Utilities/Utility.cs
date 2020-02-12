using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities
{
    public class Utility
    {
    }

    public enum EntityStatus
    {
        ACTIVE,
        INACTIVE,
        DELETED
    }

    public enum ContactStatus
    {
        Active,
        BlackListed
    }

    public enum GroupType
    {
        UNONYMOUS,
        DAILY,
        WEEKLY,
        MONTHLY,
        DEFAULT
    }

    public enum SmsCategory
    {
        INBOX,
        OUTBOX,
        AUTOREPLY
    }
    public enum MessageType
    {
        SINGLE,
        BULK,
        QUERY,
        SUBSCRIPTION,
        DAILYSMS,
        WEEKLYSMS,
        MONTHLYSMS
    }

    public enum SmsBillingType
    {
        CONTACT,
        FACTORY,
        HQ
    }
    public enum SmsStatus
    {
        SENT, //The message has successfully been sent by our network.
        SUCCESS, //The message has successfully been delivered to the receiver’s handset. This is a final status.
        REJECTED, //The message has been rejected by the MSP. This is a final status.
        SUBMITTED, //The message has successfully been submitted to the MSP (Mobile Service Provider).
        BUFFERED, //The message has been queued by the MSP.
        FAILED, //The message could not be delivered to the receiver’s handset. This is a final status.
        UNKNOWN, //The message could not be delivered to the receiver’s handset. This is a final status.
        PENDING, //The message is pending approval.
        RECEIVED //INCOMING SMS.
    }

    public enum ErrorCode
    {
        OK = 200,
        NOTFOUND = 404,
        BADREQUEST = 400,
        INTERNALSERVERERROR = 500,

    }
    //notification type
    public enum NotificationType
    {
        error,
        success,
        warning,
        info,
        question
    }

    public enum MonthsOfTheYear
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum EntitySize
    {
        Large,
        Medium,
        Small
    }

    public enum StockType
    {
        Inventory,
        Asset
    }
}
