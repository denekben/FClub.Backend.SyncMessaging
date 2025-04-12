﻿using FClub.Backend.Common.Exceptions;

namespace Notifications.Domain.Entities
{
    public sealed class NotificationSettings
    {
        public Guid Id { get; init; }

        public bool AllowAttendanceNotifications { get; set; }
        public uint AttendanceNotificationPeriod { get; set; }
        public uint AttendanceNotificationReSendPeriod { get; set; }
        public string AttendanceEmailSubject { get; set; }
        public Guid? AttendanceNotificationId { get; set; }
        public Notification? AttendanceNotification { get; set; }

        public bool AllowTariffNotifications { get; set; }
        public string TariffEmailSubject { get; set; }
        public Guid? TariffNotificationId { get; set; }
        public Notification? TariffNotification { get; set; }

        public bool AllowBranchfNotifications { get; set; }
        public string BranchEmailSubject { get; set; }
        public Guid? BranchNotificationId { get; set; }
        public Notification? BranchNotification { get; set; }

        private NotificationSettings() { }

        private NotificationSettings(
            bool allowAttendanceNotifications, uint attendanceNotificationPeriod,
            uint attendanceNotificationReSendPeriod, string attendanceEmailSubject, Guid? attendanceNotificationId,
            bool allowTariffNotifications, string tariffEmailSubject, Guid? tariffNotificationId,
            bool allowBranchfNotifications, string branchEmailSubject, Guid? branchNotificationId)
        {
            Id = Guid.NewGuid();

            AllowAttendanceNotifications = allowAttendanceNotifications;
            AttendanceNotificationPeriod = attendanceNotificationPeriod;
            AttendanceNotificationReSendPeriod = attendanceNotificationReSendPeriod;
            AttendanceEmailSubject = attendanceEmailSubject;
            AttendanceNotificationId = attendanceNotificationId;

            AllowTariffNotifications = allowTariffNotifications;
            TariffEmailSubject = tariffEmailSubject;
            TariffNotificationId = tariffNotificationId;

            AllowBranchfNotifications = allowBranchfNotifications;
            BranchEmailSubject = branchEmailSubject;
            BranchNotificationId = branchNotificationId;
        }

        public static NotificationSettings Create(
            bool allowAttendanceNotifications, uint attendanceNotificationPeriod,
            uint attendanceNotificationReSendPeriod, string attendanceEmailSubject, Guid? attendanceNotificationId,
            bool allowTariffNotifications, string tariffEmailSubject, Guid? tariffNotificationId,
            bool allowBranchfNotifications, string branchEmailSubject, Guid? branchNotificationId)
        {
            if (allowAttendanceNotifications && attendanceNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationId]. Entered value {attendanceNotificationId}");
            if (allowTariffNotifications && tariffNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[tariffNotificationId]. Entered value {tariffNotificationId}");
            if (allowBranchfNotifications && branchNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[branchNotificationId]. Entered value {branchNotificationId}");
            if (attendanceNotificationPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationPeriod}");
            if (attendanceNotificationReSendPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationReSendPeriod}");
            if (string.IsNullOrWhiteSpace(attendanceEmailSubject))
                throw new DomainException($"Invalid value for NotificationSettings[attendanceEmailSubject]. Entered value {attendanceEmailSubject}");
            if (string.IsNullOrWhiteSpace(tariffEmailSubject))
                throw new DomainException($"Invalid value for NotificationSettings[tariffEmailSubject]. Entered value {tariffEmailSubject}");
            if (string.IsNullOrWhiteSpace(branchEmailSubject))
                throw new DomainException($"Invalid value for NotificationSettings[attendanceEmailSubject]. Entered value {branchEmailSubject}");

            return new NotificationSettings(
                allowAttendanceNotifications, attendanceNotificationPeriod,
                attendanceNotificationReSendPeriod, attendanceEmailSubject, attendanceNotificationId,
                allowTariffNotifications, tariffEmailSubject, tariffNotificationId,
                allowBranchfNotifications, branchEmailSubject, branchNotificationId);
        }

        public void UpdateDetails(
            bool allowAttendanceNotifications, uint attendanceNotificationPeriod,
            uint attendanceNotificationReSendPeriod, string attendanceEmailSubject, Guid? attendanceNotificationId,
            bool allowTariffNotifications, string tariffEmailSubject, Guid? tariffNotificationId,
            bool allowBranchfNotifications, string branchEmailSubject, Guid? branchNotificationId)
        {
            if (allowAttendanceNotifications && attendanceNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationId]. Entered value {attendanceNotificationId}");
            if (allowTariffNotifications && tariffNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[tariffNotificationId]. Entered value {tariffNotificationId}");
            if (allowBranchfNotifications && branchNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[branchNotificationId]. Entered value {branchNotificationId}");
            if (attendanceNotificationPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationPeriod}");
            if (attendanceNotificationReSendPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationReSendPeriod}");
            if (string.IsNullOrWhiteSpace(attendanceEmailSubject))
                throw new DomainException($"Invalid value for NotificationSettings[attendanceEmailSubject]. Entered value {attendanceEmailSubject}");
            if (string.IsNullOrWhiteSpace(tariffEmailSubject))
                throw new DomainException($"Invalid value for NotificationSettings[tariffEmailSubject]. Entered value {tariffEmailSubject}");
            if (string.IsNullOrWhiteSpace(branchEmailSubject))
                throw new DomainException($"Invalid value for NotificationSettings[attendanceEmailSubject]. Entered value {branchEmailSubject}");


            AllowAttendanceNotifications = allowAttendanceNotifications;
            AttendanceNotificationPeriod = attendanceNotificationPeriod;
            AttendanceNotificationReSendPeriod = attendanceNotificationReSendPeriod;
            AttendanceEmailSubject = attendanceEmailSubject;
            AttendanceNotificationId = attendanceNotificationId;

            AllowTariffNotifications = allowTariffNotifications;
            TariffEmailSubject = tariffEmailSubject;
            TariffNotificationId = tariffNotificationId;

            AllowBranchfNotifications = allowBranchfNotifications;
            BranchEmailSubject = branchEmailSubject;
            BranchNotificationId = branchNotificationId;
        }
    }
}
