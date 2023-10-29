using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.domain
{
    public enum ErrorCode
    {
        /// <summary>
        /// Mã lỗi nhập liệu
        /// </summary>
        /// CreatedBy: txphuc (17/07/2023)
        BadRequest = 1000,

        /// <summary>
        /// Mã lỗi không tìm thấy tài nguyên
        /// </summary>
        /// CreatedBy: txphuc (17/07/2023)
        NotFound = 1001,

        /// <summary>
        /// Mã lỗi trùng mã
        /// </summary>
        /// CreatedBy: txphuc (17/07/2023)
        ConflictCode = 1002,

        /// <summary>
        /// Lỗi hệ thống
        /// </summary>
        /// CreatedBy: txphuc (17/07/2023)
        ServerError = 1003,

        /// <summary>
        /// Lỗi phụ thuộc
        /// </summary>
        /// CreatedBy: txphuc (01/08/2023)
        ConstraintError = 1004,

        /// <summary>
        /// Lỗi nhập mã quá lớn
        /// </summary>
        /// CreatedBy: txphuc (08/08/2023)
        MaxCodeError = 1005
    }
}
