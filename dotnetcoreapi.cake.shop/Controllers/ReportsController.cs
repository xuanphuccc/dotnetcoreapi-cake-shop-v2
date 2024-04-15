using dotnetcoreapi.cake.shop.domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi.cake.shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        /// <summary>
        /// Lấy tổng số đơn hàng đã bán
        /// </summary>
        /// <returns></returns>
        [HttpGet("TotalOrders")]
        public async Task<IActionResult> GetTotalOrders() { 
            var result = await _reportRepository.GetTotalOrders();

            return Ok(result);
        }

        /// <summary>
        /// Lấy tổng doanh thu
        /// </summary>
        /// <returns></returns>
        [HttpGet("TotalRevenue")]
        public async Task<IActionResult> GetTotalRevenue() {
            var result = await _reportRepository.GetTotalRevenue();

            return Ok(result);
        }

        /// <summary>
        /// Lấy tổng số khách hàng
        /// </summary>
        /// <returns></returns>
        [HttpGet("TotalCustomers")]
        public async Task<IActionResult> GetTotalCustomers() {
            var result = await _reportRepository.GetTotalCustomers();

            return Ok(result);
        }
    }
}
