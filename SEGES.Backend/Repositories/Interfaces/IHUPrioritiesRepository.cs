﻿using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Interfaces
{
    public interface IHUPrioritiesRepository
    {
        Task<ActionResponse<HUPriority>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<HUPriority>>> GetAsync();
        Task<ActionResponse<IEnumerable<HUPriority>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    }
}
