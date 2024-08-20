﻿using B1TestTask.Domain.Context;
using B1TestTask.Domain.Entities;
using B1TestTask.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace B1TestTask.Domain.Repositories.Impllementations
{
    internal class FileRepository : IFileRepository
    {
        private readonly FirstTaskDbContext _dbContext;

        public FileRepository(FirstTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MergedFile?> CreateAsync(MergedFile entity, CancellationToken token = default)
        {
            var created = await _dbContext.Files.AddAsync(entity, token);

            if (created != null)
            {
                await _dbContext.SaveChangesAsync(token);
            }

            return created?.Entity;
        }

        public async Task DeleteAsync(MergedFile entity, CancellationToken token = default)
        {
            var deleted = _dbContext.Files.Remove(entity);

            if (deleted != null)
            {
                await _dbContext.SaveChangesAsync(token);
            }
        }

        public async Task<MergedFile?> GetAsync(int id, CancellationToken token = default)
        {
            return await _dbContext.Files.FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<IEnumerable<MergedFile>> GetByPredicateAsync(Expression<Func<MergedFile, bool>> predicate, CancellationToken token = default)
        {
            return await _dbContext.Files.Where(predicate).ToListAsync(token);
        }

        public async Task<MergedFile?> UpdateAsync(MergedFile entity, CancellationToken token = default)
        {
            var updated = _dbContext.Files.Update(entity);

            if (updated != null)
            {
                await _dbContext.SaveChangesAsync(token);
            }

            return updated?.Entity;
        }
    }
}