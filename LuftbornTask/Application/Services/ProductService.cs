using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IRepository<Product> productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Id = Guid.NewGuid();

            await _productRepository.AddAsync(product);
            await _unitOfWork.Commit();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(productDto.Id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            _mapper.Map(productDto, product);
            _productRepository.Update(product);

            await _unitOfWork.Commit();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            _productRepository.Remove(product);
            await _unitOfWork.Commit();
        }
    }
}