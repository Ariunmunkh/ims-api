using System;

namespace Connection.Model
{
    /// <summary>
    /// Серверт биелэгдсэн хүсэлтийн бүтцийг агуулах классс
    /// </summary>
    [Serializable]
    public sealed class MResult
    {
        #region Талбараууд

        #region Rows
        /// <summary>
        /// Хүсэлтэнд харгалзах нийт бичлэгийн тоо
        /// </summary>
        public int totalrow { get; set; }

        /// <summary>
        /// Агуулагдсан мөр мэдээллийн тоо
        /// </summary>
        public int affectedrows { get; set; }
        #endregion


        #region Өгөгдөл

        /// <summary>
        /// Хүлээн авсан өгөгдөл, буцаах обьект
        /// </summary>
        public object retdata { get; set; }

        /// <summary>
        /// Хүлээн авсан хариу, буцаах хариуны төрөл
        /// </summary>
        public int rettype { get; set; }

        /// <summary>
        /// Хүлээн авсан параметр, буцаах утга
        /// </summary>
        public object[] retparams { get; set; }
        #endregion

        #region Бусад
        /// <summary>
        /// Хүлээн авсан мэдээлэл, буцаах мессеж
        /// </summary>
        public string retmsg { get; set; }

        /// <summary>
        /// Хүлээн авсан мэдээлэл, буцаах дэлгэрэнгүй мессеж 
        /// </summary>
        public string retstacktrace { get; set; }

        /// <summary>
        /// Өмнө илгээсэн хүсэлтийн Wait = True бол хүсэлт биелүүлэгч талд хүсэлтийн дугаар шинээр үүсгэх бөгөөд 
        /// энэ дугаарын утга нь тухайн талбараар хүсэлт гаргагч руу илгээгдэнэ.
        /// </summary>
        public int traceno { get; set; }
        #endregion

        #endregion

    }
}
