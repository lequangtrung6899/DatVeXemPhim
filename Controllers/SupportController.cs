using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers;

// Ca sử dụng "Hỗ trợ khách hàng" phía khách — 3 mục ở footer: Câu hỏi thường gặp,
// Chính sách đổi/trả vé, và Liên hệ (form gửi yêu cầu hỗ trợ, nhân viên xem ở trang quản trị).
public class SupportController : BaseController
{
    public SupportController(ApplicationDbContext db) : base(db) { }

    // GET /ho-tro/cau-hoi-thuong-gap
    [HttpGet, Route("/ho-tro/cau-hoi-thuong-gap")]
    public IActionResult Faq() => View();

    // GET /ho-tro/doi-tra-ve
    [HttpGet, Route("/ho-tro/doi-tra-ve")]
    public IActionResult Policy() => View();

    // GET /ho-tro/lien-he
    [HttpGet, Route("/ho-tro/lien-he")]
    public IActionResult Contact()
    {
        var vm = new ContactFormVM();
        if (ViewBag.Customer is Customer customer)
        {
            vm.FullName = customer.FullName;
            vm.Email = customer.Email;
            vm.Phone = customer.Phone;
        }
        return View(vm);
    }

    // POST /ho-tro/lien-he
    [HttpPost, Route("/ho-tro/lien-he")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ContactSubmit(ContactFormVM form)
    {
        if (!ModelState.IsValid)
        {
            form.Error = string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return View("Contact", form);
        }

        Db.ContactMessages.Add(new ContactMessage
        {
            FullName = form.FullName!.Trim(),
            Email = form.Email!.Trim(),
            Phone = string.IsNullOrWhiteSpace(form.Phone) ? null : form.Phone.Trim(),
            Subject = form.Subject!.Trim(),
            Message = form.Message!.Trim(),
            CreatedAt = DateTime.Now
        });
        await Db.SaveChangesAsync();

        return View("Contact", new ContactFormVM { Sent = true });
    }
}
