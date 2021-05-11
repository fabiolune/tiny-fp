﻿using System;
using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;
using static TinyFp.Prelude;
using static TinyFpTest.Constants.Errors;

namespace TinyFpTest.Services
{
    public class ValidationSearchService : ISearchService
    {
        private const string BLANK_SPACE = " ";

        private readonly ISearchService _searchService;

        public ValidationSearchService(ISearchService searchService)
        {
            _searchService = searchService;
        }


        public Task<Either<ApiError, Product[]>> SearchProductsAsync(string forName)
            => ValidateSpaces(forName)
                .MatchAsync(_ => _searchService.SearchProductsAsync(forName),
                            _ => Task.FromResult((Either<ApiError, Product[]>)_));

        private Validation<ApiError, Unit> ValidateSpaces(string forName)
            => forName.Contains(BLANK_SPACE) ?
                Fail<ApiError, Unit>(InvalidInput) :
                Success<ApiError, Unit>(Unit.Default);
    }
}
