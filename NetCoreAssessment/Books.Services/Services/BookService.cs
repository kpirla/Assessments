using AutoMapper;
using Books.DTO.Book;
using Books.Models.DataContext;
using Books.Models.Entities;
using Books.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Services.Services
{
    public class BookService : IBookInterface
    {
        private readonly IMapper iMapper;
        private readonly AppDbContext dbContext;
        private readonly IConfiguration iConfig;
        private readonly ILogger<BookService> iLogger;
        public BookService(IMapper iMapper, AppDbContext dbContext, ILogger<BookService> iLogger, IConfiguration iConfig)
        {
            this.iMapper = iMapper;
            this.iLogger = iLogger;
            this.dbContext = dbContext;
            this.iConfig = iConfig;
        }
        // Method to get Book List using EF query by Publisher, Author(last,first), Title sorting.
        public async Task<List<BookResp>> BookListByPAT()
        {
            List<BookResp> outBookList = new List<BookResp>();
            try
            {
                outBookList = await ActualBookList();
            }
            catch (Exception ex)
            {
                iLogger.LogError(ex, ex.Message);
            }
            return outBookList.OrderBy(x => x.Publisher).
                   ThenBy(x => String.Concat(x.AuthorLastName ?? "", x.AuthorFirstName ?? "")).
                   ThenBy(x => x.Title).ToList();
        }
        // Method to get Book List using EF query by Author(last,first), Title sorting.
        public async Task<List<BookResp>> BookListByAT()
        {
            List<BookResp> outBookList = new List<BookResp>();
            try
            {
                outBookList = await ActualBookList();
            }
            catch (Exception ex)
            {
                iLogger.LogError(ex, ex.Message);
            }
            return outBookList.OrderBy(x => String.Concat(x.AuthorLastName ?? "", x.AuthorFirstName ?? "")).
                   ThenBy(x => x.Title).ToList();
        }
        // Method to get Total Price of the Books using EF query.
        public async Task<decimal> TotalPrice()
        {
            decimal totalPrice = 0;
            try
            {
                totalPrice = await dbContext.Book.SumAsync(x => x.Price);
            }
            catch (Exception ex)
            {
                iLogger.LogError(ex, ex.Message);
            }
            return totalPrice;
        }
        // Method to get Book List using Stored Procedure by Publisher, Author(last,first), Title sorting.
        public async Task<List<BookResp>> BookListByPATSP()
        {
            List<BookResp> outBookList = new List<BookResp>();
            try
            {
                outBookList = await BookListBySP(true);
            }
            catch (Exception ex)
            {
                iLogger.LogError(ex, ex.Message);
            }
            return outBookList;
        }
        // Method to get Book List using Stored Procedure by Author(last,first), Title sorting.
        public async Task<List<BookResp>> BookListByATSP()
        {
            List<BookResp> outBookList = new List<BookResp>();
            try
            {
                outBookList = await BookListBySP(false);
            }
            catch (Exception ex)
            {
                iLogger.LogError(ex, ex.Message);
            }
            return outBookList;
        }
        // Method to bulk insert Book List into database using EF query.
        public async Task<bool> InsertBookList(List<BookResp> inBookList)
        {
            try
            {
                List<Book> dbBookList = new List<Book>();
                dbBookList = iMapper.Map(inBookList, dbBookList);
                await dbContext.Book.AddRangeAsync(dbBookList);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                iLogger.LogError(ex, ex.Message);
            }
            return true;
        }
        public async Task<List<BookResp>> ActualBookList()
        {
            List<Book> dbBookList = new List<Book>();
            List<BookResp> outBookList = new List<BookResp>();
            try
            {
                dbBookList = await dbContext.Book.AsNoTracking().ToListAsync() ?? new List<Book>();
                outBookList = iMapper.Map(dbBookList, outBookList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outBookList;
        }
        public async Task<List<BookResp>> BookListBySP(bool isSortByPublisher)
        {
            List<Book> dbBookList = new List<Book>();
            List<BookResp> outBookList = new List<BookResp>();
            string isSortByPublisherParams = iConfig["SqlParameters:IsSortByPublisher"];
            try
            {
                SqlParameter sqlIsSortByPublisher = new SqlParameter(isSortByPublisherParams, isSortByPublisher);
                dbBookList = await dbContext.Book.FromSqlRaw("EXEC usp_BookList " + isSortByPublisherParams, sqlIsSortByPublisher).ToListAsync() ?? new List<Book>();
                outBookList = iMapper.Map(dbBookList, outBookList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outBookList;
        }
    }
}
