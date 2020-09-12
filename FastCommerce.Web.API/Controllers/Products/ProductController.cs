﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.ObjectDtos.Product;
using FastCommerce.Business.ProductManager;
using FastCommerce.Business.ProductManager.Abstract;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager ProductManager)
        {
            _productManager = ProductManager;
        }

        /// <summary>
        /// CreateIndexs
        /// </summary>
        /// <param name="ProductElasticIndexDto">
        /// Activation Key 
        /// </param>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>

        [ActionName("CreateIndexs"), Route("CreateIndexs")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateIndexs(ProductElasticIndexDto productElasticIndexDto)
        {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                await _productManager.CreateIndexes(productElasticIndexDto);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<Product>>"/>
        /// </returns>
        [HttpGet("Get")]
        public async Task<Response<Product>> Get()
        {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.Get();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        /// <summary>
        /// AddProduct
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<Product>>"/>
        /// </returns>
        [HttpPost("AddProduct")]
        public async Task<Response<Product>> AddProduct(Product product)
        {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.Data = await _productManager.AddProduct(product);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        /// <summary>
        /// GetByPlace
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<Product>>"/>
        /// </returns>
        [HttpGet("GetByPlace")]
        public async Task<Response<Product>> GetByCategories([FromBody]GetByCategoriesRequest req)
        {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.GetByCategories(req);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }

    }
}
