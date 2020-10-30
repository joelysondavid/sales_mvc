﻿using Microsoft.AspNetCore.Mvc;
using SalesMVC.Models;
using SalesMVC.Models.ViewModel;
using SalesMVC.Services;

namespace SalesMVC.Controllers
{
    /// <summary>
    /// Saller controller
    /// </summary>
    public class SellerController : Controller
    {
        #region Properties
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        #endregion

        #region Constructors
        public SellerController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Action index
        /// </summary>
        /// <returns>Index</returns>
        public IActionResult Index()
        {
            var sellers = _sellerService.FindAll();
            return View(sellers);
        }

        /// <summary>
        /// View create
        /// </summary>
        /// <returns>View create</returns>
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var sellerViewModel = new SellerFormViewModel { Departments = departments };
            return View(sellerViewModel);
        }

        /// <summary>
        /// Save new Saller in db
        /// </summary>
        /// <param name="seller">New obj seller</param>
        /// <returns>Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Delete a seller
        /// </summary>
        /// <param name="id">Id of seller to delete</param>
        /// <returns>Redirect to action index</returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var model = _sellerService.FindById(id.Value);

            if (model == null)
                return NotFound();

            return View("Delete", model);
        }

        /// <summary>
        /// Delete an seller by id
        /// </summary>
        /// <param name="id">Id of seller to delete</param>
        /// <returns>List View</returns>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _sellerService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var model = _sellerService.FindById(id);

            return View(model);
        }
        #endregion
    }
}
