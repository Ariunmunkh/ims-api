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
        /// Нэвтэрсэн ажилтаны дугаар
        /// </summary>
        public string EmpID { get; set; }

        /// <summary>
        /// Хэлтэс газар.
        /// </summary>
        public string DepID { get; set; }

        /// <summary>
        /// Албан тушаал.
        /// </summary>
        public string PositionID { get; set; }

        /// <summary>
        /// Дэлгэцийн дугаар objectId.
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// Нэвтэрсэн компани
        /// </summary>
        public string CompanyID { get; set; }

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
            EmpID = "";
            DepID = "";
            PositionID = null;
            UseRLS = true;
            CompanyID = "";
            Language = "mn-MN";
        }
        #endregion
    }
}
