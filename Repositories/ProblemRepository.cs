﻿using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpertalSystem.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly IRepository<Problem> _repository;

        public ProblemRepository(IRepository<Problem> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Problem entity)
            => await _repository.AddAsync(entity);

        /// <summary>
        /// Returns all questions
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Problem>> FindAsync(Expression<Func<Problem, bool>> expression)
            => await _repository.FindAsync(expression);

        /// <summary>
        /// Returns questions by given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Problem>> FindAsync(IssueTypes type)
            => await _repository.FindAsync(p => p.IssueTypes == type);

        public async Task<IEnumerable<Problem>> FindAsync()
            => await _repository.FindAsync();

        public async Task<Problem> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<Problem> GetAsync(Expression<Func<Problem, bool>> expression)
            => await _repository.GetAsync(expression);

        public async Task UpdateAsync(Problem entity)
            => await _repository.UpdateAsync(entity);
    }
}
