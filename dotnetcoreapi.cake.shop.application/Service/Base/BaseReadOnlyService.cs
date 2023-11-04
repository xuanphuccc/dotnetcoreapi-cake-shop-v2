using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.application
{
    public abstract class BaseReadOnlyService<TEntity, TEntityDto> : IBaseReadOnlyService<TEntityDto>
    {
        #region Fields
        protected readonly IBaseReadOnlyRepository<TEntity> _baseReadOnlyRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructors
        public BaseReadOnlyService(IBaseReadOnlyRepository<TEntity> baseReadOnlyRepository, IMapper mapper)
        {
            _baseReadOnlyRepository = baseReadOnlyRepository;
            _mapper = mapper;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntityDto>> GetAllEntitiesAsync()
        {
            var allEntities = await _baseReadOnlyRepository.GetAllEntities().ToListAsync();

            var allEntityDtos = _mapper.Map<IEnumerable<TEntityDto>>(allEntities);

            return allEntityDtos;
        }

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="entityId">ID của bản ghi</param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> GetEntityByIdAsync(int entityId)
        {
            var entity = await _baseReadOnlyRepository.GetEntityByIdAsync(entityId);

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }
        #endregion
    }
}
