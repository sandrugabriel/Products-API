using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProduseApi.Data;
using ProduseApi.Dto;
using ProduseApi.Models;
using ProduseApi.Repository.Interfaces;
using System;


namespace ProduseApi.Repository
{
    public class ProdusRepository : IProdusRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public ProdusRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Produs>> GetAllAsync()
        {
            return await _context.Produs.ToListAsync();
        }

        public async Task<Produs> GetByIdAsync(int id)
        {
            List<Produs> all = await _context.Produs.ToListAsync();

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id == id) return all[i];
            }

            return null;
        }

        public async Task<Produs> GetByNameAsync(string name)
        {
            List<Produs> all = await _context.Produs.ToListAsync();

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Name.Equals(name))
                {
                    return all[i];
                }
            }

            return null;
        }


        public async Task<Produs> Create(CreateRequest request)
        {

            var product = _mapper.Map<Produs>(request);

            _context.Produs.Add(product);

            await _context.SaveChangesAsync();

            return product;

        }

        public async Task<Produs> Update(int id, UpdateRequest request)
        {

            var product = await _context.Produs.FindAsync(id);

            product.Pret = request.Pret ?? product.Pret;
            product.Name = request.Name ?? product.Name;
            product.Expirare = request.Expirare ?? product.Expirare;

            _context.Produs.Update(product);

            await _context.SaveChangesAsync();

            return product;

        }

        public async Task<Produs> DeleteById(int id)
        {
            var product = await _context.Produs.FindAsync(id);

            _context.Produs.Remove(product);

            await _context.SaveChangesAsync();

            return product;
        }


    }
}
