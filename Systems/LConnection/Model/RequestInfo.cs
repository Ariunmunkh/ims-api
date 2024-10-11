using System;

namespace Connection.Model
{
    /// <summary>
    /// Серверт биелэгдсэн хүсэлтийн бүтцийг агуулах классс
    /// </summary>
    [Serializable]
    public sealed class RequestInfo
    {
        /// <summary>
        /// Хэрэглэгчийн дугаар
        /// </summary>
        public long UserID { get; set; }


        /// <summary>
        /// Хэрэглэгчийн үүрэг 1 Admin, 2 Sub-admin, 3 Coach, 4 Others, 5 Volunteer
        /// </summary>
        public long Roleid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Language { get; set; } = string.Empty;
    }
}
