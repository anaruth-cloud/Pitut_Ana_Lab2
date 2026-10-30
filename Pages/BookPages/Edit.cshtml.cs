using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pitut_Ana_Lab2.Models;

namespace Pitut_Ana_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Pitut_Ana_Lab2Context _context;

    public EditModel(Pitut_Ana_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories).ThenInclude(b => b.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Book == null)
        {
            return NotFound();
        }

        //apelam PopulateAssignedCategoryData pentru a obtine informatiile necesare checkbox-urilor
        PopulateAssignedCategoryData(_context, Book);

        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(i => i.Publisher)
            .Include(i => i.Author)
            .Include(i => i.BookCategories)
                .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            i => i.Title, i => i.AuthorID,
            i => i.Price, i => i.PublishingDate, i => i.PublisherID))
        {
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Book care este editata
        UpdateBookCategories(_context, selectedCategories, bookToUpdate);
        PopulateAssignedCategoryData(_context, bookToUpdate);
        return Page();
    }
}
