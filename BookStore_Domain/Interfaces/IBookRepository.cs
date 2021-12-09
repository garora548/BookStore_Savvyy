using BookStore_Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore_Domain.Interfaces
{
    public interface IBookRepository
    {
        Response<List<Book>> GetAll();
        Response<Book> GetById(int Id);
        Response<int> Save(Book book);
        Response<int> Update(Book book);
        Response<int> Delete(Book book);
    }
}
