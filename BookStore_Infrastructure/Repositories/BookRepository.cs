using BookStore_Domain.Interfaces;
using BookStore_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore_Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _context = context;
            LoadSampleData();
        }
        public Response<int> Delete(Book book)
        {
            Response<int> response = new Response<int>();
            try
            {
                _context.Books.Remove(book);
                _context.SaveChanges();

                response.Data = book.Id;
                response.Message = "Successfully Deleted";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = 0;
                response.Message = "Issue while Deleting: " + ex.Message;
                response.Status = false;
            }
            return response;
        }

        public Response<List<Book>> GetAll()
        {
            Response<List<Book>> response = new Response<List<Book>>();
            try
            {
                var books = _context.Books.ToList();
                response.Data = books;
                response.Status = true;
                response.Message = "Successfully Retreived books " + books.Count();
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting books " + ex.Message;
                response.Status = false;
                response.Data = null;
            }
            return response;
        }

        public Response<Book> GetById(int Id)
        {
            Response<Book> response = new Response<Book>();
            try
            {
                var book = _context.Books.Find(Id);
                response.Data = book;
                response.Status = true;
                response.Message = "Successfully Retreived book";
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting book" + ex.Message;
                response.Status = false;
                response.Data = null;
            }
            return response;
        }

        public Response<int> Save(Book book)
        {
            Response<int> response = new Response<int>();
            int id = 0;
            try
            {
                var books = _context.Books.ToList();
                if (books != null && books.Count() > 0)
                {
                    id = books.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                }
                id = id + 1;
                book.Id = id;
                _context.Books.Add(book);
                _context.SaveChanges();
                

                response.Data = id;
                response.Message = "Successfully saved";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = 0;
                response.Message = "Issue while saving: " + ex.Message;
                response.Status = false;
            }
            return response;
        }

        public Response<int> Update(Book book)
        {
            Response<int> response = new Response<int>();            
            try
            {
                _context.Books.Update(book);
                _context.SaveChanges();

                response.Data = book.Id;
                response.Message = "Successfully Updated";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = 0;
                response.Message = "Issue while updating: " + ex.Message;
                response.Status = false;
            }
            return response;
        }

        public void LoadSampleData()
        {
            if (_context.Books.Count() <= 0)
            {
                Book book1 = new Book()
                {
                    Title = "The Power of Habit",
                    Description = "Motivational",
                    Author = "Charles",
                    CoverImage = "abcd.jped",
                    Price = 20.8m
                };
                _context.Books.Add(book1);
                Book book2 = new Book()
                {
                    Title = "How Technology Works",
                    Description = "About Technology",
                    Author = "John",
                    CoverImage = "xyz.jped",
                    Price = 30.99m
                };
                _context.Books.Add(book2);

                _context.SaveChanges();
            }
        }
    }
}
