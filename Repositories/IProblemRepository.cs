﻿using ExpertalSystem.Domain;
using ExpertalSystem.Dtos;
using ExpertalSystem.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Repositories
{
    public interface IProblemRepository : IRepository<Problem>
    {
        Task<IEnumerable<Problem>> FindAsync(IssueType type);
    }
}