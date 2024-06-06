using System;

namespace Connection.Model
{
    /// <summary>
    /// Серверт биелэгдсэн хүсэлтийн бүтцийг агуулах классс
    /// </summary>
    [Serializable]
    public sealed class MClientHeaderInfo
    {
        #region Талбараууд
        /// <summary>
        /// Хэрэглэгчийн нэвтрэх нэр.
        /// </summary>
        public string UserID { get; set; }


        /// <summary>
        /// Дэд эрх шалгах эсэх
        /// </summary>
        public bool UseRLS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Language { get; set; }
        #endregion

        #region Constructors
        public MClientHeaderInfo()
        {
            UserID = "";
            UseRLS = true;
            Language = "mn-MN";
        }
        #endregion
    }
}
