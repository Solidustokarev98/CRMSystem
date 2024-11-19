using CRMSystem.Db;
using CRMSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using CRMSystem.ViewModels;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList(); // Получаем список пользователей
        var model = new UserListViewModel // Создаем экземпляр модели
        {
            Users = users,
            PagingInfo = new PagingInfo() // Если у вас есть логика для пагинации, добавьте ее здесь
        };
        return View(model);
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
		if (User.IsInRole(Role.Admin))
		{
			if (ModelState.IsValid)
			{
				user.IsAdmin = false;
				_context.Users.Add(user);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			// Если модель не валидна, верните обратно в представление с ошибками
			return View(user);
		}

		return Unauthorized();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {

        if (User.IsInRole("Admin"))
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null && !user.IsAdmin)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        return Unauthorized();
    }
}