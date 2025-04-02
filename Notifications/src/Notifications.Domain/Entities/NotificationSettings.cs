using FClub.Backend.Common.Exceptions;

namespace Notifications.Domain.Entities
{
    public sealed class NotificationSettings
    {
        public Guid Id { get; init; }

        public bool AllowAttendanceNotifications { get; private set; }
        public uint AttendanceNotificationPeriod { get; private set; }
        public Guid? AttendanceNotificationId { get; private set; }
        public Notification? AttendanceNotification { get; private set; }

        public bool AllowTariffNotifications { get; private set; }
        public Guid? TariffNotificationId { get; private set; }
        public Notification? TariffNotification { get; private set; }

        public bool AllowBranchfNotifications { get; private set; }
        public Guid? BranchNotificationId { get; private set; }
        public Notification? BranchNotification { get; private set; }

        private NotificationSettings() { }

        private NotificationSettings(
            bool allowAttendanceNotifications, uint attendanceNotificationPeriod, Guid? attendanceNotificationId,
            bool allowTariffNotifications, Guid? tariffNotificationId,
            bool allowBranchfNotifications, Guid? branchNotificationId)
        {
            Id = Guid.NewGuid();

            AllowAttendanceNotifications = allowAttendanceNotifications;
            AttendanceNotificationPeriod = attendanceNotificationPeriod;
            AttendanceNotificationId = attendanceNotificationId;

            AllowTariffNotifications = allowTariffNotifications;
            TariffNotificationId = tariffNotificationId;

            AllowBranchfNotifications = allowBranchfNotifications;
            BranchNotificationId = branchNotificationId;
        }

        public static NotificationSettings Create(
            bool allowAttendanceNotifications, uint attendanceNotificationPeriod, Guid? attendanceNotificationId,
            bool allowTariffNotifications, Guid? tariffNotificationId,
            bool allowBranchfNotifications, Guid? branchNotificationId)
        {
            if (attendanceNotificationPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationPeriod}");

            return new NotificationSettings(
                allowAttendanceNotifications, attendanceNotificationPeriod, attendanceNotificationId,
                allowTariffNotifications, tariffNotificationId,
                allowBranchfNotifications, branchNotificationId);
        }
    }
}
