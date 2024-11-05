using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Application.BorrowRecords;
using LibraryManagement.Application.BorrowRecords.Get;
using LibraryManagement.Application.BorrowRecords.List;
using LibraryManagement.Domain.BorrowRecordAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowRecordController : ApiController
    {
        private readonly ISender _sender;

        public BorrowRecordController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBorrowRecord([FromBody] CreateBorrowRecordCommand request)
        {
            var result = await _sender.Send(request);

            if (result.IsError)
                return Problem(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListBorrowRecords(
            [FromQuery] ListBorrowRecordQuery request = null)
        {
            if(request == null)
                request = new ListBorrowRecordQuery();
            
            var result = await _sender.Send(request);
            
            if(result.IsError)
                return Problem(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetBorrowRecord([FromQuery] GetBorrowRecordQuery request)
        {
            var result = await _sender.Send(request);
            
            if(result.IsError)
                return Problem(result.Errors);

            return Ok(result.Value);
        }
    }
}
