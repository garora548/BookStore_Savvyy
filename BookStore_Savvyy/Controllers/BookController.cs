using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore_Domain.Interfaces;
using BookStore_Domain.Models;
using BookStore_Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Savvyy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {        
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            Response<List<Book>> response = new Response<List<Book>>();
            try
            {
                response = _bookRepository.GetAll();               
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting books " + ex.Message;
                response.Status = false;
                response.Data = null;
            }

            return Ok(response);
        }

        [HttpPost]
        public ActionResult Save([FromBody]Book book)
        {
            Response<int> response = new Response<int>();
            try
            {
                response = _bookRepository.Save(book);                           
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting books " + ex.Message;
                response.Status = false;                
            }

            return Ok(response);
        }

        [HttpGet("GetById")]
        public ActionResult GetById(int Id)
        {
            Response<Book> response = new Response<Book>();
            try
            {
                response = _bookRepository.GetById(Id);
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting book " + ex.Message;
                response.Status = false;
                response.Data = null;
            }
            return Ok(response);
        }

        [HttpPut]
        public ActionResult Update([FromBody] Book book)
        {
            Response<int> response = new Response<int>();
            try
            {
                if (book.Id <= 0)
                {
                    response.Message = "Please provide us the correct Id to update";
                    response.Status = false;                    
                }
                var bookResp = _bookRepository.GetById(book.Id);
                if (bookResp.Status && bookResp.Data != null)
                {
                    response = _bookRepository.Update(book);
                }
                else
                {
                    response.Message = "Not able to find the Book to update";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting book " + ex.Message;
                response.Status = false;                
            }
            return Ok(response);
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            Response<int> response = new Response<int>();
            try
            {
                if (Id <= 0)
                {
                    response.Message = "Please provide us the correct Id to delete";
                    response.Status = false;
                }
                var bookResp = _bookRepository.GetById(Id);
                if (bookResp.Status && bookResp.Data != null)
                {
                    response = _bookRepository.Delete(bookResp.Data);
                }
                else
                {
                    response.Message = "Not able to find the Book to delete";
                    response.Status = false;
                }                
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting book " + ex.Message;
                response.Status = false;                
            }
            return Ok(response);
        }

    }
}
