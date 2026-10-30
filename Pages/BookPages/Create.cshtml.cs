using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pitut_Ana_Lab2.Models;

namespace Pitut_Ana_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Pitut_Ana_Lab2Context _context;

    public CreateModel(Pitut_Ana_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        // daca am adaugat o proprietate FullName in clasa Author
        ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

        var book = new Book();
        book.BookCategories = new List<BookCategory>();
        PopulateAssignedCategoryData(_context, book);
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; }

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        var newBook = new Book();
        if (selectedCategories != null)
        {
            newBook.BookCategories = new List<BookCategory>();
            foreach (var cat in selectedCategories)
            {
                var catToAdd = new BookCategory
                {
                    CategoryID = int.Parse(cat)
                };
                newBook.BookCategories.Add(catToAdd);
            }
        }
        Book.BookCategories = newBook.BookCategories;
        _context.Book.Add(Book);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}