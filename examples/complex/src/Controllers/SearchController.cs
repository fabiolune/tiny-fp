﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TinyFpTest.Services;
using TinyFp;
using TinyFpTest.Services.Api;
using System;

namespace TinyFpTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public Task<IActionResult> Search([FromQuery]string forName)
            => _searchService
                .SearchProductsAsync(forName)
                .MatchAsync(
                    _ => new JsonResult(_), 
                    FromApiError
                );

        private static IActionResult FromApiError(ApiError _)
            => new ContentResult 
            {
                StatusCode = (int)_.StatusCode,
                Content = _.Code
            };
    }
}
